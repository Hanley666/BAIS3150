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
            SqlConnection connection = new SqlConnection();
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

                if (dataReader.HasRows)
                {


                    while (dataReader.Read())
                    {
                        customerList.Add(new Customer
                        {
                            CustomerId = (int)dataReader[dataReader.GetOrdinal("CustomerId")],
                            FirstName = dataReader[dataReader.GetOrdinal("FirstName")].ToString(),
                            LastName = dataReader[dataReader.GetOrdinal("LastName")].ToString(),
                            Address = dataReader[dataReader.GetOrdinal("Address")].ToString(),
                            City = dataReader[dataReader.GetOrdinal("City")].ToString(),
                            Province = dataReader[dataReader.GetOrdinal("Province")].ToString(),
                            PostalCode = dataReader[dataReader.GetOrdinal("PostalCode")].ToString()
                        });
                    }
                }
                return customerList;
            }
            catch (Exception ex)
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

        public Customer GetCustomerById(int customerId)
        {
            SqlConnection connection = new SqlConnection();
            SqlCommand command = null;
            SqlParameter parameter;
            SqlDataReader dataReader = null;
            Customer customer;

            try
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                command = new SqlCommand("GetCustomerById", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                parameter = new SqlParameter
                {
                    ParameterName = "@CustomerId",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    SqlValue = customerId
                };
                command.Parameters.Add(parameter);

                dataReader = command.ExecuteReader();

                if (dataReader.HasRows)
                {
                    dataReader.Read();

                    customer = new Customer
                    {
                        CustomerId = customerId,
                        FirstName = dataReader[dataReader.GetOrdinal("FirstName")].ToString(),
                        LastName = dataReader[dataReader.GetOrdinal("LastName")].ToString(),
                        Address = dataReader[dataReader.GetOrdinal("Address")].ToString(),
                        City = dataReader[dataReader.GetOrdinal("City")].ToString(),
                        Province = dataReader[dataReader.GetOrdinal("Province")].ToString(),
                        PostalCode = dataReader[dataReader.GetOrdinal("PostalCode")].ToString(),
                    };
                    return customer;
                }
                return null;
            }
            catch (Exception ex)
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

        public bool GetSaleNumberByCustomerId(int customerId)
        {
            SqlConnection connection = new SqlConnection();
            SqlCommand command = null;
            SqlParameter parameter;
            SqlDataReader dataReader = null;

            try
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                command = new SqlCommand("GetSaleNumberByCustomerId", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                parameter = new SqlParameter
                {
                    ParameterName = "@CustomerId",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    SqlValue = customerId
                };
                command.Parameters.Add(parameter);

                dataReader = command.ExecuteReader();

                if (dataReader.HasRows)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Get Sale by Id Failed", ex);
            }
            finally
            {
                connection?.Close();
                dataReader?.Close();
                command?.Dispose();
            }

        }

        public void CreateCustomer(Customer customer)
        {
            SqlConnection connection = new SqlConnection();
            SqlCommand command = null;
            SqlParameter parameter;


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
                    SqlValue = customer.FirstName
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@LastName",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = customer.LastName
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@Address",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = customer.Address
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@City",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = customer.City
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@Province",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = customer.Province
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@PostalCode",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = customer.PostalCode
                };
                command.Parameters.Add(parameter);

                command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception("CreateCustomer Failed:", ex);
            }
            finally
            {
                connection?.Close();
                command?.Dispose();
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            SqlConnection connection = new SqlConnection();
            SqlCommand command = null;
            SqlParameter parameter;

            try
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                command = new SqlCommand("UpdateCustomer", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                parameter = new SqlParameter
                {
                    ParameterName = "@CustomerId",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    Value = customer.CustomerId
                };
                command.Parameters.Add(parameter);
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

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("UpdateCustomer Failed:", ex);
            }
            finally
            {
                connection?.Close();
                command?.Dispose();
            }
        }

        public void DeleteCustomer(int customerId)
        {
            SqlConnection connection = new SqlConnection();
            SqlCommand command = null;
            SqlParameter parameter;

            try
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                command = new SqlCommand("DeleteCustomer", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                parameter = new SqlParameter
                {
                    ParameterName = "@CustomerId",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    Value = customerId
                };
                command.Parameters.Add(parameter);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("DeleteCustomer Failed:", ex);
            }
            finally
            {
                connection?.Close();
                command?.Dispose();
            }
        }
    }
}
