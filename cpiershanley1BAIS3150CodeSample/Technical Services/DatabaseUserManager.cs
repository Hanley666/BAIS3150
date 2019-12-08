using cpiershanley1BAIS3150CodeSample.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace cpiershanley1BAIS3150CodeSample.Technical_Services
{
    public class DatabaseUserManager
    {
        private readonly string _connectionString;
        public DatabaseUserManager(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DatabaseUser GetDatabaseUser()
        {
            SqlConnection BAIS3150 = null;
            SqlDataReader dataReader = null;
            SqlCommand command = null;

            try
            {
                BAIS3150 = new SqlConnection()
                {
                    ConnectionString = _connectionString
                };
                BAIS3150.Open();

                command = new SqlCommand
                {
                    Connection = BAIS3150,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "GetDatabaseUser"
                };

                dataReader = command.ExecuteReader();

                DatabaseUser currentDatabaseUser = new DatabaseUser();

                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        currentDatabaseUser.CurrentUser = (string)dataReader["CurrentUser"];
                        currentDatabaseUser.SystemUser = (string)dataReader["SystemUser"];
                        currentDatabaseUser.SessionUser = (string)dataReader["SessionUser"];
                    }
                }
                return currentDatabaseUser;
            }
            finally
            {
                dataReader?.Close();
                BAIS3150?.Close();
                command?.Dispose();
            }       
        }
    }
}
