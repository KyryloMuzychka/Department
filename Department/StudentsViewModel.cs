using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace DepartmentServer
{
    /// <summary>
    /// View model class for managing student data.
    /// </summary>
    public class StudentsViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Database context for interacting with student data.
        /// </summary>
        private readonly DataClasses1DataContext db = new DataClasses1DataContext();

        /// <summary>
        /// Represents the ID of the student.
        /// </summary>
        private int _studentId;

        /// <summary>
        /// Represents the name of the student.
        /// </summary>
        private string _studentName;

        /// <summary>
        /// Represents the login username of the student.
        /// </summary>
        private string _studentLogin;

        /// <summary>
        /// Represents the password associated with the student's login.
        /// </summary>
        private string _studentPassword;

        /// <summary>
        /// Represents the foreign key referencing the group to which the student belongs.
        /// </summary>
        private int _groupFk;

        /// <summary>
        /// Represents the list of student data.
        /// </summary>
        private List<Student> _studentData;

        /// <summary>
        /// Gets or sets the list of student data.
        /// </summary>
        public List<Student> StudentData
        {
            get { return _studentData; }
            set
            {
                _studentData = value;
                OnPropertyChanged(nameof(StudentData));
            }
        }

        /// <summary>
        /// Loads student data from the database.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the student ID.
        /// </summary>
        public int StudentId
        {
            get => _studentId;
            set
            {
                _studentId = value;
                OnPropertyChanged(nameof(StudentId));
            }
        }

        /// <summary>
        /// Gets or sets the student name.
        /// </summary>
        public string StudentName
        {
            get => _studentName;
            set
            {
                _studentName = value;
                OnPropertyChanged(nameof(StudentName));
            }
        }

        /// <summary>
        /// Gets or sets the student login.
        /// </summary>
        public string StudentLogin
        {
            get => _studentLogin;
            set
            {
                _studentLogin = value;
                OnPropertyChanged(nameof(StudentLogin));
            }
        }

        /// <summary>
        /// Gets or sets the student password.
        /// </summary>
        public string StudentPassword
        {
            get => _studentPassword;
            set
            {
                _studentPassword = value;
                OnPropertyChanged(nameof(StudentPassword));
            }
        }

        /// <summary>
        /// Gets or sets the group foreign key.
        /// </summary>
        public int GroupFk
        {
            get => _groupFk;
            set
            {
                _groupFk = value;
                OnPropertyChanged(nameof(GroupFk));
            }
        }

        /// <summary>
        /// Event raised when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Invokes the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Adds a new student record to the database.
        /// </summary>
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

        /// <summary>
        /// Updates an existing student record in the database.
        /// </summary>
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

        /// <summary>
        /// Deletes a student record from the database.
        /// </summary>
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

        /// <summary>
        /// Searches for a student record in the database.
        /// </summary>
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

        /// <summary>
        /// Clears the input fields for student data.
        /// </summary>
        private void ClearStudentData()
        {
            StudentName = string.Empty;
            StudentLogin = string.Empty;
            StudentPassword = string.Empty;
            GroupFk = 0;
        }

        /// <summary>
        /// Validates input for adding or updating student records.
        /// </summary>
        /// <returns>True if input is valid, otherwise false.</returns>
        private bool ValidateInput()
        {
            return !string.IsNullOrWhiteSpace(StudentName) &&
                   !string.IsNullOrWhiteSpace(StudentLogin) &&
                   !string.IsNullOrWhiteSpace(StudentPassword);
        }
    }
}
