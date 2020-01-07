using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.AusstellungsHelfer.ViewModels
{
    /// <summary>
    /// Stellt den Dienst zum Verwalten der Kundenview zur
    /// verfügung
    /// </summary>
   public class KundenViewModel : ViewModels.BasisViewModel
    {

        /// <summary>
        /// Internes Feld für die Eigenschft
        /// </summary>
        private Model.DTOs.Kunde _AktuellerKunde = null;


        /// <summary>
        /// Ruft den derzeit aktiven Kunden ab
        /// oder legt diesen fest.
        /// </summary>
        public Model.DTOs.Kunde AktuellerKunde
        {
            get
            {
                if (this._AktuellerKunde == null)
                {
                    this.AktiviereBeschäftigt();

                    this._AktuellerKunde = new Model.DTOs.Kunde();


                    this.AppKontext.Protokoll.Eintragen($"Der Kunde {this._AktuellerKunde.Nachname} aus der Stadt {this._AktuellerKunde.Stadt} wurde als Aktueller Kunde festgeleg...");


                    this.DeaktiviereBeschäftigt();
                }


                return this._AktuellerKunde;
            }

            set
            {



                if (value != null)
                {

                    this._AktuellerKunde.KundenNummer = 0;
                    //Neuer Verweis
                    this._AktuellerKunde = value;

                    this.FensterManager.BestellungsViewModel.BestellungZurücksetzen();

                    this.FensterManager.BestellungsViewModel.BestellungenAktualisieren(_AktuellerKunde.KundenNummer);

                    //this._AktuelleBestellung = null;
                    //this._Bestellungen = null;




                    //View Benachrichtigen.
                    this.OnPropertyChanged();
                    this.OnPropertyChanged("AktuellerKunde");
                    this.OnPropertyChanged("AktuellerKundenNachname");
                  //  this.OnPropertyChanged("FensterManager.BestellungsViewModel.Bestellungen");
                }


            }
        }

        private string _KundenSuchName = string.Empty;

        /// <summary>
        /// Legt den Aktuellen kunden Suchnamen fest.
        /// </summary>
        public string KundenSuchName
        {
            get
            {
                return this._KundenSuchName;
            }

            set
            {
                if (value != null && value != string.Empty)
                {
                    this._AktuelleKunden = null;

                    _KundenSuchName = value;

                    this._AktuelleKunden = this._AktualisiereKunden(value, string.Empty);// this.AktuellerKundenNachname);

                    this.OnPropertyChanged("AktuelleKunden");

                    this.OnPropertyChanged();


                }
            }
        }





        /// <summary>
        /// Erzeugt eine Kundenliste
        /// </summary>
        /// <param name="nachname">Nachname des Kunden.</param>
        /// <param name="vorname">Vorname des Kunden.</param>
        /// <returns>Kundenliste</returns>
        private Model.DTOs.Kunden _AktualisiereKunden(string nachname, string vorname)
        {

            this._AktuelleKunden = null;
            var BestellungsManager = this.AppKontext.Erzeuge<Model.Manager.KundenManager>();

            var Kunden = BestellungsManager.SucheKunden(nachname, vorname);

            return Kunden;
        }





        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private Model.DTOs.Kunden _AktuelleKunden = null;



        /// <summary>
        /// Ruft die Aktuell verfügbare Kundenliste ab
        /// </summary>
        public Model.DTOs.Kunden AktuelleKunden
        {
            get
            {
                if (this._AktuelleKunden == null)
                {

                    this._AktuelleKunden = new Model.DTOs.Kunden();

                }


                return this._AktuelleKunden;
            }

        }

        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private NiedSchwa.Windows.Befehl _KundeAnlegen = null;

        /// <summary>
        /// Ruft den Befehl ab, mit dem 
        /// ein neuer Kunde angelegt werden kann.
        /// </summary>
        public NiedSchwa.Windows.Befehl KundeAnlegen
        {
            get
            {
                if (this._KundeAnlegen == null)
                {
                    this.AktiviereBeschäftigt();

                    this._KundeAnlegen = new Windows.Befehl(
                        daten =>
                        {
                            this.AktiviereBeschäftigt();

                            var KundenDatenSindOK = false;

                            var KundenPrüfer = new Erweiterungen.KundenDatenPrüfer();

                            //Kundenfelder Prüfen...
                            KundenDatenSindOK = KundenPrüfer.KundenNamenPrüfen(this.AktuellerKunde.Vorname) &&
                                                KundenPrüfer.KundenNamenPrüfen(this.AktuellerKunde.Nachname) &&
                                                KundenPrüfer.KundenNamenPrüfen(this.AktuellerKunde.Stadt) &&
                                                KundenPrüfer.KundenNamenPrüfen(this.AktuellerKunde.Straße) &&
                                                KundenPrüfer.TelefonNummerPrüfen(this.AktuellerKunde.TelNummer) &&
                                                KundenPrüfer.HausnummerPrüfen(this.AktuellerKunde.HausNummer);


                            //Kundendaten speichern...
                            if (KundenDatenSindOK && this._KundeSpeichern())
                            {
                                System.Windows.MessageBox.Show(
                                $"Kunde {this.AktuellerKunde.Nachname} {this.AktuellerKunde.Vorname} wurde Gespeichert",
                                "Neuekunde Anlegen",
                                 System.Windows.MessageBoxButton.OK,
                                System.Windows.MessageBoxImage.Information);


                                this.AppKontext.Protokoll.Eintragen($"Kunde {this.AktuellerKunde.Nachname} {this.AktuellerKunde.Vorname} wurde Gespeichert");


                                var Manager = this.AppKontext.Erzeuge<Model.Manager.KundenManager>();



                                this._AktuellerKunde = Manager.NeuKundeSuchen(this.AktuellerKunde.Nachname, this.AktuellerKunde.Vorname).FirstOrDefault();

                                this._AktuelleKunden = null;
                                this.FensterManager.BestellungsViewModel.BestellungZurücksetzen();

                                //this._Bestellungen = null;
                                //this._AktuelleBestellung = null;


                                this.OnPropertyChanged("AktuelleKunden");
                                this.OnPropertyChanged("AktuellerKunde");
                                this.OnPropertyChanged("Bestellungen");

                            }
                            else
                            {
                                System.Windows.MessageBox.Show(
                                $"Kunde konnte nicht angelegt werden. Bitte prüfen Sie die Eingegebnen Daten",
                                "Neuekunde Anlegen",
                                 System.Windows.MessageBoxButton.OK,
                                System.Windows.MessageBoxImage.Error);
                            }


                            this.DeaktiviereBeschäftigt();
                        },
                        //CanExecute Methode...
                        daten => this.KundeIstGesetzt
                        );

                    this.AppKontext.Protokoll.Eintragen("Der Befehl Kunde Anlegen wurde gecachet...");

                    this.DeaktiviereBeschäftigt();
                }

                return this._KundeAnlegen;
            }
        }


       

        /// <summary>
        /// Prüft ob die Aktuellen Eingaben der Kundenfelder
        /// Valid sind. 
        /// </summary>
        public bool KundeIstGesetzt
        {
            get
            {

                return this.AktuellerKunde != null &&
                        this.AktuellerKunde.Vorname != string.Empty &&
                        this.AktuellerKunde.Nachname != string.Empty &&
                        this.AktuellerKunde.PLZ > 1000 &&
                        this.AktuellerKunde.PLZ < 9999 &&
                        this.AktuellerKunde.Stadt != string.Empty &&
                        this.AktuellerKunde.Straße != string.Empty &&
                        this.AktuellerKunde.TelNummer != string.Empty &&
                        this.AktuellerKunde.HausNummer != string.Empty;

            }
        }


        /// <summary>
        /// Speichert die Eingaben der Kundenfelder in der Datenbank ab.
        /// </summary>
        /// <returns>True wenn erfolgreich gespeichert wurde.</returns>
        private bool _KundeSpeichern()
        {

            var Manager = this.AppKontext.Erzeuge<Model.Manager.KundenManager>();
            var Ergebins = false;


            if (this.KundeIstGesetzt)
            {
                Ergebins = Manager.NeuenKundenSpeichern(this.AktuellerKunde);

            }

            return Ergebins;

        }


        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private NiedSchwa.Windows.Befehl _KundenfelderLeeren = null;

        /// <summary>
        /// Ruft den Befehl ab, mit dem 
        /// ein neuer Kunde angelegt werden kann.
        /// </summary>
        public NiedSchwa.Windows.Befehl KundenfelderLeeren
        {
            get
            {
                if (this._KundenfelderLeeren == null)
                {
                    this.AktiviereBeschäftigt();

                    this._KundenfelderLeeren = new Windows.Befehl(
                        daten =>
                        {
                            this._KundenfelderReset();
                            this.FensterManager.BestellungsViewModel.BestellungZurücksetzen();
                        }
                        ,
                        //CanExecute Methode...
                        daten => this.KundeIstGesetzt
                        );

                    this.AppKontext.Protokoll.Eintragen("Der Befehl Kunde Anlegen wurde gecachet...");

                    this.DeaktiviereBeschäftigt();
                }

                return this._KundenfelderLeeren;
                ;
            }
        }


        /// <summary>
        /// Setzt alle KundenDaten auf null.
        /// </summary>
        private void _KundenfelderReset()
        {
            this._KundenSuchName = null;
            this._AktuellerKunde = null;
            this.FensterManager.BestellungsViewModel.BestellungZurücksetzen();
        //    this._Bestellungen = null;
            this._AktuelleKunden = null;
            this.OnPropertyChanged("AktuellerKunde");
            this.OnPropertyChanged("AktuelleKunden");
            this.OnPropertyChanged("Bestellungen");
            this.OnPropertyChanged("KundenSuchName");
            
        }


    }
}
