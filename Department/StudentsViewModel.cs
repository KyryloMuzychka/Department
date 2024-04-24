using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.Entity;

namespace DepartmentServer
{
    public class StudentsViewModel : INotifyPropertyChanged
    {
        private readonly DataClasses1DataContext db = new DataClasses1DataContext();
        private int _studentId;
        private string _studentName;
        private string _studentLogin;
        private string _studentPassword;
        private int _groupFk;

        private List<Student> _studentData;
        public List<Student> StudentData
        {
            get { return _studentData; }
            set
            {
                _studentData = value;
                OnPropertyChanged(nameof(StudentData));
            }
        }

        public void LoadData()
        {
            try
            {
                // Fetch student data from the database
                StudentData = db.Students.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading student data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public int StudentId
        {
            get => _studentId;
            set
            {
                _studentId = value;
                OnPropertyChanged(nameof(StudentId));
            }
        }

        public string StudentName
        {
            get => _studentName;
            set
            {
                _studentName = value;
                OnPropertyChanged(nameof(StudentName));
            }
        }

        public string StudentLogin
        {
            get => _studentLogin;
            set
            {
                _studentLogin = value;
                OnPropertyChanged(nameof(StudentLogin));
            }
        }

        public string StudentPassword
        {
            get => _studentPassword;
            set
            {
                _studentPassword = value;
                OnPropertyChanged(nameof(StudentPassword));
            }
        }

        public int GroupFk
        {
            get => _groupFk;
            set
            {
                _groupFk = value;
                OnPropertyChanged(nameof(GroupFk));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddStudent()
        {
            if (ValidateInput())
            {
                var student = new Student
                {
                    student_name = StudentName,
                    student_login = StudentLogin,
                    student_password = StudentPassword,
                    group_fk = GroupFk
                };

                db.Students.InsertOnSubmit(student);
                db.SubmitChanges();
                MessageBox.Show("New student has been added");
            }
            else
            {
                MessageBox.Show("Please provide valid input.");
            }
        }

        public void UpdateStudent()
        {
            if (int.TryParse(StudentId.ToString(), out int studentId))
            {
                var student = db.Students.FirstOrDefault(s => s.student_id == studentId);

                if (student != null)
                {
                    student.student_name = StudentName;
                    student.student_login = StudentLogin;
                    student.student_password = StudentPassword;
                    student.group_fk = GroupFk;

                    db.SubmitChanges();
                    MessageBox.Show("Successfully Updated");
                }
                else
                {
                    MessageBox.Show("Student not found.");
                }
            }
            else
            {
                MessageBox.Show("Please provide a valid student ID.");
            }
        }

        public void DeleteStudent()
        {
            if (int.TryParse(StudentId.ToString(), out int studentId))
            {
                var student = db.Students.FirstOrDefault(s => s.student_id == studentId);

                if (student != null)
                {
                    db.Students.DeleteOnSubmit(student);
                    db.SubmitChanges();
                    MessageBox.Show("Successfully Deleted");
                }
                else
                {
                    MessageBox.Show("Student not found.");
                }
            }
            else
            {
                MessageBox.Show("Please provide a valid student ID.");
            }
        }

        public void SearchStudent()
        {
            if (int.TryParse(StudentId.ToString(), out int studentId))
            {
                var student = db.Students.FirstOrDefault(s => s.student_id == studentId);

                if (student != null)
                {
                    StudentName = student.student_name;
                    StudentLogin = student.student_login;
                    StudentPassword = student.student_password;
                    GroupFk = (int)(student.group_fk);
                    MessageBox.Show("Student found.");
                }
                else
                {
                    MessageBox.Show("Student not found.");
                    ClearStudentData();
                }
            }
            else
            {
                MessageBox.Show("Please provide a valid student ID.");
                ClearStudentData();
            }
        }

        private void ClearStudentData()
        {
            StudentName = string.Empty;
            StudentLogin = string.Empty;
            StudentPassword = string.Empty;
            GroupFk = 0;            
        }

        private bool ValidateInput()
        {
            return !string.IsNullOrWhiteSpace(StudentName) &&
                   !string.IsNullOrWhiteSpace(StudentLogin) &&
                   !string.IsNullOrWhiteSpace(StudentPassword);
        }
    }
}
