Alter Table ZipCode_Info Add State varchar(2);
Alter Table ZipCode_Info Alter column City varchar(25);

Alter Table Instructor_Info Alter column Instructor_First_Name nvarchar(25);
Alter Table Instructor_Info Alter column Instructor_Last_Name nvarchar(25);
Alter Table Instructor_Info add Street_Adress nvarchar(50);
Alter Table Instructor_Info add Zip_Code nvarchar(5);

Alter Table Course_Info Alter column Cost numeric(9,2);
Alter Table Course_Info add Course_Name nvarchar(50);
Alter Table Course_Info add Course_Prerequisite numeric(8,0);

Alter Table Student_Info Alter column Student_First_Name nvarchar(25);
Alter Table Student_Info Alter column Student_Last_Name nvarchar(25);
Alter table student_info add Street_Address nvarchar(50);
Alter table student_info add Zip_Code nvarchar(5);

Alter Table Section_Info Alter column Section_No numeric(3,0);
Alter table section_info add Location nvarchar(50);
Alter table section_info add Capacity numeric(3,0);

Alter table Enrollment_Info add Enrollment_Date date;

Alter table Grade_Info add Numeric_Grade numeric(3,0);
Alter table Grade_Info alter column Grade_Code_Occurrence numeric(38,0);
