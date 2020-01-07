using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.Anwendung.Controller
{

    //Neue Version: Erweitern einer generischen Klasse
    //20190117

    /// <summary>
    /// Stellt einen Dienst zum Speichern und Lesen
    /// von Fensterpositionen in und aus einer 
    /// Xml-Datei bereit.
    /// </summary>
    internal class FensterXmlController : Generisch.XmlController<Daten.FensterListe>
    {

    }

    ///// <summary>
    ///// Stellt einen Dienst zum Speichern und Lesen
    ///// von Fensterpositionen in und aus einer 
    ///// Xml-Datei bereit.
    ///// </summary>
    //internal class FensterXmlController : Anwendungsobjekt
    //{

//    /// <summary>
//    /// Schreibt die Daten im Xml Format
//    /// in die angegebene Datei.
//    /// </summary>
//    /// <param name="pfad">Vollständiger Name der Datei,
//    /// die benutzt werden soll.</param>
//    /// <param name="daten">Die Liste mit den Informationen,
//    /// die als Xml gespeichert werden sollen.</param>
//    /// <exception cref="System.Exception">Wird ausgelöst,
//    /// wenn das Speichern nicht erfolgreich war.</exception>
//    public void Speichern(string pfad, NiedSchwa.Anwendung.Daten.FensterListe daten)
//    {
//        //Den Schreiber Initialisierung...
//        using (var Schreiber = new System.IO.StreamWriter(pfad))
//        {
//            //Daten Schreiben...
//            var Serialisierer = new System.Xml.Serialization.XmlSerializer(daten.GetType());
//            Serialisierer.Serialize(Schreiber, daten);

//        }
//    }

//    /// <summary>
//    /// Gibt die Daten aus der Xml Datei zurück.
//    /// </summary>
//    /// <param name="pfad">Vollständiger Name der Datei,
//    /// die benutzt werden soll.</param>
//    /// <exception cref="System.Exception">Wird ausgelöst, wenn
//    /// das Lesen nicht erfolgreich ist.</exception>
//    public NiedSchwa.Anwendung.Daten.FensterListe Lesen(string pfad)
//    {
//        //Das Ergebnis vorbelegen...
//        NiedSchwa.Anwendung.Daten.FensterListe Ergebnis = null;

//        //Daten Leser Erzeugen und Daten lesen.
//        using (var Leser = new System.IO.StreamReader(pfad))
//        {
//            var Serialisierer = new System.Xml.Serialization.XmlSerializer(typeof(NiedSchwa.Anwendung.Daten.FensterListe));
//            Ergebnis = (NiedSchwa.Anwendung.Daten.FensterListe)Serialisierer.Deserialize(Leser);
//        }

//        return Ergebnis;
//    }

//}
}
