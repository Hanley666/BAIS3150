using BAIS3150WebAppDemo.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace BAIS3150WebAppDemo.Technical_Services
{
    public class CategoriesController
    {
        public List<Category> GetNorthwindCategories()
        {
            SqlConnection NorthwindConnection = null;
            SqlCommand GetCategoriesCommand = null;
            SqlDataReader CategoriesDataReader = null;
            List<Category> CategoriesList = new List<Category>();

            try
            {
                NorthwindConnection = new SqlConnection()
                {
                    ConnectionString = @"Persist Security Info=False;Integrated Security=True;Database=Northwind;Server=(localdb)\MSSQLLocalDB;"
                };
                NorthwindConnection.Open();

                GetCategoriesCommand = new SqlCommand
                {
                    Connection = NorthwindConnection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "GetNorthwindCategories"
                };

                CategoriesDataReader = GetCategoriesCommand.ExecuteReader();

                if (CategoriesDataReader.HasRows)
                {
                    while (CategoriesDataReader.Read())
                    {
                       
                        CategoriesList.Add(new Category
                        {
                            CategoryName = CategoriesDataReader[CategoriesDataReader.GetOrdinal("CategoryName")].ToString(),
                            Description = CategoriesDataReader[CategoriesDataReader.GetOrdinal("CategoryName")].ToString(),
                            Picture = (Byte[])(CategoriesDataReader[CategoriesDataReader.GetOrdinal("CategoryName")]),
                        }); 
                    }
                }
            }
            finally
            {
                if (NorthwindConnection != null)
                    NorthwindConnection.Close();
                if (CategoriesDataReader != null)
                    CategoriesDataReader.Close();
                if (GetCategoriesCommand != null)
                    GetCategoriesCommand.Dispose();
            }
            return CategoriesList;
        }
    }
    
}
