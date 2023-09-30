using Municipale.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Municipale
{
    public class Db
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["db"].ConnectionString.ToString();
        private static readonly SqlConnection conn = new SqlConnection(connectionString);

        public static List<Anagrafica> getAnagrafiche() 
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM Anagrafica", conn);
                SqlDataReader sqlDataReader;
                conn.Open();
                List<Anagrafica> Anagrafiche = new List<Anagrafica>();
                sqlDataReader = cmd.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    Anagrafica newAnagrafica = new Anagrafica(
                        Convert.ToInt32(sqlDataReader["IDAnagrafica"]),
                        sqlDataReader["Nome"].ToString(),
                        sqlDataReader["Cognome"].ToString(),
                        sqlDataReader["Indirizzo"].ToString(),
                        sqlDataReader["Citta"].ToString(),
                        sqlDataReader["CAP"].ToString(),
                        sqlDataReader["Cod_Fisc"].ToString()
                        );

                    Anagrafiche.Add(newAnagrafica);

                }

                return Anagrafiche;
            }
            catch (Exception)
            {
                return new List<Anagrafica>();
            }
            finally 
            { 
                conn.Close(); 
            }
        }

        public static void addAnagrafica(Anagrafica anagrafica) 
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
            "INSERT INTO Anagrafica (Nome, Cognome, Indirizzo, Citta, CAP, Cod_Fisc) " +
            "VALUES (@Nome, @Cognome, @Indirizzo, @Citta, @CAP, @Cod_Fisc)", conn);

                cmd.Parameters.AddWithValue("@Nome", anagrafica.Nome);
                cmd.Parameters.AddWithValue("@Cognome", anagrafica.Cognome);
                cmd.Parameters.AddWithValue("@Indirizzo", anagrafica.Indirizzo);
                cmd.Parameters.AddWithValue("@Citta", anagrafica.Citta);
                cmd.Parameters.AddWithValue("@CAP", anagrafica.CAP);
                cmd.Parameters.AddWithValue("@Cod_Fisc", anagrafica.Cod_Fisc);
                

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            finally
            {
                conn.Close();
            }




        }
        
        
        
        
        
        
        public static List<Verbale> GetVerbale() 
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT v.*, a.Cognome AS AnagraficaCognome, a.Nome AS AnagraficaNome, tv.Descrizione AS Violazione FROM Verbali as v " +
                    "INNER JOIN Anagrafica AS a ON v.IDAnagrafica = a.IDAnagrafica INNER JOIN TipoViolazione AS tv ON v.IDViolazione = tv.IdViolazione", conn);
                SqlDataReader sqlDataReader;
                conn.Open();
                List<Verbale> verbali = new List<Verbale>();
                sqlDataReader = cmd.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    Verbale newVerbale = new Verbale(
                        Convert.ToInt32(sqlDataReader["IDVerbali"]),
                        Convert.ToDateTime(sqlDataReader["DataViolazione"]),
                        sqlDataReader["IndirizzoViolazione"].ToString(),
                        sqlDataReader["Nominativo_Agente"].ToString(),
                        Convert.ToDateTime(sqlDataReader["DataTrascrizioneVerbale"]),
                        Convert.ToDecimal(sqlDataReader["Importo"]),
                        Convert.ToInt32(sqlDataReader["DecrementoPunti"]),
                        Convert.ToInt32(sqlDataReader["IDAnagrafica"]),
                        Convert.ToInt32(sqlDataReader["IDViolazione"])     
                        );

                    newVerbale.NomeAnagrafica = sqlDataReader["AnagraficaNome"].ToString();
                    newVerbale.CognomeAnagrafica = sqlDataReader["AnagraficaCognome"].ToString();
                    newVerbale.TipoViolazione = sqlDataReader["Violazione"].ToString();

                    verbali.Add(newVerbale);

                }

                return verbali;
            }
            catch (Exception)
            {
                return new List<Verbale>();
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Verbale> GetVerbaleImporto()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT v.*, a.Cognome AS AnagraficaCognome, a.Nome AS AnagraficaNome, tv.Descrizione AS Violazione FROM Verbali as v " +
                    "INNER JOIN Anagrafica AS a ON v.IDAnagrafica = a.IDAnagrafica INNER JOIN TipoViolazione AS tv ON v.IDViolazione = tv.IdViolazione WHERE Importo>400", conn);
                SqlDataReader sqlDataReader;
                conn.Open();
                List<Verbale> verbali = new List<Verbale>();
                sqlDataReader = cmd.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    Verbale newVerbale = new Verbale(
                        Convert.ToInt32(sqlDataReader["IDVerbali"]),
                        Convert.ToDateTime(sqlDataReader["DataViolazione"]),
                        sqlDataReader["IndirizzoViolazione"].ToString(),
                        sqlDataReader["Nominativo_Agente"].ToString(),
                        Convert.ToDateTime(sqlDataReader["DataTrascrizioneVerbale"]),
                        Convert.ToDecimal(sqlDataReader["Importo"]),
                        Convert.ToInt32(sqlDataReader["DecrementoPunti"]),
                        Convert.ToInt32(sqlDataReader["IDAnagrafica"]),
                        Convert.ToInt32(sqlDataReader["IDViolazione"])
                        );

                    newVerbale.NomeAnagrafica = sqlDataReader["AnagraficaNome"].ToString();
                    newVerbale.CognomeAnagrafica = sqlDataReader["AnagraficaCognome"].ToString();
                    newVerbale.TipoViolazione = sqlDataReader["Violazione"].ToString();

                    verbali.Add(newVerbale);

                }

                return verbali;
            }
            catch (Exception)
            {
                return new List<Verbale>();
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Anagrafica> GetVerbaleAnagrafica()
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT A.Nome AS Nome, A.Cognome AS Cognome, COUNT(V.IDAnagrafica) AS TotaleVerbali FROM Anagrafica AS A INNER JOIN Verbali AS V ON A.IDAnagrafica=V.IDAnagrafica GROUP BY A.Nome, A.Cognome ", conn);
                SqlDataReader sqlDataReader;
                conn.Open();
                List<Anagrafica> Anagrafiche = new List<Anagrafica>();
                sqlDataReader = cmd.ExecuteReader();

                while (sqlDataReader.Read())
                {
                        Anagrafica newAnagrafica = new Anagrafica();

                    newAnagrafica.Nome = sqlDataReader["Nome"].ToString();
                    newAnagrafica.Cognome= sqlDataReader["Cognome"].ToString();

                    newAnagrafica.totVerbali = Convert.ToInt32(sqlDataReader["TotaleVerbali"]);



                    Anagrafiche.Add(newAnagrafica);

                }

                return Anagrafiche;
            }
            catch (Exception)
            {
                return new List<Anagrafica>();
            }
            finally
            {
                conn.Close();
            }
        }

        public static List<Anagrafica> GetPuntiAnagrafica() 
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT A.Nome AS Nome, A.Cognome AS Cognome, SUM(V.DecrementoPunti) AS TotalePunti FROM Anagrafica AS A INNER JOIN Verbali AS V ON A.IDAnagrafica=V.IDAnagrafica GROUP BY A.Nome, A.Cognome ", conn);
                SqlDataReader sqlDataReader;
                conn.Open();
                List<Anagrafica> Anagrafiche = new List<Anagrafica>();
                sqlDataReader = cmd.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    Anagrafica newAnagrafica = new Anagrafica();

                    newAnagrafica.Nome = sqlDataReader["Nome"].ToString();
                    newAnagrafica.Cognome = sqlDataReader["Cognome"].ToString();

                    newAnagrafica.totPuntiDecurtati = Convert.ToInt32(sqlDataReader["TotalePunti"]);



                    Anagrafiche.Add(newAnagrafica);

                }

                return Anagrafiche;
            }
            catch (Exception)
            {
                return new List<Anagrafica>();
            }
            finally
            {
                conn.Close();
            }



        }

        public static List<Verbale> GetVerbaliPunti10() 
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT v.*, a.Cognome AS AnagraficaCognome, a.Nome AS AnagraficaNome, tv.Descrizione AS Violazione FROM Verbali as v " +
                    "INNER JOIN Anagrafica AS a ON v.IDAnagrafica = a.IDAnagrafica INNER JOIN TipoViolazione AS tv ON v.IDViolazione = tv.IdViolazione WHERE v.DecrementoPunti>10", conn);
                SqlDataReader sqlDataReader;
                conn.Open();
                List<Verbale> verbali = new List<Verbale>();
                sqlDataReader = cmd.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    Verbale newVerbale = new Verbale(
                        Convert.ToInt32(sqlDataReader["IDVerbali"]),
                        Convert.ToDateTime(sqlDataReader["DataViolazione"]),
                        sqlDataReader["IndirizzoViolazione"].ToString(),
                        sqlDataReader["Nominativo_Agente"].ToString(),
                        Convert.ToDateTime(sqlDataReader["DataTrascrizioneVerbale"]),
                        Convert.ToDecimal(sqlDataReader["Importo"]),
                        Convert.ToInt32(sqlDataReader["DecrementoPunti"]),
                        Convert.ToInt32(sqlDataReader["IDAnagrafica"]),
                        Convert.ToInt32(sqlDataReader["IDViolazione"])
                        );

                    newVerbale.NomeAnagrafica = sqlDataReader["AnagraficaNome"].ToString();
                    newVerbale.CognomeAnagrafica = sqlDataReader["AnagraficaCognome"].ToString();
                    newVerbale.TipoViolazione = sqlDataReader["Violazione"].ToString();

                    verbali.Add(newVerbale);

                }

                return verbali;
            }
            catch (Exception)
            {
                return new List<Verbale>();
            }
            finally
            {
                conn.Close();
            }





        }

        public static void addVerbale (Verbale verbale) 
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                "INSERT INTO Verbali (DataViolazione, IndirizzoViolazione, Nominativo_Agente, DataTrascrizioneVerbale, Importo, DecrementoPunti, IDAnagrafica, IDViolazione) " +
                "VALUES (@DataViolazione, @IndirizzoViolazione, @Nominativo_Agente, @DataTrascrizioneVerbale, @Importo, @DecrementoPunti, @IDAnagrafica, @IDViolazione)", conn);

                cmd.Parameters.AddWithValue("@DataViolazione", verbale.DataViolazione);
                cmd.Parameters.AddWithValue("@IndirizzoViolazione",verbale.IndirizzoViolazione);
                cmd.Parameters.AddWithValue("@Nominativo_Agente", verbale.Nominativo_Agente);
                cmd.Parameters.AddWithValue("@DataTrascrizioneVerbale", verbale.DataTrascrizioneVerbale);
                cmd.Parameters.AddWithValue("@Importo", verbale.Importo);
                cmd.Parameters.AddWithValue("@DecrementoPunti", verbale.DecrementoPunti);
                cmd.Parameters.AddWithValue("@IDAnagrafica", verbale.IDAnagrafica);
                cmd.Parameters.AddWithValue("@IDViolazione", verbale.IDViolazione);

                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            finally
            {
                conn.Close();
            }





        }



        public static List<TipoViolazione> GetViolazioni() 
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM TipoViolazione ", conn);
                SqlDataReader sqlDataReader;
                conn.Open();
                List<TipoViolazione> violazioni = new List<TipoViolazione>();
                sqlDataReader = cmd.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    TipoViolazione newViolazione = new TipoViolazione(
                        Convert.ToInt32(sqlDataReader["IdViolazione"]),
                        sqlDataReader["Descrizione"].ToString()
                       
                        );

                    violazioni.Add(newViolazione);

                }

                return violazioni;
            }
            catch (Exception)
            {
                return new List<TipoViolazione>();
            }
            finally
            {
                conn.Close();
            }


        }

        public static void AddViolazione(TipoViolazione violazione) 
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO TipoViolazione (Descrizione) VALUES(@Descrizione)", conn);

                cmd.Parameters.AddWithValue("@Descrizione", violazione.Descrizione);
               


                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
            }
            finally
            {
                conn.Close();
            }







        }







    }
}