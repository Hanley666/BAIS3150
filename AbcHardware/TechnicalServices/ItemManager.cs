using AbcHardware.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace AbcHardware.TechnicalServices
{
    public class ItemManager
    {
        private readonly string _connectionString;

        public ItemManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Item> GetItems()
        {
            SqlConnection connection = new SqlConnection();
            SqlCommand command = null;
            SqlDataReader dataReader = null;
            List<Item> itemsList = new List<Item>();


            try
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                command = new SqlCommand("GetItems", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                dataReader = command.ExecuteReader();

                if(dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        itemsList.Add(new Item
                        {
                            ItemCode = dataReader[dataReader.GetOrdinal("ItemCode")].ToString(),
                            Description = dataReader[dataReader.GetOrdinal("Description")].ToString(),
                            UnitPrice = (decimal)dataReader[dataReader.GetOrdinal("UnitPrice")],
                            QuantityOnHand = (int)dataReader[dataReader.GetOrdinal("InventoryOnHand")],
                            Discontinued = (bool)dataReader[dataReader.GetOrdinal("Discontinued")]
                        });
                    }
                }                
                return itemsList;
            }
            catch(Exception ex)
            {
                throw new Exception("GetItems Failed", ex);
            }
            finally
            {
                connection?.Close();
                dataReader?.Close();
                command?.Dispose();
            }
        }

        public Item GetItemByItemCode(string itemCode)
        {
            SqlConnection connection = new SqlConnection();
            SqlCommand command = null;
            SqlDataReader dataReader = null;
            SqlParameter parameter;
            Item item;


            try
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                command = new SqlCommand("GetItems", connection)
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

                dataReader = command.ExecuteReader();

                if (dataReader.HasRows)
                {
                    dataReader.Read();

                        item = new Item
                        {
                            ItemCode = dataReader[dataReader.GetOrdinal("ItemCode")].ToString(),
                            Description = dataReader[dataReader.GetOrdinal("Description")].ToString(),
                            UnitPrice = (decimal)dataReader[dataReader.GetOrdinal("UnitPrice")],
                            QuantityOnHand = (int)dataReader[dataReader.GetOrdinal("InventoryOnHand")],
                            Discontinued = (bool)dataReader[dataReader.GetOrdinal("Discontinued")]
                        };
                    return item;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("GetItems Failed", ex);
            }
            finally
            {
                connection?.Close();
                dataReader?.Close();
                command?.Dispose();
            }
        }
    
        public void CreateItem(Item item)
        {
            SqlConnection connection = new SqlConnection();
            SqlCommand command = null;
            SqlParameter parameter;

            try
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                command = new SqlCommand("CreateItem", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                parameter = new SqlParameter
                {
                    ParameterName = "@ItemCode",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = item.ItemCode
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@Description",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = item.Description
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@UnitPrice",
                    SqlDbType = SqlDbType.Money,
                    Direction = ParameterDirection.Input,
                    SqlValue = item.UnitPrice
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@InventoryOnHand",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    SqlValue = item.QuantityOnHand
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@Discontinued",
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Input,
                    SqlValue = item.Discontinued
                };
                command.Parameters.Add(parameter);

                command.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                throw new Exception("Create Item Failed:", ex);
            }
            finally
            {
                connection?.Close();
                command?.Dispose();
            }
        }

        public void UpdateItem(Item item)
        {
            SqlConnection connection = new SqlConnection();
            SqlCommand command = null;
            SqlParameter parameter;

            try
            {
                connection.ConnectionString = _connectionString;
                connection.Open();

                command = new SqlCommand("UpdateItem", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                parameter = new SqlParameter
                {
                    ParameterName = "@ItemCode",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = item.ItemCode
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@Description",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = item.Description
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@UnitPrice",
                    SqlDbType = SqlDbType.Money,
                    Direction = ParameterDirection.Input,
                    SqlValue = item.UnitPrice
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@InventoryOnHand",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    SqlValue = item.QuantityOnHand
                };
                command.Parameters.Add(parameter);

                parameter = new SqlParameter
                {
                    ParameterName = "@Discontinued",
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Input,
                    SqlValue = item.Discontinued
                };
                command.Parameters.Add(parameter);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Update Item Failed:", ex);
            }
            finally
            {
                connection?.Close();
                command?.Dispose();
            }
        }

        public void DeleteItem(string itemCode, bool discontinued)
        {
            SqlConnection connection = new SqlConnection();
            SqlCommand command = null;
            SqlParameter parameter;

            try
            {
                command = new SqlCommand("DeleteItem", connection)
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
                    ParameterName = "@Description",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = discontinued
                };
                command.Parameters.Add(parameter);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Delete Item Failed:", ex);
            }
            finally
            {
                connection?.Close();
                command?.Dispose();
            }
        }
       
    }
}
