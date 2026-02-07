/*
Question 1 – Normalization (Foundation)

You are given the following table used by a legacy application:

EmployeeSalesRaw
----------------------------------------------------
EmpId
EmpName
Department
Email
SaleMonth
SaleYear
SaleAmount


Tasks:

Identify normalization issues in the above table.

Normalize the structure up to 3NF.

Create appropriate tables with:

Primary keys

Foreign key relationships

Tables should at least include: Employee, Department, Sales.

Question 2 – ALTER TABLE (Business Change Request)

After reviewing Question 1, the business says:

“We want to track employee performance points.”

Tasks:

Use ALTER TABLE to add a column called BonusPoints to the Employee table.

Set default value as 0.

⚠️ This question depends on Question 1.

Question 3 – CHECK Constraint (Mid-Requirement Change)

Business rule update:

“Bonus points must always be between 0 and 100.”

Tasks:

Add a CHECK constraint on BonusPoints.

Ensure invalid values cannot be inserted or updated.

Question 4 – INNER JOIN (Mandatory)

Write a query to display:

Employee Name

Department Name

Sale Month

Sale Year

Sale Amount

Only include employees who have made at least one sale.

Use INNER JOIN only.

Question 5 – Date Function (Mandatory)

Write a query to calculate total sales for each employee for the current year.

Requirements:

Use SQL Server date functions

Do not hardcode the year

Question 6 – SUBSTRING and LEFT (Mandatory)

The business wants username suggestions.

Rule:

First 3 characters of employee name

First 2 characters of department name

Append employee ID

Example:

Marimuthu + IT + 101 → MarIT101


Tasks:

Generate the username using SUBSTRING and LEFT.

Display Employee Name and Generated Username.

Question 7 – Subquery (Mandatory)

Write a query to find employees whose total sales amount is greater than the average sales amount of all employees.

Rules:

Use a subquery

No joins in the outer query for calculation

Question 8 – UNION (Mandatory)

The business wants a consolidated report:

Employees who made sales above ₹50,000

Employees who made sales below ₹10,000

Tasks:

Write two separate SELECT queries.

Combine them using UNION.

Display: Employee Name, Sale Amount, Category (High / Low)

Question 9 – Trigger (Mandatory, Real-World Logic)

Business rule:

“Whenever a sale is inserted, automatically update BonusPoints.”

Rule:

If SaleAmount ≥ 50,000 → add 10 points

If SaleAmount ≥ 20,000 → add 5 points

Else → no bonus

Tasks:

Create an AFTER INSERT trigger on Sales table.

Ensure BonusPoints updates automatically.

Question 10 – Integrated Validation Query (Final)

Write a single query that shows:

Employee Name

Department

Total Sales

BonusPoints

Performance Grade

A → BonusPoints ≥ 50

B → BonusPoints between 20 and 49

C → Below 20

This question validates whether:

Normalization is correct

Trigger worked

Aggregations are correct
*/

Create table EmployeeSalesRaw(
EmpId int,
EmpName nvarchar(50),
Department nvarchar(50),
Email nvarchar(100),
SaleMonth nvarchar(50),
SaleYear nvarchar(50),
SaleAmount float
)

---1
create table Employee(
EmpId int not null Primary Key,
EmpName nvarchar(50) not null,
DeptId int not null,
Email nvarchar(100) not null);  

create table Department(
DeptId int not null Primary Key,
DepartmentName nvarchar(50) not null)

create table Sales(
SalesId int not null Primary Key,
SalesMonth int not null,
SalesYear int not null,
SalesAmount float not null,
EmpId int)

Alter table Sales add Constraint FK_Sales_Employee Foreign Key(EmpId) references Employee(EmpId); 
Alter table Employee add Constraint FK_Employee_Depatment Foreign key(DeptId) references Department(DeptId);

---2
Alter table Employee add BonusPoints int;
Alter table Employee add Constraint DF_Employee_Bonus default 0 for BonusPoints;

---3
Alter table Employee add Constraint Validtion Check (BonusPoints between 0 and 100);

---4
Select Employee.EmpName,Department.DepartmentName,Sales.SalesMonth,Sales.SalesYear,Sales.SalesAmount
from Employee inner join Department on Employee.DeptId=Department.DeptId 
inner join Sales on Employee.EmpId=Sales.EmpId;

---5
Select sum(SalesAmount) from Sales where SalesYear=Year(GetDate());

---6
Select Employee.EmpName,CONCAT(substring(Employee.EmpName,0,4),left(Department.DepartmentName,2),Employee.EmpId)
from Employee inner join Department on Employee.DeptId=Department.DeptId;

---7
Select Avg(SalesAmount) from Sales;
Select Employee.EmpName,sum(Sales.SalesAmount) as TotalSales from Employee inner join Sales on Employee.EmpId=Sales.EmpId
group by Employee.EmpName having sum(Sales.SalesAmount)>(Select Avg(SalesAmount) from Sales)

---8
Select Employee.EmpName,Sales.SalesAmount,'High' as Category from Employee inner join Sales on Employee.EmpId=Sales.EmpId
where Sales.SalesAmount>50000 
union 
Select Employee.EmpName,Sales.SalesAmount,'Low' as Category from Employee inner join Sales on Employee.EmpId=Sales.EmpId
where Sales.SalesAmount<10000;

---9
Create Trigger UpdateBonus
on Sales
After insert
as
begin
 UPDATE Employee
    SET Employee.BonusPoints = Employee.BonusPoints + x.BonusPoints
    FROM Employee
    INNER JOIN (
        SELECT 
            EmpId,
            SUM(
                CASE 
                    WHEN SalesAmount >= 50000 THEN 10
                    WHEN SalesAmount >= 20000 THEN 5
                    ELSE 0
                END
            ) AS BonusPoints
        FROM inserted
        GROUP BY EmpId
    ) x ON Employee.EmpId = x.EmpId;
end

---10
Select Employee.EmpName,Department.DepartmentName,Sum(Sales.SalesAmount) as TotalSales,Employee.BonusPoints,
Case
when Employee.BonusPoints>=50 then 'A'
when Employee.BonusPoints>=20 then 'B'
else 'C'
end as Performance from Employee inner join Department on Employee.DeptId=Department.DeptId
inner join Sales on Employee.EmpId=Sales.EmpId
group by
Employee.EmpName, 
Department.DepartmentName, 
Employee.BonusPoints;


insert into Department
(DeptId,DepartmentName)
values
(1,'IT'),
(2,'Sales'),
(3,'HR')

insert into Employee(EmpId,EmpName,Email,DeptId) values
(101, 'Marimuthu','marimuthu@company.com',1),
(102, 'Ananya','ananya@company.com',2),
(103, 'Rahul','rahul@company.com',2),
(104, 'Sneha','ananya@company.com',3),
(105, 'Karthik','rahul@company.com',1)

insert into Sales(EmpId,SalesId,SalesMonth,SalesYear,SalesAmount) values
(101, 1,1, 2026, 60000),
(101, 2,2, 2026, 25000),
(102, 3,1, 2026, 8000),
(102, 4,3, 2026, 12000),
(103, 5,1, 2026, 45000),
(103, 6,2, 2026, 55000),
(104, 7,12, 2025, 5000),
(105, 8,11, 2025, 30000);

insert into Sales(EmpId,SalesId,SalesMonth,SalesYear,SalesAmount) values
(103,9,10,2025,55000),
(103,10,1,2026,60000),
(103,11,1,2026,65000),
(103,12,2,2025,57000),
(101,13,2,2026,60000),(101,14,10,2025,60000),(101,15,8,2025,50000)