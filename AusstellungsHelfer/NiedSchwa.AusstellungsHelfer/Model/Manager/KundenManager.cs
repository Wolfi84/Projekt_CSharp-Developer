using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.AusstellungsHelfer.Model.Manager
{
    /// <summary>
    /// Stellt den Dienst zum Verwalten der Kundendaten
    /// zur Verfügung.
    /// </summary>
    public class KundenManager : Model.Manager.BasisDatenManger
    {

        /// <summary>
        /// Speichert einen neuen Kunden in die Datenbank.
        /// </summary>
        /// <param name="kunde">Kundendaten</param>
        /// <returns>True wenn das Speichern erfolgreich war.</returns>
        public bool NeuenKundenSpeichern(DTOs.Kunde kunde)
        {
            bool Ergebnis = false;

            this.AktiviereBeschäftigt();

            try
            {

                Ergebnis = this.Controller.NeuenKundenAnlegen(kunde);

                this.AppKontext.Protokoll.Eintragen(String.Concat("Der Kunde ", kunde.Vorname, ", ",
                                                                 $"{kunde.Nachname}", " wurde gespeichert."));
            }
            catch (Exception Ex)
            {
                this.OnFehlerAufgetreten(new Anwendung.FehlerAufgetretenEventArgs(Ex));
            }

            this.DeaktiviereBeschäftigt();

            return Ergebnis;

        }


        /// <summary>
        /// Erstellt eine Liste gespeicherten Kunden
        /// </summary>
        /// <param name="nachname">Nachname des Kunden.</param>
        /// <param name="vorname">Vorname des Kunden.</param>
        /// <returns>Gefundene Kunden</returns>
        public DTOs.Kunden SucheKunden(string nachname, string vorname)
        {
            this.AktiviereBeschäftigt();

            var Ergebnis = new DTOs.Kunden();

            try
            {
                Ergebnis = this.Controller.HohleKunden(nachname, vorname);

                this.AppKontext.Protokoll.Eintragen(String.Concat($"Es wurden {Ergebnis.Count} Kunden gefunden."));

            }
            catch (Exception Ex)
            {
                this.OnFehlerAufgetreten(new Anwendung.FehlerAufgetretenEventArgs(Ex));
            }

            this.DeaktiviereBeschäftigt();

            return Ergebnis;
        }


        /// <summary>
        /// Erstellt eine Liste gespeicherten Kunden
        /// </summary>
        /// <param name="kundennummer">Kundennummer des gesuchten Kunden.</param>
        /// <returns>Gefundene Kunden</returns>
        public DTOs.Kunden SucheKunden(int kundennummer)
        {
            this.AktiviereBeschäftigt();

            var Ergebnis = new DTOs.Kunden();

            try
            {
                Ergebnis = this.Controller.HohleKunden(kundennummer);



            }
            catch (Exception Ex)
            {
                this.OnFehlerAufgetreten(new Anwendung.FehlerAufgetretenEventArgs(Ex));
            }

            this.DeaktiviereBeschäftigt();

            return Ergebnis;
        }

        /// <summary>
        /// Erstellt eine Liste gespeicherten Kunden
        /// </summary>
        /// <param name="nachname">Nachname des Kunden.</param>
        /// <param name="vorname">Vorname des Kunden.</param>
        /// <returns>Gefundene Kunden</returns>
        /// <remarks>Es wird der Kunde mit der zu letzt erstellten Kundennummer welche zu den 
        /// übergebenen Parametern passt ausgewählt.</remarks>
        public DTOs.Kunden NeuKundeSuchen(string nachname, string vorname)
        {
            this.AktiviereBeschäftigt();

            var Ergebnis = new DTOs.Kunden();

            try
            {

                Ergebnis = this.Controller.NeuKundeSuchen(nachname, vorname);

                this.AppKontext.Protokoll.Eintragen(
                                         String.Concat($"Es wurde einen Kundenliste mit {Ergebnis.Count} Einträgen",
                                            $"für den Suchnamen {nachname} erstellt."));
            }
            catch (Exception Ex)
            {
                this.OnFehlerAufgetreten(new Anwendung.FehlerAufgetretenEventArgs(Ex));
            }

            this.DeaktiviereBeschäftigt();
            return Ergebnis;
        }



    }
}
