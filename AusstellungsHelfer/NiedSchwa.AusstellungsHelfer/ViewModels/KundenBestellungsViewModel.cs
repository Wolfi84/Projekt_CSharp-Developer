using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.AusstellungsHelfer.ViewModels
{
    /// <summary>
    /// Stellt den Dienst zum Verwalten des KundenBestellungs Views zur verfügung
    /// </summary>
    /// <remarks> Dieses ViewModel wurde durch 2 ViewModels
    ///  <seealso cref="ViewModels.KundenViewModel"/> und <seealso cref="ViewModels.BestellungsViewModel"/>
    ///  geteilt.</remarks>
    public class KundenBestellungsViewModel : ViewModels.BasisViewModel//NiedSchwa.Anwendung.Daten.DatenAnwendungsobjekt
    {
        //Änderung 20190629 Niedermayr
        // Aufteilung in 2 Seperate Viewmodels Kunden- und BestellungsViewModel

        ///// <summary>
        ///// Ruft Die Infrastruktur der Anwendung ab
        ///// oder legt diese Fest.
        ///// </summary>
        //public ViewModels.FensterManager FensterManager { get; set; }


        ///// <summary>
        ///// Internes Feld für die Eigenschft
        ///// </summary>
        //private Model.DTOs.Kunde _AktuellerKunde = null;


        ///// <summary>
        ///// Ruft den derzeit aktiven Kunden ab
        ///// oder legt diesen fest.
        ///// </summary>
        //public Model.DTOs.Kunde AktuellerKunde
        //{
        //    get
        //    {
        //        if (this._AktuellerKunde == null)
        //        {
        //            this.AktiviereBeschäftigt();

        //            this._AktuellerKunde = new Model.DTOs.Kunde();


        //            this.AppKontext.Protokoll.Eintragen($"Der Kunde {this._AktuellerKunde.Nachname} aus der Stadt {this._AktuellerKunde.Stadt} wurde als Aktueller Kunde festgeleg...");


        //            this.DeaktiviereBeschäftigt();
        //        }


        //        return this._AktuellerKunde;
        //    }

        //    set
        //    {



        //        if (value != null )
        //        {

        //            this._AktuellerKunde.KundenNummer = 0;
        //            //Neuer Verweis
        //            this._AktuellerKunde = value;

        //            this._AktuelleBestellung = null;
        //            this._Bestellungen = null;
                    

                    

        //            //View Benachrichtigen.
        //            this.OnPropertyChanged();
        //            this.OnPropertyChanged("AktuellerKunden");
        //            this.OnPropertyChanged("AktuellerKundenNachname");
        //            this.OnPropertyChanged("Bestellungen");
        //        }


        //    }
        //}

        //private string _KundenSuchName = string.Empty;

        ///// <summary>
        ///// Legt den Aktuellen kunden Suchnamen fest.
        ///// </summary>
        //public string KundenSuchName
        //{
        //    get
        //    {
        //        return this._KundenSuchName;
        //    }

        //    set
        //    {
        //        if (value != null && value != string.Empty)
        //        {
        //            this._AktuelleKunden = null;

        //            _KundenSuchName = value;                

        //            this._AktuelleKunden = this._AktualisiereKunden(value, string.Empty);// this.AktuellerKundenNachname);

        //            this.OnPropertyChanged("AktuelleKunden");

        //            this.OnPropertyChanged();


        //        }
        //    }
        //}


     


        ///// <summary>
        ///// Erzeugt eine Kundenliste
        ///// </summary>
        ///// <param name="nachname">Nachname des Kunden.</param>
        ///// <param name="vorname">Vorname des Kunden.</param>
        ///// <returns>Kundenliste</returns>
        //private Model.DTOs.Kunden _AktualisiereKunden(string nachname, string vorname)
        //{

        //    this._AktuelleKunden = null;
        //    var BestellungsManager = this.AppKontext.Erzeuge<Model.Manager.BestellungsManager>();

        //    var Kunden = BestellungsManager.SucheKunden(nachname, vorname);

        //    return  Kunden;
        //}





        ///// <summary>
        ///// Internes Feld für die Eigenschaft.
        ///// </summary>
        //private Model.DTOs.Kunden _AktuelleKunden = null;



        ///// <summary>
        ///// Ruft die Aktuell verfügbare Kundenliste ab
        ///// </summary>
        //public Model.DTOs.Kunden AktuelleKunden
        //{
        //    get
        //    {
        //        if(this._AktuelleKunden == null)
        //        {

        //            this._AktuelleKunden = new Model.DTOs.Kunden();
                   
        //        }


        //        return this._AktuelleKunden;
        //    }

        //}

        ///// <summary>
        ///// Internes Feld für die Eigenschaft.
        ///// </summary>
        //private Model.DTOs.Bestellungen _Bestellungen = null;

        ///// <summary>
        ///// Ruft die für den Aktiven Kunden vorhandenen Bestellungen ab.
        ///// </summary>
        //public Model.DTOs.Bestellungen Bestellungen
        //{
        //    get
        //    {
        //        if (this._Bestellungen == null && this.AktuellerKunde.KundenNummer > 0)
        //        {
        //            this.AktiviereBeschäftigt();

        //            //Den BestellManager Temporär Erzeugen
        //            var BestellManager = this.AppKontext.Erzeuge<Model.Manager.BestellungsManager>();
        //            this._Bestellungen = BestellManager.HoleBestellungen(this.AktuellerKunde.KundenNummer);


        //            this.AppKontext.Protokoll.Eintragen("Die Bestellliste wurde aktualisiert...");
        //            this.DeaktiviereBeschäftigt();

        //        }

        //        return this._Bestellungen;
        //    }

        //    set
        //    {
        //        if(this._Bestellungen == null)
        //        {
        //            this._Bestellungen = new Model.DTOs.Bestellungen();
        //        }
        //        this._Bestellungen = value;
        //        this.OnPropertyChanged();
        //    }
        //}

        ///// <summary>
        ///// Internes Feld für die Eigenschaft
        ///// </summary>
        //private Model.DTOs.Bücher _Bücher = null;


        ///// <summary>
        ///// ruft die Aktuelle Bücher ab
        ///// </summary>
        //public Model.DTOs.Bücher Bücher
        //{
        //    get
        //    {
        //        if (this._Bücher == null)
        //        {
                    
        //            this.AktiviereBeschäftigt();

        //            //BestellungsManager Temporär anlegen um Ressourcen zu schonen
        //            var BestellManager = this.AppKontext.Erzeuge<Model.Manager.BestellungsManager>();
        //            this._Bücher = BestellManager.HoleBuchListe(this.AktuelleBestellung);

        //            //Das Anwendungsfenster Aktualisieren
        //            this.OnPropertyChanged();

        //            this.AppKontext.Protokoll.Eintragen("Die Buchbestellungen wurden aktualisiert...");
        //            this.DeaktiviereBeschäftigt();
        //        }

        //        return this._Bücher;
        //    }
        //}


        ///// <summary>
        ///// Internes Feld für die Eigenschaft
        ///// </summary>
        //private Model.DTOs.Buch _AktuellesBuch = null;

        ///// <summary>
        ///// Ruft das Aktuell Ausgewählte Buch der Aktuellen Buchliste ab
        ///// oder legt dieses Fest.
        ///// </summary>
        //public Model.DTOs.Buch AktuellesBuch
        //{
        //    get
        //    {
        //        if(this._AktuellesBuch == null)
        //        {
        //            this._AktuellesBuch = new Model.DTOs.Buch();
        //            this.OnPropertyChanged();
        //        }
        //        return this._AktuellesBuch;
        //    }
        //    set
        //    {
                

        //        this._AktuellesBuch = value;

        //        this._GesuchtesBuch = null;
        //        this._AnzahlBücherSoll = null;
        //        this.Buchnummer =  null;

        //        this.OnPropertyChanged("GesuchtesBuch");
        //        this.OnPropertyChanged("Buchnummer");
        //        this.OnPropertyChanged("AnzahlBücherSoll");
        //        this.OnPropertyChanged();
        //    }
        //}


        ///// <summary>
        ///// Internes Feld für die Eigenschaft
        ///// </summary>
        //private Model.DTOs.Buch _GesuchtesBuch = null;

        ///// <summary>
        ///// Ruft das Aktuell Gesuchte Buch der Aktuellen Buchliste ab
        ///// oder legt dieses Fest.
        ///// </summary>
        //public Model.DTOs.Buch GesuchtesBuch
        //{
        //    get
        //    {

        //        return this._GesuchtesBuch;
        //    }
        //    set
        //    {

        //        if (value != null)
        //        {
                  
        //            this._GesuchtesBuch = value;

        //            this.OnPropertyChanged();

        //        }
        //    }
        //}


        ///// <summary>
        ///// Internes feld für die Eigenschft
        ///// </summary>
        //private int? _Buchnummer = null;

        ///// <summary>
        ///// Legt die zu suchende Buchnummer fest
        ///// oder gibt dieses zurück.
        ///// </summary>
        //public int? Buchnummer
        //{
        //    get
        //    {
        //        return this._Buchnummer;
        //    }

        //    set
        //    {
        //        if (value != null)
        //        {

        //            this._Buchnummer = value;
        //            var Manager = this.AppKontext.Erzeuge<Model.Manager.BestellungsManager>();

        //            this.GesuchtesBuch = Manager.SucheBuch(value.Value);
        //            this.OnPropertyChanged("GesuchtesBuch");
        //        }
        //    }
        //}


        ///// <summary>
        ///// Internes Feld für die Eigenschaft
        ///// </summary>
        //private int? _AnzahlBücherSoll;

        ///// <summary>
        ///// Ruft die Aktuell zu bestellende Anzahl an büchern ab
        ///// oder legt diese fest.
        ///// </summary>
        //public int? AnzahlBücherSoll
        //{
        //    get
        //    {
        //        return this._AnzahlBücherSoll;
        //    }

        //    set
        //    {
        //        this._AnzahlBücherSoll = 1;
        //        this.OnPropertyChanged();

        //        this._AnzahlBücherSoll = value;
        //    }
        //}

        ///// <summary>
        ///// Internes Feld für die Eigenschaft.
        ///// </summary>
        //private NiedSchwa.Windows.Befehl _KundeAnlegen = null;

        ///// <summary>
        ///// Ruft den Befehl ab, mit dem 
        ///// ein neuer Kunde angelegt werden kann.
        ///// </summary>
        //public NiedSchwa.Windows.Befehl KundeAnlegen
        //{
        //    get
        //    {
        //        if (this._KundeAnlegen == null)
        //        {
        //            this.AktiviereBeschäftigt();

        //            this._KundeAnlegen = new Windows.Befehl(
        //                daten =>
        //                {
        //                    this.AktiviereBeschäftigt();

        //                    var KundenDatenSindOK = false;

        //                    //Kundenfelder Prüfen...
        //                    KundenDatenSindOK = Erweiterungen.KundenDatenPrüfer.KundenNamenPrüfen(this.AktuellerKunde.Vorname) &&
        //                                        Erweiterungen.KundenDatenPrüfer.KundenNamenPrüfen(this.AktuellerKunde.Nachname) &&
        //                                        Erweiterungen.KundenDatenPrüfer.KundenNamenPrüfen(this.AktuellerKunde.Stadt) &&
        //                                        Erweiterungen.KundenDatenPrüfer.KundenNamenPrüfen(this.AktuellerKunde.Straße) &&
        //                                        Erweiterungen.KundenDatenPrüfer.TelefonNummerPrüfen(this.AktuellerKunde.TelNummer) &&
        //                                        Erweiterungen.KundenDatenPrüfer.HausnummerPrüfen(this.AktuellerKunde.HausNummer);


        //                    //Kundendaten speichern...
        //                    if (KundenDatenSindOK && this._KundeSpeichern())
        //                    {
        //                        System.Windows.MessageBox.Show(
        //                        $"Kunde {this.AktuellerKunde.Nachname} {this.AktuellerKunde.Vorname} wurde Gespeichert",
        //                        "Neuekunde Anlegen",
        //                         System.Windows.MessageBoxButton.OK,
        //                        System.Windows.MessageBoxImage.Information);


        //                        this.AppKontext.Protokoll.Eintragen($"Kunde {this.AktuellerKunde.Nachname} {this.AktuellerKunde.Vorname} wurde Gespeichert");


        //                        var Manager = this.AppKontext.Erzeuge<Model.Manager.BestellungsManager>();



        //                        this._AktuellerKunde = Manager.NeuKundeSuchen(this.AktuellerKunde.Nachname, this.AktuellerKunde.Vorname).FirstOrDefault();

        //                        this._AktuelleKunden = null;
        //                        this._Bestellungen = null;
        //                        this._AktuelleBestellung = null;


        //                        this.OnPropertyChanged("AktuelleKunden");
        //                        this.OnPropertyChanged("AktuellerKunde");
        //                        this.OnPropertyChanged("Bestellungen");

        //                    }
        //                    else
        //                    {
        //                        System.Windows.MessageBox.Show(
        //                        $"Kunde konnte nicht angelegt werden. Bitte prüfen Sie die Eingegebnen Daten",
        //                        "Neuekunde Anlegen",
        //                         System.Windows.MessageBoxButton.OK,
        //                        System.Windows.MessageBoxImage.Error);
        //                    }


        //                    this.DeaktiviereBeschäftigt();
        //                },
        //                //CanExecute Methode...
        //                daten => this._KundeIstGesetzt
        //                );

        //            this.AppKontext.Protokoll.Eintragen("Der Befehl Kunde Anlegen wurde gecachet...");

        //            this.DeaktiviereBeschäftigt();
        //        }

        //        return this._KundeAnlegen;
        //    }
        //}


        ///// <summary>
        ///// Prüft ob die Aktuellen Eingaben der Kundenfelder
        ///// Valid sind. 
        ///// </summary>
        //private bool _KundeIstGesetzt
        //{
        //    get
        //    {

        //        return  this.AktuellerKunde != null &&
        //                this.AktuellerKunde.Vorname != string.Empty &&
        //                this.AktuellerKunde.Nachname != string.Empty &&
        //                this.AktuellerKunde.PLZ > 1000 &&
        //                this.AktuellerKunde.PLZ < 9999 &&
        //                this.AktuellerKunde.Stadt != string.Empty &&
        //                this.AktuellerKunde.Straße != string.Empty &&
        //                this.AktuellerKunde.TelNummer != string.Empty &&
        //                this.AktuellerKunde.HausNummer != string.Empty ;

        //    }
        //}

        ///// <summary>
        ///// Speichert die Eingaben der Kundenfelder in der Datenbank ab.
        ///// </summary>
        ///// <returns>True wenn erfolgreich gespeichert wurde.</returns>
        //private bool _KundeSpeichern()
        //{

        //    var Manager = this.AppKontext.Erzeuge<Model.Manager.BestellungsManager>();
        //    var Ergebins = false;


        //    if(this._KundeIstGesetzt)
        //    {
        //        Ergebins = Manager.NeuenKundenSpeichern(this.AktuellerKunde);

        //    }
                        
        //    return Ergebins; 

        //}


        ///// <summary>
        ///// Internes Feld für die Eigenschaft.
        ///// </summary>
        //private NiedSchwa.Windows.Befehl _NeueBestellungAnlegen = null;

        ///// <summary>
        ///// Ruft den Befehl ab, mit dem 
        ///// eine neue Bestellung angelegt werden kann.
        ///// </summary>
        //public NiedSchwa.Windows.Befehl NeueBestellungAnlegen
        //{
        //    get
        //    {
        //        if (this._NeueBestellungAnlegen == null)
        //        {
        //            this.AktiviereBeschäftigt();

        //            this._NeueBestellungAnlegen = new Windows.Befehl(
        //                //Execute Methode...
        //                daten =>
        //                {

        //                    this.AktiviereBeschäftigt();


        //                    var Manager = this.AppKontext.Erzeuge<Model.Manager.BestellungsManager>();

        //                    var Ergebnis = Manager.SucheKunden(this.AktuellerKunde.KundenNummer);


        //                    if (this._KundeIstGesetzt &&  this.AktuellerKunde.KundenNummer > 0 && 
        //                            Ergebnis.Count > 0 && this.AktuellerKunde.Equals(Ergebnis[0]))
        //                    {
        //                        this._AktuelleBestellung = null;

        //                        this._Bücher = null;

        //                        //Neue Bestellung in den Cache schreiben...
        //                        this._Bestellungen.Add(
        //                            new Model.DTOs.Bestellung
        //                            {
        //                                BestellDatum = DateTime.Now,
        //                                BestellStatus = Model.DTOs.BestellStatus.Neu


        //                            });

        //                        this._AktuelleBestellung = this.Bestellungen.LastOrDefault();

        //                        //this._Bestellungen = null;
        //                        this._Bücher = null;
        //                        this.OnPropertyChanged("Bestellungen");
        //                        this.OnPropertyChanged("AktuelleBestellung");
        //                        this.OnPropertyChanged("Bücher");


        //                        this.AktiviereBeschäftigt();
        //                    }
        //                    else
        //                    {
        //                        System.Windows.MessageBox.Show(
        //                          $"Kunde muss erst angelegt weren.",
        //                          "Neue Bestellung anlegen.",
        //                          System.Windows.MessageBoxButton.OK,
        //                          System.Windows.MessageBoxImage.Stop);
        //                    }
        //                },
        //                   //CanExecute Methode...
        //                   daten => this._KundeIstGesetzt
        //                );
                
        //            this.AppKontext.Protokoll.Eintragen("Der Befehl wurde gecachet...");
                
        //            this.DeaktiviereBeschäftigt();
        //        }

        //        return this._NeueBestellungAnlegen;
        //    }
        //}


   

        ///// <summary>
        ///// Internes Feld für die Eigenschaft.
        ///// </summary>
        //private NiedSchwa.AusstellungsHelfer.Model.DTOs.Bestellung _AktuelleBestellung = null;



        ///// <summary>
        ///// Ruft die Aktuelle Bestellung für den Aktuellen kunden ab
        ///// oder legt diese fest.
        ///// </summary>
        //public NiedSchwa.AusstellungsHelfer.Model.DTOs.Bestellung AktuelleBestellung
        //{
        //    get
        //    {
        //        if(this._AktuelleBestellung == null)
        //        {
        //            this._AktuelleBestellung = new Model.DTOs.Bestellung();
        //        }

        //        return this._AktuelleBestellung;
        //    }
        //    set
        //    {
        //        if (this.AktuelleBestellung != value)
        //        {

        //            this._AktuelleBestellung = value;

        //            this._Bücher = null;
        //            this._AktuellesBuch = null;
        //            this._GesuchtesBuch = null;
        //            this.OnPropertyChanged();
        //            this.OnPropertyChanged("Bücher");
        //            this.OnPropertyChanged("GesuchtesBuch");
        //        }
        //    }

        //}



        ///// <summary>
        ///// Internes Feld für die Eigenschaft.
        ///// </summary>
        //private NiedSchwa.Windows.Befehl _BuchHinzufügen = null;

        ///// <summary>
        ///// Ruft den Befehl ab, mit dem 
        ///// ein Buch der Neuen Bestellung hizugefügt wird.
        ///// </summary>
        //public NiedSchwa.Windows.Befehl BuchHinzufügen
        //{
        //    get
        //    {
        //        if (this._BuchHinzufügen == null)
        //        {
        //            this.AktiviereBeschäftigt();

        //            this._BuchHinzufügen = new Windows.Befehl(
        //                //Execute Methode...
        //                daten =>
        //                {
        //                    if (this._KundeIstGesetzt && this.GesuchtesBuch != null && 
        //                        GesuchtesBuch.Name != string.Empty && 
        //                            this.AnzahlBücherSoll != null && 
        //                                this.AnzahlBücherSoll >0 && this.Buchnummer != null && this.Buchnummer > 0)
        //                    {



        //                        var Ergebnis = (from bu in this.Bücher
        //                                        where bu.Bestellnummer == GesuchtesBuch.Bestellnummer
        //                                        select bu).FirstOrDefault();

        //                        //Prüfen ob das Buch bereits in der Bestellung ist..
        //                        if (this.Bücher.Contains(Ergebnis))
        //                        {
        //                            //Buch aus Liste löschen...
        //                            this.Bücher.RemoveAt(this.Bücher.IndexOf(Ergebnis));

        //                            //Parameter des Kopierten Objekts ändern...
        //                            Ergebnis.AnzahlBestellt += this.AnzahlBücherSoll.Value;

        //                            if (Ergebnis.AnzahlBestellt > 0)
        //                            {
        //                                //Objekt der Liste Hinzufügen...
        //                                this.Bücher.Add(Ergebnis);
        //                            }
                                  
        //                        }
        //                        else
        //                        {
        //                            //Bestellte Buchanzahl übergeben...
        //                            this.GesuchtesBuch.AnzahlBestellt = this.AnzahlBücherSoll.Value;

        //                            //Neue Bestellung in den Cache schreiben...
        //                            this.Bücher.Add(GesuchtesBuch); 
        //                        }


        //                        //Update und Clear...
        //                        this.OnPropertyChanged("Bücher");

        //                        this.GesuchtesBuch = null;

        //                        this._Buchnummer = null;

        //                        this.AnzahlBücherSoll = null;

        //                        this.OnPropertyChanged("AnzahlBücherSoll");
        //                        this.OnPropertyChanged("Buchnummer");
        //                    }


        //                    this.DeaktiviereBeschäftigt();
        //                },
        //                   //CanExecute Methode...                          
        //                   daten => this._KundeIstGesetzt && this.AktuelleBestellung.Auftragsnummer == 0
        //                );

        //            this.AppKontext.Protokoll.Eintragen("Der Befehl wurde gecachet...");

        //            this.DeaktiviereBeschäftigt();
        //        }

        //        return this._BuchHinzufügen;
        //    }
        //}


        ///// <summary>
        ///// Internes Feld für die Eigenschaft.
        ///// </summary>
        //private NiedSchwa.Windows.Befehl _BuchPositionLöschen = null;

        ///// <summary>
        ///// Ruft den Befehl ab, mit dem 
        ///// die noch unbestellte Buchposition gelöscht werden kann.
        ///// </summary>
        //public NiedSchwa.Windows.Befehl BuchPositionLöschen
        //{
        //    get
        //    {
        //        if (this._BuchPositionLöschen == null)
        //        {
        //            this.AktiviereBeschäftigt();

        //            this._BuchPositionLöschen = new Windows.Befehl(
        //                //Execute Methode...
        //                daten =>
        //                {
        //                    if (this.AktuellesBuch.AnzahlGeliefert == 0)
        //                    {
                               
        //                        Bücher.Remove(this.AktuellesBuch);
        //                        this.OnPropertyChanged("Bücher");
        //                    }


        //                    this.DeaktiviereBeschäftigt();
        //                },
        //                   //CanExecute Methode...
        //                   daten => this.AktuellesBuch.AnzahlGeliefert == 0 && 
        //                        this.AktuellesBuch.Bestellnummer > 0 &&
        //                            this.AktuelleBestellung.Auftragsnummer == 0
        //                );

        //            this.AppKontext.Protokoll.Eintragen("Der Befehl wurde gecachet...");

        //            this.DeaktiviereBeschäftigt();
        //        }

        //        return this._BuchPositionLöschen;
        //    }
        //}

        ///// <summary>
        ///// Internes Feld für die Eigenschaft.
        ///// </summary>
        //private NiedSchwa.Windows.Befehl _PositionÄndern = null;

        ///// <summary>
        ///// Ruft den Befehl ab, mit dem 
        ///// die noch unbestellte Buchposition gelöscht werden kann.
        ///// </summary>
        //public NiedSchwa.Windows.Befehl PositionÄndern
        //{
        //    get
        //    {
        //        if (this._PositionÄndern == null)
        //        {


        //            this._PositionÄndern = new Windows.Befehl(
        //                //Execute Methode...
        //                daten =>
        //                {

        //                    if (this.AnzahlBücherSoll != null && (this.AktuellesBuch.AnzahlGeliefert + this.AnzahlBücherSoll.Value) <= this.AktuellesBuch.AnzahlBestellt &&
        //                             (this.AktuellesBuch.AnzahlGeliefert + this.AnzahlBücherSoll.Value) >= 0)
        //                    {
        //                        this.AktuellesBuch.AnzahlGeliefert += this.AnzahlBücherSoll.Value;
        //                    }
        //                    else
        //                    {

        //                        System.Windows.MessageBox.Show(
        //                            $"Es wurden {this.AktuellesBuch.AnzahlBestellt} Bücher bestellt. \r Bitte Prüfen Sie die Eingegebene Anzahl an Büchern.",
        //                            "Bestellposition Ändern",
        //                            System.Windows.MessageBoxButton.OK,
        //                            System.Windows.MessageBoxImage.Information);
        //                    }
        //                    var Manager = this.AppKontext.Erzeuge<Model.Manager.BestellungsManager>();


        //                    var Ergebnis = Manager.AktualisiereBestellposition(this.AktuellesBuch, this.AktuelleBestellung);
        //                    // SQL Update

        //                    //this.AktuellesBuch.Bestellnummer;

        //                    this._Bücher = null;
        //                    this._AktuellesBuch = null;
        //                    this._AnzahlBücherSoll = null;

        //                    this.OnPropertyChanged("Bücher");
        //                    this.OnPropertyChanged("AktuellesBuch");
        //                    this.OnPropertyChanged("AnzahlBücherSoll");
        //                    // ist anzahl = null

        //                    // Bücher = null
        //                },
        //                //CanExecute Methode...
        //                daten => this.AktuellesBuch.AnzahlBestellt > 0 && 
        //                    this.AktuelleBestellung.BestellStatus == Model.DTOs.BestellStatus.Bestellt &&
        //                        this.Bücher.Count > 0 && this.AktuellesBuch != null
        //                );


        //        }

        //        return this._PositionÄndern;
        //    }
        //}




        ///// <summary>
        ///// Internes Feld für die Eigenschaft.
        ///// </summary>
        //private NiedSchwa.Windows.Befehl _BestellungSpeichern = null;

        ///// <summary>
        ///// Ruft den Befehl ab, mit dem 
        ///// eine Bestellung in der Datenbank gespeichert wird.
        ///// </summary>
        //public NiedSchwa.Windows.Befehl BestellungSpeichern
        //{
        //    get
        //    {
        //        if (this._BestellungSpeichern == null)
        //        {
        //            this.AktiviereBeschäftigt();

        //            this._BestellungSpeichern = new Windows.Befehl(
        //                //Execute Methode...
        //                daten =>
        //                {


        //                    if (this._KundeIstGesetzt &&
        //                        this.AktuelleBestellung.Auftragsnummer == 0 &&
        //                            this.Bücher.Count > 0)
        //                    {
        //                        //Bestellung Speichern...



        //                //Manager und Ergebnis auskommentieren und var kommentieren um das Speichern in die Datenbank zu verhindern.!


        //                        var Manager = this.AppKontext.Erzeuge<Model.Manager.BestellungsManager>();
        //                          var Ergebnis = Manager.NeueBestellungSpeichern(this.Bücher, this.AktuellerKunde, this.AktuelleBestellung);

        //                       // var Ergebnis = true;


               


        //                        //Wenn das Speichern Erfolgreich war...
        //                        if (Ergebnis)
        //                        {

        //                            //Message an Benutzer und abfrage ob Rechnung gedruckt werden soll...
        //                            var MessageErgebnis = System.Windows.MessageBox.Show(
        //                                                  $"Bestellung wurde gespeichert! \rSoll eine Rechnung Gedruckt werden?",
        //                                                  "Bestellung speichern",
        //                                                  System.Windows.MessageBoxButton.YesNo,
        //                                                  System.Windows.MessageBoxImage.Question);


        //                            //Ergebnis der Abfrage auswerteen...
        //                            switch (MessageErgebnis)
        //                            {
        //                                //Rechnung wird nicht gedruckt...
        //                                case System.Windows.MessageBoxResult.No:

        //                                    break;


        //                                //Rechnung Drucken...
        //                                case System.Windows.MessageBoxResult.Yes:

        //                                    //Print Dialog erzeugen..
        //                                    //könnte durch eigenen dialog ersetzt werden
        //                                    System.Windows.Controls.PrintDialog Dialog = new System.Windows.Controls.PrintDialog();

        //                                   Nullable<bool> DialogErgebnis = Dialog.ShowDialog();
        //                                    if (DialogErgebnis == true)

        //                                    {
        //                                        //Page einstellen
        //                                        Dialog.PageRangeSelection = System.Windows.Controls.PageRangeSelection.AllPages;
        //                                        Dialog.UserPageRangeEnabled = true;
                                                
        //                                        Dialog.PrintTicket.PageOrientation = System.Printing.PageOrientation.Landscape;

        //                                        Dialog.PrintTicket.PageScalingFactor = 100;

        //                                        Dialog.PageRangeSelection = System.Windows.Controls.PageRangeSelection.AllPages;


        //                                        //Manager Erzeugen...
        //                                        var DruckManager = this.AppKontext.Erzeuge<Model.Manager.DruckManager>();


        //                                        var Druckgröße = new System.Windows.Size { Height = Dialog.PrintableAreaHeight, Width = Dialog.PrintableAreaWidth };

                                                
        //                                    //Test für mehrseitige dokumente
        //                                    //
        //                                        //for (int i = 0; i<300; i++)
        //                                        //{
        //                                        //    this.Bücher.Add(this.Bücher[0]);
        //                                        //}

        //                                    //   Dialog.PrintDocument(DruckManager.GesamtBestellungErzeugen(this.Bücher, new Model.DTOs.GesamtBestellung { DateFrom = System.DateTime.Now,DateTo = System.DateTime.Now }, Druckgröße).DocumentPaginator, "Rechnung Nr.: , Nachname des Kunden.");

        //                                        //Dokument erzeugen und dem Dialog übergeben...                                                                                             |_TODO Variablen einufügen für Druckauftragsname...
        //                                            Dialog.PrintDocument(DruckManager.RechnungsDokumentErzeugen(this.Bücher, this.AktuelleBestellung, this.AktuellerKunde, Druckgröße).DocumentPaginator, "Rechnung Nr.: , Nachname des Kunden.");


        //                                        this.AppKontext.Protokoll.Eintragen($"Die Rechnung {this.AktuelleBestellung.Auftragsnummer} wurde für den Gedruckt...");
        //                                        //Cleanup...
        //                                    }
        //                                    break;
                                            
        //                            }

        //                        }


        //                        //Cache leeren...
        //                        this._Bestellungen = null;
        //                        this._AktuelleBestellung = null;
        //                        this._AktuellesBuch = null;
        //                        this._Bücher = null;

        //                        this.AktuelleBestellung = this.Bestellungen.FirstOrDefault();

        //                        //View benachrichtigen...
        //                        this.OnPropertyChanged("Bestellungen");
        //                        this.OnPropertyChanged("AktuelleBestellung");
        //                        this.OnPropertyChanged("AktuellesBuch");
        //                        this.OnPropertyChanged("Bücher");


        //                    }
        //                    else
        //                    {
        //                        System.Windows.MessageBox.Show(
        //                        "Bestellung konnte nicht gespeichert werden.",
        //                        "Bestellung speichern",
        //                        System.Windows.MessageBoxButton.OK,
        //                        System.Windows.MessageBoxImage.Error);
        //                    }

        //                }
        //                ,
        //                   //CanExecute Methode...
        //                   daten => this._KundeIstGesetzt &&
        //                        this.AktuelleBestellung.Auftragsnummer == 0 &&
        //                            this.Bücher.Count > 0
        //                );
        //    }
        //        this.AppKontext.Protokoll.Eintragen("Der Befehl wurde gecachet...");

        //        this.DeaktiviereBeschäftigt();


        //        return this._BestellungSpeichern;
        //    }
        //}

//        /// <summary>
//        /// Internes Feld für die Eigenschaft.
//        /// </summary>
//        private NiedSchwa.Windows.Befehl _KundenfelderLeeren = null;

//        /// <summary>
//        /// Ruft den Befehl ab, mit dem 
//        /// ein neuer Kunde angelegt werden kann.
//        /// </summary>
//        public NiedSchwa.Windows.Befehl KundenfelderLeeren
//        {
//            get
//            {
//                if (this._KundenfelderLeeren == null)
//                {
//                    this.AktiviereBeschäftigt();

//                    this._KundenfelderLeeren = new Windows.Befehl(
//                        daten =>
//                        {
//                            this._KundenfelderReset();
//                        }
//                        ,
//                        //CanExecute Methode...
//                        daten => this._KundeIstGesetzt
//                        );

//                    this.AppKontext.Protokoll.Eintragen("Der Befehl Kunde Anlegen wurde gecachet...");

//                    this.DeaktiviereBeschäftigt();
//                }

//                return this._KundenfelderLeeren;
//;
//            }
//        }

//        /// <summary>
//        /// Setzt alle KundenDaten auf null.
//        /// </summary>
//        private void _KundenfelderReset()
//        {
//            this._KundenSuchName = null;
//            this._AktuellerKunde = null;
//            this._Bestellungen = null;
//            this._AktuelleKunden = null;
//            this.OnPropertyChanged("AktuellerKunde");
//            this.OnPropertyChanged("AktuelleKunden");
//            this.OnPropertyChanged("Bestellungen");
//            this.OnPropertyChanged("KundenSuchName");
//        }


    }
}
