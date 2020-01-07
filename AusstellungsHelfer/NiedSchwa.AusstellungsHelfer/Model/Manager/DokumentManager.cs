using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Die Erweiterungen aktivieren
using NiedSchwa.AusstellungsHelfer.Erweiterungen;

namespace NiedSchwa.AusstellungsHelfer.Model.Manager
{
    /// <summary>
    /// Stellt Hilfs Methoden zum Erzeugen von Dokumenten bereit
    /// </summary>
   public class DokumentManager : NiedSchwa.Anwendung.Daten.DatenAnwendungsobjekt
    {
        /// <summary>
        /// Erstellt ein Rechnungsdokument.
        /// </summary>
        /// <param name="bücher">Bestellpositionen</param>
        /// <param name="bestellung">Bestellungsdaten</param>
        /// <param name="kunde">Kundendaten</param>
        /// <param name="druckgröße">Die Größe des Druckbereichs</param>
        /// <returns>Das Erstellte Rechnungsdokument.</returns>
        public System.Windows.Documents.FixedDocument RechnungsDokumentErzeugen(DTOs.Bücher bücher, DTOs.Bestellung bestellung, DTOs.Kunde kunde, System.Windows.Size druckgröße)
        {

            //Ein neues Dokument erstellen...
            System.Windows.Documents.FixedDocument Dokument = new System.Windows.Documents.FixedDocument();

            //Die Dokumentgröße den Druckbereich übergeben...
            Dokument.DocumentPaginator.PageSize = druckgröße;


            double pageWidth = Dokument.DocumentPaginator.PageSize.Width;          
            double pageHeight = Dokument.DocumentPaginator.PageSize.Height;



            //Listenindex initialisieren...
            int ListenIndex = 0;

            while (ListenIndex < bücher.Count)
            {
                //Den Seiten Kontext erzeugen...
                System.Windows.Documents.PageContent SeitenKontext = new System.Windows.Documents.PageContent();

                //Eine Seite erzeugen...
                System.Windows.Documents.FixedPage Seite = new System.Windows.Documents.FixedPage();

                //Ein neues Size Objekt erzeugen...
                System.Windows.Size sz = new System.Windows.Size(pageWidth, pageHeight);

                //Seitenmaße setzen...
                Seite.Measure(sz);

                //Seite Positionieren...
                Seite.Arrange(new System.Windows.Rect(new System.Windows.Point(), sz));

                //Hintergrund festlegen...
                Seite.Background = System.Windows.Media.Brushes.White;


                //Seitenmaße setzen...
                Seite.Width = pageWidth;
                Seite.Height = pageHeight;


                //Die Ränder der Gezeichneten Daten Setzen...
                double DatenRandSeite = 20;
                double DatenRandOben = 0.0;

                double KopfzeileRandSeite = 10;
                double KopfzeileRandOben = 20;


                //Kopfzeilen einügen...
                var KopfzeileRechts = new System.Windows.Controls.TextBlock();
                var KopfzeileLinks = new System.Windows.Controls.TextBlock();


                KopfzeileRechts.FontFamily = new System.Windows.Media.FontFamily("Courier New");
                KopfzeileRechts.FontSize = 11.0;

                KopfzeileLinks.FontFamily = new System.Windows.Media.FontFamily("Courier New");
                KopfzeileLinks.FontSize = 11.0;


                System.Text.StringBuilder StringBauer = new StringBuilder();

                StringBauer.AppendLine(String.Concat("Kunde: ".Füllen(" ", 16), kunde.Nachname));
                StringBauer.AppendLine(String.Concat("Kundennummer: ".Füllen(" ", 16), kunde.KundenNummer.ToString()));
                StringBauer.AppendLine(String.Concat("Datum: ".Füllen(" ", 16), bestellung.BestellDatum));

                KopfzeileRechts.Text = StringBauer.ToString();
                KopfzeileLinks.Text = StringBauer.ToString();


                System.Windows.Documents.FixedPage.SetLeft(KopfzeileRechts, DatenRandSeite + pageWidth + KopfzeileRandSeite - (pageWidth));
                System.Windows.Documents.FixedPage.SetTop(KopfzeileRechts, DatenRandOben + KopfzeileRandOben);
                Seite.Children.Add(KopfzeileRechts);

                System.Windows.Documents.FixedPage.SetLeft(KopfzeileLinks, DatenRandSeite + pageWidth + KopfzeileRandSeite - (pageWidth / 2));
                System.Windows.Documents.FixedPage.SetTop(KopfzeileLinks, DatenRandOben + KopfzeileRandOben);
                Seite.Children.Add(KopfzeileLinks);

                //Layout Zeichnen...
                Seite.UpdateLayout();

                DatenRandOben = KopfzeileRechts.ActualHeight;

                //String Objekt initialisieren... 
                var LinieText = " ";

                //Textblöcke Erzeugen...
                var LinieRechts = new System.Windows.Controls.TextBlock();
                var LinieLinks = new System.Windows.Controls.TextBlock();

                LinieRechts.FontFamily = new System.Windows.Media.FontFamily("Courier New");
                LinieRechts.FontSize = 11;


                LinieLinks.FontFamily = LinieRechts.FontFamily;
                LinieLinks.FontSize = LinieRechts.FontSize;

                //StringBuilder neu Initialisieren und Das Rendern Anstoßen...
                StringBauer.Clear();
                StringBauer.Append(LinieText);
                Seite.UpdateLayout();

                //LinienRechst hinzufügen...
                System.Windows.Documents.FixedPage.SetLeft(LinieRechts, DatenRandSeite + pageWidth - (pageWidth / 2));
                System.Windows.Documents.FixedPage.SetTop(LinieRechts, DatenRandOben);
                Seite.Children.Add(LinieRechts);

                //Linien Links hinzufügen...
                System.Windows.Documents.FixedPage.SetLeft(LinieLinks, DatenRandSeite);
                System.Windows.Documents.FixedPage.SetTop(LinieLinks, DatenRandOben);
                Seite.Children.Add(LinieLinks);


                //Linie Erzeugen...
                while (LinieRechts.ActualWidth < (pageWidth - (pageWidth / 2) - (2 * DatenRandSeite)))
                {
                    StringBauer.Append("_");
                    LinieRechts.Text = StringBauer.ToString();
                    Seite.UpdateLayout();
                }

                LinieLinks.Text = LinieText.Füllen("_", LinieRechts.Text.Length);//StringBauer.ToString();

                DatenRandOben += LinieRechts.ActualHeight;



                //Überschrift erzeugen...
                var ÜberschriftLinks = new System.Windows.Controls.TextBlock();
                var ÜberschriftRechts = new System.Windows.Controls.TextBlock();

                //Stringbuilder cache leeren...
                StringBauer.Clear();           
                
                //Kopfzeile einfügen
                StringBauer.Append(String.Concat("Anzahl".Füllen(" ", 12), "BestNr.:".Füllen(" ", 30), "Titel, Autor".Füllen(" ", 86), "Preis".Füllen(" ", 15)));

                //Den Text den beiden Textblöcken übergeben...
                ÜberschriftLinks.Text = StringBauer.ToString();
                ÜberschriftRechts.Text = StringBauer.ToString();


                //Die Beiden Textblöcke dem Kontext hinzufügen und ausrichten...
                System.Windows.Documents.FixedPage.SetLeft(ÜberschriftLinks, DatenRandSeite + KopfzeileRandSeite);
                System.Windows.Documents.FixedPage.SetTop(ÜberschriftLinks, DatenRandOben);
                Seite.Children.Add(ÜberschriftLinks);

                System.Windows.Documents.FixedPage.SetLeft(ÜberschriftRechts, DatenRandSeite + KopfzeileRandSeite + pageWidth - (pageWidth / 2));
                System.Windows.Documents.FixedPage.SetTop(ÜberschriftRechts, DatenRandOben);
                Seite.Children.Add(ÜberschriftRechts);


                //Seitenlayout aktualisieren...
                Seite.UpdateLayout();

                //Den ZeilenIndex erhöhen...
                DatenRandOben += ÜberschriftRechts.ActualHeight;
                DatenRandOben += LinieRechts.ActualHeight;



                //Textfelder für die Bestelldaten erzeugen...
                var BestellpositionenLinks = new System.Windows.Controls.TextBlock();
                var BestellpositionenRechts = new System.Windows.Controls.TextBlock();

                //Textfelder einstellen...
                BestellpositionenLinks.FontFamily = new System.Windows.Media.FontFamily("Courier New");
                BestellpositionenLinks.FontSize = 11.0;

                BestellpositionenRechts.FontFamily = new System.Windows.Media.FontFamily("Courier New");
                BestellpositionenRechts.FontSize = 11.0;

                //Textfelder der Seite hinzufügen und positionieren...
                System.Windows.Documents.FixedPage.SetLeft(BestellpositionenLinks, DatenRandSeite + KopfzeileRandSeite);
                System.Windows.Documents.FixedPage.SetTop(BestellpositionenLinks, DatenRandOben);
                Seite.Children.Add(BestellpositionenLinks);

                System.Windows.Documents.FixedPage.SetLeft(BestellpositionenRechts, DatenRandSeite + KopfzeileRandSeite + pageWidth - (pageWidth / 2));
                System.Windows.Documents.FixedPage.SetTop(BestellpositionenRechts, DatenRandOben);
                Seite.Children.Add(BestellpositionenRechts);

                //Den Cache des String Builders leeren...
                StringBauer.Clear();              

                //Solange Platz ist werden Bestellpositionen hinzugefügt...
                while (DatenRandOben + LinieRechts.ActualHeight < pageHeight - (LinieRechts.ActualHeight * 2))
                {

                    //Beim erreichen des Listenendes abbrechen...
                    if (ListenIndex >= bücher.Count)
                    {
                        break;
                    }
                    else
                    { 

                        //Den Text für den Textblock erstellen...
                        BestellpositionenLinks.Text = StringBauer.AppendLine(String.Concat(" ", bücher[ListenIndex].AnzahlBestellt.ToString().Füllen(" ", 10),
                                                                        bücher[ListenIndex].Bestellnummer.ToString().Füllen(" ", 10),
                                                                        String.Concat(bücher[ListenIndex].Name, ", ", bücher[ListenIndex].Autor).Füllen(" ", 50),
                                                                        bücher[ListenIndex].Preis.ToString().Füllen(" ", 7))).ToString();

                    //Textblock Rechts füllen...
                    BestellpositionenRechts.Text = StringBauer.ToString();

                    //Layout updaten...
                    Seite.UpdateLayout();

                    //Abbruchbedingung aktualisieren...
                    DatenRandOben += LinieRechts.ActualHeight;
                    //Index erhöhen.
                    ListenIndex++;
                    }

                }
                    

            //Die erstellte Seite dem Seitenkontext hizufügen...
            ((System.Windows.Markup.IAddChild)SeitenKontext).AddChild(Seite);


                //Die Seite fertig erstellte Seite dem Dokument hizufügen hizufügen..
                Dokument.Pages.Add(SeitenKontext);
            }

            //Zurückgegen.
            return Dokument;
        }


        /// <summary>
        /// Erstellt ein Bestellungsdokument
        /// </summary>
        /// <param name="bücher">Bestellpositionen</param>
        /// <param name="gesamtbestllung">Datenstruktur mit ort und Datumsbereich</param>
        /// <param name="druckgröße">Verfügbare Druckgröße</param>
        /// <returns></returns>
        public System.Windows.Documents.FixedDocument GesamtBestellungErzeugen(DTOs.Bücher bücher, DTOs.Gesamtbestllung gesamtbestllung, System.Windows.Size druckgröße)
        {

            return this.GesamtBestellungErzeugen(bücher, gesamtbestllung.DatumVon, gesamtbestllung.DatumBis, druckgröße, gesamtbestllung.Veranstaltungsort);
        }

        /// <summary>
        /// Erstellt ein Bestellungsdokument
        /// </summary>
        /// <param name="bücher">Bestellpositionen</param>
        /// <param name="druckgröße">Verfügbare Druckgröße</param>
        /// <param name="datumBis">Datums Obergrenze</param>
        /// <param name="datumVon">Datums Untergrenze</param>
        /// <param name="ort">Ort der Veranstaltung</param>
        /// <returns>Das Bestellungsdokument.</returns>
        public System.Windows.Documents.FixedDocument GesamtBestellungErzeugen(DTOs.Bücher bücher,System.DateTime datumVon, System.DateTime datumBis, System.Windows.Size druckgröße, string ort)
        {

            //Ein neues Dokument erstellen...
            System.Windows.Documents.FixedDocument Dokument = new System.Windows.Documents.FixedDocument();

            //Die Dokumentgröße den Druckbereich übergeben...
            Dokument.DocumentPaginator.PageSize = druckgröße;


            double pageWidth = Dokument.DocumentPaginator.PageSize.Width;
            double pageHeight = Dokument.DocumentPaginator.PageSize.Height;



            //Listenindex initialisieren...
            int ListenIndex = 0;

            while (ListenIndex < bücher.Count)
            {
                //Den Seiten Kontext erzeugen...
                System.Windows.Documents.PageContent SeitenKontext = new System.Windows.Documents.PageContent();

                //Eine Seite erzeugen...
                System.Windows.Documents.FixedPage Seite = new System.Windows.Documents.FixedPage();

                //Ein neues Size Objekt erzeugen...
                System.Windows.Size sz = new System.Windows.Size(pageWidth, pageHeight);

                //Seitenmaße setzen...
                Seite.Measure(sz);

                //Seite Positionieren...
                Seite.Arrange(new System.Windows.Rect(new System.Windows.Point(), sz));

                //Hintergrund festlegen...
                Seite.Background = System.Windows.Media.Brushes.White;


                //Seitenmaße setzen...
                Seite.Width = pageWidth;
                Seite.Height = pageHeight;


                //Die Ränder der Gezeichneten Daten Setzen...
                double DatenRandSeite = 20;
                double DatenRandOben = 0.0;

                double KopfzeileRandSeite = 10;
                double KopfzeileRandOben = 20;


                //Kopfzeilen einügen...
                var Kopfzeile = new System.Windows.Controls.TextBlock();



                Kopfzeile.FontFamily = new System.Windows.Media.FontFamily("Courier New");
                Kopfzeile.FontSize = 12.0;




                System.Text.StringBuilder StringBauer = new StringBuilder();

                int FüllOffset = 0;

                //Leerzeile oben einfügen
                StringBauer.AppendLine(" ");
                //Veranstaltungsort einfügen falls dieser angegeben...
                if(ort != string.Empty)
                {
                    StringBauer.AppendLine(String.Concat("Veranstaltungsort: ".Füllen(" ",14), ort));
                    FüllOffset = 3;
                }

                //Bestellung Von Datum einfügen...
                StringBauer.AppendLine(String.Concat("Bestellung vom: ".Füllen(" ", 16 + FüllOffset), datumVon.ToShortDateString()));

                //Wenn sich das Bis Datum vom von Datum unterscheidet dieses einfügen...
                if (datumVon != datumBis)
                {
                    StringBauer.AppendLine(String.Concat("bis: ".Füllen(" ", 16 + FüllOffset), datumBis.ToShortDateString()));
                }

                //Text der Kopfzeile hinzufügen...
                Kopfzeile.Text = StringBauer.ToString();

                //Label einfügen...
                System.Windows.Documents.FixedPage.SetLeft(Kopfzeile, DatenRandSeite  + KopfzeileRandSeite );
                System.Windows.Documents.FixedPage.SetTop(Kopfzeile, DatenRandOben + KopfzeileRandOben);
                Seite.Children.Add(Kopfzeile);


                //Layout Zeichnen...
                Seite.UpdateLayout();

                DatenRandOben = Kopfzeile.ActualHeight;


                //String Objekt initialisieren... 
                var LinieText = " ";

                //Textblöcke Erzeugen...
                var Linie = new System.Windows.Controls.TextBlock();


                Linie.FontFamily = new System.Windows.Media.FontFamily("Courier New");
                Linie.FontSize = 12;


                //StringBuilder neu Initialisieren und Das Rendern Anstoßen...
                StringBauer.Clear();
                StringBauer.Append(LinieText);
                Seite.UpdateLayout();

                //LinienRechst hinzufügen...
                System.Windows.Documents.FixedPage.SetLeft(Linie, DatenRandSeite);
                System.Windows.Documents.FixedPage.SetTop(Linie, DatenRandOben);
                Seite.Children.Add(Linie);


                //Linie Erzeugen...
                while (Linie.ActualWidth < (pageWidth -  (2 * DatenRandSeite)))
                {
                    StringBauer.Append("_");
                    Linie.Text = StringBauer.ToString();
                    Seite.UpdateLayout();
                }

               

                DatenRandOben += Linie.ActualHeight;


                //Überschrift erzeugen...
                var Überschrift = new System.Windows.Controls.TextBlock();


                //Stringbuilder cache leeren...
                StringBauer.Clear();

                //Kopfzeile einfügen
                StringBauer.Append(String.Concat("Anzahl".Füllen(" ", 12), "BestNr.:".Füllen(" ", 28), "Titel, Autor".Füllen(" ", 45), "Verlag".Füllen(" ", 15)));

                //Den Text den beiden Textblöcken übergeben...
                Überschrift.Text = StringBauer.ToString();

                

                //Den Textblöck mit dem Kontext hinzufügen und ausrichten...
                System.Windows.Documents.FixedPage.SetLeft(Überschrift, DatenRandSeite + KopfzeileRandSeite);
                System.Windows.Documents.FixedPage.SetTop(Überschrift, DatenRandOben);
                Überschrift.FontFamily = new System.Windows.Media.FontFamily("Courier New");
                Überschrift.FontSize = 13.0;
                Seite.Children.Add(Überschrift);

                //Seitenlayout aktualisieren...
                Seite.UpdateLayout();

                //Den ZeilenIndex erhöhen...
                DatenRandOben += Überschrift.ActualHeight;
                DatenRandOben += Linie.ActualHeight;



                //Textfelder für die Bestelldaten erzeugen...
                var Bestellposition = new System.Windows.Controls.TextBlock();

                //Textfelder einstellen...
                Bestellposition.FontFamily = new System.Windows.Media.FontFamily("Courier New");
                Bestellposition.FontSize = 12.0;


                //Textfelder der Seite hinzufügen und positionieren...
                System.Windows.Documents.FixedPage.SetLeft(Bestellposition, DatenRandSeite + KopfzeileRandSeite);
                System.Windows.Documents.FixedPage.SetTop(Bestellposition, DatenRandOben);
                Seite.Children.Add(Bestellposition);


                //Den Cache des String Builders leeren...
                StringBauer.Clear();

                //Solange Platz ist werden Bestellpositionen hinzugefügt...
                while (DatenRandOben + Linie.ActualHeight < pageHeight - (Linie.ActualHeight * 2))
                {

                    //Beim erreichen des Listenendes abbrechen...
                    if (ListenIndex >= bücher.Count)
                    {
                        break;
                    }
                    else
                    {

                        //Den Text für den Textblock erstellen...
                        Bestellposition.Text = StringBauer.AppendLine(String.Concat(" ", bücher[ListenIndex].AnzahlBestellt.ToString().Füllen(" ", 13),
                                                                        bücher[ListenIndex].Bestellnummer.ToString().Füllen(" ", 23),
                                                                        String.Concat(bücher[ListenIndex].Name, ", ", bücher[ListenIndex].Autor).Füllen(" ",52),
                                                                        bücher[ListenIndex].Verlag.ToString().Füllen(" ", 7))).ToString();



                        //Layout updaten...
                        Seite.UpdateLayout();

                        //Abbruchbedingung aktualisieren...
                        DatenRandOben += Linie.ActualHeight;
                        //Index erhöhen.
                        ListenIndex++;
                    }

                }


            //Die erstellte Seite dem Seitenkontext hizufügen...
            ((System.Windows.Markup.IAddChild)SeitenKontext).AddChild(Seite);


                //Die Seite fertig erstellte Seite dem Dokument hizufügen hizufügen..
                Dokument.Pages.Add(SeitenKontext);
            }

            //Zurückgegen.
            return Dokument;
        }








    }
}
