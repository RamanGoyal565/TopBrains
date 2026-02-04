using System;

class Program
{
    public decimal Balance { get; private set; }

    public Program(decimal initialBalance)
    {
        Balance = initialBalance;
    }

    public void Deposit(decimal amount)
    {
        if (amount < 0)
            throw new Exception("Deposit amount cannot be negative");

        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount > Balance)
            throw new Exception("Insufficient funds.");

        Balance -= amount;
    }
}

class UnitTest
{
    public static void Test_Deposit_ValidAmount()
    {
        var account = new Program(100m);
        account.Deposit(50m);

        Console.WriteLine(account.Balance == 150m);
    }

    public static void Test_Deposit_NegativeAmount()
    {
        var account = new Program(100m);
        bool result;

        try
        {
            account.Deposit(-20m);
            result = false;
        }
        catch
        {
            result = true;
        }

        Console.WriteLine(result);
    }

    public static void Test_Withdraw_ValidAmount()
    {
        var account = new Program(100m);
        account.Withdraw(40m);

        Console.WriteLine(account.Balance == 60m);
    }

    public static void Test_Withdraw_InsufficientFunds()
    {
        var account = new Program(100m);
        bool result;

        try
        {
            account.Withdraw(150m);
            result = false;
        }
        catch
        {
            result = true;
        }

        Console.WriteLine(result);
    }
}

class MainProgram
{
    static void Main()
    {
        UnitTest.Test_Deposit_ValidAmount();
        UnitTest.Test_Deposit_NegativeAmount();
        UnitTest.Test_Withdraw_ValidAmount();
        UnitTest.Test_Withdraw_InsufficientFunds();
    }
}
