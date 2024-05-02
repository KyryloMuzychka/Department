using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace DepartmentServer
{
    /// <summary>
    /// View model class for managing teacher data.
    /// </summary>
    public class TeachersViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Database context for interacting with teacher data.
        /// </summary>
        private readonly DataClasses1DataContext db = new DataClasses1DataContext();

        /// <summary>
        /// Represents the ID of the teacher.
        /// </summary>
        private int _teacherId;

        /// <summary>
        /// Represents the name of the teacher.
        /// </summary>
        private string _teacherName;

        /// <summary>
        /// Represents the login username of the teacher.
        /// </summary>
        private string _teacherLogin;

        /// <summary>
        /// Represents the password associated with the teacher's login.
        /// </summary>
        private string _teacherPassword;

        /// <summary>
        /// Represents the list of teacher data.
        /// </summary>
        private List<Teacher> _teacherData;

        /// <summary>
        /// Gets or sets the list of teacher data.
        /// </summary>
        public List<Teacher> TeacherData
        {
            get { return _teacherData; }
            set
            {
                _teacherData = value;
                OnPropertyChanged(nameof(TeacherData));
            }
        }

        /// <summary>
        /// Loads teacher data from the database.
        /// </summary>
        public void LoadData()
        {
            try
            {
                using (var dbContext = new DataClasses1DataContext())
                {
                    // Fetch teacher data from the database
                    TeacherData = dbContext.Teachers.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading teacher data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Gets or sets the teacher ID.
        /// </summary>
        public int TeacherId
        {
            get => _teacherId;
            set
            {
                _teacherId = value;
                OnPropertyChanged(nameof(TeacherId));
            }
        }

        /// <summary>
        /// Gets or sets the teacher name.
        /// </summary>
        public string TeacherName
        {
            get => _teacherName;
            set
            {
                _teacherName = value;
                OnPropertyChanged(nameof(TeacherName));
            }
        }

        /// <summary>
        /// Gets or sets the teacher login.
        /// </summary>
        public string TeacherLogin
        {
            get => _teacherLogin;
            set
            {
                _teacherLogin = value;
                OnPropertyChanged(nameof(TeacherLogin));
            }
        }

        /// <summary>
        /// Gets or sets the teacher password.
        /// </summary>
        public string TeacherPassword
        {
            get => _teacherPassword;
            set
            {
                _teacherPassword = value;
                OnPropertyChanged(nameof(TeacherPassword));
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
        /// Adds a new teacher record to the database.
        /// </summary>
        public void AddTeacher()
        {
            if (ValidateInput())
            {
                var teacher = new Teacher
                {
                    teacher_name = TeacherName,
                    teacher_login = TeacherLogin,
                    teacher_password = TeacherPassword
                };

                db.Teachers.InsertOnSubmit(teacher);
                db.SubmitChanges();
                MessageBox.Show("New teacher has been added");
            }
            else
            {
                MessageBox.Show("Please provide valid input.");
            }
        }

        /// <summary>
        /// Updates an existing teacher record in the database.
        /// </summary>
        public void UpdateTeacher()
        {
            if (int.TryParse(TeacherId.ToString(), out int teacherId))
            {
                var teacher = db.Teachers.FirstOrDefault(t => t.teacher_id == teacherId);

                if (teacher != null)
                {
                    teacher.teacher_name = TeacherName;
                    teacher.teacher_login = TeacherLogin;
                    teacher.teacher_password = TeacherPassword;

                    db.SubmitChanges();
                    MessageBox.Show("Successfully Updated");
                }
                else
                {
                    MessageBox.Show("Teacher not found.");
                }
            }
            else
            {
                MessageBox.Show("Please provide a valid teacher ID.");
            }
        }

        /// <summary>
        /// Deletes a teacher record from the database.
        /// </summary>
        public void DeleteTeacher()
        {
            if (int.TryParse(TeacherId.ToString(), out int teacherId))
            {
                var teacher = db.Teachers.FirstOrDefault(t => t.teacher_id == teacherId);

                if (teacher != null)
                {
                    db.Teachers.DeleteOnSubmit(teacher);
                    db.SubmitChanges();
                    MessageBox.Show("Successfully Deleted");
                }
                else
                {
                    MessageBox.Show("Teacher not found.");
                }
            }
            else
            {
                MessageBox.Show("Please provide a valid teacher ID.");
            }
        }

        /// <summary>
        /// Searches for a teacher record in the database.
        /// </summary>
        public void SearchTeacher()
        {
            if (int.TryParse(TeacherId.ToString(), out int teacherId))
            {
                var teacher = db.Teachers.FirstOrDefault(t => t.teacher_id == teacherId);

                if (teacher != null)
                {
                    TeacherName = teacher.teacher_name;
                    TeacherLogin = teacher.teacher_login;
                    TeacherPassword = teacher.teacher_password;
                    MessageBox.Show("Teacher found.");
                }
                else
                {
                    MessageBox.Show("Teacher not found.");
                    ClearTeacherData();
                }
            }
            else
            {
                MessageBox.Show("Please provide a valid teacher ID.");
                ClearTeacherData();
            }
        }

        /// <summary>
        /// Clears the input fields for teacher data.
        /// </summary>
        private void ClearTeacherData()
        {
            TeacherName = string.Empty;
            TeacherLogin = string.Empty;
            TeacherPassword = string.Empty;
        }

        /// <summary>
        /// Validates input for adding or updating teacher records.
        /// </summary>
        /// <returns>True if input is valid, otherwise false.</returns>
        private bool ValidateInput()
        {
            return !string.IsNullOrWhiteSpace(TeacherName) &&
                   !string.IsNullOrWhiteSpace(TeacherLogin) &&
                   !string.IsNullOrWhiteSpace(TeacherPassword);
        }
    }
}
