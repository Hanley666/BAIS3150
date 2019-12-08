using BAIS3150WebAppDemo.Technical_Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BAIS3150WebAppDemo.Domain
{
    public class BCS
    {
        #region Student Methods
        public bool EnrollStudent(Student AcceptedStudent)
        {
            bool Confirmation;
            StudentController StudentManager = new StudentController();
            Confirmation = StudentManager.AddStudent(AcceptedStudent);
            return Confirmation;
        }
        public Student FindStudent(string studentId)
        {
            Student searchedStudent;
            StudentController StudentManager = new StudentController();
            searchedStudent = StudentManager.GetStudent(studentId);
            return searchedStudent;
        }
        public bool ModifyStudent(Student EnrolledStudent)
        {
            bool Success;
            StudentController StudentManager = new StudentController();
            Success = StudentManager.UpdateStudent(EnrolledStudent);
            return Success;
        }
        public bool RemoveStudent(string StudentId)
        {
            bool Success;
            StudentController StudentManager = new StudentController();
            Success = StudentManager.DeleteStudent(StudentId);
            return Success;
        }
        public List<Student> FindStudentsByProgram(string ProgramCode)
        {
            List<Student> StudentList;
            StudentController StudentManager = new StudentController();
            StudentList = StudentManager.GetStudents(ProgramCode);
            return StudentList;
        }
        #endregion

        #region Program Methods
        public bool CreateProgram(string ProgramCode, string Description)
        {
            bool Confirmation;
            NAITProgramController ProgramManager = new NAITProgramController();
            Confirmation = ProgramManager.AddProgram(ProgramCode, Description);
            return Confirmation;
        }
        public NAITProgram FindProgram(string ProgramCode)
        {
            NAITProgram SearchedProgram;
            NAITProgramController ProgramManager = new NAITProgramController();
            SearchedProgram = ProgramManager.GetProgram(ProgramCode);
            return SearchedProgram;
        }
        public List<NAITProgram> FindPrograms()
        {
            List<NAITProgram> ProgramsList;
            NAITProgramController ProgramManager = new NAITProgramController();
            ProgramsList = ProgramManager.GetPrograms();

            return ProgramsList;
        }
        public bool UpdateProgramDescription(NAITProgram SelectedProgram)
        {
            bool Success;
            NAITProgramController ProgramManager = new NAITProgramController();
            Success = ProgramManager.UpdateProgram(SelectedProgram);
            return Success;
        }
        public bool DeleteProgram(string ProgramCode)
        {
            bool Success;
            NAITProgramController ProgramManager = new NAITProgramController();
            Success = ProgramManager.DeleteProgram(ProgramCode);
            return Success;
        }
        #endregion
    }
}
