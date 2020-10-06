using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_Livraria.Extentions
{
    public static class DataBaseExtention
    {
        public static void CreateDatabase(string connectionString)
        {
            var builder = new NpgsqlConnectionStringBuilder(connectionString);
            var databaseName = builder.Database; // REMEMBER ORIGINAL DB NAME
            builder.Database = "postgres"; // TEMPORARILY USE POSTGRES DATABASE

            var exist = chkDBExists(connectionString, databaseName);

            if (!exist)
            {
                // Create connection to database server
                using (var connection = new NpgsqlConnection(builder.ConnectionString))
                {
                    connection.Open();

                    // Create database
                    var createCommand = connection.CreateCommand();
                    createCommand.CommandText = string.Format(@"CREATE DATABASE ""{0}"" ENCODING = 'UTF8'", databaseName);
                    createCommand.ExecuteNonQuery();

                    connection.Close();
                }
            }
        }

        private static bool chkDBExists(string connectionStr, string dbname)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionStr))
            {
                using (NpgsqlCommand command = new NpgsqlCommand
                    ($"SELECT DATNAME FROM pg_catalog.pg_database WHERE DATNAME = '{dbname}'", conn))
                {
                    try
                    {
                        conn.Open();
                        var i = command.ExecuteScalar();
                        conn.Close();
                        if (i.ToString().Equals(dbname)) //always 'true' (if it exists) or 'null' (if it doesn't)
                            return true;
                        else return false;
                    }
                    catch (Exception e) { return false; }
                }
            }
        }

    }
}
