using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace DepartmentServer
{
    /// <summary>
    /// Represents the view model for managing groups.
    /// </summary>
    public class GroupsViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Database context for interacting with group data.
        /// </summary>
        private readonly DataClasses1DataContext db = new DataClasses1DataContext();

        /// <summary>
        /// Represents the ID of the group.
        /// </summary>
        private int _groupId;

        /// <summary>
        /// Represents the name of the group.
        /// </summary>
        private string _groupName;

        /// <summary>
        /// Represents the list of group data.
        /// </summary>
        private List<StudentsGroup> _groupData;

        /// <summary>
        /// Gets or sets the list of groups.
        /// </summary>
        public List<StudentsGroup> GroupData
        {
            get { return _groupData; }
            set
            {
                _groupData = value;
                OnPropertyChanged(nameof(GroupData));
            }
        }

        /// <summary>
        /// Loads group data from the database.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the group ID.
        /// </summary>
        public int GroupId
        {
            get => _groupId;
            set
            {
                _groupId = value;
                OnPropertyChanged(nameof(GroupId));
            }
        }

        /// <summary>
        /// Gets or sets the group name.
        /// </summary>
        public string GroupName
        {
            get => _groupName;
            set
            {
                _groupName = value;
                OnPropertyChanged(nameof(GroupName));
            }
        }

        /// <summary>
        /// Event that is raised when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            // Invoke the PropertyChanged event with the name of the property that has changed
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Adds a new group.
        /// </summary>
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

        /// <summary>
        /// Updates an existing group.
        /// </summary>
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

        /// <summary>
        /// Deletes a group.
        /// </summary>
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

        /// <summary>
        /// Searches for a group by ID.
        /// </summary>
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

        /// <summary>
        /// Validates the input by checking if the group name is not null, empty, or contains only whitespace characters.
        /// </summary>
        /// <returns>True if the input is valid; otherwise, false.</returns>
        private bool ValidateInput()
        {
            return !string.IsNullOrWhiteSpace(GroupName);
        }
    }
}
