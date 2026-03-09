create table ZipCode_Info(
	ZipCode char(5),
	City nvarchar(10),
);
create table Instructor_Info(
	Instructor_ID numeric(8,0),
	Instructor_First_Name nvarchar(15),
	Instructor_Last_Name nvarchar(15)
);
create table Course_Info(
	Course_No numeric(8,0),
	Cost numeric(5,2)
);
create table Student_Info(
	Student_ID numeric(8,0),
	Student_First_Name nvarchar(15),
	Student_Last_Name nvarchar(15),
);
create table Section_Info(
	Section_ID numeric(8,0),
	Course_No numeric(8,0),
	Section_No numeric(5),
	Instructor_ID numeric(8,0),
);
create table Enrollment_Info(
	Student_ID numeric(8,0),
	Section_ID numeric(8,0),
);
create table Grade_Info(
	Student_ID numeric(8,0),
	Section_ID numeric(8,0),
	Grade_Type_Code char(2),
	Grade_Code_Occurrence numeric(5)
);