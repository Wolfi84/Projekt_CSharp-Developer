using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.Anwendung
{
    /// <summary>
    /// Stellt die Methode dar, die das FehlerAufgetreten Ereignis behandeln
    /// </summary>
    /// <param name="sender">Bei Ereignisbehandlern immer der erste Parameter.</param>
    /// <param name="e">Bei Ereignisbehandlern immer der zweite Parameter</param>
    /// <remarks>Bei eigenen Ereignissen mit Daten der zweite Schritt.</remarks>
    public delegate void FehlerAufgetretenEventHandler(object sender, FehlerAufgetretenEventArgs e);


    /// <summary>
    /// Stellt die Daten für das FehlerAufgetreten Ereignis bereit.
    /// </summary>
    /// <remarks>Bei eigenen Ereignissen mit Daten der erste Schritt.</remarks>
    public class FehlerAufgetretenEventArgs : System.EventArgs
    {

        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private System.Exception _Ausnahme = null;

        /// <summary>
        /// Ruft die Ursache des Fehlers ab.
        /// </summary>
        public System.Exception Ausnahme
        {
            get
            {
                return this._Ausnahme;
            }
        }

        /// <summary>
        /// Initialisiert ein neues FehlerAufgetretenEventArgs Objekt.
        /// </summary>
        /// <param name="ausnahme">Die Ursache des Fehlers</param>
        public FehlerAufgetretenEventArgs(System.Exception ausnahme)
        {
            this._Ausnahme = ausnahme;
        }
    }
}
