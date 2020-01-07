using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.Anwendung.Daten
{
    /// <summary>
    /// Stellt die Infrastruktur für eine Datenbankanwendung bereit.
    /// </summary>
    public class DatenAnwendungskontext : NiedSchwa.Anwendung.Anwendungskontext
    {

        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private NiedSchwa.Anwendung.Daten.ProtokollManager _Protokoll = null;

        /// <summary>
        /// Ruft den Dienst zum Verwalten des
        /// Anwendungsprotokolls ab.
        /// </summary>
        public ProtokollManager Protokoll
        {
            get
            {
                if (this._Protokoll == null)
                {
                    this._Protokoll = this.Erzeuge<ProtokollManager>();
                }

                return this._Protokoll;
            }
        }

        /// <summary>
        /// Gibt ein initialisiertes Anwendungsobjekt zurück.
        /// </summary>
        /// <typeparam name="T">Der Anwendungstyp, der benötigt wird.</typeparam>
        /// <remarks>Hinterlegt zusätzlich einen Protokolleintrag,
        /// dass ein neues Objekt initialisiert wurde.</remarks>
        public override T Erzeuge<T>()
        {
            var NeuesObjekt = base.Erzeuge<T>();


            if (!(NeuesObjekt is ProtokollManager))
            {
                this.Protokoll.Eintragen($"{NeuesObjekt} wurde initialisiert...", ProtokollEintragTyp.NeueInstanz);
            }


            //damit der Fehler automatisch im Protokoll steht...
            var Anwendungsobjekt = NeuesObjekt as NiedSchwa.Anwendung.Anwendungsobjekt;
            if (Anwendungsobjekt != null)
            {
                const string FehlerMuster = "Ausnahme in {0}:\r\n{1}";

                Anwendungsobjekt.FehlerAufgetreten += (sender, e)
                    => this.Protokoll.Eintragen(
                        string.Format(FehlerMuster, sender, e.Ausnahme.Message),
                        ProtokollEintragTyp.Fehler);
                if (!(NeuesObjekt is ProtokollManager))
                {
                    this.Protokoll.Eintragen($"{Anwendungsobjekt} behandelt FehlerAufgetreten...");
                }
            }

                return NeuesObjekt;
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private static System.Random _Zufallsgenerator = null;

        /// <summary>
        /// Ruft den Zufallsgenerator der Anwendung ab.
        /// </summary>
        public System.Random Zufallsgenerator
        {
            get
            {
                if (DatenAnwendungskontext._Zufallsgenerator == null)
                {
                    DatenAnwendungskontext._Zufallsgenerator = new System.Random();
                    this.Protokoll.Eintragen( 
                            "Die Anwendung hat den Zufallsgenerator erstellt...",
                            ProtokollEintragTyp.NeueInstanz);
                }

                return DatenAnwendungskontext._Zufallsgenerator;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _SqlServer = null;

        /// <summary>
        /// Ruft den Namen des SQL Servers ab,
        /// auf dem die Datenbank läuft,
        /// oder legt diesen fest.
        /// </summary>
        public string SqlServer
        {
            get
            {
                return this._SqlServer;
            }
            set
            {
                this._SqlServer = value;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _SqlServerDatenbank = null;

        /// <summary>
        /// Ruft den Namen der SQL Server Datenbank ab,
        /// oder legt diesen fest.
        /// </summary>
        public string SqlServerDatenbank
        {
            get
            {
                return this._SqlServerDatenbank;
            }
            set
            {
                this._SqlServerDatenbank = value;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _DatenbankPfad = string.Empty;

        /// <summary>
        /// Ruft den Speicherort der SQL Server Datenbank ab,
        /// wenn die Datenbank dynamisch angehängt werden soll,
        /// oder legt diesen fest.
        /// </summary>
        /// <remarks>Diese Eigenschaft leer lassen, wenn
        /// die Datenbank fix am Server angehängt ist.</remarks>
        public string DatenbankPfad
        {
            get
            {
                return this._DatenbankPfad;
            }
            set
            {
                this._DatenbankPfad = value;
            }
        }

        /// <summary>
        /// Gibt ein initialisiertes Anwendungsobjekt zurück.
        /// </summary>
        /// <param name="klasse">Der Anwendungstyp, der benötigt wird.</param>
        /// <remarks>Hinterlegt zusätzlich einen Protokolleintrag,
        /// dass ein neues Objekt initialisiert wurde.</remarks>
        public object Erzeuge(Type klasse)
        {

            var ErzeugeInfo = this.GetType().GetMethod("Erzeuge", new Type[] { });

            return ErzeugeInfo.MakeGenericMethod(klasse).Invoke(this, null);
            
        }

        /// <summary>
        /// Ruft den Thread-Dienst einer
        /// WPF Anwendung ab oder legt diesen fest.
        /// </summary>
        /// <remarks>Notwendig, damit das Hinzufügen
        /// von Protokolleinträgen für WPF threadsicher wird.</remarks>
        public object Dispatcher { get; set; }
    }
}
