using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.Windows
{
    /// <summary>
    /// Stellt für WPF einen Befehl bereit,
    /// der für die Datenbindung benutzt werden kann.
    /// </summary>
    public class Befehl : System.Windows.Input.ICommand
    {
        /// <summary>
        /// Wird ausgelöst, wenn sich der Zustand
        /// des Befehls von "Erlaubt" auf "Nicht erlaubt"
        /// oder umgekehrt ändert.
        /// </summary>
        /// <remarks>Wird mit dem WPF CommandManager verknüpft.</remarks>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                System.Windows.Input.CommandManager.RequerySuggested += value;
            }
            remove
            {
                System.Windows.Input.CommandManager.RequerySuggested -= value;
            }
        }

        /// <summary>
        /// Internes Feld für die Methode,
        /// die prüft, ob der Befehl aktuell 
        /// ausgeführt werden darf.
        /// </summary>
        private System.Predicate<object> _CanExecuteMethode = null;

        /// <summary>
        /// Gibt einen Wahrheitswert zurück,
        /// ob der Befehl aktuell ausgeführt werden darf.
        /// </summary>
        /// <param name="parameter">Zusatzdaten der Datenbindung.</param>
        public bool CanExecute(object parameter)
        {
            return this._CanExecuteMethode == null ? true : this._CanExecuteMethode(parameter);
        }

        /// <summary>
        /// Internes Feld für die gekapselte
        /// Methode, die dieser Befehl ausführen soll.
        /// </summary>
        private System.Action<object> _ExecuteMethode = null;

        /// <summary>
        /// Wird aufgerufen, wenn der Befehl ausgeführt wird.
        /// </summary>
        /// <param name="parameter">Zusatzdaten der Datenbindung</param>
        public void Execute(object parameter)
        {
            this._ExecuteMethode(parameter);
        }

        /// <summary>
        /// Initialisiert ein neues Befehl-Objekt.
        /// </summary>
        /// <param name="executeMethode">Die Methode, die ausgeführt
        /// werden soll, wenn der Befehl benutzt wird.</param>
        public Befehl(System.Action<object> executeMethode) : this(executeMethode, canExecuteMethode: null)
        {

        }

        /// <summary>
        /// Initialisiert ein neues Befehl-Objekt.
        /// </summary>
        /// <param name="executeMethode">Die Methode, die ausgeführt
        /// werden soll, wenn der Befehl benutzt wird.</param>
        /// <param name="canExecuteMethode">Die Methode, die prüft,
        /// ober der Befehl aktuell ausgeführt werden darf.</param>
        public Befehl(System.Action<object> executeMethode, System.Predicate<object> canExecuteMethode)
        {

            if (executeMethode == null)
            {
                throw new ArgumentException("Die Methode für den Befehl muss angegeben werden!");
            }

            this._ExecuteMethode = executeMethode;
            this._CanExecuteMethode = canExecuteMethode;
        }
    }
}
