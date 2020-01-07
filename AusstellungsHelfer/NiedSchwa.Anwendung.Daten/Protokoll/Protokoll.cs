using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.Anwendung.Daten
{
    /// <summary>
    /// Listet unterschiedliche
    /// Varianten von Protokolleinträgen auf.
    /// </summary>
    public enum ProtokollEintragTyp
    {
        /// <summary>
        /// Kennzeichnet einen Hinweis im Protkoll.
        /// </summary>
        Normal,
        /// <summary>
        /// Kennzeichnet einen Eintrag,
        /// der auf ein neues Objekt hinweist.
        /// </summary>
        NeueInstanz,
        /// <summary>
        /// Kennzeichnet einen Eintrag, der
        /// beachtet werden soll.
        /// </summary>
        Warnung,
        /// <summary>
        /// Kennzeichnet einen Eintrag
        /// wegen einer Ausnahme.
        /// </summary>
        Fehler,
        /// <summary>
        /// Kennzeichnet einen Eintrag,
        /// wenn die Anwendung eine Arbeit beginnt
        /// </summary>
        Beschäftigt
    }

    /// <summary>
    /// Stellt eine Liste von Protokolleinträgen bereit.
    /// </summary>
    /// <remarks>Für die WPF Datenbindung ist die
    /// ObservableCollection zu bevorzugen.</remarks>
    public class ProtokollEintragListe : System.Collections.ObjectModel.ObservableCollection<ProtokollEintrag>
    {

    }

    /// <summary>
    /// Beschreibt eine Protokollzeile.
    /// </summary>
    public class ProtokollEintrag
    {
        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private DateTime _ErstelltUm = DateTime.Now;

        /// <summary>
        /// Ruft den Zeitpunkt ab, zu dem
        /// der Eintrag erstellt wurde, oder legt diesen fest.
        /// </summary>
        public DateTime ErstelltUm
        {
            get
            {
                return this._ErstelltUm;
            }
            set
            {
                this._ErstelltUm = value;
            }
        }

        /// <summary>
        /// Ruft die Art des Eintrags ab
        /// oder legt diese fest.
        /// </summary>
        public ProtokollEintragTyp Typ { get; set; }

        /// <summary>
        /// Ruft den Informationstext des
        /// Eintrags ab oder legt diesen fest.
        /// </summary>
        public string Text { get; set; }
    }
}
