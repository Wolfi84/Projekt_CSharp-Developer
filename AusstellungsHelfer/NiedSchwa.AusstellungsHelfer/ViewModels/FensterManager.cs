using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.AusstellungsHelfer.ViewModels
{
    /// <summary>
    /// Stellt den Dienst zum verwalten des Fensters 
    /// zur Verfügung
    /// </summary>
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
            fenster.Closing += (sender, e) =>
            {
                this.AktiviereBeschäftigt("FensterPositionSpeichern");

                var Position = new NiedSchwa.Anwendung.Daten.Fenster { Name = fenster.Name };

                //Position initialisieren:
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

                this.DeaktiviereBeschäftigt("FensterPositionSpeichern");


                //den DataContext freigeben, weil sonst
                fenster.DataContext = null;
            };

            this.DeaktiviereBeschäftigt();
        }


        /// <summary>
        /// Ruft die aktuelle Anwendungssprache
        /// ab oder legt diese fest.
        /// </summary>
        public NiedSchwa.Anwendung.Daten.Sprache AktuelleSprache
        {
            get
            {
                return this.AppKontext.Sprachen.AktuelleSprache;
            }
            set
            {

                if (this.AppKontext.Sprachen.AktuelleSprache.Code != value.Code)
                {
                    System.Windows.MessageBox.Show(
                        Properties.Texte.Sprachwechsel,
                        Properties.Texte.Anwendungstitel,

                        System.Windows.MessageBoxButton.OK,
                        System.Windows.MessageBoxImage.Information);

                    this.AppKontext.Protokoll.Eintragen(
                        Properties.Texte.Sprachwechsel,
                        Anwendung.Daten.ProtokollEintragTyp.Fehler);
                }

                this.AppKontext.Sprachen.AktuelleSprache = value;

                this.OnPropertyChanged();
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private Model.DTOs.Aufgaben _Aufgaben = null;

        /// <summary>
        /// Ruft die Anwendungspunkte ab.
        /// </summary>
        public Model.DTOs.Aufgaben Aufgaben
        {
            get
            {
                if (this._Aufgaben == null)
                {
                    this.AktiviereBeschäftigt();

                    var AufgabenManager = this.AppKontext.Erzeuge<Model.Manager.AufgabenManager>();
                    this._Aufgaben = AufgabenManager.Liste;

                    this.AppKontext.Protokoll.Eintragen("Das Fenster hat die Aufgaben gecachet...");

                    //Die Standardaufgabe aktivieren...
                    var i = NiedSchwa.AusstellungsHelfer.Properties.Settings.Default.AktuelleAufgabe;
                    if (this._Aufgaben != null && i < this._Aufgaben.Count)
                    {
                        this.AktuelleAufgabe = this._Aufgaben[i];
                        this.AppKontext.Protokoll.Eintragen($"Die Aufgabe \"{this.AktuelleAufgabe.Name}\" wurde aktiviert...");
                    }

                    this.DeaktiviereBeschäftigt();
                }

                return this._Aufgaben;
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private Model.DTOs.Aufgabe _AktuelleAufgabe = null;

        /// <summary>
        /// Ruft den aktuellen Anwendungspunkt
        /// ab oder legt diesen fest.
        /// </summary>
        public Model.DTOs.Aufgabe AktuelleAufgabe
        {
            get
            {
                return this._AktuelleAufgabe;
            }
            set
            {
                this._AktuelleAufgabe = value;

                if (this._AktuelleAufgabe.Arbeitsbereich == null
                    && this._AktuelleAufgabe.ArbeitsbereichTyp != null)
                {
                    this._AktuelleAufgabe.Arbeitsbereich
                        = System.Activator.CreateInstance(this._AktuelleAufgabe.ArbeitsbereichTyp)
                        as System.Windows.Controls.UserControl;
                }

                this.OnPropertyChanged();
            }
        }


        ///// <summary>
        ///// Internes Feld für die Eigenschaft
        ///// </summary>
        //private ViewModels.KundenBestellungsViewModel _KundenBestellungsViewModel = null;

        ///// <summary>
        ///// Ruft das Viewmodel zum Verwalten der Kundenbestellungs View zur verfügung
        ///// </summary>
        //public ViewModels.KundenBestellungsViewModel KundenBestellungsViewModel
        //{
        //    get
        //    {
        //        if(this._KundenBestellungsViewModel == null)
        //        {
        //            this.AktiviereBeschäftigt();

        //            this._KundenBestellungsViewModel = this.AppKontext.Erzeuge<ViewModels.KundenBestellungsViewModel>();
        //            this._KundenBestellungsViewModel.FensterManager = this;


        //            this.AppKontext.Protokoll.Eintragen("Das KundenBestellungsViewModel wurde erzeugt...", Anwendung.Daten.ProtokollEintragTyp.NeueInstanz);
        //            this.DeaktiviereBeschäftigt();
        //        }

        //        return this._KundenBestellungsViewModel;
        //    }

        //}


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private ViewModels.GesamtBestellungsViewModel _GesamtBestellungsViewModel = null;

        /// <summary>
        /// Ruft das ViewModel für die Gesamtbestellungs View ab.
        /// </summary>
        public ViewModels.GesamtBestellungsViewModel GesamtBestellungsViewModel
        {
            get
            {
                if(this._GesamtBestellungsViewModel == null)
                {
                    this.AktiviereBeschäftigt();

                    this._GesamtBestellungsViewModel = this.AppKontext.Erzeuge<ViewModels.GesamtBestellungsViewModel>();
                    this._GesamtBestellungsViewModel.FensterManager = this;


                    this.AppKontext.Protokoll.Eintragen("Das GesamtBestellungsViewModel wurde erzeugt...", Anwendung.Daten.ProtokollEintragTyp.NeueInstanz);
                    this.DeaktiviereBeschäftigt();

                    
                }
                return this._GesamtBestellungsViewModel;
            }
        }


        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private ViewModels.BestellungsViewModel _BestellungsViewModel = null;

        /// <summary>
        /// Ruft das ViewModel für die Gesamtbestellungs View ab.
        /// </summary>
        public ViewModels.BestellungsViewModel BestellungsViewModel
        {
            get
            {
                if (this._BestellungsViewModel == null)
                {
                    this.AktiviereBeschäftigt();

                    this._BestellungsViewModel = this.AppKontext.Erzeuge<ViewModels.BestellungsViewModel>();
                    this._BestellungsViewModel.FensterManager = this;


                    this.AppKontext.Protokoll.Eintragen("Das BestellungsViewModel wurde erzeugt...", Anwendung.Daten.ProtokollEintragTyp.NeueInstanz);
                    this.DeaktiviereBeschäftigt();


                }
                return this._BestellungsViewModel;
            }
        }



        /// <summary>
        /// Internes Feld für die Eigenschaft
        /// </summary>
        private ViewModels.KundenViewModel _KundenViewModel = null;

        /// <summary>
        /// Ruft das ViewModel für die Gesamtbestellungs View ab.
        /// </summary>
        public ViewModels.KundenViewModel KundenViewModel
        {
            get
            {
                if (this._KundenViewModel == null)
                {
                    this.AktiviereBeschäftigt();

                    this._KundenViewModel = this.AppKontext.Erzeuge<ViewModels.KundenViewModel>();
                    this._KundenViewModel.FensterManager = this;


                    this.AppKontext.Protokoll.Eintragen("Das BestellungsViewModel wurde erzeugt...", Anwendung.Daten.ProtokollEintragTyp.NeueInstanz);
                    this.DeaktiviereBeschäftigt();


                }
                return this._KundenViewModel;
            }
        }




        /// <summary>
        /// Internes Feld für die Eigenschaft.
        /// </summary>
        private ViewModels.AdministratorViewModel _AdministratorViewModel = null;


        /// <summary>
        /// Ruft das ViewModel für die AdministratorView auf.
        /// </summary>
        public ViewModels.AdministratorViewModel AdministratorViewModel
        {
            get
            {
                if(this._AdministratorViewModel == null)
                {
                    this.AktiviereBeschäftigt();

                    this._AdministratorViewModel = this.AppKontext.Erzeuge<ViewModels.AdministratorViewModel>();
                    this._AdministratorViewModel.FensterManager = this;


                    this.AppKontext.Protokoll.Eintragen("Das AdministratorViewModel wurde erzeugt...",Anwendung.Daten.ProtokollEintragTyp.NeueInstanz);

                }
                return this._AdministratorViewModel;
            }
        }
    }
}
