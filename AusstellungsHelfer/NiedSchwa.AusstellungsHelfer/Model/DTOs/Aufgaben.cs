using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.AusstellungsHelfer.Model.DTOs
{
    /// <summary>
    /// Stellt eine Liste von Anwendungspunkten bereit.
    /// </summary>
    public class Aufgaben : System.Collections.ObjectModel.ObservableCollection<Aufgabe>
    {

    }

    /// <summary>
    /// Beschreibt einen Anwendungspunkt
    /// </summary>
    public class Aufgabe
    {
        /// <summary>
        /// Ruft den Namen des Anwendungspunkts 
        /// ab oder legt diesen fest.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ruft das Zeichen ab, das als
        /// Symbol für den Anwendungspunkt 
        /// benutzt werden soll, oder legt dieses fest.
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Ruft die View ab, die für den
        /// Anwendungspunkt benutzt werden soll,
        /// oder legt diese fest.
        /// </summary>
        public System.Windows.Controls.UserControl Arbeitsbereich { get; set; }

        /// <summary>
        /// Ruft den Typ des Steuerelements ab,
        /// das für den Arbeitsbereich benutzt wird,
        /// oder legt diesen fest.
        /// </summary>
        public Type ArbeitsbereichTyp { get; set; }
    }
}
