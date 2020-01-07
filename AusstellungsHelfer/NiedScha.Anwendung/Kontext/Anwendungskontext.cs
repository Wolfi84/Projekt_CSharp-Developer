using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Zum Aktivieren der eigenen Erweiterungsmethoden
using NiedSchwa.Anwendung.Erweiterungen;

namespace NiedSchwa.Anwendung
{
    /// <summary>
    /// Stellt die Infrastruktur für eine NiedSchwa Anwendung bereit.
    /// </summary>
    /// <remarks>Hier handelt es sich um Xml-Dokumentationskommentar.</remarks>
    public class Anwendungskontext : System.Object
    {

        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        /// <remarks>Wird zum Cachen benötigt.</remarks>
        private FensterManager _Fenster = null;


        /// <summary>
        /// Ruft den Dienst zum Verwalten der 
        /// Anwendungsfenster ab.
        /// </summary>
        /// <remarks>Weil "oder legt fest" fehlt,
        /// ist die Eigenschaft schreibgeschützt,
        /// d.h. es nur der Getter ohne Setter 
        /// zu implementieren.</remarks>
        public FensterManager Fenster
        {
            get
            {


                if (this._Fenster == null )
                {

                    this._Fenster = this.Erzeuge<FensterManager>();
                }

                return this._Fenster;
            }
        }


        /// <summary>
        /// Gibt ein initialisiertes Anwendungsobjekt zurück.
        /// </summary>
        /// <typeparam name="T">Der Anwendungstyp, der benötigt wird.</typeparam>
        /// <returns>Ein Objekt, wo die AppKontext Eigenschaft
        /// bereits voreingestellt ist und weitere Vorbereitungen getroffen wurden.</returns>
        public virtual T Erzeuge<T>() where T : IAppKontext, new()
        {
            var Ergebnis = new T();


            //Die Infrastruktur übergeben
            Ergebnis.AppKontext = this;

            return Ergebnis;
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private NiedSchwa.Anwendung.SprachenManager _Sprachen = null;

        /// <summary>
        /// Ruft den Dienst zum Verwalten der
        /// Anwendungssprachen ab.
        /// </summary>
        public NiedSchwa.Anwendung.SprachenManager Sprachen
        {
            get
            {

                if (this._Sprachen == null)
                {
                    this._Sprachen = this.Erzeuge<NiedSchwa.Anwendung.SprachenManager>();
                }

                return this._Sprachen;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private string _AnwendungsdatenPfadLokal = null;

        /// <summary>
        /// Ruft das lokale Datenverzeichnis für
        /// die Anwendung im Benutzerprofil ab.
        /// </summary>
        /// <remarks>Es ist sichergestellt, dass das 
        /// Verzeichnis exisitert.</remarks>
        public string AnwendungsdatenPfadLokal
        {
            get
            {
                if (this._AnwendungsdatenPfadLokal == null)
                {
                    this._AnwendungsdatenPfadLokal
                        = System.IO.Path.Combine(
                            System.Environment.GetFolderPath(
                                Environment.SpecialFolder.LocalApplicationData),
                            this.HoleFirmenname(),
                            this.HoleProdukt(),
                            this.HoleVersion()
                    );
                }


                System.IO.Directory.CreateDirectory(this._AnwendungsdatenPfadLokal);

                return this._AnwendungsdatenPfadLokal;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private string _AnwendungsdatenPfad = null;

        /// <summary>
        /// Ruft das Datenverzeichnis für
        /// die Anwendung im Benutzerprofil ab.
        /// </summary>
        /// <remarks>Es ist sichergestellt, dass das 
        /// Verzeichnis exisitert.</remarks>
        public string AnwendungsdatenPfad
        {
            get
            {
                if (this._AnwendungsdatenPfad == null)
                {
                    this._AnwendungsdatenPfad
                        = System.IO.Path.Combine(
                            System.Environment.GetFolderPath(
                                Environment.SpecialFolder.ApplicationData),
                            this.HoleFirmenname(),
                            this.HoleProdukt(),
                            this.HoleVersion()
                    );
                }


                System.IO.Directory.CreateDirectory(this._AnwendungsdatenPfad);

                return this._AnwendungsdatenPfad;
            }
        }

        /// <summary>
        /// Beendet den Infrastrukturdienst.
        /// </summary>
        /// <remarks>Hier werden z. B. Fensterpositionen gespeichert.</remarks>
        public virtual void Herunterfahren()
        {

            if (this._Fenster != null)
            {
                this.Fenster.Speichern();
            }
        
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        /// <remarks>Die Anwendung kann nur genau 
        /// eine Anwendungsverzeichnis besitzen, deshalb "statisch".</remarks>
        private static string _Anwendungspfad = null;

        /// <summary>
        /// Ruft das Verzeichnis ab, aus dem die Anwendung gestartet wurde.
        /// </summary>
        public string Anwendungspfad
        {
            get
            {

                if (Anwendungskontext._Anwendungspfad == null)
                {
                    Anwendungskontext._Anwendungspfad
                        = System.IO.Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName);
                }

                return Anwendungskontext._Anwendungspfad;
            }
        }

    }
}
