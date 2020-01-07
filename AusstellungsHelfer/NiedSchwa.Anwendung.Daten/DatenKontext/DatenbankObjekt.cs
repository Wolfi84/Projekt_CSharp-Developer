using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NiedSchwa.Anwendung.Daten
{
    /// <summary>
    /// Stellt die Grundlage bereit für Objekte,
    /// die einen SQL Server Datenbankzugriff benötigen.
    /// </summary>
    public abstract class DatenbankObjekt : DatenAnwendungsobjekt
    {

        /// <summary>
        /// Internes Feld für die gecachte Eigenschaft.
        /// </summary>
        private static string _ConnectionString = null;

        /// <summary>
        /// Ruft die Verbindungszeichenfolge für
        /// den in der Infratstruktur eingestellten
        /// SQL Server und Datenbank ab.
        /// </summary>
        protected string ConnectionString
        {
            get
            {
                if (DatenbankObjekt._ConnectionString == null)
                {

                    var ConnectionBauer = new System.Data.SqlClient.SqlConnectionStringBuilder();

                    ConnectionBauer.DataSource = this.AppKontext.SqlServer;

                    //Falls ein Datenbank-Pfad vorhanden ist,
                    //wird davon ausgegangen, dass es sich
                    //um eine dynamisch angehängte Datenbank handelt
                    if (this.AppKontext.DatenbankPfad != string.Empty)
                    {
                        ConnectionBauer.AttachDBFilename
                            = System.IO.Path.Combine(
                                this.AppKontext.DatenbankPfad,
                                $"{this.AppKontext.SqlServerDatenbank}.mdf");

                    }
                    else
                    {
                        //Ohne Datenbank-Pfad wird davon
                        //ausgegangen, dass die Datenbank 
                        //fix am Sql Server angehängt ist
                        ConnectionBauer.InitialCatalog = this.AppKontext.SqlServerDatenbank;



                        //Innerhalb einer Firma UNBEDINGT MIT
                        //der integrierten Sicherheit arbeiten,
                        //d.h. mit Benutzern vom Active Directory (AD)
                        ConnectionBauer.IntegratedSecurity = true;

                    }

                    DatenbankObjekt._ConnectionString = ConnectionBauer.ConnectionString;

                    this.AppKontext.Protokoll.Eintragen(
                        $"Die Anwendung hat den Connectionstring\r\n\t{DatenbankObjekt._ConnectionString}\r\nberechnet und gecachet...");
                }

                return DatenbankObjekt._ConnectionString;
            }
        }
    }
}
