using AbcHardware.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AbcHardware.TechnicalServices
{
    public class CustomerManager
    {
        private readonly string _connectionString;

        public CustomerManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Customer> GetCustomers()
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlDataReader dataReader = null;
            List<Customer> customerList = new List<Customer>();

            try
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                command = new SqlCommand("GetCustomers", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                dataReader = command.ExecuteReader();

                if(dataReader.HasRows)
                {
                    

                    while(dataReader.Read())
                    {
                        customerList.Add(new Customer
                        {
                            CustomerId = dataReader.GetOrdinal("CustomerId"),
                            FirstName = dataReader.GetOrdinal("FirstName").ToString(),
                            LastName = dataReader.GetOrdinal("LastName").ToString(),
                            Address = dataReader.GetOrdinal("Address").ToString(),
                            City = dataReader.GetOrdinal("City").ToString(),
                            Province = dataReader.GetOrdinal("Province").ToString(),
                            PostalCode = dataReader.GetOrdinal("PostalCode").ToString()
                        });
                    }
                }
                return customerList;
            }
            catch(Exception ex)
            {
                throw new Exception("GetCustomers Failed:", ex);
            }
            finally
            {
                dataReader?.Close();
                command?.Dispose();
                connection?.Close();
            }
        }

        public void CreateCustomer(Customer customer)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            SqlParameter parameter = null;
            

            try
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                command = new SqlCommand("CreateCustomer", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                parameter = new SqlParameter
                {
                    ParameterName = "@FirstName",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Value = customer.FirstName
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@LastName",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Value = customer.LastName
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@Address",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Value = customer.Address
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@City",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Value = customer.City
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@Province",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Value = customer.Province
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@PostalCode",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    Value = customer.PostalCode
                };
                command.Parameters.Add(parameter);

            }
            catch(Exception ex)
            {
                throw new Exception("CreateCustomer Failed:", ex);
            }
            finally
            {
                connection?.Close();
                command?.Dispose();
            }
        }
           
    }
}
