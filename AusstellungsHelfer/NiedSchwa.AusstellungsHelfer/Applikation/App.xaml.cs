

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


//Erweiterung einbinden
using NiedSchwa.Anwendung.Daten.Erweiterungen;




namespace NiedSchwa.AusstellungsHelfer
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {

        [System.STAThread]
        private static void Main()
        {

          
            //Unsere Infrastruktur hochfahren...
            var AppKontext = new NiedSchwa.Anwendung.Daten.DatenAnwendungskontext();

            ////Die Protokolleinträge sollen gespeichert werden...
            //
            if (NiedSchwa.AusstellungsHelfer.Properties.Settings.Default.LoggingEinAus)
            {
                AppKontext.Protokoll.Pfad = System.IO.Path.Combine(AppKontext.AnwendungsdatenPfadLokal, "Protokoll.log");
                AppKontext.Protokoll.Zusammenräumen(maxGenerationen: 2);
            }




            AppKontext.DatenbankPfad = NiedSchwa.AusstellungsHelfer.Properties.Settings.Default.DatenbankPfadExe;


            //Präprozessor Debugg lassen...
#if (DEBUG)

            
            AppKontext.DatenbankPfad = NiedSchwa.AusstellungsHelfer.Properties.Settings.Default.DatenbankPfad;
#endif



            if (!System.IO.Path.IsPathRooted(AppKontext.DatenbankPfad))
            {
                AppKontext.DatenbankPfad = AppKontext.DatenbankPfad.ErzeugePfad(AppKontext.Anwendungspfad);
            }

            AppKontext.SqlServer = NiedSchwa.AusstellungsHelfer.Properties.Settings.Default.SqlServer;
            AppKontext.SqlServerDatenbank = NiedSchwa.AusstellungsHelfer.Properties.Settings.Default.Datenbank;

            //Für WPF die Anwendung initialisieren
            var WpfApp = new App();

            //Damit die in App.xaml definierten
            //Ressourcen geladen werden...
            WpfApp.InitializeComponent();

            //Der Infrastruktur den Thread-Dienst mitteilen...
            AppKontext.Dispatcher = WpfApp.Dispatcher;


            //die Anwendungssprache einstellen
            AppKontext.Sprachen.Festlegen(NiedSchwa.AusstellungsHelfer.Properties.Settings.Default.AktuelleSprache);

            //Vorbereitungen für Mehrsprachige anwendung.

            ////Im Thread eine neue Kultur für die eingestellte Sprache einstellen.
            ////Default wird Deutsch eingestellt da davon ausgegangen wird das der
            ////Großteil der User Deutsch spricht.
            ////
            //System.Threading.Thread.CurrentThread.CurrentUICulture
            //    = new System.Globalization.CultureInfo(AppKontext.Sprachen.AktuelleSprache.Code);

            //if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name
            //    != System.Threading.Thread.CurrentThread.CurrentCulture.Parent.Name)
            //{
            //    AppKontext.Protokoll.Eintragen(
            //        "Die Oberflächensprache unterscheidet sich von der Sprache für die Zahlenformatierung!",
            //        Anwendung.Daten.ProtokollEintragTyp.Warnung);
            //}



            ////Sprachen-Manager seine Caches entleert
            ////und die Namen der Sprachen sicher übersetzt werden...
            //AppKontext.Sprachen.Aktualisieren();

            //Das ViewModel starten...
            var VM = AppKontext.Erzeuge<ViewModels.FensterManager>();

            VM.Starten<Hauptfenster>();


            ////Die aktuelle Sprache in die Konfiguration übernehmen...
            //NiedSchwa.AusstellungsHelfer.Properties.Settings.Default.AktuelleSprache
            //    = AppKontext.Sprachen.AktuelleSprache.Code;

            //Damit z. B. die Fensterpositionen gespeichert werden...
            AppKontext.Herunterfahren();

            //Damit die Konfiguration gespeichert wird...
            NiedSchwa.AusstellungsHelfer.Properties.Settings.Default.Save();


        }



    }
}
