using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.AusstellungsHelfer.Model.Manager
{
    /// <summary>
    /// Stellt Die Basisdienste der Managerklassen zur verfügung.
    /// </summary>
    public abstract class BasisDatenManger : NiedSchwa.Anwendung.Daten.DatenAnwendungsobjekt
    {

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private Controller.BestellunsSQLController _Controller = null;


        /// <summary>
        /// Ruft den SQL-Controller ab mit dem die
        /// Gesamtbestelldaten erzeugt werden
        /// </summary>
        protected Controller.BestellunsSQLController Controller
        {
            get
            {
                if (this._Controller == null)
                {
                    AktiviereBeschäftigt();
                    this._Controller = this.AppKontext.Erzeuge<Controller.BestellunsSQLController>();

                    this.AppKontext.Protokoll.Eintragen("Der SQL - Controller wurde erzeugt...");
                    DeaktiviereBeschäftigt();

                }

                return _Controller;
            }
        }

    }
}
