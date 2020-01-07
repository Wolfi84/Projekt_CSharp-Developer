using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.AusstellungsHelfer.ViewModels
{
    /// <summary>
    /// Stellt Dienste zum Verwalten der Administrator View zur 
    /// Verfügung.
    /// </summary>
   public class AdministratorViewModel : ViewModels.BasisViewModel
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private Model.DTOs.BuchPreisgruppe _AktuellePreisgruppe = null;

        /// <summary>
        /// Ruft die Aktuelle Preisgruppe ab oder legt diese fest.
        /// </summary>
        public Model.DTOs.BuchPreisgruppe AktuellePreisgruppe
        {
            get
            {
                if (this._AktuellePreisgruppe == null)
                {
                    this._AktuellePreisgruppe = this.Preisgruppen[0];
                    
                }
                return this._AktuellePreisgruppe;
            }
            set
            {
                if(value != null)
                {
                    this._AktuellePreisgruppe = value;
                    this.OnPropertyChanged();
                }
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private Model.DTOs.BuchPreisgruppen _Preisgruppen = null;

        /// <summary>
        /// Ruft die Aktuell Verfügbaren Preisgruppen ab.
        /// </summary>
        public Model.DTOs.BuchPreisgruppen Preisgruppen
        {
            get
            {
                if(this._Preisgruppen == null)
                {
                    var Manager = this.AppKontext.Erzeuge<Model.Manager.BestellungsManager>();
                    this._Preisgruppen = Manager.SuchePreisgruppen();
                }

                return this._Preisgruppen;
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private Model.DTOs.Bücher _Bücher = null;



        /// <summary>
        /// Ruft eine Buchliste Ab.
        /// </summary>
        public Model.DTOs.Bücher Bücher
        {
            get
            {
                if(this._Bücher == null)
                {
                    this._Bücher = new Model.DTOs.Bücher();
                }

                return this._Bücher;
            }

        }


        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private Model.DTOs.Buch _Buch;


        /// <summary>
        /// Ruft die Daten des Buches ab
        /// oder legt diese Fest.
        /// </summary>
        public Model.DTOs.Buch Buch
        {
            get
            {
                if(this._Buch == null)
                {
                    this._Buch = new Model.DTOs.Buch();
                }

                return _Buch;
            }

            set
            {


                this._Buch = value;
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private NiedSchwa.Windows.Befehl _BuchSpeichern = null;

        /// <summary>
        /// Ruft den Befehl ab, mit dem 
        /// Die Buchdaten in die Datenbank geschrieben werden.
        /// </summary>
        public NiedSchwa.Windows.Befehl BuchSpeichern
        {
            get
            {
                if (this._BuchSpeichern == null)
                {
                    this.AktiviereBeschäftigt();

                    this._BuchSpeichern = new Windows.Befehl(
                        daten =>
                        {
                            //BestellManager erzeugen...
                            var Manager = this.AppKontext.Erzeuge<Model.Manager.BestellungsManager>();

                            //Prüfen ob Buchnummer schon Vorhanden...
                            var Buch = Manager.SucheBuch(this.Buch.Bestellnummer.Value);


                            //Buchnummer auf Vorhandensein Prüfen...
                            if (Buch.Bestellnummer != null &&
                             Buch.Bestellnummer.Value != 0)
                            {
                                System.Windows.MessageBox.Show(String.Concat(
                                $"Ein Buch mit \rBestellnummer: {Buch.Bestellnummer.Value}",
                                $"\rName, Autor: {Buch.Name}, {Buch.Autor}",
                                "\rist bereits Vorhanden!"),
                                "Eingabe Prüfen!",
                                 System.Windows.MessageBoxButton.OK,
                                System.Windows.MessageBoxImage.Stop);
                            }
                            else
                            {
                                this.Buch.BuchPreisgruppe = this.AktuellePreisgruppe;

                                if(!Manager.BuchSpeichern(this.Buch))
                                {
                                    System.Windows.MessageBox.Show(String.Concat(
                                    $"Das Buch mit \rBestellnummer: {this.Buch.Bestellnummer.Value}",
                                    $"\rName, Autor: {this.Buch.Name}, {this.Buch.Autor}",
                                    "\rkonnte nicht Gespeichert werden!"),
                                    "Buch Hinzufügen",
                                    System.Windows.MessageBoxButton.OK,
                                    System.Windows.MessageBoxImage.Error);
                                }

                                this._BücherAktualisieren();
                                this.OnPropertyChanged("Bücher");

                            }

                            
                        }
                        ,
                        //CanExecute Methode...
                        daten => this.BuchDatenSindVorhanden
                        );

                    this.AppKontext.Protokoll.Eintragen("Der Befehl Buch speichern wurde gecachet...");

                    this.DeaktiviereBeschäftigt();
                }

                return this._BuchSpeichern;
                ;
            }
        }



        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private NiedSchwa.Windows.Befehl _AlleBücherAbrufen = null;

        /// <summary>
        /// Ruft den Befehl ab, mit dem 
        /// alle Vorhandenen Bücher abgerufen werden.
        /// </summary>
        public NiedSchwa.Windows.Befehl AlleBücherAbrufen
        {
            get
            {
                if (this._AlleBücherAbrufen == null)
                {
                    this.AktiviereBeschäftigt();

                    this._AlleBücherAbrufen = new Windows.Befehl(
                        daten =>
                        {
                            //Bücher holen...
                            this._BücherAktualisieren();
                            
                            //View Benachrichtigen.
                            this.OnPropertyChanged("Bücher");
                            ;
                        }
                        //,
                        ////CanExecute Methode...
                        //daten => true
                        );

                    this.AppKontext.Protokoll.Eintragen("Der Befehl AlleBücherAbrufen wurde gecachet...");

                    this.DeaktiviereBeschäftigt();
                }

                return this._AlleBücherAbrufen;
                ;
            }
        }

        /// <summary>
        /// Aktualisiert die Liste der
        /// Verfügbaren Bücher.
        /// </summary>
        private void _BücherAktualisieren()
        {
            //Den Manager erzeugen...
            var Manager = this.AppKontext.Erzeuge<Model.Manager.BestellungsManager>();

            //Bücher Holen...
            this._Bücher = Manager.SucheBücher();

        }



        /// <summary>
        /// Ruft Den Zustand ob Die Buchdaten Valid sind
        /// ab.
        /// </summary>
        public bool BuchDatenSindVorhanden
        {
            get
            {
                return this._BuchdatenPrüfen(this.Buch);
            }

        }


        /// <summary>
        /// Prüft die Validität der Bucheingabe.
        /// </summary>
        /// <param name="buch"></param>
        /// <returns>True wenn Daten Korrekt sind.</returns>
        private bool _BuchdatenPrüfen( Model.DTOs.Buch buch)
        {
            bool Ergebnis = false;

            var Prüfer = new Erweiterungen.BuchDatenPrüfer();

            Ergebnis =  buch.Bestellnummer != null &&
                        buch.Bestellnummer != null && buch.Bestellnummer > 0 &&                      
                        Prüfer.NamePrüfen(buch.Name) &&
                        Prüfer.NamePrüfen(buch.Verlag) &&
                        buch.Preis > 0.0;

            return Ergebnis;

        }
    }
}
