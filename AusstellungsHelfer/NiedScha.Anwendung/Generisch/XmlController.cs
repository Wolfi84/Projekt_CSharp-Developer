using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.Anwendung.Generisch
{
    /// <summary>
    /// Stellt einen Dienst zum Speichern und Lesen
    /// von Auflistungen in und aus einer 
    /// Xml-Datei bereit.
    /// </summary>  
    public class XmlController<T> : Anwendungsobjekt
    {

        /// <summary>
        /// Schreibt die Daten im Xml Format
        /// in die angegebene Datei.
        /// </summary>
        /// <param name="pfad">Vollständiger Name der Datei,
        /// die benutzt werden soll.</param>
        /// <param name="daten">Die Liste mit den Informationen,
        /// die als Xml gespeichert werden sollen.</param>
        /// <exception cref="System.Exception">Wird ausgelöst,
        /// wenn das Speichern nicht erfolgreich war.</exception>
        public void Speichern(string pfad, T daten)
        {

            //Den Datenschreiber Instanzieren...
            using (var Schreiber = new System.IO.StreamWriter(pfad))
            {
                //Daten Speichern.
                var Serialisierer = new System.Xml.Serialization.XmlSerializer(daten.GetType());
                Serialisierer.Serialize(Schreiber, daten);

            } 
        }

        /// <summary>
        /// Gibt die Daten aus der Xml Datei zurück.
        /// </summary>
        /// <param name="pfad">Vollständiger Name der Datei,
        /// die benutzt werden soll.</param>
        /// <exception cref="System.Exception">Wird ausgelöst, wenn
        /// das Lesen nicht erfolgreich ist.</exception>
        public T Lesen(string pfad)
        {
            //Das Ergebins Erzeugen...
            T Ergebnis = default(T);


            //Den Datenleser initialiseren...
            using (var Leser = new System.IO.StreamReader(pfad))
            {
                //Die Daten Lesen.
                var Serialisierer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                Ergebnis = (T)Serialisierer.Deserialize(Leser);
            }

            return Ergebnis;
        }

    }
}
