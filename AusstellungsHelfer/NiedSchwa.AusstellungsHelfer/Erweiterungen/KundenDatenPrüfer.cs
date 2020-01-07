using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.AusstellungsHelfer.Erweiterungen
{
    /// <summary>
    /// Stellt diverse Prüfmethoden zur KundenValidierung zur verfügung.
    /// </summary>
    public class KundenDatenPrüfer : EingabeFilter

    { 

        /// <summary>
        /// Prüft einen Namen auf den Inhalt aller Filter.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>True wenn kein Filterzeichen enthalten ist.</returns>
        public bool KundenNamenPrüfen(string name)
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

        /// <summary>
        /// Prüft die Adresse auf vorhandensein eines @ zeichen.
        /// </summary>
        /// <param name="eMail">E-Mail String</param>
        /// <returns>True wenn eines der zeichen vorhanden ist.</returns>
        public bool EmailAdressePrüfen(string eMail)
        {

            return eMail.Contains("@");
            
        }


        /// <summary>
        /// Orüft die Richtigkeit der Eingegebenen nummer.
        /// </summary>
        /// <param name="nummer">Telefonnummer</param>
        /// <returns></returns>
        public bool TelefonNummerPrüfen(string nummer)
        {
            var Ergebnis = false;

            if (nummer != null)
            {

                Ergebnis = nummer.Length > 7 &&
                        !nummer.StringPrüfen(this.SonderZeichenFilter) &&
                            nummer.StringPrüfen(this.ZahlenFilter); 
            }
                            
            return Ergebnis;
        }


        /// <summary>
        /// Prüft auf Vorhandensein von zahlen
        /// </summary>
        /// <param name="hausnummer">Hausnummer incl. zusatzzeichen</param>
        /// <returns></returns>
        public bool HausnummerPrüfen(string hausnummer)
        {
            return hausnummer.StringPrüfen(this.ZahlenFilter);


        }


    


    }
}
