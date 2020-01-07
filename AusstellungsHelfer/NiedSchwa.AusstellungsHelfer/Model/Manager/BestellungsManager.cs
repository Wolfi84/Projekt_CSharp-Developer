using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.AusstellungsHelfer.Model.Manager
{
    /// <summary>
    /// Stellt einen Dienst zum verwalten der Kundenbestellungen
    /// zur Verfügung
    /// </summary>
   public class BestellungsManager : Model.Manager.BasisDatenManger
    {
        
        /// <summary>
        /// Ruft die Bestellung eines Kunden ab
        /// </summary>
        /// <param name="bestellung">Bestellungsdaten zum suchen der Bücher</param>
        /// <returns>Buchliste.</returns>
        public DTOs.Bücher HoleBuchListe(DTOs.Bestellung bestellung)
        {           
            this.AktiviereBeschäftigt();

            var BuchListe = new DTOs.Bücher();

            try
            {

                //hole die Bücher für die übergebene Bestellnummer...
                BuchListe = this.Controller.HohleBücher(bestellung.Auftragsnummer);

                this.AppKontext.Protokoll.Eintragen("Die Buchliste wurde geholt...");

            }
            catch (Exception ex)
            {

                this.OnFehlerAufgetreten(new Anwendung.FehlerAufgetretenEventArgs(ex));
            }

            this.DeaktiviereBeschäftigt();

            return BuchListe;

        }


      

  
        /// <summary>
        /// Ruft die Aktuelle Bestellliste ab
        /// </summary>
        /// <param name="kundennummer">Kundennummer für die gesucht werden soll.</param>
        /// <returns>Bestellungen des Kunden.</returns>
        public DTOs.Bestellungen HoleBestellungen(int kundennummer)
        {
                AktiviereBeschäftigt();

            var Bestellungen = new DTOs.Bestellungen();

            try
            {
                 Bestellungen = this.Controller.HohleBestellungen(kundennummer);

                this.AppKontext.Protokoll.Eintragen("Die Bestellliste wurde gelesen...");

            }
            catch (Exception ex)
            {
                this.OnFehlerAufgetreten(new Anwendung.FehlerAufgetretenEventArgs(ex));
               
                
            }

            DeaktiviereBeschäftigt();

            return Bestellungen;
                           
                
        }


        /// <summary>
        /// Aktualisiert die Anzahl des Gelieferten Buches einer Bestellung
        /// </summary>
        /// <param name="buch">Buchdaten die Aktualisert werden sollen</param>
        /// <param name="bestellung">Bestellungsdaten welche aktualisiert werden sollen.</param>
        /// <returns>True wenn erfolgreich Aktualisiert wurde.</returns>
        public bool AktualisiereBestellposition(DTOs.Buch buch, DTOs.Bestellung bestellung)
        {
            var Ergebnis = false;

            this.AktiviereBeschäftigt();

            try
            {
                
                Ergebnis = this.Controller.AktualisiereBuchposition(buch, bestellung);

                this.AppKontext.Protokoll.Eintragen(
                    $"Die Anzahl der Gelieferten Bücher mit der Buchnummer {buch.Bestellnummer} wurde um {buch.AnzahlGeliefert} Stk. Aktualisiert.");
            }
            catch (Exception Ex)
            {

                this.OnFehlerAufgetreten(new Anwendung.FehlerAufgetretenEventArgs(Ex));
            }

            this.DeaktiviereBeschäftigt();

            return Ergebnis;
        }


        /// <summary>
        /// Speichert eine Neue Bestellung und speichert diese 
        /// in der SQL Datenbank ab.
        /// </summary>
        /// <param name="bücher">Die Bücher der Bestellung</param>
        /// <param name="kunde">Der Kunde der die Bestellung aufgibt.</param>
        /// <param name="bestellung">Die Daten der Bestellung</param>
        /// <returns>1=ok 0=Fehler</returns>
        public bool NeueBestellungSpeichern(DTOs.Bücher bücher, DTOs.Kunde kunde, DTOs.Bestellung bestellung)
        {
            this.AktiviereBeschäftigt();

            //Ergebinsse vorbelegen...
            bool NeueBestellungGespeichert = false;
            bool BücherWurdenGespeichert = false;

            int Bestellnummer = 0;


            //Bestellstatus ändern...
            bestellung.BestellStatus = DTOs.BestellStatus.Bestellt;

            try
            {
                
                //Bestellung speichern...
                NeueBestellungGespeichert = this.Controller.NeueBestellungSpeichern(bestellung, kunde.KundenNummer);


                //Aktuelle Bestellnummer holen....
                Bestellnummer = this.Controller.HohleBestellungen(kunde.KundenNummer).FirstOrDefault().Auftragsnummer;

                //Speichern der einzelnen Buchpositionen...
                BücherWurdenGespeichert = this.Controller.BuchpositionenSpeichern(bücher, kunde.KundenNummer, Bestellnummer);

                this.AppKontext.Protokoll.Eintragen(String.Concat($"Der Kunde mit der KdNr.: {kunde.KundenNummer} ",
                                                                    $"Hat {bücher.Count} Bücher bestellt."));
                                                                    

            }
            catch (Exception Ex)
            {

                this.OnFehlerAufgetreten(new Anwendung.FehlerAufgetretenEventArgs(Ex));
            }

            this.DeaktiviereBeschäftigt();

            return NeueBestellungGespeichert && BücherWurdenGespeichert;
        }



        /// <summary>
        /// Holt ein Buch und gibt dieses zurück
        /// </summary>
        /// <param name="buchnummer">Bestellnummer</param>
        /// <returns>Das gesuchte Buch</returns>
        public DTOs.Buch SucheBuch(int buchnummer)
        {
            this.AktiviereBeschäftigt();
            var Ergebnis = new DTOs.Buch();

            try
            {
                Ergebnis = this.Controller.HoleBuch(buchnummer);

                this.AppKontext.Protokoll.Eintragen($"Das Buch {Ergebnis.Name} wurde für die Buchnummer {buchnummer} gefunden.");
            }
            catch (Exception Ex)
            {
                this.OnFehlerAufgetreten(new Anwendung.FehlerAufgetretenEventArgs(Ex));
            }


            this.DeaktiviereBeschäftigt();
            return Ergebnis;
        }



        /// <summary>
        /// Holt eine Liste mit gespeicherten Büchern
        /// </summary>
        /// <returns>Das gesuchte Buch</returns>
        public DTOs.Bücher SucheBücher()
        {
            this.AktiviereBeschäftigt();
            var Ergebnis = new DTOs.Bücher();

            try
            {
                Ergebnis = this.Controller.HoleBücher();

                
            }
            catch (Exception Ex)
            {
                this.OnFehlerAufgetreten(new Anwendung.FehlerAufgetretenEventArgs(Ex));
            }


            this.DeaktiviereBeschäftigt();
            return Ergebnis;
        }


        /// <summary>
        /// Holt eine Liste mit gespeicherten Büchern
        /// </summary>
        /// <returns>Das gesuchte Buch</returns>
        public DTOs.BuchPreisgruppen SuchePreisgruppen()
        {
            this.AktiviereBeschäftigt();
            var Ergebnis = new DTOs.BuchPreisgruppen();

            try
            {
                Ergebnis = this.Controller.HoleBuchPreisgruppen();


            }
            catch (Exception Ex)
            {
                this.OnFehlerAufgetreten(new Anwendung.FehlerAufgetretenEventArgs(Ex));
            }


            this.DeaktiviereBeschäftigt();
            return Ergebnis;
        }


        /// <summary>
        /// Speichert die Buchdaten 
        /// </summary>
        /// <param name="buch">Zu Speichernde Buchdaten</param>
        /// <returns>True wenn erfolgreich gespeichert wurde.</returns>
        public bool BuchSpeichern(DTOs.Buch buch)
        {
            this.AktiviereBeschäftigt();

            var Ergebnis = false;

            try
            {
                Ergebnis = this.Controller.BuchSpeichern(buch);
            }
            catch (Exception Ex)
            {
                this.OnFehlerAufgetreten(new Anwendung.FehlerAufgetretenEventArgs(Ex));
            }



            return Ergebnis;
        }


        /// <summary>
        /// Ruft alle Bestellungen während eines 
        /// bestimmten Zeitraumes ab
        /// </summary>
        /// <returns></returns>
        public Model.DTOs.Bücher HoleGesamtBuchListe(DateTime DateFrom, DateTime DateTo)
        {
            AktiviereBeschäftigt();

            var GesamtBuchListe = this.Controller.HoleGesamtBuchListe(DateFrom, DateTo);

            this.AppKontext.Protokoll.Eintragen("Die Gesamtbestellungen wurden geholt...");

            DeaktiviereBeschäftigt();
            return GesamtBuchListe;
        }

    }
}
