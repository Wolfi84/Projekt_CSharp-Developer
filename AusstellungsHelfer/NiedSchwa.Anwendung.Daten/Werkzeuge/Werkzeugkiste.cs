using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.Anwendung.Daten.Erweiterungen
{
    /// <summary>
    /// Stellt diverse Hilfsmethoden als Erweiterungen bereit.
    /// </summary>
    public static class Werkzeugkiste
    {

        /// <summary>
        /// Gibt eine absolute Pfadangabe zurück.
        /// </summary>
        /// <param name="endeTeil">Eine relative Pfadangabe</param>
        /// <param name="basisTeil">Der absolute Pfad, mit dem der Pfad beginnen soll.</param>
        /// <returns>Eine absolute Pfadangabe bestehend aus Basis- und Endeteil.</returns>
        public static string ErzeugePfad(this string endeTeil, string basisTeil )
        {

            var EndeTeile = endeTeil.Split(System.IO.Path.DirectorySeparatorChar);
            var BasisTeile = basisTeil.Split(System.IO.Path.DirectorySeparatorChar);

            var AnzahlEbenenZurück = (from t in EndeTeile where t == ".." select t).Count();

            var NeueTeile 
                = (from t in BasisTeile
                   select t).Take(BasisTeile.Length - AnzahlEbenenZurück)
                .Union(
                    (from t in EndeTeile
                     select t).Skip(AnzahlEbenenZurück)
                ).ToArray();

            if (NeueTeile.Length > 0 && NeueTeile[0].EndsWith(":"))
            {
                NeueTeile[0] += System.IO.Path.DirectorySeparatorChar;
            }

            return System.IO.Path.Combine(NeueTeile);
        }

        /// <summary>
        /// Gibt den Inhalt der Quelle als gewünschten Typ zurück.
        /// </summary>
        /// <typeparam name="T">Der gewünschte Typ der Quelle.</typeparam>
        /// <param name="quelle">Die Daten, deren Typ gewechselt werden soll.</param>
        /// <remarks>Dabei wird eine "tiefe" Kopie der Quelle erzeugt.</remarks>
        public static T WechselnZu<T>(this object quelle)
        {

            var ZielTyp = typeof(T);
            var QuellTyp = quelle.GetType();

            //Der Typ der Quelle und des Ziels ist identisch
            if (ZielTyp == QuellTyp)
            {
                return (T)quelle;
            }
            else
            {
                //Der Zieltyp ist ein Array, die Quelle eine Liste
                if (ZielTyp.IsArray && quelle is System.Collections.IList)
                {
                    var ElementTyp = ZielTyp.GetElementType();
                    var Quelldaten = quelle as System.Collections.IList;
                    dynamic ErgebnisArray = System.Array.CreateInstance(ElementTyp, Quelldaten.Count);

                    for (int i = 0; i < Quelldaten.Count; i++)
                    {
                        //Für jedes Element der Quelle WechselnZu rekursiv aufrufen...
                        ErgebnisArray.SetValue(Quelldaten[i].WechselnZu(ElementTyp), i);
                    }

                    //Wegen "dynamic" prüft 
                    //erst der JIT - Compiler
                    return ErgebnisArray;

                }
                //Die Quelle ist ein Array, das Ziel eine Liste
                if (QuellTyp.IsArray && ZielTyp is System.Collections.IList)
                {
                    throw new NotImplementedException();
                }

                //Die Typen sind nicht identisch und keine 
                //Arrays bzw. Listen. Deshalb alle Eigenschaften kopieren
                var Ziel = System.Activator.CreateInstance<T>();

                foreach (var Eigenschaft in quelle.GetType().GetProperties())
                {
                    var ZielEigenschaft = Ziel.GetType().GetProperty(Eigenschaft.Name);
                    if (ZielEigenschaft != null)
                    {
                        ZielEigenschaft.SetValue(
                            Ziel, 
                            Eigenschaft.GetValue(quelle).WechselnZu(ZielEigenschaft.PropertyType));
                    }
                }

                return Ziel;
            }
        }

        /// <summary>
        /// Damit die Methodenbeschreibung nicht jedes Mal ermittelt werden muss...
        /// </summary>
        private static System.Reflection.MethodInfo _WechselnZuBeschreibung 
            = typeof(Werkzeugkiste).GetMethod("WechselnZu", new Type[] { typeof(object) });

        /// <summary>
        /// Gibt den Inhalt der Quelle als gewünschten Typ zurück.
        /// </summary>
        /// <param name="quelle">Die Daten, deren Typ gewechselt werden soll.</param>
        /// <param name="typ">Der gewünschte Zieltyp</param>
        /// <remarks>Dabei wird eine "tiefe" Kopie der Quelle erzeugt.</remarks>
        public static object WechselnZu(this object quelle, Type typ)
        {
            return Werkzeugkiste._WechselnZuBeschreibung.MakeGenericMethod(typ)
                .Invoke(null, new object[] { quelle });
        }





    }
}
