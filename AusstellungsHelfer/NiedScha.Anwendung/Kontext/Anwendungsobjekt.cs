using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.Anwendung
{
    /// <summary>
    /// Unsterstützt sämtliche Objekte einer NiedSchwa Anwendung.
    /// </summary>
    public abstract class Anwendungsobjekt : System.Object, IAppKontext
    {
        /// <summary>
        /// Ruft das Infrastrukturobjekt ab oder legt dieses fest.
        /// </summary>
        public Anwendungskontext AppKontext { get; set; }


        /// <summary>
        /// Wird ausgelöst, wenn eine Ausnahme 
        /// in der Anwendung aufgetreten ist.
        /// </summary>
        public event FehlerAufgetretenEventHandler FehlerAufgetreten;



        /// <summary>
        /// Löst das Ereignis FehlerAufgetreten aus.
        /// </summary>
        /// <param name="e">Das Objekt mit den Ereignisdaten</param>
        protected virtual void OnFehlerAufgetreten(FehlerAufgetretenEventArgs e)
        {
            if (this.FehlerAufgetreten != null)
            {
                this.FehlerAufgetreten(this, e);
            }
        }

    }
}
