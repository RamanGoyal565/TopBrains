CREATE TABLE Sales_Raw

(

    OrderID INT,

    OrderDate VARCHAR(20),

    CustomerName VARCHAR(100),

    CustomerPhone VARCHAR(20),

    CustomerCity VARCHAR(50),

    ProductNames VARCHAR(200),   -- Multiple products comma-separated

    Quantities VARCHAR(100),     -- Multiple quantities comma-separated

    UnitPrices VARCHAR(100),     -- Multiple prices comma-separated

    SalesPerson VARCHAR(100)

);

INSERT INTO Sales_Raw VALUES

(101, '2024-01-05', 'Ravi Kumar', '9876543210', 'Chennai',

 'Laptop,Mouse', '1,2', '55000,500', 'Anitha'),

 

(102, '2024-01-06', 'Priya Sharma', '9123456789', 'Bangalore',

 'Keyboard,Mouse', '1,1', '1500,500', 'Anitha'),

 

(103, '2024-01-10', 'Ravi Kumar', '9876543210', 'Chennai',

 'Laptop', '1', '54000', 'Suresh'),

 

(104, '2024-02-01', 'John Peter', '9988776655', 'Hyderabad',

 'Monitor,Mouse', '1,1', '12000,500', 'Anitha'),

 

(105, '2024-02-10', 'Priya Sharma', '9123456789', 'Bangalore',

 'Laptop,Keyboard', '1,1', '56000,1500', 'Suresh');

 select * from Sales_Raw;

CREATE TABLE [dbo].[CustomerMaster](
	[CustID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[PhoneNumber] [nvarchar](10) NOT NULL,
	[City] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_CustomerMaster] PRIMARY KEY CLUSTERED 
(
	[CustID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[ProductMaster](
	[PID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[UnitPrice] [float] NOT NULL,
 CONSTRAINT [PK_ProductMaster] PRIMARY KEY CLUSTERED 
(
	[PID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[SalesMaster](
	[SID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SalesMaster] PRIMARY KEY CLUSTERED 
(
	[SID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

CREATE TABLE [dbo].[OrderDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[OID] [int] NOT NULL,
	[PID] [int] NOT NULL,
	[Qty] [int] NOT NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_ProductMaster] FOREIGN KEY([PID])
REFERENCES [dbo].[ProductMaster] ([PID])
GO

ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_ProductMaster]

CREATE TABLE [dbo].[CustomerOrderDetails](
	[OID] [int] NOT NULL,
	[CID] [int] NOT NULL,
	[SID] [int] NOT NULL,
	[Date] [date] NOT NULL,
 CONSTRAINT [PK_CustomerOrderDetails] PRIMARY KEY CLUSTERED 
(
	[OID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CustomerOrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_CustomerOrderDetails_CustomerMaster] FOREIGN KEY([CID])
REFERENCES [dbo].[CustomerMaster] ([CustID])
GO

ALTER TABLE [dbo].[CustomerOrderDetails] CHECK CONSTRAINT [FK_CustomerOrderDetails_CustomerMaster]
GO

ALTER TABLE [dbo].[CustomerOrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_CustomerOrderDetails_SalesMaster] FOREIGN KEY([SID])
REFERENCES [dbo].[SalesMaster] ([SID])
GO

ALTER TABLE [dbo].[CustomerOrderDetails] CHECK CONSTRAINT [FK_CustomerOrderDetails_SalesMaster]
GO

create procedure dbo.PopulateSalesData
as
begin
----CustomerMaster population
INSERT INTO CustomerMaster (CustID, Name, PhoneNumber, City)
SELECT
    DENSE_RANK() OVER (ORDER BY CustomerPhone) AS CustID,
    CustomerName,
    CustomerPhone,
    CustomerCity
FROM Sales_Raw
GROUP BY CustomerName, CustomerPhone, CustomerCity;
Select * from CustomerMaster;

---SalesMaster population
Insert into	SalesMaster(SID,Name)
select 
	DENSE_RANK() OVER(ORDER BY SalesPerson) AS SID,
	SalesPerson
	from Sales_Raw group By(SalesPerson);
Select * from SalesMaster;

---ProductMaster population
WITH ProductSplit AS (
    SELECT
        TRIM(p.value) AS ProductName,
        CAST(pr.value AS FLOAT) AS UnitPrice
    FROM Sales_Raw sr
    CROSS APPLY STRING_SPLIT(sr.ProductNames, ',', 1) p
    CROSS APPLY STRING_SPLIT(sr.UnitPrices, ',', 1) pr
    WHERE p.ordinal = pr.ordinal
)
INSERT INTO ProductMaster (PID, Name, UnitPrice)
SELECT
    DENSE_RANK() OVER (ORDER BY ProductName, UnitPrice) AS PID,
    ProductName,
    UnitPrice
FROM ProductSplit
GROUP BY ProductName, UnitPrice;
select * from ProductMaster;

---CustomerOrderDetails population
INSERT INTO CustomerOrderDetails (OID, CID, SID, Date)
SELECT
    sr.OrderID,
    cm.CustID,
    sm.SID,
    CAST(sr.OrderDate AS DATE)
FROM Sales_Raw sr
inner JOIN CustomerMaster cm
    ON sr.CustomerPhone = cm.PhoneNumber
inner JOIN SalesMaster sm
    ON sr.SalesPerson = sm.Name;
Select * from CustomerOrderDetails;

---orderdetails population
WITH OrderSplit AS (
    SELECT
        sr.OrderID AS OID,
        TRIM(p.value) AS ProductName,
        CAST(pr.value AS FLOAT) AS UnitPrice,
        CAST(q.value AS INT) AS Qty
    FROM Sales_Raw sr
    CROSS APPLY STRING_SPLIT(sr.ProductNames, ',', 1) p
    CROSS APPLY STRING_SPLIT(sr.UnitPrices, ',', 1) pr
    CROSS APPLY STRING_SPLIT(sr.Quantities, ',', 1) q
    WHERE p.ordinal = pr.ordinal
      AND p.ordinal = q.ordinal
)
INSERT INTO OrderDetails (OID, PID, Qty)
SELECT
    os.OID,
    pm.PID,
    os.Qty
FROM OrderSplit os
inner JOIN ProductMaster pm
    ON pm.Name = os.ProductName
   AND pm.UnitPrice = os.UnitPrice;

select * from OrderDetails;
end
exec dbo.PopulateSalesData;

---Third Highest Total Sales (Analytical Query)
with OrderSales as(
select 
    od.OID,
    sum(od.Qty * pm.UnitPrice) as TotalAmount
    from OrderDetails od
    inner join ProductMaster pm
    on od.PID=pm.PID
    group by od.OID
)
Select TotalAmount from OrderSales
order by TotalAmount desc
offset 2 rows
fetch next 1 row only;

---Write a query to list SalesPerson names whose total sales amount is greater than ?60,000.
with SalesRevenue as(
select 
    sm.Name as SalesPerson,
    sum(od.Qty * pm.UnitPrice) as TotalRevenue
    from OrderDetails od
    inner join ProductMaster pm
    on od.PID=pm.PID
    inner join CustomerOrderDetails cod
    on od.OID=cod.OID
    inner join SalesMaster sm
    on cod.SID=sm.SID
    group by sm.Name
    having sum(od.Qty * pm.UnitPrice)>60000
)
select * from SalesRevenue;

---The company wants to find customers who spent more than the average customer spending.
Select 
    cm.Name as CustomerName,
    sum(od.Qty * pm.UnitPrice) as TotalSpent
    from OrderDetails od
    inner join ProductMaster pm
    on od.PID=pm.PID
    inner join CustomerOrderDetails cod
    on od.OID=cod.OID
    inner join CustomerMaster cm
    on cod.CID=cm.CustID
    group by cm.Name having sum(od.Qty * pm.UnitPrice)>
    (SELECT AVG(CustomerTotal)
        FROM (
            SELECT
                SUM(od2.Qty * pm2.UnitPrice) AS CustomerTotal
            FROM CustomerMaster c2
            inner JOIN CustomerOrderDetails cod2
                ON c2.CustID = cod2.CID
            inner JOIN OrderDetails od2
                ON cod2.OID = od2.OID
            inner JOIN ProductMaster pm2
                ON od2.PID = pm2.PID
            GROUP BY c2.CustID) AvgCalc);

---String & Date Functions
Select cm.name,Month(cod.date) as OrderMonth
from CustomerMaster cm join
CustomerOrderDetails cod on
cm.CustID=cod.CID
where cod.Date between '2024-01-01' and '2024-01-31';