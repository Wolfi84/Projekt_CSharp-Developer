using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.AusstellungsHelfer.Model.Manager
{
    /// <summary>
    /// Stellt einen Dienst zum
    /// Verwalten der Anwendungspunkte bereit.
    /// </summary>
    public class AufgabenManager : NiedSchwa.Anwendung.Daten.DatenAnwendungsobjekt
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private DTOs.Aufgaben _Liste = null;

        /// <summary>
        /// Ruft die Liste mit den unterstützten
        /// Anwendungspunkten ab.
        /// </summary>
        public DTOs.Aufgaben Liste
        {
            get
            {
                if (this._Liste == null)
                {
                    this._Liste = new DTOs.Aufgaben();

                   
                    this._Liste.Add(new DTOs.Aufgabe
                    {
                        Symbol = "",
                        Name = "Kundendaten",
                        ArbeitsbereichTyp = typeof(KundenView)
                    });
                    this._Liste.Add(new DTOs.Aufgabe
                    {
                        Symbol = "",
                        Name = "Bestellungen",
                        ArbeitsbereichTyp = typeof(KundenBestellungsViev)
                    });

                    this._Liste.Add(new DTOs.Aufgabe
                    {
                        Symbol = "",
                        Name = "Bestell-Liste",
                        ArbeitsbereichTyp = typeof(GesamtBestellungsView)
                    });

                    this._Liste.Add(new DTOs.Aufgabe
                    {
                        Symbol = "",
                        Name = "Administrator",
                        ArbeitsbereichTyp = typeof(Views.AdministratorView)
                    });
                }

                return this._Liste;
            }
        }

    }
}
