Exec sp_help Course_Info;
Select ZipCode,City,State from ZipCode_Info;
Select distinct(State) from ZipCode_Info;
Select StudentId,CONCAT(Student_First_Name,' ',Student_Last_Name) as Name from Student_Info;
Select Concat(zipcode,',',City,',',State) as Address from ZipCode_Info;
Select Course_Name from Course_Info;
Select Course_Name,Cost from Course_Info;
Select * from Course_Info;
Select Instructor_Last_Name,Zip_Code from Instructor_Info;
Select distinct(Zip_Code) from Student_Info;
Select Student_First_Name,Student_Last_Name from Student_Info;
Select Distinct(City),ZipCode,State from ZipCode_Info;

