using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.AusstellungsHelfer.Model.DTOs
{
    /// <summary>
    /// Stellt eine Liste von Kunden Objekten
    /// zur verfügung
    /// </summary>
    public class Kunden : System.Collections.ObjectModel.ObservableCollection<Kunde>
    {

    }


    /// <summary>
    /// Stellt Informationen eines Kunden
    /// zur verfügung.
    /// </summary>
    /// <remarks>Auf das Implementieren der Objekt.GetHashCode() wurde verzichtet
    /// die Wahrung des Studios wird ignoriert.</remarks>
    public class Kunde
    {

        /// <summary>
        /// Überschreibt die Base.ToStrig()
        /// </summary>
        /// <returns>Gibt einen Leerstrig zurück!</returns>
        /// <remarks>Diese Methode wird verwendet um im Suchfeld des UIs ein
        /// leeres Textfeld zu erhalten.</remarks>
        public override string ToString()
        {
            return string.Empty;
        }

        /// <summary>
        /// Ruft den Vornamen eines Kunden ab
        /// oder legt diesen fest.
        /// </summary>
        public string Vorname { get; set; }

        /// <summary>
        /// Ruft den Nachnamen eines Kunden ab
        /// oder legt diesen fest.
        /// </summary>
        public string Nachname { get; set; }

        /// <summary>
        /// Ruft die Kundennummer ab
        /// oder legt diese fest.
        /// </summary>
        public int KundenNummer { get; set; }

        /// <summary>
        /// Ruft den Straßennamen ab
        /// oder legt diesen fest.
        /// </summary>
        public string Straße { get; set; }

        /// <summary>
        /// Ruft die Hausnummer ab
        /// oder legt diesen fest.
        /// </summary>
        public string HausNummer { get; set; }

        /// <summary>
        /// Ruft die Postleitzahl ab
        /// oder legt diesen fest.
        /// </summary>
        public int? PLZ { get; set; }

        /// <summary>
        /// Ruft den Namen des Wohnortes ab
        /// oder legt diesen fest.
        /// </summary>
        public string Stadt { get; set; }

        /// <summary>
        /// Ruft den Telefonnummer des Kunden ab
        /// oder legt diesen fest.
        /// </summary>
        public string TelNummer { get; set; }


        /// <summary>
        /// Ruft die E-Mail Adresse ab
        /// oder legt diesen fest.
        /// </summary>
        public string E_Mail { get; set; }


        /// <summary>
        /// Vergleicht Übergebene Objekt auf Wertgleichheit.
        /// </summary>
        /// <param name="obj">Das zu Prüfende Objekt</param>
        /// <returns>True wenn das geprüfte Objekt die gleichen werte hat.</returns>
        public override bool Equals(object obj)
        {
            var Ergebnis = false;

            var Vergleich = obj as DTOs.Kunde;

            if (Vergleich != null)
            {

                Ergebnis =  this.KundenNummer == Vergleich.KundenNummer &&
                            String.Compare(this.Nachname, Vergleich.Nachname) == 0 &&
                            String.Compare(this.Vorname, Vergleich.Vorname) == 0 &&
                            String.Compare(this.Straße, Vergleich.Straße) == 0 &&
                            this.PLZ == Vergleich.PLZ &&
                            String.Compare(this.Stadt, Vergleich.Stadt) == 0 &&
                            String.Compare(this.TelNummer, Vergleich.TelNummer) == 0 &&
                            String.Compare(this.E_Mail, Vergleich.E_Mail) == 0;

            }
            return Ergebnis;
        }
    }
}
