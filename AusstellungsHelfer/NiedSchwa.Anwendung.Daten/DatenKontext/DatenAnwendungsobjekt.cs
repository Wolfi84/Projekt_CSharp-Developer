using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.Anwendung.Daten
{
    /// <summary>
    /// Untersützt alle Datenbank Anwendungsobjekte.
    /// </summary>
    public abstract class DatenAnwendungsobjekt : NiedSchwa.Anwendung.Anwendungsobjekt, System.ComponentModel.INotifyPropertyChanged
    {

        /// <summary>
        /// Ruft das Infrastrukturobjekt der Datenbankanwendung
        /// ab oder legt dieses fest.
        /// </summary>
        /// <remarks>Als Feld wird die Eigenschaft 
        /// der Basisklasse benutzt.</remarks>
        public new NiedSchwa.Anwendung.Daten.DatenAnwendungskontext AppKontext
        {
            get
            {

                return base.AppKontext as NiedSchwa.Anwendung.Daten.DatenAnwendungskontext;
            }
            set
            {
                
                base.AppKontext = value;
            }
        }

        /// <summary>
        /// Wird ausgelöst, wenn sich der Inhalt einer
        /// Eigenschaft geändert hat.
        /// </summary>
        /// <remarks>Notwendig für die WPF Datenbindung.</remarks>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Löst das Ereignis PropertyChanged aus.
        /// </summary>
        /// <param name="name">Die Bezeichnung der Eigenschaft,
        /// deren Inhalt geändert wurde.</param>
        /// <remarks>Eine fehlende name-Einstellung wird mit dem
        /// Aufrufernamen voreingestellt.</remarks>
        protected virtual void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName]string name = "")
        {
            //Wegen Multithreadings, damit
            //der Garbage Collector das Objekt mit
            //dem Behandler nicht wegräumt...
            var BehandlerKopie = this.PropertyChanged;

            if (BehandlerKopie != null)
            {
                BehandlerKopie(this, new PropertyChangedEventArgs(name));
            }
        }

        /// <summary>
        /// Wird vom AktiviereBeschäftigt erhöht
        /// und vom DeaktivereBeschäftigt vermindert.
        /// </summary>
        private int _BinBeschäftigtEbene = 0;

        /// <summary>
        /// Schreibt in das Protokoll, dass eine
        /// Methode zu laufen beginnt...
        /// </summary>
        public virtual void AktiviereBeschäftigt([System.Runtime.CompilerServices.CallerMemberName]string methode = "")
        {

            this.BinBeschäftigt = true;
            this._BinBeschäftigtEbene++;

            this.AppKontext.Protokoll.Eintragen($"{methode} startet...", ProtokollEintragTyp.Beschäftigt);
        }

        /// <summary>
        /// Schreibt in das Protokoll, dass eine
        /// Methode beendet ist.
        /// </summary>
        public virtual void DeaktiviereBeschäftigt([System.Runtime.CompilerServices.CallerMemberName]string methode = "")
        {
            this.AppKontext.Protokoll.Eintragen($"{methode} beendet.");

            this._BinBeschäftigtEbene--;

            if (this._BinBeschäftigtEbene <= 0)
            {
                this.BinBeschäftigt = false;
                this._BinBeschäftigtEbene = 0;
            }
        }

        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private bool _BinBeschäftigt = false;

        /// <summary>
        /// Ruft einen Wahrheitswert ab,
        /// der angibt, ob die Anwendung gerade arbeitet,
        /// oder legt diesen fest.
        /// </summary>
        public bool BinBeschäftigt
        {
            get
            {
                return this._BinBeschäftigt;
            }
            set
            {
                this._BinBeschäftigt = value;
                this.OnPropertyChanged();
            }
        }
    }
}
