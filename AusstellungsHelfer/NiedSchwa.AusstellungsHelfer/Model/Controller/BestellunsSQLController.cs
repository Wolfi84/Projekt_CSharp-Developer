using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.AusstellungsHelfer.Model.Controller
{
    /// <summary>
    /// Stellt den Dienst zum lesen und schreiben der Buchlisten
    /// und der Bestellung
    /// aus einer SQL Datenbank zur verfügung
    /// </summary>
    public class BestellunsSQLController : NiedSchwa.Anwendung.Daten.DatenbankObjekt
    {

        /// <summary>
        /// Ruft eine Liste mit Büchern ab 
        /// </summary>
        /// <param name="Bestellnummer">Bestellungsnummer des Auftrages</param>
        /// <returns>Buchliste des vorhandenen kunden.</returns>
        /// <remarks>Gibt eine leeere Buchliste zurück falls der kunde noch keine
        /// Bestellungen hat.</remarks>
        public DTOs.Bücher HohleBücher(int Bestellnummer)
        {

            DTOs.Bücher Ergebnis = new DTOs.Bücher();

            using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
            {

                //Befehl Initialisieren...
                using (var Befehl = new System.Data.SqlClient.SqlCommand("HoleBücherBestellung", Verbindung))
                {
                   
                    //Command Initialisieren...
                    Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                   
                    Befehl.Parameters.AddWithValue("@bestellnummer", Bestellnummer);

                    Befehl.Prepare();

                    Verbindung.Open();

                    //Daten Lesen...
                    using (var Leser = Befehl.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (Leser.Read())
                        {

                            Ergebnis.Add(new DTOs.Buch
                            {
                                Autor = Leser["Autor"].ToString()
                               ,
                                Bestellnummer = (int)Leser["buchnummer"]
                               ,
                                Name = Leser["Titel"].ToString()
                               ,
                                Preis = (float)Leser["Preis"]
                               ,
                               
                                Verlag = Leser["Verlag"].ToString()
                               ,
                                AnzahlBestellt = (int)Leser["anzahlBestellt"]
                               ,
                                AnzahlGeliefert = (int)Leser["anzahlGeliefert"],
                                BuchPreisgruppe = new DTOs.BuchPreisgruppe
                                {
                                    PreisgruppenIndex = (int)Leser["Id"]
                                    ,
                                    Preisgruppe  = Leser["buchgruppe"].ToString()
                                    ,
                                    Prozentsatz = (float)Leser["Prozent"]
                               
                                }
                            });
                        }
                    }
                }

            }


            return Ergebnis;
        }


        /// <summary>
        /// Sucht nach einem Buch mit der übergebenen Buchnummer
        /// </summary>
        /// <param name="buchnummer"></param>
        /// <returns>Das gesuchete Buch</returns>
        /// <remarks>Gibt ein leeres Buch zurück falls das gesuchte nicht gespeichert ist.</remarks>
        public DTOs.Buch HoleBuch(int buchnummer)
        {
            var Ergebnis = new DTOs.Buch();

            //Verbindung aufbauen
            using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
            {

                //Befehl erzeugen
                using (var Befehl = new System.Data.SqlClient.SqlCommand("HoleBuch", Verbindung))
                {
                    //Befehlstyp festlegen
                    Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                    //Parameter übergabe
                    Befehl.Parameters.AddWithValue("@buchnummer", buchnummer);

                    //Befehl Initialiseren
                    Befehl.Prepare();

                    //Verbindung öffnen
                    Verbindung.Open();

                    //Daten lesen
                    using (var Leser = Befehl.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {

                        while (Leser.Read())
                        {

                            Ergebnis.Autor = Leser["Autor"].ToString();

                            Ergebnis.Bestellnummer = (int)Leser["buchnummer"];

                            Ergebnis.Name = Leser["Titel"].ToString();

                            Ergebnis.Preis = (float)Leser["Preis"];

                            Ergebnis.Verlag = Leser["Verlag"].ToString();

                            Ergebnis.BuchPreisgruppe = new DTOs.BuchPreisgruppe
                            {
                                PreisgruppenIndex = (int)Leser["Id"],
                                Prozentsatz = (float)Leser["Prozent"],
                                Preisgruppe = Leser["buchgruppe"].ToString()

                            };

                        }
                    }
                }

            }
            return Ergebnis;
        }



        /// <summary>
        /// Ruft alle Bestellungen die für einen Kunden gespeichert sind ab.
        /// </summary>
        /// <param name="kundennummer">Kundendaten</param>
        /// <returns>Bestellungen des kunden.</returns>
        /// <remarks>Gibt eine leere Bestellliste zurück falls noch keine für den kunden gespeichert sind.</remarks>
        public DTOs.Bestellungen HohleBestellungen(int kundennummer)
        {
            DTOs.Bestellungen Ergebnis = new DTOs.Bestellungen();

            //Verbindung aufbauen
            using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
            {

                //Befehl erzeugen
                using (var Befehl = new System.Data.SqlClient.SqlCommand("HoleKundenBestellungen", Verbindung))
                {
                    //Befehlstyp festlegen
                    Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                    //Parameter übergabe
                    Befehl.Parameters.AddWithValue("@kundennummer", kundennummer);

                    //Befehl Initialiseren
                    Befehl.Prepare();

                    //Verbindung öffnen
                    Verbindung.Open();

                    //Daten lesen
                    using (var Leser = Befehl.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        int i = 0;
                        while (Leser.Read())
                        {
                            DTOs.BestellStatus Status;
                            Ergebnis.Add(new DTOs.Bestellung
                            {
                                Auftragsnummer = (int)Leser["Bestellnummer"]


                            });


                            //Datum für die View in String konvertieren 
                            System.DateTime Datum = (System.DateTime)Leser["Datum"];

                            Ergebnis[i].BestellDatum = Datum;//.ToShortDateString();

                            //Enum für die View in String Konvertieren
                            if (Enum.TryParse<DTOs.BestellStatus>(Leser["Bestellstatus"].ToString(), true, out Status))
                            {
                                Ergebnis[i].BestellStatus = Status;
                            }

                            i++;
                        }
                    }
                }

            }


            return Ergebnis;
        }

        /// <summary>
        /// Speichert eine Bestellung
        /// </summary>
        /// <param name="bestellung">Die zu speichernde Bestellung</param>
        /// <param name="kundennummer">Die Kundennummer für die Bestellung</param>
        /// <returns>True wenn Speichern erfolgreich ist.</returns>
        public bool NeueBestellungSpeichern(DTOs.Bestellung bestellung, int kundennummer)
        {

            int Ergebnis = 0;

            using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
            {

                //Befehl Vorbereiten
                using (var Befehl = new System.Data.SqlClient.SqlCommand("NeueBestellungSpeichern", Verbindung))
                {
                    //Befehlstyp festlegen
                    Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                    //Die Parameter übergeben                 
                    Befehl.Parameters.AddWithValue("@kundennummer", kundennummer);
                    Befehl.Parameters.AddWithValue("@datum", bestellung.BestellDatum);
                    Befehl.Parameters.AddWithValue("@bestellstatus", bestellung.BestellStatus.ToString());

                    Befehl.Prepare();

                    Verbindung.Open();


                    Ergebnis = Befehl.ExecuteNonQuery();

                }

            }

            return Ergebnis != 0;
        }

        /// <summary>
        /// Speichert die Bücher einer neuen Bestellung in der Datenbank.
        /// </summary>
        /// <param name="bücher"> Buchliste der Bestellung</param>
        /// <param name="kundennummer">Kundennummer</param>
        /// <param name="bestellnummer">Bestellnummer falls diese nicht in der Datenbank erstellt wird.</param>
        /// <returns>True wenn Speichern erfolgreich ist.</returns>
        /// <remarks>Derzeit wird die Bestellnummer beim Speichern in der Datenbank selbst generiert. 
        /// Dies könnte auch zu einem Späteren Zeitpunkt von der Applikation durchgefürt werden.</remarks>
        public bool BuchpositionenSpeichern(DTOs.Bücher bücher, int kundennummer, int bestellnummer)
        {
            int Ergebnis = 0;

            using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
            {

                //Befehl Vorbereiten
                using (var Befehl = new System.Data.SqlClient.SqlCommand("NeueBestellPositionSpeichern", Verbindung))
                {
                    //Befehlstyp festlegen
                    Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                    //Die Parameter übergeben                 
                    Befehl.Parameters.Add("@buchnummer", System.Data.SqlDbType.Int);
                    Befehl.Parameters.Add("@bestellnummer", System.Data.SqlDbType.Int);
                    Befehl.Parameters.Add("@anzahlBestellt", System.Data.SqlDbType.Int);
                    Befehl.Parameters.Add("@anzahlGeliefert", System.Data.SqlDbType.Int);

                    Befehl.Prepare();

                    Verbindung.Open();

                    Befehl.Transaction = Verbindung.BeginTransaction();

                    foreach (var buch in bücher)
                    {
                        Befehl.Parameters["@buchnummer"].Value = buch.Bestellnummer;
                        Befehl.Parameters["@bestellnummer"].Value = bestellnummer;
                        Befehl.Parameters["@anzahlBestellt"].Value = buch.AnzahlBestellt;
                        Befehl.Parameters["@anzahlGeliefert"].Value = buch.AnzahlGeliefert;

                        Ergebnis += Befehl.ExecuteNonQuery();
                    }


                    Befehl.Transaction.Commit();

                }

            }

            return Ergebnis != 0;
        }

        /// <summary>
        /// Ruft eine Kundenliste ab
        /// </summary>
        /// <param name="nachname">Nachname des Kunden</param>
        /// <param name="vorname">Vorname des Kunden</param>
        /// <returns>Eine Kundenliste welche mit dem nachnamen übereinstimmt.</returns>
        public DTOs.Kunden HohleKunden(string nachname, string vorname)
        {
            var Ergebnis = new DTOs.Kunden();

            if (nachname == null)
            {
                nachname = string.Empty;
            }
            if (vorname == null)
            {
                vorname = string.Empty;
            }


            //Verbindung aufbauen...
            using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
            {

                //Command Initialisieren...
                using (var Befehl = new System.Data.SqlClient.SqlCommand("KundeSuchen", Verbindung))
                {
                    
                    Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                   
                    Befehl.Parameters.AddWithValue("@vorname", vorname);
                    Befehl.Parameters.AddWithValue("@nachname", nachname);

                    Befehl.Prepare();

                    Verbindung.Open();

                    //Daten lesen...
                    using (var Leser = Befehl.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (Leser.Read())
                        {

                            Ergebnis.Add(new DTOs.Kunde
                            {
                                Vorname = Leser["Vorname"].ToString()
                                ,
                                Nachname = Leser["Nachname"].ToString()
                                ,
                                KundenNummer = (int)Leser["Kundennummer"]
                                ,
                                Straße = Leser["Straße"].ToString()
                                ,
                                HausNummer = Leser["Nummer"].ToString()
                                ,
                                PLZ = (int)Leser["Postleitzahl"]
                                ,
                                Stadt = Leser["Ort"].ToString()
                                ,
                                TelNummer = Leser["Telefonnummer"].ToString()
                                ,
                                E_Mail = Leser["E-Mail"].ToString()
                            });
                        }
                    }
                }

            }


            return Ergebnis;
        }


        /// <summary>
        /// Ruft eine Kundenliste ab und gibt den letzten erstellten Kunden
        /// zurück.
        /// </summary>
        /// <param name="nachname">Nachname des Kunden</param>
        /// <param name="vorname">Vorname des Kunden</param>
        /// <returns>Kundneliste</returns>
        public DTOs.Kunden NeuKundeSuchen(string nachname, string vorname)
        {
            var Ergebnis = new DTOs.Kunden();

            if (nachname == null)
            {
                nachname = string.Empty;
            }
            if (vorname == null)
            {
                vorname = string.Empty;
            }


            //Verbindung aufbauen...
            using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
            {

                //Command Initialisieren...
                using (var Befehl = new System.Data.SqlClient.SqlCommand("NeuKundenSuche", Verbindung))
                {
                    
                    Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                    
                    Befehl.Parameters.AddWithValue("@vorname", vorname);
                    Befehl.Parameters.AddWithValue("@nachname", nachname);

                    Befehl.Prepare();

                    Verbindung.Open();

                    //Daten holen...
                    using (var Leser = Befehl.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (Leser.Read())
                        {

                            Ergebnis.Add(new DTOs.Kunde
                            {
                                Vorname = Leser["Vorname"].ToString()
                                ,
                                Nachname = Leser["Nachname"].ToString()
                                ,
                                KundenNummer = (int)Leser["Kundennummer"]
                                ,
                                Straße = Leser["Straße"].ToString()
                                ,
                                HausNummer = Leser["Nummer"].ToString()
                                ,
                                PLZ = (int)Leser["Postleitzahl"]
                                ,
                                Stadt = Leser["Ort"].ToString()
                                ,
                                TelNummer = Leser["Telefonnummer"].ToString()
                                ,
                                E_Mail = Leser["E-Mail"].ToString()
                            });
                        }
                    }
                }

            }


            return Ergebnis;
        }


        /// <summary>
        /// Ruft eine Kundenliste die zu der übergebenen Kundennummer passen.
        /// </summary>
        /// <param name="kundennummer">kundennummer des Kunden</param>
        /// <returns>Kundenliste</returns>
        public DTOs.Kunden HohleKunden(int kundennummer)
        {
            var Ergebnis = new DTOs.Kunden();


            //Verbindung Initialisieren...
            using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
            {

                //Command Initialisieren...
                using (var Befehl = new System.Data.SqlClient.SqlCommand("KundeSuchenNachNummer", Verbindung))
                {
                    
                    Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                    
                    Befehl.Parameters.AddWithValue("@kundennummer", kundennummer);

                    Befehl.Prepare();

                    Verbindung.Open();


                    using (var Leser = Befehl.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {
                        while (Leser.Read())
                        {

                            Ergebnis.Add(new DTOs.Kunde
                            {
                                Vorname = Leser["Vorname"].ToString()
                                ,
                                Nachname = Leser["Nachname"].ToString()
                                ,
                                KundenNummer = (int)Leser["Kundennummer"]
                                ,
                                Straße = Leser["Straße"].ToString()
                                ,
                                HausNummer = Leser["Nummer"].ToString()
                                ,
                                PLZ = (int)Leser["Postleitzahl"]
                                ,
                                Stadt = Leser["Ort"].ToString()
                                ,
                                TelNummer = Leser["Telefonnummer"].ToString()
                                ,
                                E_Mail = Leser["E-Mail"].ToString()
                            });
                        }
                    }
                }

            }


            return Ergebnis;
        }


        /// <summary>
        /// Speichert einen Neuen Kunden in die Datenbank.
        /// </summary>
        /// <param name="kunde">Der zu Speichernde Kunde</param>
        /// <returns>True wenn erfolgreich.</returns>
        public bool NeuenKundenAnlegen(DTOs.Kunde kunde)
        {
            var Ergebnis = 0;

            using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
            {

                //Befehl Vorbereiten
                using (var Befehl = new System.Data.SqlClient.SqlCommand("KundeHinzufügen", Verbindung))
                {
                    //Befehlstyp festlegen
                    Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                    //Die Parameter übergeben
                    //  Befehl.Parameters.AddWithValue("@kundennummer", kunde.KundenNummer);

                    Befehl.Parameters.AddWithValue("@vorname", kunde.Vorname);

                    //Befehl.Parameters.Add("@vorname",System.Data.SqlDbType.NVarChar);
                    //Befehl.Parameters["@vorname"].Value = kunde.Vorname;

                    Befehl.Parameters.AddWithValue("@nachname", kunde.Nachname);
                    Befehl.Parameters.AddWithValue("@postleitzahl", kunde.PLZ.Value);
                    Befehl.Parameters.AddWithValue("@ort", kunde.Stadt);
                    Befehl.Parameters.AddWithValue("@straße", kunde.Straße);
                    Befehl.Parameters.AddWithValue("@nummer", kunde.HausNummer);
                    Befehl.Parameters.AddWithValue("@telefonnummer", kunde.TelNummer);

                    if (kunde.E_Mail != null && kunde.E_Mail != string.Empty)
                    {
                        Befehl.Parameters.AddWithValue("@e_mail", kunde.E_Mail);
                    }
                    else
                    {
                        Befehl.Parameters.AddWithValue("e_mail", string.Empty);
                    }

                    Befehl.Prepare();

                    Verbindung.Open();

                    Ergebnis = Befehl.ExecuteNonQuery();

                }

            }

            return (Ergebnis != 0);
        }


        /// <summary>
        /// Aktualisert die Bestellte Buchposition.
        /// </summary>
        /// <param name="buch">Buch welches aktualisiert werden soll.</param>
        /// <param name="bestellung">Bestellungsdetails zu diesem Buch</param>
        /// <returns>True wenn Aktualisieren erfolgreich war.</returns>
        public bool AktualisiereBuchposition(DTOs.Buch buch, DTOs.Bestellung bestellung)
        {
            var Ergebnis = 0;

            using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
            {

                //Befehl Vorbereiten
                using (var Befehl = new System.Data.SqlClient.SqlCommand("AktualisiereBestellPosition", Verbindung))
                {
                    //Befehlstyp festlegen
                    Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                    //Die Parameter übergeben  
                    Befehl.Parameters.AddWithValue("@buchnummer", buch.Bestellnummer);
                    Befehl.Parameters.AddWithValue("@bestellnummer", bestellung.Auftragsnummer);
                    Befehl.Parameters.AddWithValue("@AnzahlGelieferteBücher", buch.AnzahlGeliefert);
                    Befehl.Parameters.AddWithValue("@AnzahlBestellteBücher", buch.AnzahlBestellt);

                    Befehl.Prepare();

                    Verbindung.Open();

                    Ergebnis = Befehl.ExecuteNonQuery();

                }

            }

            return Ergebnis != 0;
        }






        /// <summary>
        /// Ruft die gesamten Bestellungen für 
        /// einen bestimmten Zeitraum aus der Datenbank ab
        /// </summary>
        /// <param name="DateFrom">Datumsbereich Anfang</param>
        /// <param name="DateTo">Datumsbereich Ende.</param>
        /// <returns>Bestellliste</returns>
        /// <remarks>Gibt eine leere Buchliste zurück wenn keine Bücher im gesuchten Zeitraum 
        /// gefunden werden.</remarks>
        public Model.DTOs.Bücher HoleGesamtBuchListe(DateTime DateFrom, DateTime DateTo)
        {
            var Ergebnis = new Model.DTOs.Bücher();

            using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
            using (var Befehl = new System.Data.SqlClient.SqlCommand("BücherSuchenNachBestelldatum", Verbindung))

            {

               //DateTime? DateFrom = DateFrom.Value.ToShortDateString();

                Befehl.CommandType = System.Data.CommandType.StoredProcedure;
                Befehl.Parameters.AddWithValue("@datefrom",DateFrom.Date);
                Befehl.Parameters.AddWithValue("@dateto", DateTo.Date);

                

                Befehl.Prepare();
                Verbindung.Open();

                using (var Leser = Befehl.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    while (Leser.Read())
                    {

                        //Neues Buch Erstellen...
                        var Buch = new Model.DTOs.Buch
                        {
                            Autor = Leser["Autor"].ToString()
                            ,
                            Verlag = Leser["Verlag"].ToString()
                            ,
                            //    Preisgruppe = Leser["Preisgruppe"].ToString()
                            //    ,
                            //   Preis = (float)Leser["Preis"]
                            //    ,
                            Bestellnummer = (int)Leser["Bestellnummer"]
                            ,
                            AnzahlBestellt = (int)Leser["AnzahlBestellt"]
                            ,
                            Name = Leser["Titel"].ToString()
                           // ,
                           // Prozentsatz = (float)Leser["Prozentsatz"]

                        };


                        //Im ergebnis nach dem Gerade erstelltem Buch suchen...
                        DTOs.Buch GesuchtesBuch = (from b in Ergebnis
                                                   where
                                                   b.Bestellnummer == Buch.Bestellnummer
                                                   select b).FirstOrDefault();

                        //Wenn Das Buch enthalten ist die Anzahl Bestellt aktualisieren,
                        //Sonst neues Buch der Liste hinzufügen.
                        if(GesuchtesBuch != null && GesuchtesBuch.Bestellnummer == Buch.Bestellnummer)
                        {
                            Ergebnis[Ergebnis.IndexOf(GesuchtesBuch)].AnzahlBestellt += Buch.AnzahlBestellt;
                        }
                        else
                        {
                            Ergebnis.Add(Buch);
                        }

                    }
                }

                    return Ergebnis;
            }
        }



        /// <summary>
        /// Sucht die Lister der Verfügbaren Bücher
        /// </summary>
        /// <returns>Die gespeicherten Bücher</returns>
        public DTOs.Bücher HoleBücher()
        {
            var Ergebnis = new DTOs.Bücher();

            //Verbindung aufbauen
            using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
            {

                //Befehl erzeugen
                using (var Befehl = new System.Data.SqlClient.SqlCommand("HoleBücher", Verbindung))
                {
                    //Befehlstyp festlegen
                    Befehl.CommandType = System.Data.CommandType.StoredProcedure;


                    //Befehl Initialiseren
                    Befehl.Prepare();

                    //Verbindung öffnen
                    Verbindung.Open();

                    //Daten lesen
                    using (var Leser = Befehl.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {

                        while (Leser.Read())
                        {
                            Ergebnis.Add(new DTOs.Buch {Autor           = Leser["Autor"].ToString(),
                                                        Bestellnummer   = (int)Leser["buchnummer"],
                                                        Name            = Leser["Titel"].ToString(),
                                                        Preis           = (float)Leser["Preis"],                                                       
                                                        Verlag          = Leser["Verlag"].ToString(),
                                                        BuchPreisgruppe = new DTOs.BuchPreisgruppe
                                                        {
                                                            Preisgruppe = Leser["buchgruppe"].ToString(),
                                                            Prozentsatz = (float)Leser["Prozent"],
                                                            PreisgruppenIndex = (int)Leser["Id"]
                                                        }
                            });

                        }
                    }
                }

            }
            return Ergebnis;
        }


        /// <summary>
        /// Holt die Aktuell Verfügbaren Buchpreisgruppen 
        /// aus der Datenbank.
        /// </summary>
        /// <returns></returns>
        public Model.DTOs.BuchPreisgruppen HoleBuchPreisgruppen()
        {
            var Ergebnis = new DTOs.BuchPreisgruppen();

            //Verbindung aufbauen
            using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
            {

                //Befehl erzeugen
                using (var Befehl = new System.Data.SqlClient.SqlCommand("HolePreisgruppen", Verbindung))
                {
                    //Befehlstyp festlegen
                    Befehl.CommandType = System.Data.CommandType.StoredProcedure;


                    //Befehl Initialiseren
                    Befehl.Prepare();

                    //Verbindung öffnen
                    Verbindung.Open();

                    //Daten lesen
                    using (var Leser = Befehl.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                    {

                        while (Leser.Read())
                        {
                            Ergebnis.Add(new DTOs.BuchPreisgruppe
                            {
                                Preisgruppe = Leser["Bezeichnung"].ToString(),
                                PreisgruppenIndex = (int)Leser["Id"],
                                Prozentsatz = (float)Leser["Prozentsatz"]


                            });

                        }
                    }
                }

                return Ergebnis;
            }

        }


        /// <summary>
        /// Speichert das Übergebene Buch in die Datenbank.
        /// </summary>
        /// <param name="buch">Buchdaten</param>
        /// <returns>True wenn erfolfgreich gespeichert wurde.</returns>
        public bool BuchSpeichern(DTOs.Buch buch)
        {
            int Ergebnis = 0;

            using (var Verbindung = new System.Data.SqlClient.SqlConnection(this.ConnectionString))
            {

                //Befehl Erstellen...
                using (var Befehl = new System.Data.SqlClient.SqlCommand("BuchSpeichern", Verbindung))
                {
                    //Befehlstyp festlegen...
                    Befehl.CommandType = System.Data.CommandType.StoredProcedure;

                    //Die Parameter übergeben...                
                    Befehl.Parameters.AddWithValue("@buchnummer", buch.Bestellnummer.Value);
                    Befehl.Parameters.AddWithValue("@titel", buch.Name);
                    Befehl.Parameters.AddWithValue("@Autor", buch.Autor);
                    Befehl.Parameters.AddWithValue("@preisgruppe", buch.BuchPreisgruppe.PreisgruppenIndex);
                    Befehl.Parameters.AddWithValue("@verlag", buch.Verlag);
                    Befehl.Parameters.AddWithValue("@preis", buch.Preis);

                    //Befehl vorbereiten...
                    Befehl.Prepare();

                    //Verbindung öffnen...
                    Verbindung.Open();

                    //Ausführen...
                    Ergebnis = Befehl.ExecuteNonQuery();


                }

            }

            return (Ergebnis != 0);
        }
    }
}

