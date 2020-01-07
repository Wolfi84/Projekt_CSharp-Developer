using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.AusstellungsHelfer.Erweiterungen
{
    /// <summary>
    /// Stellt diverse Erweiterungsmethoden zur verfügung
    /// </summary>
    public static class Werkzeugkiste
    {

        /// <summary>
        /// Fügt dem ende des Objektes so viele
        /// füllzeichen ein bis die maximale länge erreicht
        /// wird.
        /// </summary>
        /// <param name="basis">Das Objekt selbst</param>
        /// <param name="füllzeichen">das füllzeichen</param>
        /// <param name="maxlänge">maximale Zeichenketten länge</param>
        /// <returns> den erweiterten String</returns>
        public static string Füllen(this string basis, string füllzeichen, int maxlänge)
        {

            while ((basis.Length + füllzeichen.Length) <= maxlänge)
            {
                basis = String.Concat(basis, füllzeichen);
            }
            return basis;
        }

        /// <summary>
        /// Prüft den String auf nummern oder Sonderzeichen
        /// </summary>
        /// <param name="name">Zu Prüfender String</param>
        /// <param name="gesuchteZeichen">Gesuchte Zeichen</param>
        /// <returns>True wenn eines der gesuchten Zeichen enthalten ist.</returns>
        public static bool StringPrüfen(this string name, string[] gesuchteZeichen)
        {
            var Ergebnis = false;

            if (name != null)
            {

                for (int i = 0; i < gesuchteZeichen.Length; i++)
                {
                    Ergebnis = name.Contains(gesuchteZeichen[i]);
                    if (Ergebnis)
                    {
                        break;
                    }
                }

            }
            return Ergebnis;
        }


    }
}
