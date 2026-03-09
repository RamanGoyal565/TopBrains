CREATE TABLE Customers
(
    CustomerID INT PRIMARY KEY,
    CustomerName VARCHAR(100),
    PhoneNumber VARCHAR(15),
    City VARCHAR(50),
    CreatedDate DATE
);
CREATE TABLE Accounts
(
    AccountID INT PRIMARY KEY,
    CustomerID INT,
    AccountNumber VARCHAR(20),
    AccountType VARCHAR(20), -- Savings / Current
    OpeningBalance DECIMAL(12,2),
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);
CREATE TABLE Transactions
(
    TransactionID INT PRIMARY KEY,
    AccountID INT,
    TransactionDate DATE,
    TransactionType VARCHAR(10), -- Deposit / Withdraw
    Amount DECIMAL(12,2),
    FOREIGN KEY (AccountID) REFERENCES Accounts(AccountID)
);
CREATE TABLE Bonus
(
    BonusID INT PRIMARY KEY,
    AccountID INT,
    BonusMonth INT,
    BonusYear INT,
    BonusAmount DECIMAL(10,2),
    CreatedDate DATE,
    FOREIGN KEY (AccountID) REFERENCES Accounts(AccountID)
);

INSERT INTO Customers VALUES
(1, 'Ravi Kumar', '9876543210', 'Chennai', '2023-01-10'),
(2, 'Priya Sharma', '9123456789', 'Bangalore', '2023-03-15'),
(3, 'John Peter', '9988776655', 'Hyderabad', '2023-06-20');

INSERT INTO Accounts VALUES
(101, 1, 'SB1001', 'Savings', 20000),
(102, 2, 'SB1002', 'Savings', 15000),
(103, 3, 'SB1003', 'Savings', 30000);

INSERT INTO Transactions VALUES
(1, 101, '2024-01-05', 'Deposit', 30000),
(2, 101, '2024-01-18', 'Withdraw', 5000),
(3, 101, '2024-02-10', 'Deposit', 25000),
(4, 102, '2024-01-07', 'Deposit', 20000),
(5, 102, '2024-01-25', 'Deposit', 35000),
(6, 102, '2024-02-05', 'Withdraw', 10000),
(7, 103, '2024-01-10', 'Deposit', 15000),
(8, 103, '2024-01-20', 'Withdraw', 5000);

Create Procedure USP_DepositWithdrawn
    @AccountID INT,
    @StartDate date,
    @EndDate date
    as
    begin
    Select TransactionType, SUM(Amount) as TotalAmount
    from Transactions
    where AccountID=@AccountID and TransactionDate between @StartDate and @EndDate
    group by TransactionType 
    end

Select * from Transactions;
Exec USP_DepositWithdrawn @AccountID=101, @StartDate='2024-01-01', @EndDate='2024-02-28'

Create Procedure USP_BonusPopulation
as
begin
    Insert into Bonus (AccountID, BonusMonth, BonusYear, BonusAmount, CreatedDate)
    Select 
        t.AccountID,
        MONTH(t.TransactionDate) as BonusMonth,
        YEAR(t.TransactionDate) as BonusYear,
        CASE 
            WHEN SUM(CASE WHEN t.TransactionType = 'Deposit' THEN t.Amount ELSE 0 END) > 50000 
            THEN 1000
            ELSE 0
        END as BonusAmount,
        GETDATE() as CreatedDate
    from Transactions t
    group by t.AccountID, MONTH(t.TransactionDate), YEAR(t.TransactionDate)
    having SUM(CASE WHEN t.TransactionType = 'Deposit' THEN t.Amount ELSE 0 END) > 50000
end

Exec USP_BonusPopulation
Select * from Bonus;

Create Procedure USP_CurrentBalance
as
begin
Select Customers.CustomerName,Accounts.AccountNumber,
(Accounts.OpeningBalance + ISNULL(SUM(CASE 
WHEN Transactions.TransactionType = 'Deposit'  THEN Transactions.Amount
WHEN Transactions.TransactionType = 'Withdraw' THEN -Transactions.Amount
ELSE 0 END), 0)
+ ISNULL(SUM(Bonus.BonusAmount), 0) ) as CurrentBalance
from Customers inner join Accounts on Customers.CustomerID=Accounts.CustomerID
left join Transactions on Accounts.AccountID=Transactions.AccountID
left join Bonus on Accounts.AccountID=Bonus.AccountID
group by Customers.CustomerName,Accounts.AccountNumber,Accounts.OpeningBalance
end;
exec USP_CurrentBalance;