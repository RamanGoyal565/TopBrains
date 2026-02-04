Alter table ZipCode_Info Add Constraint Zip_PK Primary Key (ZipCode);
Alter table zipcode_info alter column City VARCHAR(25) not null;
Alter table zipcode_info alter column State VARCHAR(25) not null;