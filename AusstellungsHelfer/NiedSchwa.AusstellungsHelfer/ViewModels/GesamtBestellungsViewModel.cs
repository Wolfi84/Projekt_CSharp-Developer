using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.AusstellungsHelfer.ViewModels
{
    /// <summary>
    /// Stellt den Dienst zum Verwalten des
    /// Gesamtbestellungsview zur Verfügung.
    /// </summary>
    public class GesamtBestellungsViewModel : ViewModels.BasisViewModel
    {
       
        /// <summary>
        /// Feld für die interne Eigenschaft
        /// </summary>
        private Model.DTOs.Bücher _GesamtBuchListe = null;

        /// <summary>
        /// Stellt eine Gesamtliste aller Bestellpositionen
        /// zur Verfügung
        /// </summary>
        public Model.DTOs.Bücher GesamtBuchListe
        {
            get
            {
                if (_GesamtBuchListe == null)
                {
                   

                    this._GesamtBuchListe = new Model.DTOs.Bücher();
                }
                return this._GesamtBuchListe;
            }

        }

        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private System.DateTime _DatumVon = System.DateTime.Now;



        /// <summary>
        /// Ruft Aktuelle Obergrenze des Suchdatumsbereichs ab
        /// oder legt diesen fest.
        /// </summary>
        public System.DateTime DatumVon
        {
            get
            {
                return this._DatumVon;
            }
            set
            {
                if(this._DatumPrüfen(value, this.DatumBis) && this._DatumVon != value)
                {
                    _GesamtBuchlisteAktualisieren(value, this.DatumBis);
                    this.OnPropertyChanged("GesamtBuchListe");
                    this.OnPropertyChanged();


                    this._DatumVon = value;
                }

            }

        }


        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private System.DateTime _DatumBis = System.DateTime.Now;

        /// <summary>
        /// Ruft Aktuelle Untergrenze des Suchdatumsbereichs ab
        /// oder legt diesen fest.
        /// </summary>
        public System.DateTime DatumBis
        {
            get
            {
                return this._DatumBis;
            }
            set
            {
                if (this._DatumPrüfen(this.DatumVon, value) && this._DatumBis != value)
                {
                    _GesamtBuchlisteAktualisieren(this.DatumVon, value);
                    this.OnPropertyChanged("GesamtBuchListe");
                    this.OnPropertyChanged();

                    this._DatumBis = value;

                }


            }

        }


        /// <summary>
        /// Aktualisiert die Gesamtbestellliste im Aktuellen
        /// Datumsbereich.
        /// </summary>
        /// <param name="datumVon">Datums Obergrenze</param>
        /// <param name="datumBis">Datums Untergrenze</param>
        private void _GesamtBuchlisteAktualisieren(System.DateTime datumVon, System.DateTime datumBis)
        {

            var Manager = this.AppKontext.Erzeuge<Model.Manager.BestellungsManager>();


            this._GesamtBuchListe = Manager.HoleGesamtBuchListe(datumVon, datumBis);
            this.OnPropertyChanged("GesamtBuchListe");

        }


        /// <summary>
        /// Prüft ob das Eingegebenen Datum vor dem bis liegt 
        /// und sendet eine Benachrichtigung an den User.
        /// </summary>
        /// <param name="von">Datums Untergrenze</param>
        /// <param name="bis">Datums Obergrenze</param>
        /// <returns>True wenn Datumseingabe in ordnung ist.</returns>
        private bool _DatumPrüfen(System.DateTime von, System.DateTime bis)
        {


            var vergleich = DateTime.Compare(von, bis);

            if (vergleich == 1)
            {
                System.Windows.MessageBox.Show
                    ("Das Datum in \"Alle Bestellungen von:\" ist ungültig! \n" +
                    "Der Wert muss zeitlich vor oder gleich dem \n" +
                    "\"Bis:\" - Datum sein",
                    "Eingabefehler",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Stop);
                this._DatumVon = this._DatumBis;

                return false;
            }

            return true;
        }



        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private NiedSchwa.Windows.Befehl _GesammtbestellungDrucken = null;

        /// <summary>
        /// Ruft den Befehl ab, mit dem 
        /// die noch unbestellte Buchposition gelöscht werden kann.
        /// </summary>
        public NiedSchwa.Windows.Befehl GesammtbestellungDrucken
        {
            get
            {
                if (this._GesammtbestellungDrucken == null)
                {
                    this.AktiviereBeschäftigt();

                    this._GesammtbestellungDrucken = new Windows.Befehl(
                        //Execute Methode...
                        daten =>
                        {
                            if(this.Veranstaltungsort == null || this.Veranstaltungsort == string.Empty)
                            {
                                System.Windows.MessageBox.Show(
                                    "Es muss ein Veranstaltungsort eingetragen werden um die Bestellliste zu drucken",
                                    "Eingabefehler",
                                    System.Windows.MessageBoxButton.OK,
                                    System.Windows.MessageBoxImage.Stop);

                                return;
                            }


                           //Print Dialog erzeugen..
                            //könnte durch eigenen dialog ersetzt werden
                            System.Windows.Controls.PrintDialog Dialog = new System.Windows.Controls.PrintDialog();

                            Nullable<bool> DialogErgebnis = Dialog.ShowDialog();
                            if (DialogErgebnis == true)

                            {
                                //Page einstellen
                                Dialog.PageRangeSelection = System.Windows.Controls.PageRangeSelection.AllPages;
                                Dialog.UserPageRangeEnabled = true;

                               
                                Dialog.PrintTicket.PageOrientation = System.Printing.PageOrientation.Portrait;

                                Dialog.PrintTicket.PageScalingFactor = 100;

                                Dialog.PageRangeSelection = System.Windows.Controls.PageRangeSelection.AllPages;


                                //Gesamtbestellungsobjekt erzeugen...
                                var Bestellung = new Model.DTOs.Gesamtbestllung{ Veranstaltungsort = this.Veranstaltungsort,
                                                                                  DatumBis = this.DatumBis,
                                                                                  DatumVon = this.DatumVon};


                                //Manager Erzeugen...
                                var DruckManager = this.AppKontext.Erzeuge<Model.Manager.DokumentManager>();

                                //Druckgröße festlegen...
                                var Druckgröße = new System.Windows.Size { Height = Dialog.PrintableAreaHeight, Width = Dialog.PrintableAreaWidth };


                                //Test für mehrseitige dokumente
                                //for (int i = 0; i<200; i++)
                                //{
                                //    this.Bücher.Add(this.Bücher[0]);
                                //}

                                //   Dialog.PrintDocument(DruckManager.GesamtBestellungErzeugen(this.Bücher, new Model.DTOs.GesamtBestellung { DateFrom = System.DateTime.Now,DateTo = System.DateTime.Now }, Druckgröße).DocumentPaginator, "Rechnung Nr.: , Nachname des Kunden.");


                                ////Dokument erzeugen und dem Dialog übergeben...                                                                                             
                                //Dialog.PrintDocument(DruckManager.GesamtBestellungErzeugen(this.GesamtBuchListe,this.DatumVon, this.DatumBis,Druckgröße).DocumentPaginator,
                                //                        $"Gesammtbestellnung von {this.DatumBis.ToShortDateString()} bis {this.DatumBis.ToShortDateString()}");

                                //Änderung laut Kundenwunsch 20190628 Niedermayr
                                //Der Veranstaltungsort soll bei der Bestellung mit Gedruckt werden.
                                Dialog.PrintDocument(DruckManager.GesamtBestellungErzeugen(this.GesamtBuchListe, Bestellung, Druckgröße).DocumentPaginator,
                                                        $"Gesammtbestellnung von {this.DatumBis.ToShortDateString()} bis {this.DatumBis.ToShortDateString()}");

                                //Cleanup...
                               // this.Veranstaltungsort = string.Empty;
                                this.DatumVon = System.DateTime.Now.Date;
                                this.DatumBis = System.DateTime.Now.Date;
                            }

                        },
                           //CanExecute Methode...
                           daten => this.GesamtBuchListe.Count > 0
                        );

                    this.AppKontext.Protokoll.Eintragen("Der Befehl wurde gecachet...");

                    this.DeaktiviereBeschäftigt();
                }

                return this._GesammtbestellungDrucken;
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _Veranstaltungsort;


        /// <summary>
        /// Ruft den Aktuellen Veranstaltungsort auf 
        /// oder legt diesen fest.
        /// </summary>
        public string Veranstaltungsort
        {
            get
            {
                return Properties.Settings.Default.Veranstaltungsort.ToString();
            }
            set
            {
                this._Veranstaltungsort = value;
            }
        }



    }
}
