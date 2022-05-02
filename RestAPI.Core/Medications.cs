using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace RestAPI.Core
{
    public class Medications
    {
        private static readonly string connectionString = "Host=localhost; Port=5432; Database=Medication; Username=postgres; Password=12345";

        // postgreSQL library variables
        private static NpgsqlConnection sqlConnection;
        private static NpgsqlDataReader sqlDataReader;
        private static NpgsqlCommand sqlCommand;


        /// <summary>
        /// Function to get all medications from the database
        /// </summary>
        /// <returns></returns>
        public static List<Medication> GetAllMedications()
        {
            try
            {
                // Creation of database connection
                using (sqlConnection = new NpgsqlConnection(connectionString))
                {
                    // getting the function from the database
                    sqlCommand = new NpgsqlCommand("Select * from medication", sqlConnection);
                    sqlCommand.CommandType = CommandType.Text;

                    // openning the connection
                    sqlConnection.Open();

                    // getting data from database
                    sqlDataReader = sqlCommand.ExecuteReader();

                    // list of medications
                    List<Medication> medications = new List<Medication>();

                    if (!sqlDataReader.HasRows)
                    {
                        sqlDataReader.DisposeAsync();
                        sqlConnection.Close();
                        return medications;
                    }

                    // procedure to deserialazation
                    while (sqlDataReader.Read())
                    {
                        medications.Add(new Medication
                        {
                            Medication_Code = Int32.Parse(sqlDataReader["medication_code"].ToString().TrimEnd()),
                            Name = (sqlDataReader["name"]).ToString().TrimEnd(),
                            Quantity = Int32.Parse(sqlDataReader["quantity"].ToString().TrimEnd()),
                            Creation_Date = ((DateTime)sqlDataReader["creation_date"])
                    });
                    }

                    // closing connection and resources
                    sqlCommand.Dispose();
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                    sqlConnection = null;
                    sqlDataReader.Dispose();

                    return medications;
                }
            }
            catch (NpgsqlException ex)
            {
                throw ex;
            }
        }

        public static bool Verify_Medication_by_Name(string name)
        {
            try
            {
               int result = 0;

                // Creation of database connection
                using (sqlConnection = new NpgsqlConnection(connectionString))
                {
                    // getting the function from the database
                    sqlCommand = new NpgsqlCommand($"Select count('-') from medication where name = '{name}'", sqlConnection);
                    sqlCommand.CommandType = CommandType.Text;

                    // openning the connection
                    sqlConnection.Open();

                    // getting data from database
                    sqlDataReader = sqlCommand.ExecuteReader();

                    if (!sqlDataReader.HasRows)
                    {
                        sqlDataReader.DisposeAsync();
                        sqlConnection.Close();
                        return false;
                    }

                    while (sqlDataReader.Read())
                    {
                        result = Int32.Parse(sqlDataReader["count"].ToString());
                    }


                     // closing connection and resources
                     sqlCommand.Dispose();
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                    sqlConnection = null;
                    sqlDataReader.Dispose();

                }
                return result == 0 ? false : true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Add_Medication(Medication medication)
        {

            try
            {

                // Creation of database connection
                using (sqlConnection = new NpgsqlConnection(connectionString))
                {
                    // getting the function from the database
                    sqlCommand = new NpgsqlCommand($"INSERT INTO medication (name, quantity) VALUES ('{medication.Name}', {medication.Quantity})", sqlConnection);
                    sqlCommand.CommandType = CommandType.Text;

                    // openning the connection
                    sqlConnection.Open();

                    // getting data from database
                    sqlDataReader = sqlCommand.ExecuteReader();


                    // closing connection and resources
                    sqlCommand.Dispose();
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                    sqlConnection = null;
                    sqlDataReader.Dispose();

                }
                return "Inserted Sucessfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Delete_Medication(int medication_code)
        {
            try
            {
                // Creation of database connection
                using (sqlConnection = new NpgsqlConnection(connectionString))
                {
                    // getting the function from the database
                    sqlCommand = new NpgsqlCommand($"Delete from medication where medication_code = {medication_code}", sqlConnection);
                    sqlCommand.CommandType = CommandType.Text;

                    // openning the connection
                    sqlConnection.Open();

                    // getting data from database
                    sqlDataReader = sqlCommand.ExecuteReader();


                    // closing connection and resources
                    sqlCommand.Dispose();
                    sqlConnection.Close();
                    sqlConnection.Dispose();
                    sqlConnection = null;
                    sqlDataReader.Dispose();

                }
                return "Deleted Sucessfully";
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
