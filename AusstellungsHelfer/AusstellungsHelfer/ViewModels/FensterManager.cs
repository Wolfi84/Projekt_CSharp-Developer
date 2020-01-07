using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.Sisharp.Teil2.ViewModels
{
    /// <summary>
    /// Steuert die Hauptoberfläche der Anwendung.
    /// </summary>
    /// <remarks>Im Rahmen von MVVM handelt es sich um
    /// ein VM (ViewModel) Objekt.</remarks>
    public class FensterManager : NiedSchwa.Anwendung.Daten.DatenAnwendungsobjekt
    {
        /// <summary>
        /// Wird vom Starten initialisiert und
        /// beim Anzeigen eines neuens Fensters benötigt.
        /// </summary>
        private static Type _FensterTyp = null;

        /// <summary>
        /// Zeigt ein Fenster des gewünschten Typs an
        /// und macht es zum Hauptfenster der WPF Anwendung.
        /// </summary>
        /// <typeparam name="T">Der Typ der anzuzeigenden View.</typeparam>
        public void Starten<T>() where T : System.Windows.Window, new()
        {

            this.AktiviereBeschäftigt("ViewModel");

            FensterManager._FensterTyp = typeof(T);

            //var Fenster = new T();
            //var Fenster = System.Activator.CreateInstance<T>();
            //damit trotzdem new funktioniert, eine Typeinschränkung...

            var Fenster = new T();

            this.InitialisiereFenster(Fenster);

            //Das ViewModel und die View "verknüpfen"...
            Fenster.DataContext = this;

            this.DeaktiviereBeschäftigt("ViewModel");

            System.Windows.Application.Current.Run(Fenster);
        }

        /// <summary>
        /// Bereitet ein Fenster für die Anzeige vor.
        /// </summary>
        /// <param name="fenster">Das Fenster-Objekt,
        /// das vorbereitet werden soll.</param>
        protected virtual void InitialisiereFenster(System.Windows.Window fenster)
        {
            this.AktiviereBeschäftigt();

            //Damit xml keine Probleme beim Formatieren
            //von Datumsangaben und anderen Zahlen hat...
            fenster.Language 
                = System.Windows.Markup.XmlLanguage.GetLanguage(
                    System.Threading.Thread.CurrentThread.CurrentCulture.IetfLanguageTag);

            //Dazu dem Fenster einen Namen verpassen
            fenster.Name = fenster.GetType().Name;

            //Zum Unterscheiden der Fenster 
            //eine Nummer so anhängen, dass
            //die erste freie Nummer wiederbenutzt wird.

            //Gut, aber aus... LINQ nicht implementiert
            //var OffeneFenster = (from f in System.Windows.Application.Current.Windows select f).ToArray();
            
            //Also, eine eigene Liste mit den aktuellen Fensternamen aufbauen
            var OffeneFenster = new System.Collections.ArrayList(System.Windows.Application.Current.Windows.Count);
            foreach (System.Windows.Window w in System.Windows.Application.Current.Windows)
            {
                OffeneFenster.Add(w.Name);
            }

            var FreieNummer = 1;
            while (OffeneFenster.Contains(fenster.Name + FreieNummer))
            {
                FreieNummer++;
            }

            fenster.Name += FreieNummer;

            //Eine alte Fensterposition wiederherstellen...
            var AltePosition = this.AppKontext.Fenster.Abrufen(fenster.Name);

            if (AltePosition != null)
            {
                fenster.Left = AltePosition.Links ?? fenster.Left;
                fenster.Top = AltePosition.Oben ?? fenster.Top;
                fenster.Width = AltePosition.Breite ?? fenster.Width;
                fenster.Height = AltePosition.Höhe ?? fenster.Height;

                //Nur Maximiert wiederherstellen, sonst normal,
                //weil minimierte Fenster von den Benutzern übersehen werden
                fenster.WindowState =
                    (System.Windows.WindowState)AltePosition.Zustand == System.Windows.WindowState.Maximized ?
                    System.Windows.WindowState.Maximized : System.Windows.WindowState.Normal;

            }

            //Dafür sorgen, dass beim Schließen
            //die Fensterposition gespeichert wird
            //(dazu ein anonymer Ereignisbehandler)
            fenster.Closing += (sender, e) =>
            {
                //Hr. Grabner; 20190214
                //-> Den CallerMemberName wegen der
                //   anonymen Methode überschreiben
                //this.AktiviereBeschäftigt();
                this.AktiviereBeschäftigt("FensterPositionSpeichern");

                var Position = new NiedSchwa.Anwendung.Daten.Fenster { Name = fenster.Name };

                //Position initialisieren:

                //Auf alle Fälle den Zustand
                Position.Zustand = (int)fenster.WindowState;

                //Die Größenangaben nur, falls ein normales Fenster
                if (fenster.WindowState == System.Windows.WindowState.Normal)
                {
                    Position.Links = (int)fenster.Left;
                    Position.Oben = (int)fenster.Top;
                    Position.Breite = (int)fenster.Width;
                    Position.Höhe = (int)fenster.Height;
                }

                this.AppKontext.Fenster.Hinterlegen(Position);

                //Hr. Grabner; 20190214
                //-> Den CallerMemberName wegen der
                //   anonymen Methode überschreiben
                //this.DeaktiviereBeschäftigt();
                this.DeaktiviereBeschäftigt("FensterPositionSpeichern");

                //Damit die Garbage Collection das
                //WPF Fenster entfernt (nirgendwo ein Dispose)
                //den DataContext freigeben, weil sonst
                //Speicherloch auftritt...
                fenster.DataContext = null;
            };

            this.DeaktiviereBeschäftigt();
        }



    }
}