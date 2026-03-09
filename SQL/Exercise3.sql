Alter table ZipCode_Info Add Constraint Zip_PK Primary Key (ZipCode);
Alter table zipcode_info alter column City VARCHAR(25) not null;
Alter table zipcode_info alter column State VARCHAR(25) not null;

Alter table Instructor_Info Alter Column InstructorID numeric(8,0) not null;
Alter table Instructor_Info Add Constraint Instructor_PK Primary Key (InstructorID);
Alter table Instructor_Info Alter Column Instructor_First_Name VARCHAR(25) not null;
Alter table Instructor_Info Alter Column Instructor_Last_Name VARCHAR(25) not null;
Alter table Instructor_Info Alter Column Zip_Code char(5);
Alter table Instructor_Info Add Constraint ZIP_Instructor_FK Foreign key(Zip_Code) references zipcode_info (ZipCode);


Alter table Course_Info Alter Column Course_No numeric(8,0) not null;
Alter table Course_Info Add Constraint Course_NO_PK Primary Key (Course_No);
Alter table Course_Info Alter Column Course_Name VARCHAR(50) not null;
Alter table Course_Info Alter Column Cost numeric(9,2) not null;

Alter table Student_Info Alter Column StudentID numeric(8,0) not null;
Alter table Student_Info Add Constraint Student_PK Primary Key (StudentID);
Alter table Student_Info Alter Column Student_First_Name VARCHAR(25) not null;
Alter table Student_Info Alter Column Student_Last_Name VARCHAR(25) not null;
Alter table Student_Info Alter Column Zip_Code char(5);
Alter table Student_Info Add Constraint ZIP_Student_FK Foreign key(Zip_Code) references zipcode_info (ZipCode);

Alter table Section_Info Alter Column Section_ID numeric(8,0) not null;
Alter table Section_Info Add Constraint Section_PK Primary Key (Section_ID);
Alter table Section_Info Alter Column Section_No numeric(8,0) not null;
Alter table Section_Info add Constraint Course_Section_FK Foreign key(Course_No) references Course_Info (Course_No);
Alter table Section_Info add Constraint Instructor_Section_FK Foreign key(Instructor_ID) references Instructor_Info (InstructorID);

Alter table Enrollment_Info Alter Column Student_ID numeric(8,0) not null;
Alter table Enrollment_Info Alter Column Section_ID numeric(8,0) not null;
Alter table Enrollment_Info Add Constraint Enrollment_Stud_Sect_PK Primary Key (Student_ID, Section_ID);
Alter table Enrollment_Info add Constraint Enrollment_Student_ID_FK Foreign key(Student_ID) references Student_Info (StudentID);
Alter table Enrollment_Info add Constraint Enrollment_Section_ID_FK Foreign key(Section_ID) references Section_Info (Section_ID);

Alter table Grade_Info Alter Column Student_ID numeric(8,0) not null;
Alter table Grade_Info Alter Column Section_ID numeric(8,0) not null;
Alter table Grade_Info Alter Column Grade_Code_Occurrence numeric(38,0) not null;
Alter table Grade_Info Alter Column Grade_Type_Code char(2) not null;
Alter table Grade_Info Add Constraint Grade_Stud_Sect_GradeCodeOcc_PK Primary Key (Student_ID, Section_ID, Grade_Type_Code, Grade_Code_Occurrence);
Alter table Grade_Info Alter Column Numeric_Grade numeric(3,0) not null;
ALTER TABLE Grade_Info ADD CONSTRAINT DF_Grade_Info_Numeric_Grade DEFAULT 0 FOR Numeric_Grade;
