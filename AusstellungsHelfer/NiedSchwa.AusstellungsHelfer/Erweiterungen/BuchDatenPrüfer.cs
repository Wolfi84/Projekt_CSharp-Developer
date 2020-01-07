using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.AusstellungsHelfer.Erweiterungen
{
    /// <summary>
    /// Stellt diverse Prüfmethden für die Buchdaten 
    /// Validierung zur verfügung.
    /// </summary>
    public class BuchDatenPrüfer : EingabeFilter
    {

        /// <summary>
        /// Prüft den übergebenen String auf Vorhandensein 
        /// von Sonderzeichen und Zahlen
        /// </summary>
        /// <param name="name"></param>
        /// <returns>True wenn eingabe korrekt</returns>
        public bool NamePrüfen(string name)
        {
            var Ergebnis = false;
            if (name != null)
            {

                //strig auf mindestlänge von 3 Zeichen prügen...
                Ergebnis = name.Length > 3 &&
                           
                           //string auf vorhandensein von Sonderzeichen prüfen...
                           !(name.StringPrüfen(this.SonderZeichenFilter)) &&

                           //string auf @ zeichen durchsuchen...
                           !(name.Contains("@")); 
            }


            return Ergebnis;
        }


        /// <summary>
        /// Prüft den übergebenen String auf Vorhandensein 
        /// von Sonderzeichen.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>True wenn Eingabe korrekt</returns>
        public bool AutorPrüfen(string name)
        {
            var Ergebnis = false;

            if (name != null)
            {
                //strig auf mindestlänge von 3 Zeichen prügen...
                Ergebnis = name.Length > 3 &&
                            //string auf vorhandensein von Zahlen Prüfen...
                            !(name.StringPrüfen(this.ZahlenFilter)) &&

                           //string auf vorhandensein von Sonderzeichen prüfen...
                           !(name.StringPrüfen(this.SonderZeichenFilter)) &&

                           //string auf @ zeichen durchsuchen...
                           !(name.Contains("@"));
            }

            return Ergebnis;
        }


    }
}
