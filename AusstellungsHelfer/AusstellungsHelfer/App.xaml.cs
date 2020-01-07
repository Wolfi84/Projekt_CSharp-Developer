using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

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

            var AppKontext = new NiedSchwa.Anwendung.Daten.DatenAnwendungskontext();

            //WPF Anwendung Erzeugen und initialisieren
            var WpfApp = new App();

            WpfApp.InitializeComponent();


            //TODO
            //Der Infrastruktur den Thread-Dienst mitteilen...
            AppKontext.Dispatcher = WpfApp.Dispatcher;

            //die Anwendungssprache einstellen
            AppKontext.Sprachen.Festlegen(AusstellungsHelfer.Properties.Settings.Default.AktuelleSprache);


            
            
            //Aktuelle Sprache Einstellen
            System.Threading.Thread.CurrentThread.CurrentUICulture
                = new System.Globalization.CultureInfo(AppKontext.Sprachen.AktuelleSprache.Code);

            if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name
                != System.Threading.Thread.CurrentThread.CurrentCulture.Parent.Name)
            {
                AppKontext.Protokoll.Eintragen(
                    "Die Oberflächensprache unterscheidet sich von der Sprache für die Zahlenformatierung!",
                    NiedSchwa.Anwendung.Daten.ProtokollEintragTyp.Warnung);
            }

            //Sprachen-Manager Cache leeren
            AppKontext.Sprachen.Aktualisieren();




            //Das ViewModel starten...
            var VM = AppKontext.Erzeuge<NiedSchwa.AusstellungsHelfer.>();

            NiedSchwa.Anwendung.Daten.


            //TODO 








            //Die aktuelle Sprache in die Konfiguration schreiben
            AusstellungsHelfer.Properties.Settings.Default.AktuelleSprache = AppKontext.Sprachen.AktuelleSprache.Code;

            //Fensterpositionen speichern
            AppKontext.Herunterfahren();

            //Konfiguration gespeichern
            AusstellungsHelfer.Properties.Settings.Default.Save();

        }
    }
}
