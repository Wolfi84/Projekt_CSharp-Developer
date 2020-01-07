using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.Anwendung.Controller
{
    /// <summary>
    /// Stellt einen Dienst zum Lesen und Schreiben
    /// von Anwendungssprachen aus bzw. in eine Xml Datei bereit.
    /// </summary>
    internal class SprachenXmlController : Generisch.XmlController<Daten.SpracheListe>
    {       
        /// <summary>
        /// Gibt die Sprachen aus den Ressourcen zurück.
        /// </summary>
        public NiedSchwa.Anwendung.Daten.SpracheListe HoleStandardsprachen()
        {
            //Ein neues Dokoment initialisieren...
            var Xml = new System.Xml.XmlDocument();

            //Eine neue Liste erstellen...
            var Ergebnis = new NiedSchwa.Anwendung.Daten.SpracheListe();

            //Das Xml File öffnen...
            Xml.LoadXml(NiedSchwa.Anwendung.Properties.Resource.Sprachen);

            //Die Liste Füllen...
            foreach (System.Xml.XmlNode s in Xml.DocumentElement.ChildNodes)
            {
               
                Ergebnis.Add(
                    new NiedSchwa.Anwendung.Daten.Sprache
                    {
                        Code = s.Attributes["code"].Value,
                        Name = s.Attributes["name"].Value
                    }
                    );

            }

            return Ergebnis;
        }

    }
}
