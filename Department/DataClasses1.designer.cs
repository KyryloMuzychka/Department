﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DepartmentServer
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Department")]
	public partial class DataClasses1DataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertStudent(Student instance);
    partial void UpdateStudent(Student instance);
    partial void DeleteStudent(Student instance);
    partial void InsertTeacher(Teacher instance);
    partial void UpdateTeacher(Teacher instance);
    partial void DeleteTeacher(Teacher instance);
    partial void InsertStudentsGroup(StudentsGroup instance);
    partial void UpdateStudentsGroup(StudentsGroup instance);
    partial void DeleteStudentsGroup(StudentsGroup instance);
    #endregion
		
		public DataClasses1DataContext() : 
				base(global::DepartmentServer.Properties.Settings.Default.DepartmentConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataClasses1DataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Student> Students
		{
			get
			{
				return this.GetTable<Student>();
			}
		}
		
		public System.Data.Linq.Table<Teacher> Teachers
		{
			get
			{
				return this.GetTable<Teacher>();
			}
		}
		
		public System.Data.Linq.Table<StudentsGroup> StudentsGroups
		{
			get
			{
				return this.GetTable<StudentsGroup>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Student")]
	public partial class Student : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _student_id;
		
		private string _student_name;
		
		private string _student_login;
		
		private string _student_password;
		
		private System.Nullable<int> _group_fk;
		
		private EntityRef<StudentsGroup> _StudentsGroup;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void Onstudent_idChanging(int value);
    partial void Onstudent_idChanged();
    partial void Onstudent_nameChanging(string value);
    partial void Onstudent_nameChanged();
    partial void Onstudent_loginChanging(string value);
    partial void Onstudent_loginChanged();
    partial void Onstudent_passwordChanging(string value);
    partial void Onstudent_passwordChanged();
    partial void Ongroup_fkChanging(System.Nullable<int> value);
    partial void Ongroup_fkChanged();
    #endregion
		
		public Student()
		{
			this._StudentsGroup = default(EntityRef<StudentsGroup>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_student_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int student_id
		{
			get
			{
				return this._student_id;
			}
			set
			{
				if ((this._student_id != value))
				{
					this.Onstudent_idChanging(value);
					this.SendPropertyChanging();
					this._student_id = value;
					this.SendPropertyChanged("student_id");
					this.Onstudent_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_student_name", DbType="VarChar(100)")]
		public string student_name
		{
			get
			{
				return this._student_name;
			}
			set
			{
				if ((this._student_name != value))
				{
					this.Onstudent_nameChanging(value);
					this.SendPropertyChanging();
					this._student_name = value;
					this.SendPropertyChanged("student_name");
					this.Onstudent_nameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_student_login", DbType="VarChar(100)")]
		public string student_login
		{
			get
			{
				return this._student_login;
			}
			set
			{
				if ((this._student_login != value))
				{
					this.Onstudent_loginChanging(value);
					this.SendPropertyChanging();
					this._student_login = value;
					this.SendPropertyChanged("student_login");
					this.Onstudent_loginChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_student_password", DbType="VarChar(100)")]
		public string student_password
		{
			get
			{
				return this._student_password;
			}
			set
			{
				if ((this._student_password != value))
				{
					this.Onstudent_passwordChanging(value);
					this.SendPropertyChanging();
					this._student_password = value;
					this.SendPropertyChanged("student_password");
					this.Onstudent_passwordChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_group_fk", DbType="Int")]
		public System.Nullable<int> group_fk
		{
			get
			{
				return this._group_fk;
			}
			set
			{
				if ((this._group_fk != value))
				{
					if (this._StudentsGroup.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.Ongroup_fkChanging(value);
					this.SendPropertyChanging();
					this._group_fk = value;
					this.SendPropertyChanged("group_fk");
					this.Ongroup_fkChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="StudentsGroup_Student", Storage="_StudentsGroup", ThisKey="group_fk", OtherKey="group_id", IsForeignKey=true)]
		public StudentsGroup StudentsGroup
		{
			get
			{
				return this._StudentsGroup.Entity;
			}
			set
			{
				StudentsGroup previousValue = this._StudentsGroup.Entity;
				if (((previousValue != value) 
							|| (this._StudentsGroup.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._StudentsGroup.Entity = null;
						previousValue.Students.Remove(this);
					}
					this._StudentsGroup.Entity = value;
					if ((value != null))
					{
						value.Students.Add(this);
						this._group_fk = value.group_id;
					}
					else
					{
						this._group_fk = default(Nullable<int>);
					}
					this.SendPropertyChanged("StudentsGroup");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Teacher")]
	public partial class Teacher : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _teacher_id;
		
		private string _teacher_name;
		
		private string _teacher_login;
		
		private string _teacher_password;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void Onteacher_idChanging(int value);
    partial void Onteacher_idChanged();
    partial void Onteacher_nameChanging(string value);
    partial void Onteacher_nameChanged();
    partial void Onteacher_loginChanging(string value);
    partial void Onteacher_loginChanged();
    partial void Onteacher_passwordChanging(string value);
    partial void Onteacher_passwordChanged();
    #endregion
		
		public Teacher()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_teacher_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int teacher_id
		{
			get
			{
				return this._teacher_id;
			}
			set
			{
				if ((this._teacher_id != value))
				{
					this.Onteacher_idChanging(value);
					this.SendPropertyChanging();
					this._teacher_id = value;
					this.SendPropertyChanged("teacher_id");
					this.Onteacher_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_teacher_name", DbType="VarChar(100)")]
		public string teacher_name
		{
			get
			{
				return this._teacher_name;
			}
			set
			{
				if ((this._teacher_name != value))
				{
					this.Onteacher_nameChanging(value);
					this.SendPropertyChanging();
					this._teacher_name = value;
					this.SendPropertyChanged("teacher_name");
					this.Onteacher_nameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_teacher_login", DbType="VarChar(100)")]
		public string teacher_login
		{
			get
			{
				return this._teacher_login;
			}
			set
			{
				if ((this._teacher_login != value))
				{
					this.Onteacher_loginChanging(value);
					this.SendPropertyChanging();
					this._teacher_login = value;
					this.SendPropertyChanged("teacher_login");
					this.Onteacher_loginChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_teacher_password", DbType="VarChar(100)")]
		public string teacher_password
		{
			get
			{
				return this._teacher_password;
			}
			set
			{
				if ((this._teacher_password != value))
				{
					this.Onteacher_passwordChanging(value);
					this.SendPropertyChanging();
					this._teacher_password = value;
					this.SendPropertyChanged("teacher_password");
					this.Onteacher_passwordChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.StudentsGroup")]
	public partial class StudentsGroup : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _group_id;
		
		private string _group_name;
		
		private EntitySet<Student> _Students;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void Ongroup_idChanging(int value);
    partial void Ongroup_idChanged();
    partial void Ongroup_nameChanging(string value);
    partial void Ongroup_nameChanged();
    #endregion
		
		public StudentsGroup()
		{
			this._Students = new EntitySet<Student>(new Action<Student>(this.attach_Students), new Action<Student>(this.detach_Students));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_group_id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int group_id
		{
			get
			{
				return this._group_id;
			}
			set
			{
				if ((this._group_id != value))
				{
					this.Ongroup_idChanging(value);
					this.SendPropertyChanging();
					this._group_id = value;
					this.SendPropertyChanged("group_id");
					this.Ongroup_idChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_group_name", DbType="VarChar(20)")]
		public string group_name
		{
			get
			{
				return this._group_name;
			}
			set
			{
				if ((this._group_name != value))
				{
					this.Ongroup_nameChanging(value);
					this.SendPropertyChanging();
					this._group_name = value;
					this.SendPropertyChanged("group_name");
					this.Ongroup_nameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="StudentsGroup_Student", Storage="_Students", ThisKey="group_id", OtherKey="group_fk")]
		public EntitySet<Student> Students
		{
			get
			{
				return this._Students;
			}
			set
			{
				this._Students.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Students(Student entity)
		{
			this.SendPropertyChanging();
			entity.StudentsGroup = this;
		}
		
		private void detach_Students(Student entity)
		{
			this.SendPropertyChanging();
			entity.StudentsGroup = null;
		}
	}
}
#pragma warning restore 1591
