using cpiershanley1BAIS3150CodeSample.Technical_Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cpiershanley1BAIS3150CodeSample.Domain
{
    public class BCS
    {
        private readonly ProgramServices _programManager;
        private readonly StudentServices _studentManager;
        private readonly DatabaseUserManager _dbUserManager;
        public BCS(IConfiguration config)
        {
            _programManager = new ProgramServices(config.GetConnectionString("DATABAIST"));
            _studentManager = new StudentServices(config.GetConnectionString("DATABAIST"));
            _dbUserManager = new DatabaseUserManager(config.GetConnectionString("DATABAIST"));
        }
        #region Student Methods
        public bool EnrollStudent(Student AcceptedStudent)
        {           
            return _studentManager.AddStudent(AcceptedStudent);
        }
        public Student FindStudent(string studentId)
        {
            return _studentManager.GetStudent(studentId);         
        }
        public bool ModifyStudent(Student EnrolledStudent)
        {
            return _studentManager.UpdateStudent(EnrolledStudent);
        }
        public bool RemoveStudent(string StudentId)
        {
            return _studentManager.DeleteStudent(StudentId);
        }
        public List<Student> FindStudentsByProgram(string ProgramCode)
        {
            return _studentManager.GetStudents(ProgramCode);
        }
        #endregion

        #region Program Methods
        public bool CreateProgram(string ProgramCode, string Description)
        {
            List<string> errorMessages = new List<string>();
            if (String.IsNullOrEmpty(ProgramCode))
                errorMessages.Add("Program Code is Required");
            if (String.IsNullOrEmpty(Description))
                errorMessages.Add("Description is Required");
            if (errorMessages.Any())
                throw new Exception(string.Join("<br/>", errorMessages));
                    
            return _programManager.AddProgram(ProgramCode, Description);
        }
        public NaitProgram FindProgram(string ProgramCode)
        {
            return _programManager.GetProgram(ProgramCode);
        }
        public List<NaitProgram> FindPrograms()
        {
            return _programManager.GetPrograms();
        }
        public bool UpdateProgramDescription(NaitProgram SelectedProgram)
        {
            return _programManager.UpdateProgram(SelectedProgram);
        }
        public bool DeleteProgram(string ProgramCode)
        {
            return _programManager.DeleteProgram(ProgramCode);
        }
        #endregion

        #region DatabaseUser Methods
        public DatabaseUser GetDatabaseUser()
        {            
            return _dbUserManager.GetDatabaseUser();            
        }
        #endregion
    }
}
