using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DepartmentServer
{
    public class TeachersViewModel : INotifyPropertyChanged
    {
        private readonly DataClasses1DataContext db = new DataClasses1DataContext();
        private int _teacherId;
        private string _teacherName;
        private string _teacherLogin;
        private string _teacherPassword;

        private List<Teacher> _teacherData;
        public List<Teacher> TeacherData
        {
            get { return _teacherData; }
            set
            {
                _teacherData = value;
                OnPropertyChanged(nameof(TeacherData));
            }
        }

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

        public int TeacherId
        {
            get => _teacherId;
            set
            {
                _teacherId = value;
                OnPropertyChanged(nameof(TeacherId));
            }
        }

        public string TeacherName
        {
            get => _teacherName;
            set
            {
                _teacherName = value;
                OnPropertyChanged(nameof(TeacherName));
            }
        }

        public string TeacherLogin
        {
            get => _teacherLogin;
            set
            {
                _teacherLogin = value;
                OnPropertyChanged(nameof(TeacherLogin));
            }
        }

        public string TeacherPassword
        {
            get => _teacherPassword;
            set
            {
                _teacherPassword = value;
                OnPropertyChanged(nameof(TeacherPassword));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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
        
        private void ClearTeacherData()
        {
            TeacherName = string.Empty;
            TeacherLogin = string.Empty;
            TeacherPassword = string.Empty;
        }


        private bool ValidateInput()
        {
            return !string.IsNullOrWhiteSpace(TeacherName) &&
                   !string.IsNullOrWhiteSpace(TeacherLogin) &&
                   !string.IsNullOrWhiteSpace(TeacherPassword);
        }
    }
}
