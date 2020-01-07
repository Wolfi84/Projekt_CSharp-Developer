using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.AusstellungsHelfer.Model.DTOs
{

    /// <summary>
    /// Listet die unterschiedlichen Bestellstadien
    /// auf
    /// </summary>
    [Flags] public enum BestellStatus
    {


        /// <summary>
        /// Kunde hat Bestellung abgeholt
        /// </summary>
        Abgeschlossen,

        /// <summary>
        /// Auftrag wird für Bestellung 
        /// vorbereitet
        /// </summary>
        Neu,

        /// <summary>
        /// Bücher wurden beim Verlag bestellt
        /// </summary>
        Bestellt,

        /// <summary>
        /// Bestellung wurde vom Kunden oder Verlag
        /// Storniert
        /// </summary>
        Storniert


    }

    /// <summary>
    /// Stellt Eine Bestellliste zur Verfügung
    /// </summary>
    public class Bestellungen : System.Collections.ObjectModel.ObservableCollection<Bestellung>
    {

    }


    /// <summary>
    /// Stellt Informationen einer Bestellung zur Verfügung
    /// </summary>  
   public class Bestellung
    {
        /// <summary>
        /// Ruft Die Auftragsnummer ab
        /// oder legt diese fest.
        /// </summary>
        public int Auftragsnummer { get; set; }


        /// <summary>
        /// Ruft den Zeitpunkt ab an dem die 
        /// Bestellung getätigt wurde oder
        /// legt diesen fest.
        /// </summary>
        public System.DateTime BestellDatum { get; set; }


        /// <summary>
        /// Ruft Die Bestellnummmer ab
        /// oder legt diese fest.
        /// </summary>
        public BestellStatus BestellStatus { get; set; }

    //    public DTOs.Bücher Bücher { get; set; }
    }
}
