using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.AusstellungsHelfer.ViewModels
{
    /// <summary>
    /// Stellt den Dienst zum Verwalten der Bestellungsview zur
    /// Verfügung.
    /// </summary>
    public class BestellungsViewModel : ViewModels.BasisViewModel
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private Model.DTOs.Bestellungen _Bestellungen = null;

        /// <summary>
        /// Ruft die für den Aktiven Kunden vorhandenen Bestellungen ab.
        /// </summary>
        public Model.DTOs.Bestellungen Bestellungen
        {
            get
            {
                if (this._Bestellungen == null && this.FensterManager.KundenViewModel.AktuellerKunde.KundenNummer > 0)
                {
                    this.AktiviereBeschäftigt();

                    //Den BestellManager Temporär Erzeugen
                    var BestellManager = this.AppKontext.Erzeuge<Model.Manager.BestellungsManager>();
                    this._Bestellungen = BestellManager.HoleBestellungen(this.FensterManager.KundenViewModel.AktuellerKunde.KundenNummer);


                    this.AppKontext.Protokoll.Eintragen("Die Bestellliste wurde aktualisiert...");
                    this.DeaktiviereBeschäftigt();

                }

                return this._Bestellungen;
            }

            set
            {
                if (this._Bestellungen == null)
                {
                    this._Bestellungen = new Model.DTOs.Bestellungen();
                }
                this._Bestellungen = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private Model.DTOs.Bücher _Bücher = null;


        /// <summary>
        /// ruft die Aktuelle Bücher ab
        /// </summary>
        public Model.DTOs.Bücher Bücher
        {
            get
            {
                if (this._Bücher == null)
                {

                    this.AktiviereBeschäftigt();

                    //BestellungsManager Temporär anlegen um Ressourcen zu schonen
                    var BestellManager = this.AppKontext.Erzeuge<Model.Manager.BestellungsManager>();
                    this._Bücher = BestellManager.HoleBuchListe(this.AktuelleBestellung);

                    //Das Anwendungsfenster Aktualisieren
                    this.OnPropertyChanged();

                    this.AppKontext.Protokoll.Eintragen("Die Buchbestellungen wurden aktualisiert...");
                    this.DeaktiviereBeschäftigt();
                }

                return this._Bücher;
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private Model.DTOs.Buch _AktuellesBuch = null;

        /// <summary>
        /// Ruft das Aktuell Ausgewählte Buch der Aktuellen Buchliste ab
        /// oder legt dieses Fest.
        /// </summary>
        public Model.DTOs.Buch AktuellesBuch
        {
            get
            {
                if (this._AktuellesBuch == null)
                {
                    this._AktuellesBuch = new Model.DTOs.Buch();
                    this.OnPropertyChanged();
                }
                return this._AktuellesBuch;
            }
            set
            {


                this._AktuellesBuch = value;

                this._GesuchtesBuch = null;
                this._AnzahlBücherSoll = null;
                this.Buchnummer = null;

                this.OnPropertyChanged("GesuchtesBuch");
                this.OnPropertyChanged("Buchnummer");
                this.OnPropertyChanged("AnzahlBücherSoll");
                this.OnPropertyChanged();
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private Model.DTOs.Buch _GesuchtesBuch = null;

        /// <summary>
        /// Ruft das Aktuell Gesuchte Buch der Aktuellen Buchliste ab
        /// oder legt dieses Fest.
        /// </summary>
        public Model.DTOs.Buch GesuchtesBuch
        {
            get
            {

                return this._GesuchtesBuch;
            }
            set
            {

                if (value != null)
                {

                    this._GesuchtesBuch = value;

                    this.OnPropertyChanged();

                }
            }
        }


        /// <summary>
        /// Internes feld für die Eigenschft
        /// </summary>
        private int? _Buchnummer = null;

        /// <summary>
        /// Legt die zu suchende Buchnummer fest
        /// oder gibt dieses zurück.
        /// </summary>
        public int? Buchnummer
        {
            get
            {
                return this._Buchnummer;
            }

            set
            {
                if (value != null)
                {

                    this._Buchnummer = value;
                    var Manager = this.AppKontext.Erzeuge<Model.Manager.BestellungsManager>();

                    this.GesuchtesBuch = Manager.SucheBuch(value.Value);
                    this.OnPropertyChanged("GesuchtesBuch");
                }
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private int? _AnzahlBücherSoll;

        /// <summary>
        /// Ruft die Aktuell zu bestellende Anzahl an büchern ab
        /// oder legt diese fest.
        /// </summary>
        public int? AnzahlBücherSoll
        {
            get
            {
                return this._AnzahlBücherSoll;
            }

            set
            {
                this._AnzahlBücherSoll = 1;
                this.OnPropertyChanged();

                this._AnzahlBücherSoll = value;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private NiedSchwa.Windows.Befehl _NeueBestellungAnlegen = null;

        /// <summary>
        /// Ruft den Befehl ab, mit dem 
        /// eine neue Bestellung angelegt werden kann.
        /// </summary>
        public NiedSchwa.Windows.Befehl NeueBestellungAnlegen
        {
            get
            {
                if (this._NeueBestellungAnlegen == null)
                {
                    this.AktiviereBeschäftigt();

                    this._NeueBestellungAnlegen = new Windows.Befehl(
                        //Execute Methode...
                        daten =>
                        {

                            this.AktiviereBeschäftigt();


                            var Manager = this.AppKontext.Erzeuge<Model.Manager.KundenManager>();

                            var Ergebnis = Manager.SucheKunden(this.FensterManager.KundenViewModel.AktuellerKunde.KundenNummer);


                            if (this.FensterManager.KundenViewModel.KundeIstGesetzt &&
                                this.FensterManager.KundenViewModel.AktuellerKunde.KundenNummer > 0 &&
                                    Ergebnis.Count > 0 && this.FensterManager.KundenViewModel.AktuellerKunde.Equals(Ergebnis[0]))
                            {
                                this._AktuelleBestellung = null;

                                this._Bücher = null;

                                //Neue Bestellung in den Cache schreiben...
                                this._Bestellungen.Add(
                                    new Model.DTOs.Bestellung
                                    {
                                        BestellDatum = DateTime.Now,
                                        BestellStatus = Model.DTOs.BestellStatus.Neu


                                    });

                                this._AktuelleBestellung = this.Bestellungen.LastOrDefault();

                                //this._Bestellungen = null;
                                this._Bücher = null;
                                this.OnPropertyChanged("Bestellungen");
                                this.OnPropertyChanged("AktuelleBestellung");
                                this.OnPropertyChanged("Bücher");


                                this.AktiviereBeschäftigt();
                            }
                            else
                            {
                                System.Windows.MessageBox.Show(
                                  $"Kunde muss erst angelegt weren.",
                                  "Neue Bestellung anlegen.",
                                  System.Windows.MessageBoxButton.OK,
                                  System.Windows.MessageBoxImage.Stop);
                            }
                        },
                           //CanExecute Methode...
                           daten => this.FensterManager.KundenViewModel.KundeIstGesetzt
                        );

                    this.AppKontext.Protokoll.Eintragen("Der Befehl wurde gecachet...");

                    this.DeaktiviereBeschäftigt();
                }

                return this._NeueBestellungAnlegen;
            }
        }




        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private NiedSchwa.AusstellungsHelfer.Model.DTOs.Bestellung _AktuelleBestellung = null;



        /// <summary>
        /// Ruft die Aktuelle Bestellung für den Aktuellen kunden ab
        /// oder legt diese fest.
        /// </summary>
        public NiedSchwa.AusstellungsHelfer.Model.DTOs.Bestellung AktuelleBestellung
        {
            get
            {
                if (this._AktuelleBestellung == null)
                {
                    this._AktuelleBestellung = new Model.DTOs.Bestellung();
                }

                return this._AktuelleBestellung;
            }
            set
            {
                if (this.AktuelleBestellung != value)
                {

                    this._AktuelleBestellung = value;

                    this._Bücher = null;
                    this._AktuellesBuch = null;
                    this._GesuchtesBuch = null;
                    this.OnPropertyChanged();
                    this.OnPropertyChanged("Bücher");
                    this.OnPropertyChanged("GesuchtesBuch");
                }
            }

        }



        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private NiedSchwa.Windows.Befehl _BuchHinzufügen = null;

        /// <summary>
        /// Ruft den Befehl ab, mit dem 
        /// ein Buch der Neuen Bestellung hizugefügt wird.
        /// </summary>
        public NiedSchwa.Windows.Befehl BuchHinzufügen
        {
            get
            {
                if (this._BuchHinzufügen == null)
                {
                    this.AktiviereBeschäftigt();

                    this._BuchHinzufügen = new Windows.Befehl(
                        //Execute Methode...
                        daten =>
                        {
                            if (this.FensterManager.KundenViewModel.KundeIstGesetzt && this.GesuchtesBuch != null &&
                                GesuchtesBuch.Name != string.Empty &&
                                    this.AnzahlBücherSoll != null &&
                                        this.AnzahlBücherSoll > 0 && this.Buchnummer != null && this.Buchnummer > 0)
                            {



                                var Ergebnis = (from bu in this.Bücher
                                                where bu.Bestellnummer == GesuchtesBuch.Bestellnummer
                                                select bu).FirstOrDefault();

                                //Prüfen ob das Buch bereits in der Bestellung ist..
                                if (this.Bücher.Contains(Ergebnis))
                                {
                                    //Buch aus Liste löschen...
                                    this.Bücher.RemoveAt(this.Bücher.IndexOf(Ergebnis));

                                    //Parameter des Kopierten Objekts ändern...
                                    Ergebnis.AnzahlBestellt += this.AnzahlBücherSoll.Value;

                                    if (Ergebnis.AnzahlBestellt > 0)
                                    {
                                        //Objekt der Liste Hinzufügen...
                                        this.Bücher.Add(Ergebnis);
                                    }

                                }
                                else
                                {
                                    //Bestellte Buchanzahl übergeben...
                                    this.GesuchtesBuch.AnzahlBestellt = this.AnzahlBücherSoll.Value;

                                    //Neue Bestellung in den Cache schreiben...
                                    this.Bücher.Add(GesuchtesBuch);
                                }


                                //Update und Clear...
                                this.OnPropertyChanged("Bücher");

                                this.GesuchtesBuch = null;

                                this._Buchnummer = null;

                                this.AnzahlBücherSoll = null;

                                this.OnPropertyChanged("AnzahlBücherSoll");
                                this.OnPropertyChanged("Buchnummer");
                            }


                            this.DeaktiviereBeschäftigt();
                        },
                           //CanExecute Methode...                          
                           daten => this.FensterManager.KundenViewModel.KundeIstGesetzt && this.AktuelleBestellung.Auftragsnummer == 0
                        );

                    this.AppKontext.Protokoll.Eintragen("Der Befehl wurde gecachet...");

                    this.DeaktiviereBeschäftigt();
                }

                return this._BuchHinzufügen;
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private NiedSchwa.Windows.Befehl _BuchPositionLöschen = null;

        /// <summary>
        /// Ruft den Befehl ab, mit dem 
        /// die noch unbestellte Buchposition gelöscht werden kann.
        /// </summary>
        public NiedSchwa.Windows.Befehl BuchPositionLöschen
        {
            get
            {
                if (this._BuchPositionLöschen == null)
                {
                    this.AktiviereBeschäftigt();

                    this._BuchPositionLöschen = new Windows.Befehl(
                        //Execute Methode...
                        daten =>
                        {
                            if (this.AktuellesBuch.AnzahlGeliefert == 0)
                            {

                                Bücher.Remove(this.AktuellesBuch);
                                this.OnPropertyChanged("Bücher");
                            }


                            this.DeaktiviereBeschäftigt();
                        },
                           //CanExecute Methode...
                           daten => this.AktuellesBuch.AnzahlGeliefert == 0 &&
                                this.AktuellesBuch.Bestellnummer > 0 &&
                                    this.AktuelleBestellung.Auftragsnummer == 0
                        );

                    this.AppKontext.Protokoll.Eintragen("Der Befehl wurde gecachet...");

                    this.DeaktiviereBeschäftigt();
                }

                return this._BuchPositionLöschen;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private NiedSchwa.Windows.Befehl _PositionÄndern = null;

        /// <summary>
        /// Ruft den Befehl ab, mit dem 
        /// die noch unbestellte Buchposition gelöscht werden kann.
        /// </summary>
        public NiedSchwa.Windows.Befehl PositionÄndern
        {
            get
            {
                if (this._PositionÄndern == null)
                {


                    this._PositionÄndern = new Windows.Befehl(
                        //Execute Methode...
                        daten =>
                        {

                            if (this.AnzahlBücherSoll != null && (this.AktuellesBuch.AnzahlGeliefert + this.AnzahlBücherSoll.Value) <= this.AktuellesBuch.AnzahlBestellt &&
                                     (this.AktuellesBuch.AnzahlGeliefert + this.AnzahlBücherSoll.Value) >= 0)
                            {
                                this.AktuellesBuch.AnzahlGeliefert += this.AnzahlBücherSoll.Value;
                            }
                            else
                            {

                                System.Windows.MessageBox.Show(
                                    $"Es wurden {this.AktuellesBuch.AnzahlBestellt} Bücher bestellt. \r Bitte Prüfen Sie die Eingegebene Anzahl an Büchern.",
                                    "Bestellposition Ändern",
                                    System.Windows.MessageBoxButton.OK,
                                    System.Windows.MessageBoxImage.Information);
                            }
                            var Manager = this.AppKontext.Erzeuge<Model.Manager.BestellungsManager>();


                            var Ergebnis = Manager.AktualisiereBestellposition(this.AktuellesBuch, this.AktuelleBestellung);
                            // SQL Update

                            //this.AktuellesBuch.Bestellnummer;

                            this._Bücher = null;
                            this._AktuellesBuch = null;
                            this._AnzahlBücherSoll = null;

                            this.OnPropertyChanged("Bücher");
                            this.OnPropertyChanged("AktuellesBuch");
                            this.OnPropertyChanged("AnzahlBücherSoll");
                            // ist anzahl = null

                            // Bücher = null
                        },
                        //CanExecute Methode...
                        daten => this.AktuellesBuch.AnzahlBestellt > 0 &&
                            this.AktuelleBestellung.BestellStatus == Model.DTOs.BestellStatus.Bestellt &&
                                this.Bücher.Count > 0 && this.AktuellesBuch != null
                        );


                }

                return this._PositionÄndern;
            }
        }




        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private NiedSchwa.Windows.Befehl _BestellungSpeichern = null;

        /// <summary>
        /// Ruft den Befehl ab, mit dem 
        /// eine Bestellung in der Datenbank gespeichert wird.
        /// </summary>
        public NiedSchwa.Windows.Befehl BestellungSpeichern
        {
            get
            {
                if (this._BestellungSpeichern == null)
                {
                    this.AktiviereBeschäftigt();

                    this._BestellungSpeichern = new Windows.Befehl(
                        //Execute Methode...
                        daten =>
                        {


                            if (this.FensterManager.KundenViewModel.KundeIstGesetzt &&
                                this.AktuelleBestellung.Auftragsnummer == 0 &&
                                    this.Bücher.Count > 0)
                            {
                                //Bestellung Speichern...



                                //Manager und Ergebnis auskommentieren und var kommentieren um das Speichern in die Datenbank zu verhindern.!


                                var Manager = this.AppKontext.Erzeuge<Model.Manager.BestellungsManager>();
                                var Ergebnis = Manager.NeueBestellungSpeichern(this.Bücher, this.FensterManager.KundenViewModel.AktuellerKunde, this.AktuelleBestellung);

                                // var Ergebnis = true;





                                //Wenn das Speichern Erfolgreich war...
                                if (Ergebnis)
                                {

                                    //Message an Benutzer und abfrage ob Rechnung gedruckt werden soll...
                                    var MessageErgebnis = System.Windows.MessageBox.Show(
                                                          $"Bestellung wurde gespeichert! \rSoll eine Rechnung Gedruckt werden?",
                                                          "Bestellung speichern",
                                                          System.Windows.MessageBoxButton.YesNo,
                                                          System.Windows.MessageBoxImage.Question);


                                    //Ergebnis der Abfrage auswerteen...
                                    switch (MessageErgebnis)
                                    {
                                        //Rechnung wird nicht gedruckt...
                                        case System.Windows.MessageBoxResult.No:

                                            break;


                                        //Rechnung Drucken...
                                        case System.Windows.MessageBoxResult.Yes:

                                            //Print Dialog erzeugen..
                                            //könnte durch eigenen dialog ersetzt werden
                                            System.Windows.Controls.PrintDialog Dialog = new System.Windows.Controls.PrintDialog();

                                            Nullable<bool> DialogErgebnis = Dialog.ShowDialog();
                                            if (DialogErgebnis == true)

                                            {
                                                //Page einstellen
                                                Dialog.PageRangeSelection = System.Windows.Controls.PageRangeSelection.AllPages;
                                                Dialog.UserPageRangeEnabled = true;

                                                Dialog.PrintTicket.PageOrientation = System.Printing.PageOrientation.Landscape;

                                                Dialog.PrintTicket.PageScalingFactor = 100;

                                                Dialog.PageRangeSelection = System.Windows.Controls.PageRangeSelection.AllPages;


                                                //Manager Erzeugen...
                                                var DruckManager = this.AppKontext.Erzeuge<Model.Manager.DokumentManager>();


                                                var Druckgröße = new System.Windows.Size { Height = Dialog.PrintableAreaHeight, Width = Dialog.PrintableAreaWidth };


                                                //Test für mehrseitige dokumente
                                                //
                                                //for (int i = 0; i<300; i++)
                                                //{
                                                //    this.Bücher.Add(this.Bücher[0]);
                                                //}

                                                //   Dialog.PrintDocument(DruckManager.GesamtBestellungErzeugen(this.Bücher, new Model.DTOs.GesamtBestellung { DateFrom = System.DateTime.Now,DateTo = System.DateTime.Now }, Druckgröße).DocumentPaginator, "Rechnung Nr.: , Nachname des Kunden.");

                                                //Dokument erzeugen und dem Dialog übergeben...                                                                                       
                                                Dialog.PrintDocument(DruckManager.RechnungsDokumentErzeugen(this.Bücher,
                                                                            this.AktuelleBestellung, 
                                                                            this.FensterManager.KundenViewModel.AktuellerKunde, 
                                                                            Druckgröße).DocumentPaginator,
                                                                                String.Concat($"Rechnung für den Kunden: {this.FensterManager.KundenViewModel.AktuellerKunde.Nachname}",  
                                                                                $"{this.FensterManager.KundenViewModel.AktuellerKunde.Vorname} wird gedruckt."));


                                                this.AppKontext.Protokoll.Eintragen(
                                                    $"Die Rechnung {this.AktuelleBestellung.Auftragsnummer} wurde für den Gedruckt...");


                                                //Cleanup...
                                            }
                                            break;

                                    }

                                }


                                //Cache leeren...
                                //this._Bestellungen = null;
                                //this._AktuelleBestellung = null;

                                this.BestellungZurücksetzen();

                                this._AktuellesBuch = null;
                                this._Bücher = null;

                                this.AktuelleBestellung = this.Bestellungen.FirstOrDefault();

                                //View benachrichtigen...
                                this.OnPropertyChanged("Bestellungen");
                                this.OnPropertyChanged("AktuelleBestellung");
                                this.OnPropertyChanged("AktuellesBuch");
                                this.OnPropertyChanged("Bücher");


                            }
                            else
                            {
                                System.Windows.MessageBox.Show(
                                "Bestellung konnte nicht gespeichert werden.",
                                "Bestellung speichern",
                                System.Windows.MessageBoxButton.OK,
                                System.Windows.MessageBoxImage.Error);
                            }

                        }
                        ,
                           //CanExecute Methode...
                           daten => this.FensterManager.KundenViewModel.KundeIstGesetzt &&
                                this.AktuelleBestellung.Auftragsnummer == 0 &&
                                    this.Bücher.Count > 0
                        );
                }
                this.AppKontext.Protokoll.Eintragen("Der Befehl wurde gecachet...");

                this.DeaktiviereBeschäftigt();


                return this._BestellungSpeichern;
            }
        }


        /// <summary>
        /// Setzt die Bestellungen auf Null
        /// </summary>
        public void BestellungZurücksetzen()
        {
            this._Bestellungen = null;
            this._AktuelleBestellung = null;
            this.OnPropertyChanged("Bestellungen");
            this.OnPropertyChanged("AktuelleBestellung");
        }


        /// <summary>
        /// Aktualisert die Bestellungen
        /// </summary>       
        public void BestellungenAktualisieren(int bestellNummer)
        {

            this.AktiviereBeschäftigt();

            //Den BestellManager Temporär Erzeugen
            var BestellManager = this.AppKontext.Erzeuge<Model.Manager.BestellungsManager>();
            this.Bestellungen = BestellManager.HoleBestellungen(bestellNummer);


            this.AppKontext.Protokoll.Eintragen("Die Bestellliste wurde aktualisiert...");
            this.DeaktiviereBeschäftigt();



        }



    }
}
