using System;

public enum AccountType
{
    Сберегательный, Стандартный
}

public struct Bank
{
    public string Name;
    public string City;

    public Bank(string _Name, string _City)
    {
        Name = _Name;
        City = _City;
    }

    public override string ToString()
    {
        return $"Банк: {Name}, Расположение: {City}";
    }
}
public abstract class Client
{
    public string Name { get; set; }
    public string SecondName { get; set; }
    public string Address { get; set; }
    protected int Balance { get; set; }
    public virtual string Info()
    {
        return ($"Имя: {Name}, Фамилия: {SecondName}, Адрес: {Address}, Баланс: {Balance}");
    }

    public override string ToString()
    {
        return Info();
    }

    public abstract void GetAccountType();
}

public interface IAccount
{
    void GetAccountType();
    void DepositMoney(int amount);
    void WithdrawMoney(int amount);
}

public class SavingsAccount : Client, IAccount
{
    public SavingsAccount(string _name, string _secondName, string _address, int _balance)
    {
        Name = _name;
        SecondName = _secondName;
        Address = _address;
        Balance = _balance;
    }

    public override void GetAccountType()
    {
        Console.WriteLine($"{AccountType.Сберегательный} счет");
    }

    void IAccount.GetAccountType()
    {
        Console.WriteLine("Это счет");
    }
    public void DepositMoney(int amount)
    {
        Balance += amount;
        Console.WriteLine($"На счет добавлено {amount}, текущий баланс: {Balance}");
    }
    public void WithdrawMoney(int amount)
    {
        if (Balance == amount)
        {
            Balance = 0;
            Console.WriteLine($"Со счета снято {amount}, текущий баланс: {Balance}");
        }
        else
        {
            Console.WriteLine("Вы можете снять со сберегательного счета только всю сумму");
        }
    }
}

public sealed class DefaultAccount : Client, IAccount
{
    public DefaultAccount(string _name, string _secondName, string _address, int _balance)
    {
        Name = _name;
        SecondName = _secondName;
        Address = _address;
        Balance = _balance;
    }

    public override void GetAccountType()
    {
        Console.WriteLine($"{AccountType.Стандартный} счет");
    }

    public void DepositMoney(int amount)
    {
        Balance += amount;
        Console.WriteLine($"На счет добавлено {amount}, текущий баланс: {Balance}");
    }

    public void WithdrawMoney(int amount)
    {
        if (Balance >= amount)
        {
            Balance -= amount;
            Console.WriteLine($"Со счета снято {amount}, текущий баланс: {Balance}");
        }
        else
        {
            Console.WriteLine("Недостаточно средств на счете.");
        }
    }
}

class Printer
{
    public void IAmPrinting(Client client)
    {
        Console.WriteLine(client.Info());
    }
}
class Program
{
    static void Main(String[] args)
    {
        SavingsAccount newAcc = new SavingsAccount("Иван", "Иванов", "БГТУ", 100);

        Console.WriteLine(newAcc.Info());
        newAcc.GetAccountType();
        newAcc.DepositMoney(100);
        newAcc.WithdrawMoney(20);
        newAcc.WithdrawMoney(200);

        DefaultAccount newacc2 = new DefaultAccount("Иван", "Иванов", "БГТУ", 100);

        newacc2.GetAccountType();
        newacc2.DepositMoney(100);
        newacc2.WithdrawMoney(20);

        IAccount test = new DefaultAccount("Андрей", "Андреев", "БГУиР", 20);
        if (newAcc is IAccount)
        {
            Console.WriteLine("1 тест");
        }
        if (newacc2 is IAccount)
        {
            Console.WriteLine("2 тест");
        }
        if(test is IAccount)
        {
            Console.WriteLine("3 тест");
        }
        if ((newAcc as Client) != null)
        {
            Console.WriteLine("newAcc преобразован в Client");
        }

        var test1 = new DefaultAccount("Иван", "Иванов", "БГТУ", 100);
        var test2 = new SavingsAccount("Андрей", "Андреев", "БГУиР", 20);
        var test3 = new DefaultAccount("Пи", "Дидди", "Десткое масло", 52);

        var Clients = new Client[3];
        var Printer = new Printer();

        Clients[0] = test1;
        Clients[1] = test2;
        Clients[2] = test3;

        foreach (Client client in Clients)
        {
            Printer.IAmPrinting(client);
        }

        IAccount test4 = new SavingsAccount("Иван", "Иванов", "БГТУ", 100);
        test4.GetAccountType();

        Bank bank = new Bank("Беларусбанк", "Минск");
        Console.WriteLine(bank.ToString());
    }
}
