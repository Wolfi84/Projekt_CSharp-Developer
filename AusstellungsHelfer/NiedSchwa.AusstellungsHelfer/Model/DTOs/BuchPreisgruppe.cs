using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.AusstellungsHelfer.Model.DTOs
{


    /// <summary>
    /// Stellt eine Liste an Buchpreisgruppendaten
    /// zur verfügung.
    /// </summary>
    public class BuchPreisgruppen : System.Collections.ObjectModel.ObservableCollection<BuchPreisgruppe>
    {

    }

    /// <summary>
    /// Stellt Informationen über die Preisgruppe
    /// eines Buches zur Verfügung.
    /// </summary>
    public class BuchPreisgruppe
    {
        /// <summary>
        /// Ruft die Preisgruppe ab oder 
        /// legt diese fest.
        /// </summary>
        public string Preisgruppe { get; set; }

        /// <summary>
        /// Ruft den Prozentsatz ab oder 
        /// legt diesen fest.
        /// </summary>
        public float Prozentsatz { get; set; }


        /// <summary>
        /// Ruft den Index der Preisgruppe ab
        /// oder legt diese fest.
        /// </summary>
        public int PreisgruppenIndex { get; set; }
    }
}
