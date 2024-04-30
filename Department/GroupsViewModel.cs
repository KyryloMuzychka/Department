using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace DepartmentServer
{
    public class GroupsViewModel : INotifyPropertyChanged
    {
        private readonly DataClasses1DataContext db = new DataClasses1DataContext();
        private int _groupId;
        private string _groupName;        

        private List<StudentsGroup> _groupData;
        public List<StudentsGroup> GroupData
        {
            get { return _groupData; }
            set
            {
                _groupData = value;
                OnPropertyChanged(nameof(GroupData));
            }
        }

        public void LoadData()
        {
            try
            {
                using (var dbContext = new DataClasses1DataContext())
                {
                    // Fetch group data from the database
                    GroupData = dbContext.StudentsGroups.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading group data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public int GroupId
        {
            get => _groupId;
            set
            {
                _groupId = value;
                OnPropertyChanged(nameof(GroupId));
            }
        }

        public string GroupName
        {
            get => _groupName;
            set
            {
                _groupName = value;
                OnPropertyChanged(nameof(GroupName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddGroup()
        {
            if (ValidateInput())
            {
                var group = new StudentsGroup
                {
                    group_name = GroupName
                };

                db.StudentsGroups.InsertOnSubmit(group);
                db.SubmitChanges();
                MessageBox.Show("New group has been added");
            }
            else
            {
                MessageBox.Show("Please provide valid input.");
            }
        }

        public void UpdateGroup()
        {
            if (int.TryParse(GroupId.ToString(), out int groupId))
            {
                var group = db.StudentsGroups.FirstOrDefault(g => g.group_id == groupId);

                if (group != null)
                {
                    group.group_name = GroupName;

                    db.SubmitChanges();
                    MessageBox.Show("Successfully Updated");
                }
                else
                {
                    MessageBox.Show("Group not found.");
                }
            }
            else
            {
                MessageBox.Show("Please provide a valid group ID.");
            }
        }

        public void DeleteGroup()
        {
            if (int.TryParse(GroupId.ToString(), out int groupId))
            {
                var group = db.StudentsGroups.FirstOrDefault(g => g.group_id == groupId);

                if (group != null)
                {
                    db.StudentsGroups.DeleteOnSubmit(group);
                    db.SubmitChanges();
                    MessageBox.Show("Successfully Deleted");
                }
                else
                {
                    MessageBox.Show("Group not found.");
                }
            }
            else
            {
                MessageBox.Show("Please provide a valid group ID.");
            }
        }

        public void SearchGroup()
        {
            if (GroupId != 0)
            {
                var group = db.StudentsGroups.FirstOrDefault(g => g.group_id == GroupId);

                if (group != null)
                {
                    GroupName = group.group_name;
                    MessageBox.Show("Group found.");
                }
                else
                {
                    MessageBox.Show("Group not found.");
                }
            }
            else
            {
                MessageBox.Show("Please provide a group ID to search.");
            }
        }

        private bool ValidateInput()
        {
            return !string.IsNullOrWhiteSpace(GroupName);
        }
    }
}
