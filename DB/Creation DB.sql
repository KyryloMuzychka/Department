CREATE DATABASE Department;

USE Department;

CREATE TABLE StudentsGroup(
	group_id INT PRIMARY KEY IDENTITY(1, 1),
	group_name VARCHAR(20)
);

CREATE TABLE Student(
	student_id INT PRIMARY KEY IDENTITY(1, 1),
	student_name VARCHAR(100),
	student_login VARCHAR(100),
	student_password VARCHAR(100),
	group_fk INT,
	FOREIGN KEY(group_fk) REFERENCES StudentsGroup(group_id)
);

CREATE TABLE Teacher(
	teacher_id INT PRIMARY KEY IDENTITY(1, 1),
	teacher_name VARCHAR(100),
	teacher_login VARCHAR(100),
	teacher_password VARCHAR(100)
);

INSERT INTO StudentsGroup (group_name) 
VALUES 
('PZ-21-1d'),
('PM-22-1d'),
('PZ-20-1d');

INSERT INTO Student (student_name, student_login, student_password, group_fk) 
VALUES 
('John Doe', 'john_doe', 'password123', 1),
('Jane Smith', 'jane_smith', 'abc123', 1),
('Alice Johnson', 'alice_johnson', 'qwerty', 2),
('Bob Anderson', 'bob_anderson', 'letmein', 2),
('Emily Brown', 'emily_brown', 'ilovecoding', 2),
('Michael Clark', 'michael_clark', 'coding123', 2),
('Sarah Davis', 'sarah_davis', 'studentpass', 2),
('Daniel Evans', 'daniel_evans', 'securepwd', 3),
('Grace Garcia', 'grace_garcia', 'grace123', 3),
('Henry Hall', 'henry_hall', 'hall456', 3);

INSERT INTO Teacher (teacher_name, teacher_login, teacher_password) 
VALUES 
('Professor X', 'prof_x', 'supersecure'),
('Dr. Y', 'dr_y', 'topsecret'),
('Mrs. Z', 'mrs_z', 'password123');

SELECT * FROM Teacher;
SELECT * FROM StudentsGroup;
SELECT * FROM Student;

GO
CREATE PROCEDURE GetGroupMemberCount
    @group_id INT
AS
BEGIN
    SELECT COUNT(*) AS member_count
    FROM Student
    WHERE group_fk = @group_id;
END;
GO

EXEC GetGroupMemberCount @group_id = 3;
