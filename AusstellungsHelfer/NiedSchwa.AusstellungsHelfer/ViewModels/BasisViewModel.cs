using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.AusstellungsHelfer.ViewModels
{
    /// <summary>
    /// Dient als Viewmodel Basisklasse für 
    /// eine Anwendung.
    /// </summary>
    public abstract class BasisViewModel : NiedSchwa.Anwendung.Daten.DatenAnwendungsobjekt
    {
        /// <summary>
        /// Ruft Die Infrastruktur der Anwendung ab
        /// oder legt diese Fest.
        /// </summary>
        public ViewModels.FensterManager FensterManager { get; set; }
    }
}
