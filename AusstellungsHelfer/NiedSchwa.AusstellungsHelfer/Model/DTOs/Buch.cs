using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.AusstellungsHelfer.Model.DTOs
{
    /// <summary>
    /// Stellt eine Liste von Bücher Objekten
    /// zur verfügung
    /// </summary>
    public  class Bücher : System.Collections.ObjectModel.ObservableCollection<Buch>
    {

    }


    /// <summary>
    /// Stellt Informationen eines Buches
    /// zur Verfügung
    /// </summary>
    public class Buch
    {

        /// <summary>
        /// Ruft die Bestellnummer ab oder 
        /// legt diese fest.
        /// </summary>
        public int? Bestellnummer { get; set; }

        /// <summary>
        /// Ruft die Auftragsnummer ab oder 
        /// legt diese fest.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ruft den Autor des Buches ab oder 
        /// legt diese fest.
        /// </summary>
        public string Autor { get; set; }

        /// <summary>
        /// Ruft den Verlag ab oder 
        /// legt diese fest.
        /// </summary>
        public string Verlag { get; set; }

        ///// <summary>
        ///// Ruft die Preisgruppe ab oder 
        ///// legt diese fest.
        ///// </summary>
        //public string Preisgruppe { get; set; }

        ///// <summary>
        ///// Ruft den Prozentsatz ab oder 
        ///// legt diesen fest.
        ///// </summary>
        //public float Prozentsatz { get; set; }



        /// <summary>
        /// Ruft die Daten der Preisgruppe ab
        /// oder legt diese fest.
        /// </summary>
        public BuchPreisgruppe BuchPreisgruppe { get; set; }

        /// <summary>
        /// Ruft den Preis des Buches ab
        /// oder legt diesen fest.
        /// </summary>
        public float Preis { get; set; }

        /// <summary>
        /// Ruft die Anzahl der bestellten Bücher ab
        /// oder legt diese fest.
        /// </summary>
        public int AnzahlBestellt { get; set; }

        /// <summary>
        /// Ruft die Anzahl der bestellten Bücher ab
        /// oder legt diese fest.
        /// </summary>
        public int AnzahlGeliefert { get; set; }

    }
}
