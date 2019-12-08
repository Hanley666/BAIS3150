using cpiershanley1BAIS3150CodeSample.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace cpiershanley1BAIS3150CodeSample.Technical_Services
{
    public class ProgramServices
    {
        private readonly string _connectionString;
        public ProgramServices(string connectionString)
        {
            _connectionString = connectionString;
        }
        public bool AddProgram(string ProgramCode, string Description)
        {
            bool Success;
            SqlConnection BAIS3150Connection = null;
            SqlCommand AddProgramCommand = null;

            try
            {
                BAIS3150Connection = new SqlConnection()
                {
                    ConnectionString = _connectionString
                };
                //(localdb)\MSSQLLocalDB
                BAIS3150Connection.Open();

                AddProgramCommand = new SqlCommand
                {
                    Connection = BAIS3150Connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "AddProgram"
                };

                SqlParameter ProgramCodeParameter = new SqlParameter
                {
                    ParameterName = "@ProgramCode",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = ProgramCode
                };
                AddProgramCommand.Parameters.Add(ProgramCodeParameter);

                SqlParameter DescriptionParameter = new SqlParameter
                {
                    ParameterName = "@Description",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = Description
                };
                AddProgramCommand.Parameters.Add(DescriptionParameter);

                AddProgramCommand.ExecuteNonQuery();
                Success = true;
            }
            finally
            {
                if (BAIS3150Connection != null)
                    BAIS3150Connection.Close();
                if (AddProgramCommand != null)
                    AddProgramCommand.Dispose();
            }
            return Success;
        }
        public NaitProgram GetProgram(string ProgramCode)
        {
            NaitProgram SearchedProgram = null;
            SqlConnection BAIS3150Connection = null;
            SqlCommand GetProgramCommand = null;
            SqlDataReader GetProgramDataReader = null;
            StudentServices StudentArray = new StudentServices(_connectionString);

            try
            {
                BAIS3150Connection = new SqlConnection()
                {
                    ConnectionString = _connectionString
                };
                BAIS3150Connection.Open();

                GetProgramCommand = new SqlCommand
                {
                    Connection = BAIS3150Connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "GetProgramByProgramCode"
                };

                SqlParameter ProgramCodeParameter = new SqlParameter
                {
                    ParameterName = "@ProgramCode",
                    DbType = DbType.String,
                    Direction = ParameterDirection.Input,
                    SqlValue = ProgramCode
                };
                GetProgramCommand.Parameters.Add(ProgramCodeParameter);


                GetProgramDataReader = GetProgramCommand.ExecuteReader();

                if (GetProgramDataReader.HasRows)
                {
                    GetProgramDataReader.Read();

                    SearchedProgram = new NaitProgram
                    {
                        Description = GetProgramDataReader[GetProgramDataReader.GetOrdinal("Description")].ToString()
                    };
                    SearchedProgram.ProgramCode = ProgramCode;
                    SearchedProgram.EnrolledStudent = StudentArray.GetStudents(ProgramCode);
                }
            }
            finally
            {
                if (GetProgramDataReader != null)
                    GetProgramDataReader.Close();
                if (GetProgramCommand != null)
                    GetProgramCommand.Dispose();
                if (BAIS3150Connection != null)
                    BAIS3150Connection.Close();
            }

            return SearchedProgram;
        }
        public List<NaitProgram> GetPrograms()
        {
            SqlConnection BAIS3150Connection = null;
            SqlCommand GetProgramCommand = null;
            SqlDataReader GetProgramDataReader = null;
            List<NaitProgram> ProgramList = new List<NaitProgram>();

            try
            {
                BAIS3150Connection = new SqlConnection()
                {
                    ConnectionString = _connectionString
                };
                BAIS3150Connection.Open();

                GetProgramCommand = new SqlCommand()
                {
                    Connection = BAIS3150Connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "GetPrograms"
                };

                GetProgramDataReader = GetProgramCommand.ExecuteReader();

                if (GetProgramDataReader.HasRows)
                {
                    while (GetProgramDataReader.Read())
                    {
                        ProgramList.Add(new NaitProgram
                        {
                            ProgramCode = GetProgramDataReader[GetProgramDataReader.GetOrdinal("ProgramCode")].ToString(),
                            Description = GetProgramDataReader[GetProgramDataReader.GetOrdinal("Description")].ToString(),
                        });
                    }
                }
            }
            finally
            {
                if (BAIS3150Connection != null)
                    BAIS3150Connection.Close();
                if (GetProgramCommand != null)
                    GetProgramCommand.Dispose();
                if (GetProgramDataReader != null)
                    GetProgramDataReader.Close();
            }
            return ProgramList;
        }
        public bool UpdateProgram(NaitProgram SelectedProgram)
        {
            bool Success = false;
            SqlConnection BAIS3150Connection = null;
            SqlCommand UpdateProgramCommand = null;
            SqlParameter ProgramParameter;

            try
            {
                BAIS3150Connection = new SqlConnection()
                {
                    ConnectionString = _connectionString
                };
                BAIS3150Connection.Open();

                UpdateProgramCommand = new SqlCommand
                {
                    Connection = BAIS3150Connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "UpdateProgramDescription"
                };
                ProgramParameter = new SqlParameter
                {
                    ParameterName = "@ProgramCode",
                    DbType = DbType.String,
                    Direction = ParameterDirection.Input,
                    SqlValue = SelectedProgram.ProgramCode
                };
                UpdateProgramCommand.Parameters.Add(ProgramParameter);

                ProgramParameter = new SqlParameter
                {
                    ParameterName = "@Description",
                    DbType = DbType.String,
                    Direction = ParameterDirection.Input,
                    SqlValue = SelectedProgram.Description
                };
                UpdateProgramCommand.Parameters.Add(ProgramParameter);



                UpdateProgramCommand.ExecuteNonQuery();
                Success = true;
            }
            finally
            {
                if (BAIS3150Connection != null)
                    BAIS3150Connection.Close();
                if (UpdateProgramCommand != null)
                    UpdateProgramCommand.Dispose();
            }

            return Success;
        }
        public bool DeleteProgram(string ProgramCode)
        {
            bool Success = false;
            SqlConnection BAIS3150Connection = null;
            SqlCommand DeleteProgramCommand = null;

            try
            {
                BAIS3150Connection = new SqlConnection()
                {
                    ConnectionString = _connectionString
                };
                BAIS3150Connection.Open();

                DeleteProgramCommand = new SqlCommand
                {
                    Connection = BAIS3150Connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "DeleteProgram"
                };

                SqlParameter DeleteStudentParameter = new SqlParameter
                {
                    ParameterName = "@ProgramCode",
                    DbType = DbType.String,
                    Direction = ParameterDirection.Input,
                    SqlValue = ProgramCode
                };
                DeleteProgramCommand.Parameters.Add(DeleteStudentParameter);

                DeleteProgramCommand.ExecuteNonQuery();
                Success = true;
            }
            finally
            {
                if (BAIS3150Connection != null)
                    BAIS3150Connection.Close();
                if (DeleteProgramCommand != null)
                    DeleteProgramCommand.Dispose();
            }
            return Success;
        }
    }
}
