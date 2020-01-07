using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NiedSchwa.AusstellungsHelfer.Erweiterungen;

namespace NiedSchwa.AusstellungsHelfer.Erweiterungen
{
    /// <summary>
    /// Stellt Einzelne Zeichensätze für die Eingabe Prüfung zur 
    /// verfügung.
    /// </summary>
   public class EingabeFilter
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private string[] _ZeichenFilter = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };


        /// <summary>
        /// Ruft den Zeichenfilter ab.
        /// </summary>
        public string[] ZeichenFilter
        {
            get
            {
                return this._ZeichenFilter;
            }
        }



        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string[] _SonderZeichenFilter = { ".", ",", ";", "-", "_", ":", "<", ">", "+", "*", "/", "@", "!", "§", "$", "&", "(", ")", "=", "^", "°", "´", "`", "#" };

        /// <summary>
        /// Ruft den Sonderzeichen Filter ab
        /// </summary>
        public string[] SonderZeichenFilter
        {
            get
            {
                return this._SonderZeichenFilter;
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string[] _ZahlenFilter = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", ".", ";" };

        /// <summary>
        /// Ruft den ZahlenFilter ab.
        /// </summary>
        public string[] ZahlenFilter
        {
            get
            {
                return this._ZahlenFilter;
            }
        }




    }
}
