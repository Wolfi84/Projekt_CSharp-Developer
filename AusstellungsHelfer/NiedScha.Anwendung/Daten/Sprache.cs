using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.Anwendung.Daten
{

    /// <summary>
    /// Stellt eine Liste von Anwendungssprachen bereit.
    /// </summary>
    public class SpracheListe : System.Collections.Generic.List<Sprache>
    {
        /// <summary>
        /// Gibt die Sprache mit dem Microsoft Code zurück.
        /// </summary>
        /// <param name="code">Microsoft Code der Sprache, die gesucht wird.</param>
        /// <returns>Null, falls die Sprache nicht vorhanden ist.</returns>
        /// <remarks>Die Groß-/Kleinschreibung wird ignoriert.</remarks>
        public Sprache Suchen(string code)
        {


            return this.Find(s => string.Compare(s.Code, code, ignoreCase: true) == 0);

        }
    }

    /// <summary>
    /// Stellt Information über eine
    /// Anwendungssprache bereit.
    /// </summary>
    /// <remarks>Eine Liste aller Codes 
    /// ist unter https://msdn.microsoft.com/en-us/library/cc233982.aspx zu finden.</remarks>
    public class Sprache : System.Object
    {

        /// <summary>
        /// Ruft den Microsoft Schlüssel der 
        /// Sprache ab oder legt diesen fest.
        /// </summary>
        /// <remarks>Standardwert ist null.</remarks>
        public string Code { get; set; }


        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private string _Name = string.Empty;

        /// <summary>
        /// Ruft die lesbare Bezeichnung der
        /// Sprache ab oder legt diese fest.
        /// </summary>
        /// <remarks>Standardwert: ein Leerstring</remarks>
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;

            }
        }

        /// <summary>
        /// Gibt eine Zeichenfolge zurück, die diese Sprache darstellt.
        /// </summary>
        public override string ToString()
        {
            return $"{this.GetType().Name}(Code=\"{this.Code}\", Name=\"{this.Name}\")";
        }

    }
}
