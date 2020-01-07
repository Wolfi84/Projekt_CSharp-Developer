using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.Anwendung.Daten
{

    /// <summary>
    /// Stellt eine Auflistung von SchwacherMethodenVerweis-Objekten bereit.
    /// </summary>
    public class SchwacherMethodenVerweisListe : System.Collections.Generic.List<SchwacherMethodenVerweis>
    {
        /// <summary>
        /// Wird ausgelöst, wenn Rückrufmethoden automatisch
        /// gelöscht wurden, weil der Besitzer nicht mehr existiert.
        /// </summary>
        public event System.EventHandler NichtStornierteRückrufe;

        /// <summary>
        /// Löst das NichtStornierteRückrufe Ereignis aus.
        /// </summary>
        protected virtual void OnNichtStornierteRückrufe()
        {
            var BehandlerKopie = this.NichtStornierteRückrufe;
            if (BehandlerKopie != null)
            {
                BehandlerKopie(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private SchwacherMethodenVerweisListe _ToteRückrufe = null;

        /// <summary>
        /// Ruft die Liste mit den Einträgen ab,
        /// der Besitzer nicht mehr vorhanden ist.
        /// </summary>
        protected SchwacherMethodenVerweisListe ToteRückrufe
        {
            get
            {
                if (this._ToteRückrufe == null)
                {
                    this._ToteRückrufe = new SchwacherMethodenVerweisListe();
                }
                return this._ToteRückrufe;
            }
        }

        /// <summary>
        /// Ruft alle hinterlegen Methoden auf.
        /// </summary>
        /// <remarks>Methoden, deren Besiter nicht
        /// mehr vorhanden ist, werden automatisch
        /// entfern.</remarks>
        public void AlleAusführen()
        {
            foreach (var Rückruf in this)
            {
                if (Rückruf.AufrufDelegate != null)
                {
                    Rückruf.AufrufDelegate.Invoke();
                }
                else
                {
                    this.ToteRückrufe.Add(Rückruf);
                }
            }

            //Sollten nicht mehr exisistierende
            //Rückrufbesitzer vorhanden sein,
            //diese aus der Liste entfernen.
            if (this._ToteRückrufe != null && this._ToteRückrufe.Count > 0)
            //Hier wird bewusst mit dem Feld gearbeitet,
            //weil die Eigenschaft das Objekt erzeugen würde und 
            //in einer sauberen Anwendung der Fall überhaupt
            //nicht eintreten darf
            {
                foreach (var Eintrag in this.ToteRückrufe)
                {
                    this.Remove(Eintrag);
                }

                this.OnNichtStornierteRückrufe();

                this.ToteRückrufe.Clear();
            }
        }

    }

    /// <summary>
    /// Kapselt eine Methode ohne die 
    /// Garbage Collection davon abzuhalten,
    /// den Besitzer zu entfernen.
    /// </summary>
    public class SchwacherMethodenVerweis
    {

        /// <summary>
        /// Internes Feld zum Kapseln der Methode
        /// ohne die Garbage Collection an der Freigabe zu hindern.
        /// </summary>
        private System.WeakReference _Methode = null;

        /// <summary>
        /// Initialisiert ein neues Objekt.
        /// </summary>
        /// <param name="methode">Ein Delegate, der in diesem
        /// Objekt gekapselt werden soll, ohne die Garbage Collection zu blockieren.</param>
        public SchwacherMethodenVerweis(System.Action methode)
        {
            this._Methode = new WeakReference(methode);

        }

        /// <summary>
        /// Ruft die Methode ab, die in
        /// diesem Objekt gekapselt ist.
        /// </summary>
        /// <remarks>null, falls der Besitzer nicht mehr lebt.</remarks>
        public System.Action AufrufDelegate
        {
            get
            {
                if (this._Methode.IsAlive)
                {

                    return this._Methode.Target as System.Action;
                }

                return null;
            }
        }

    }
}
