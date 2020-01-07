using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.Anwendung.Daten
{
    /// <summary>
    /// Stellt einen Dienst zum Verwalten eines
    /// Anwendungsprotokolls bereit.
    /// </summary>
    public class ProtokollManager : NiedSchwa.Anwendung.Daten.DatenAnwendungsobjekt
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private ProtokollEintragListe _Liste = null;

        /// <summary>
        /// Ruft die Protokolleinträge ab.
        /// </summary>
        public ProtokollEintragListe Liste
        {
            get
            {
                if (this._Liste == null)
                {
                    this._Liste = new ProtokollEintragListe();
                }

                return this._Liste;
            }
        }

        /// <summary>
        /// Fügt dem Protokoll einen normalen Eintrag hinzu.
        /// </summary>
        /// <param name="text">Die Information, die im Protokoll stehen soll.</param>
        public virtual void Eintragen(string text)
        {
            this.Eintragen(new ProtokollEintrag { Typ = ProtokollEintragTyp.Normal, Text = text });
        }

        /// <summary>
        /// Fügt dem Protokoll einen Eintrag hinzu.
        /// </summary>
        /// <param name="text">Die Information, die im Protokoll stehen soll.</param>
        /// <param name="typ">Die Art des Eintrags.</param>
        public virtual void Eintragen(string text, ProtokollEintragTyp typ)
        {
            this.Eintragen(new ProtokollEintrag { Typ = typ, Text = text });
        }

        /// <summary>
        /// Fügt dem Protokoll einen Eintrag hinzu.
        /// </summary>
        /// <param name="zeile">Der Protokolleintrag, der erstellt werden soll.</param>
        /// <remarks>Die Threadsicherheit ist für WPF Anwendungen,
        /// wenn in der Infrastruktur der Dispatcher eingestellt ist, 
        /// gewährleistet, sonst nicht.</remarks>
        public virtual void Eintragen(ProtokollEintrag zeile)
        {

            if (zeile.Typ == ProtokollEintragTyp.Fehler)
            {
                this.FehlerVorhanden = true;
            }

            if (this.AppKontext.Dispatcher != null)
            {
                //Damit die Threadsicherheit gewährleistet ist,
                //die Schritte, die überall Daten ändern,
                //über den Threaddienst von WPF aufrufen...
                var InvokeBeschreibung = this.AppKontext.Dispatcher.GetType()
                    .GetMethod("Invoke", new Type[] { typeof(System.Action) });

                InvokeBeschreibung.Invoke(
                    this.AppKontext.Dispatcher, 
                    new object[] 
                    {
                        new System.Action(
                            () =>
                            {
                                this.Liste.Add(zeile);
                                this.Rückrufe.AlleAusführen();
                                this.Speichern(zeile);
                            }
                        )
                    });

            }
            else
            {
                //Die Threadsicherheit ist nicht gewährleistet...
                this.Liste.Add(zeile);
                this.Rückrufe.AlleAusführen();
                this.Speichern(zeile);
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private SchwacherMethodenVerweisListe _Rückrufe = null;

        /// <summary>
        /// Ruft die Liste zum Sammeln der abonnierten
        /// Rückmethoden ab.
        /// </summary>
        protected SchwacherMethodenVerweisListe Rückrufe
        {
            get
            {
                if (this._Rückrufe == null)
                {
                    this._Rückrufe = new SchwacherMethodenVerweisListe();
                    this.Eintragen(
                        $"{this} hat die Liste für die Rückrufe erstellt...", 
                        ProtokollEintragTyp.NeueInstanz);

                    //Falls nicht stornierte Rückrufe gefunden wurden,
                    //einen Fehlereintrag im Protokoll hinterlassen
                    this._Rückrufe.NichtStornierteRückrufe 
                        += (sender, e) 
                        //Damit keine Rekursiv auftritt
                        => this.Liste.Add(new ProtokollEintrag
                        {
                            Text =
                            "Nicht stornierte Rückrufe entdeckt. Alle nicht mehr benötigten Rückrufe stornieren!",
                            Typ = ProtokollEintragTyp.Fehler
                        });
                }

                return this._Rückrufe;
            }
        }

        /// <summary>
        /// Bittet den ProtokollManager bei einem
        /// neuen Eintrag die angegebene Methode auszuführen.
        /// </summary>
        /// <param name="rückrufMethode">Die Methode, die 
        /// bei einem neuen Protokoll aufgerufen werden soll.</param>
        public virtual void AbonniereRückruf(System.Action rückrufMethode)
        {
            this.Rückrufe.Add(new SchwacherMethodenVerweis(rückrufMethode));

            this.Eintragen(
                $"{this} hat einen Rückruf für {rückrufMethode.Target}({rückrufMethode.Target.GetHashCode()}) abonniert...", 
                ProtokollEintragTyp.Warnung);
        }

        /// <summary>
        /// Bittet den ProtokollManager die angegebene Methode 
        /// nicht mehr auszuführen.
        /// </summary>
        /// <param name="rückrufMethode">Die Methode, die 
        /// nicht mehr aufgerufen werden soll.</param>
        public virtual void StorniereRückruf(System.Action rückrufMethode)
        {
            var Rückruf = (from r in this.Rückrufe
                           where r.AufrufDelegate.Method == rückrufMethode.Method
                           && r.AufrufDelegate.Target == rückrufMethode.Target
                           select r).FirstOrDefault();

            if (Rückruf != null)
            {
                this.Rückrufe.Remove(Rückruf);
                this.Eintragen(
                    $"{this} hat einen Rückruf für {rückrufMethode.Target}({rückrufMethode.Target.GetHashCode()}) storniert.");

            }

        }

        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private bool _FehlerVorhanden = false;

        /// <summary>
        /// Ruft einen Wahrheitswert ab,
        /// ob im Protokoll ein unbestätigter
        /// Fehler vorhanden oder legt diesen fest.
        /// </summary>
        public bool FehlerVorhanden
        {
            get
            {
                return this._FehlerVorhanden;
            }
            set
            {
                this._FehlerVorhanden = value;

                if (value)
                {
                    this.FehlerPulsierenAsync();

                }

                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private bool _FehlerPulsierend = false;

        /// <summary>
        /// Ruft einen pulisierenden Wahrheitswert
        /// ab, wenn ein unbestätigter Fehler im
        /// Protokoll vorhanden ist.
        /// </summary>
        public bool FehlerPulsierend
        {
            get
            {
                return this._FehlerPulsierend;
            }
        }

        /// <summary>
        /// Interes Hilfsfeld für die Methode, die
        /// in einem eigenen Thread FehlerPulsierend berechnet.
        /// </summary>
        /// <remarks>Solche Methoden heißen "Asynchron"</remarks>
        private bool _FehlerPulsierendLäuft = false;

        /// <summary>
        /// Wechselt im Intervall von einer 1/2 Sekunde
        /// FehlerPulsierend von True auf False
        /// solange unbestätigte Fehler vorhanden sind.
        /// </summary>
        /// <remarks>Kennzeichen von Methoden, die
        /// in einem eigenen Thread laufen, ist Suffix "Async"</remarks>
        protected virtual void FehlerPulsierenAsync()
        {


            if (!this._FehlerPulsierendLäuft)
            {
                this._FehlerPulsierendLäuft = true;

                //Mit Hilfe von TAP die Methode in
                //einem eigenen Thread laufen lassen
                System.Threading.Tasks.Task.Run(() =>
                {
                    while (this.FehlerVorhanden)
                    {
                        this._FehlerPulsierend = !this._FehlerPulsierend;

                        //Damit die Datenbindung die neue Einstellung mitbekommt...
                        this.OnPropertyChanged("FehlerPulsierend");

                        System.Threading.Thread.Sleep(this._FehlerPulsierend ? 1000 : 200);
                    }

                    //Sicherstellen, dass die LED ausgeht
                    this._FehlerPulsierend = false;
                    this.OnPropertyChanged("FehlerPulsierend");

                    this._FehlerPulsierendLäuft = false;
                });
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private string _Pfad = string.Empty;

        /// <summary>
        /// Ruft den vollständigten Dateinamen
        /// der Protokolldatei ab, die zum Speichern
        /// eines neuen Eintrags benutzt werden soll,
        /// oder legt diesen fest.
        /// </summary>
        /// <remarks>Sollen die Protkolleinträge nicht
        /// gespeichert, die Einstellung leer lassen.</remarks>
        public string Pfad
        {
            get
            {
                return this._Pfad;
            }
            set
            {
                this._Pfad = value;
            }
        }

        /// <summary>
        /// Schreibt den Inhalt des Eintrags in
        /// die im Pfad angegebene Textdatei.
        /// </summary>
        /// <param name="eintrag">Protokolleintrag, der gespeichert werden soll.</param>
        /// <remarks>Sollten beim Speichern Fehler auftreten,
        /// wird die Funktion automatisch deaktiviert.</remarks>
        protected virtual void Speichern(ProtokollEintrag eintrag)
        {
            

            if (this.Pfad != string.Empty)
            {
                //Blockvariablen!!!
                const int MaxVersuche = 10;
                int Versuche = MaxVersuche;

                do
                {

                    try
                    {
                        using (var Schreiber = new System.IO.StreamWriter(
                            this.Pfad,
                            append: true,
                            encoding: System.Text.Encoding.Default))
                        {
                            const string AusgabeMuster = "{0}\t{1}\t{2}";

                            Schreiber.WriteLine(
                                string.Format(
                                    AusgabeMuster,
                                    eintrag.ErstelltUm + (MaxVersuche - Versuche + 1).ToString().PadLeft(3), //{0}
                                    eintrag.Typ.ToString().PadRight(this.MaximaleLängeProtokollTyp),         //{1}
                                    eintrag.Text.Replace("\r\n"," ").Replace("\t"," ")                       //{2}

                                    ));

                            //Alles OK
                            Versuche = 0;

                        }
                    }
                    catch (System.IO.IOException ioEx)
                    {
                        //Wahrscheinlich ist die Datei gerade gesperrt
                        //Noch einmal probieren
                        Versuche--;
                        System.Threading.Thread.Sleep(this.AppKontext.Zufallsgenerator.Next(200, 300 + 1));
                    }
                    catch (System.Exception ex)
                    {
                        //Sollten beim Speichern Fehler auftreten,
                        //die Funktion deaktivieren
                        Versuche = 0;
                        this.Pfad = string.Empty;
                    }

                } while (Versuche != 0);
            }
        }

        /// <summary>
        /// Komprimiert das aktuelle Protkoll und
        /// erstellt ein neues.
        /// </summary>
        /// <param name="maxGenerationen">Die Anzahl der
        /// aufzubewahrenden alten Protokolle in den Zips.</param>
        public virtual void Zusammenräumen(int maxGenerationen)
        {
            if (System.IO.File.Exists(this.Pfad))
            {
                //Platz schaffen für das neue Zip
                for (var i = maxGenerationen; i > 1; i--)
                {
                    var Älter = $"{this.Pfad}.{i}.zip";
                    var Neuer = $"{this.Pfad}.{i - 1}.zip";

                    if (System.IO.File.Exists(Älter))
                    {
                        System.IO.File.Delete(Älter);
                    }

                    if (System.IO.File.Exists(Neuer))
                    {
                        System.IO.File.Move(Neuer, Älter);
                    }
                }

                //Das aktuelle Protokoll zippen
                using (var ZipDatei = new System.IO.FileStream($"{this.Pfad}.1.zip", System.IO.FileMode.Create))
                {
                    using (var ZipArchiv = new System.IO.Compression.ZipArchive(
                                                    ZipDatei, 
                                                    System.IO.Compression.ZipArchiveMode.Create))
                    {
                        using (var Schreiber = new System.IO.StreamWriter(
                                                        ZipArchiv.CreateEntry(
                                                            System.IO.Path.GetFileName(this.Pfad)
                                                                                ).Open()))
                        {
                            using (var Leser = new System.IO.StreamReader(this.Pfad, System.Text.Encoding.Default))
                            {
                                Schreiber.Write(Leser.ReadToEnd());
                            }
                        }
                    }
                }

                //Das aktuelle Protokoll löschen
                System.IO.File.Delete(this.Pfad);

                this.Eintragen($"{this} hat das alte Protokoll gezippt. {maxGenerationen} Generation werden behalten...");
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private int? _MaximaleLängeProtokollTyp = null;

        /// <summary>
        /// Ruft die Anzahl der Zeichen des
        /// längsten ProtkollEintragTyp-Eintrags ab.
        /// </summary>
        protected int MaximaleLängeProtokollTyp
        {
            get
            {
                if (!this._MaximaleLängeProtokollTyp.HasValue)
                {
                    this._MaximaleLängeProtokollTyp 
                        = (from e in System.Enum.GetNames(typeof(NiedSchwa.Anwendung.Daten.ProtokollEintragTyp))
                           select e.Length).Max();

                    this.Eintragen($"{this} hat den längsten Protokolltyp ermittelt...");

                }

                return this._MaximaleLängeProtokollTyp.Value;
            }
        }
    }
}
