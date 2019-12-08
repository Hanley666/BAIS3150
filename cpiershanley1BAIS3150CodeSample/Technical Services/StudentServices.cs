using cpiershanley1BAIS3150CodeSample.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace cpiershanley1BAIS3150CodeSample.Technical_Services
{
    public class StudentServices
    {
        private readonly string _connectionString;

        public StudentServices(string connectionString)
        {
            _connectionString = connectionString;
        }
        public bool AddStudent(Student AcceptedStudent)
        {
            SqlConnection BAIS3150Connection;
            bool Success;
            //(localdb)\MSSQLLocalDB
            BAIS3150Connection = new SqlConnection()
            {
                ConnectionString = _connectionString
            };
            BAIS3150Connection.Open();

            SqlCommand AddStudentCommand = new SqlCommand
            {
                Connection = BAIS3150Connection,
                CommandType = CommandType.StoredProcedure,
                CommandText = "AddStudent"
            };

            SqlParameter StudentIdParameter = new SqlParameter
            {
                ParameterName = "@StudentId",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = AcceptedStudent.StudentId
            };
            AddStudentCommand.Parameters.Add(StudentIdParameter);

            SqlParameter FirstNameParameter = new SqlParameter
            {
                ParameterName = "@FirstName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = AcceptedStudent.FirstName
            };
            AddStudentCommand.Parameters.Add(FirstNameParameter);

            SqlParameter LastNameParameter = new SqlParameter
            {
                ParameterName = "@LastName",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = AcceptedStudent.LastName
            };
            AddStudentCommand.Parameters.Add(LastNameParameter);

            SqlParameter EmailParameter = new SqlParameter
            {
                ParameterName = "@Email",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = AcceptedStudent.Email
            };
            AddStudentCommand.Parameters.Add(EmailParameter);

            SqlParameter ProgramCodeParameter = new SqlParameter
            {
                ParameterName = "@ProgramCode",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Input,
                SqlValue = AcceptedStudent.ProgramCode
            };
            AddStudentCommand.Parameters.Add(ProgramCodeParameter);

            AddStudentCommand.ExecuteNonQuery();
            AddStudentCommand.Dispose();
            BAIS3150Connection.Close();

            Success = true;
            return Success;
        }
        public Student GetStudent(string StudentId)
        {
            Student SearchedStudent = null;
            SqlConnection BAIS3150Connection = null;
            SqlCommand GetStudentCommand = null;
            SqlDataReader GetStudentDataReader = null;

            try
            {
                BAIS3150Connection = new SqlConnection()
                {
                    ConnectionString = _connectionString
                };
                BAIS3150Connection.Open();

                GetStudentCommand = new SqlCommand
                {
                    Connection = BAIS3150Connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "GetStudentByStudentId"
                };

                SqlParameter StudentIdParameter = new SqlParameter
                {
                    ParameterName = "@StudentId",
                    DbType = DbType.String,
                    Direction = ParameterDirection.Input,
                    SqlValue = StudentId
                };
                GetStudentCommand.Parameters.Add(StudentIdParameter);


                GetStudentDataReader = GetStudentCommand.ExecuteReader();

                if (GetStudentDataReader.HasRows)
                {
                    GetStudentDataReader.Read();

                    SearchedStudent = new Student
                    {
                        StudentId = GetStudentDataReader[GetStudentDataReader.GetOrdinal("StudentId")].ToString(),
                        FirstName = GetStudentDataReader[GetStudentDataReader.GetOrdinal("FirstName")].ToString(),
                        LastName = GetStudentDataReader[GetStudentDataReader.GetOrdinal("LastName")].ToString(),
                        Email = GetStudentDataReader[GetStudentDataReader.GetOrdinal("Email")].ToString(),
                        ProgramCode = GetStudentDataReader[GetStudentDataReader.GetOrdinal("ProgramCode")].ToString()
                    };

                }
            }
            finally
            {
                if (GetStudentDataReader != null)
                    GetStudentDataReader.Close();
                if (GetStudentCommand != null)
                    GetStudentCommand.Dispose();
                if (BAIS3150Connection != null)
                    BAIS3150Connection.Close();
            }

            return SearchedStudent;
        }
        public List<Student> GetStudents(string ProgramCode)
        {
            SqlConnection BAIS3150Connection = null;
            SqlCommand GetStudentsCommand = null;
            SqlDataReader GetStudentsDataReader = null;
            List<Student> StudentList = new List<Student>();

            try
            {
                BAIS3150Connection = new SqlConnection()
                {
                    ConnectionString = _connectionString
                };
                BAIS3150Connection.Open();

                GetStudentsCommand = new SqlCommand
                {
                    Connection = BAIS3150Connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "GetStudentByProgramCode"
                };

                SqlParameter ProgramCodeParameter = new SqlParameter
                {
                    ParameterName = "@ProgramCode",
                    SqlDbType = SqlDbType.VarChar,
                    Direction = ParameterDirection.Input,
                    SqlValue = ProgramCode
                };
                GetStudentsCommand.Parameters.Add(ProgramCodeParameter);

                GetStudentsDataReader = GetStudentsCommand.ExecuteReader();

                if (GetStudentsDataReader.HasRows)
                {
                    while (GetStudentsDataReader.Read())
                    {
                        StudentList.Add(new Student
                        {
                            StudentId = GetStudentsDataReader[GetStudentsDataReader.GetOrdinal("StudentID")].ToString(),
                            FirstName = GetStudentsDataReader[GetStudentsDataReader.GetOrdinal("FirstName")].ToString(),
                            LastName = GetStudentsDataReader[GetStudentsDataReader.GetOrdinal("LastName")].ToString(),
                            Email = GetStudentsDataReader[GetStudentsDataReader.GetOrdinal("Email")].ToString()
                        });
                    }
                }
            }
            finally
            {
                if (BAIS3150Connection != null)
                    BAIS3150Connection.Close();
                if (GetStudentsDataReader != null)
                    GetStudentsDataReader.Close();
                if (GetStudentsCommand != null)
                    GetStudentsCommand.Dispose();
            }
            return StudentList;
        }
        public bool UpdateStudent(Student EnrolledStudent)
        {
            bool Success = false;
            SqlConnection BAIS3150Connection = null;
            SqlCommand UpdateStudentCommand = null;
            SqlParameter StudentParameter;

            try
            {
                BAIS3150Connection = new SqlConnection()
                {
                    ConnectionString = _connectionString
                };
                BAIS3150Connection.Open();

                UpdateStudentCommand = new SqlCommand
                {
                    Connection = BAIS3150Connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "UpdateStudent"
                };
                StudentParameter = new SqlParameter
                {
                    ParameterName = "@StudentId",
                    DbType = DbType.String,
                    Direction = ParameterDirection.Input,
                    SqlValue = EnrolledStudent.StudentId
                };
                UpdateStudentCommand.Parameters.Add(StudentParameter);

                StudentParameter = new SqlParameter
                {
                    ParameterName = "@FirstName",
                    DbType = DbType.String,
                    Direction = ParameterDirection.Input,
                    SqlValue = EnrolledStudent.FirstName
                };
                UpdateStudentCommand.Parameters.Add(StudentParameter);

                StudentParameter = new SqlParameter
                {
                    ParameterName = "@LastName",
                    DbType = DbType.String,
                    Direction = ParameterDirection.Input,
                    SqlValue = EnrolledStudent.LastName
                };
                UpdateStudentCommand.Parameters.Add(StudentParameter);

                StudentParameter = new SqlParameter
                {
                    ParameterName = "@Email",
                    DbType = DbType.String,
                    Direction = ParameterDirection.Input,
                    SqlValue = EnrolledStudent.Email
                };
                UpdateStudentCommand.Parameters.Add(StudentParameter);

                StudentParameter = new SqlParameter
                {
                    ParameterName = "@ProgramCode",
                    DbType = DbType.String,
                    Direction = ParameterDirection.Input,
                    SqlValue = EnrolledStudent.ProgramCode
                };
                UpdateStudentCommand.Parameters.Add(StudentParameter);

                UpdateStudentCommand.ExecuteNonQuery();
                Success = true;
            }
            finally
            {
                if (BAIS3150Connection != null)
                    BAIS3150Connection.Close();
                if (UpdateStudentCommand != null)
                    UpdateStudentCommand.Dispose();
            }

            return Success;
        }
        public bool DeleteStudent(string StudentId)
        {
            bool Success = false;
            SqlConnection BAIS3150Connection = null;
            SqlCommand DeleteStudentCommand = null;

            try
            {
                BAIS3150Connection = new SqlConnection()
                {
                    ConnectionString = _connectionString
                };
                BAIS3150Connection.Open();

                DeleteStudentCommand = new SqlCommand
                {
                    Connection = BAIS3150Connection,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "DeleteStudent"
                };

                SqlParameter DeleteStudentParameter = new SqlParameter
                {
                    ParameterName = "@StudentId",
                    DbType = DbType.String,
                    Direction = ParameterDirection.Input,
                    SqlValue = StudentId
                };
                DeleteStudentCommand.Parameters.Add(DeleteStudentParameter);

                DeleteStudentCommand.ExecuteNonQuery();
                Success = true;
            }
            finally
            {
                if (BAIS3150Connection != null)
                    BAIS3150Connection.Close();
                if (DeleteStudentCommand != null)
                    DeleteStudentCommand.Dispose();
            }
            return Success;
        }
    }
}

