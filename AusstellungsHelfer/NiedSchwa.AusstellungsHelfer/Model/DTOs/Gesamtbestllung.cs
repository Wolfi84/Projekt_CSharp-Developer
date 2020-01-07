using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.AusstellungsHelfer.Model.DTOs
{
    /// <summary>
    /// Stellt Informationen zu der
    /// Gesammtbestllung bereit.
    /// </summary>
    public class Gesamtbestllung
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private string _Veranstaltungsort;


        /// <summary>
        /// Ruft den Aktuellen Veranstaltungsort auf 
        /// oder legt diesen fest.
        /// </summary>
        public string Veranstaltungsort
        {
            get
            {
                return _Veranstaltungsort;
            }



            set
            {
                _Veranstaltungsort = value;
                // this.OnPropertyChanged();
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private System.DateTime _DatumVon = System.DateTime.Now;



        /// <summary>
        /// Ruft Aktuelle Obergrenze des Suchdatumsbereichs ab
        /// oder legt diesen fest.
        /// </summary>
        public System.DateTime DatumVon
        {
            get
            {
                return this._DatumVon;
            }
            set
            {
                this._DatumVon = value;

            }

        }

        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private System.DateTime _DatumBis = System.DateTime.Now;

        /// <summary>
        /// Ruft Aktuelle Untergrenze des Suchdatumsbereichs ab
        /// oder legt diesen fest.
        /// </summary>
        public System.DateTime DatumBis
        {
            get
            {
                return this._DatumBis;
            }
            set
            {

                this._DatumBis = value;



            }

        }


    }
}
