using AbcHardware.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AbcHardware.TechnicalServices
{
    public class Sales
    {
        private readonly string _connectionString;

        public Sales(string connectionString)
        {
            _connectionString = connectionString;
        }


        public int ProcessSale(ABCSale sale)
        {
            SqlConnection connection = new SqlConnection();
            SqlTransaction transaction = null;

            try
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                transaction = connection.BeginTransaction();

                var saleNumber = CreateSale(sale, connection, transaction);

                foreach (var saleItem in sale.SaleItems)
                {
                    var exists = CheckSaleItemExists(saleItem.ItemCode, saleNumber, connection, transaction);

                    if (exists == true)
                    {
                        throw new Exception("SaleItem already Exists for this Sale");
                    }
                    else
                    {
                        CreateSaleItem(saleNumber, saleItem, connection, transaction);
                        UpdateInventory(saleItem.ItemCode, saleItem.Quantity.Value, connection, transaction);
                    }
                }
                transaction.Commit();
                return saleNumber;
            }
            catch(Exception ex)
            {
                transaction?.Rollback();
                return 0;
            }
            finally
            {
                connection?.Close();
            }

        }

        private int CreateSale(ABCSale sale, SqlConnection connection, SqlTransaction transaction)
        {
            SqlCommand command = null;
            SqlParameter parameter;

            try
            {
                command = new SqlCommand("CreateSale", connection, transaction)
                {
                    CommandType = CommandType.StoredProcedure
                };

                parameter = new SqlParameter
                {
                    ParameterName = "@SaleDate",
                    SqlDbType = SqlDbType.DateTime,
                    Direction = ParameterDirection.Input,
                    SqlValue = sale.SaleDate
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@SubTotal",
                    SqlDbType = SqlDbType.Money,
                    Direction = ParameterDirection.Input,
                    SqlValue = sale.SubTotal
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@Gst",
                    SqlDbType = SqlDbType.Money,
                    Direction = ParameterDirection.Input,
                    SqlValue = sale.Gst
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@SaleTotal",
                    SqlDbType = SqlDbType.Money,
                    Direction = ParameterDirection.Input,
                    SqlValue = sale.SaleTotal
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@SalePerson",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = sale.SalePerson
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@CustomerId",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    SqlValue = sale.CustomerId
                };
                command.Parameters.Add(parameter);

                return (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Create Sale Failed", ex);
            }
            finally
            {
                command?.Dispose();
            }
        }

        private void CreateSaleItem(int saleNumber, SaleItem saleItem, SqlConnection connection, SqlTransaction transaction)
        {
            SqlCommand command = null;
            SqlParameter parameter;

            try
            {
                command = new SqlCommand("CreateSaleItem", connection, transaction)
                {
                    CommandType = CommandType.StoredProcedure
                };

                parameter = new SqlParameter
                {
                    ParameterName = "@ItemCode",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = saleItem.ItemCode
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@SaleNumber",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    SqlValue = saleNumber
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@Quantity",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    SqlValue = saleItem.Quantity
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@ItemTotal",
                    SqlDbType = SqlDbType.Money,
                    Direction = ParameterDirection.Input,
                    SqlValue = (saleItem.Quantity * saleItem.UnitPrice)
                };
                command.Parameters.Add(parameter);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Create SaleItem Failed", ex);
            }
            finally
            {
                command?.Dispose();
            }
        }

        private void UpdateInventory(string itemCode, int quantity, SqlConnection connection, SqlTransaction transaction)
        {
            SqlCommand command = null;
            SqlParameter parameter;

            try
            {
                command = new SqlCommand("UpdateInventoryAmount", connection, transaction)
                {
                    CommandType = CommandType.StoredProcedure
                };

                parameter = new SqlParameter
                {
                    ParameterName = "@ItemCode",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = itemCode
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@Quantity",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    SqlValue = quantity
                };
                command.Parameters.Add(parameter);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Check Sale Item Failed", ex);
            }
            finally
            {
                command?.Dispose();
            }
        }

        private bool CheckSaleItemExists(string itemCode, int saleNumber, SqlConnection connection, SqlTransaction transaction)
        {
            SqlCommand command = null;
            SqlParameter parameter;
            SqlDataReader dataReader = null;

            try
            {
                command = new SqlCommand("GetSaleItemByPK", connection, transaction)
                {
                    CommandType = CommandType.StoredProcedure
                };

                parameter = new SqlParameter
                {
                    ParameterName = "@ItemCode",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = itemCode
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@SaleNumber",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    SqlValue = saleNumber
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
                throw new Exception("Check Sale Item Failed", ex);
            }
            finally
            {
                dataReader?.Close();
                command?.Dispose();
            }

        }
    }
}
