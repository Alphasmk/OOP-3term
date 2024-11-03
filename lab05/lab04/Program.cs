using System;
using System.Linq;

namespace lab05
{
    public enum AccountType
    {
        Сберегательный, Стандартный
    }

    public struct Person
    {
        public string FIO { get; set; }
        public int ID { get; set; }

        public string Address { get; set; }
        public Person(string fio, int id, string address)
        {
            FIO = fio;
            ID = id;
            Address = address;
        }

        public override string ToString()
        {
            return $"ФИО клиента: {FIO}, ID: {ID}";
        }
    }

    public class Bank
    {
        public List<Client> persons { get; set; } = new List<Client>();

        public void AddClient(Client client)
        {
            persons.Add(client);
        }

        public void DeleteClient(Client client)
        {
            if (persons.Contains(client))
            {
                persons.Remove(client);
            }
        }

        public void GetClientInfo(int id)
        {
            for(int i = 0; i < persons.Count; i++)
            {
                if (persons[i].Id == id)
                {
                    Console.WriteLine(persons[i].ToString());
                }
            }
        }

        public void PrintBankInfo()
        {
            int i = 1;
            foreach(Client client in persons)
            {
                Console.WriteLine($"{i}: {client.ToString()}");
                i++;
            }
        }

        public int ClientAccountSum()
        {
            int sum = 0;
            for (int i = 0; i < persons.Count; i++)
            {
                if (persons[i].Balance > 0 && !persons[i].isBlocked)
                {
                    sum += persons[i].Balance;
                }
            }
            return sum;
        }
        public void SortClientsByBalance()
        {
            var sortedClients = persons.OrderBy(client => client.Balance).ToList();

            Console.WriteLine("Список клиентов, отсортированный по балансу:");
            int i = 1;
            foreach (var client in sortedClients)
            {
                Console.WriteLine($"{i}: {client.FIO}, Баланс: {client.Balance}");
                i++;
            }
        }
    }

    public class BankController
    {
        private Bank bank;

        BankController(Bank bank)
        {
            this.bank = bank;
        }

        public int PositiveBalanceSum()
        {
            int sum = 0;
            for (int i = 0; i < bank.persons.Count; i++)
            {
                if (bank.persons[i].Balance > 0)
                {
                    sum += bank.persons[i].Balance;
                }
            }
            return sum;
        }

        public int NegativeBalanceSum()
        {
            int sum = 0;
            for (int i = 0; i < bank.persons.Count; i++)
            {
                if (bank.persons[i].Balance < 0)
                {
                    sum += bank.persons[i].Balance;
                }
            }
            return sum;
        }
    }
    public abstract partial class Client
    {
        public string FIO { get; set; }
        public string Address { get; set; }
        public int Balance { get; set; }
        public int Id { get; set; }

        public bool isBlocked = false;
    }

    public interface IAccount
    {
        void GetAccountType();
        void DepositMoney(int amount);
        void WithdrawMoney(int amount);
    }

    public class SavingsAccount : Client, IAccount
    {
        public SavingsAccount(string _FIO,  string _address, int _balance, int _id)
        {
            FIO = _FIO;
            Address = _address;
            Balance = _balance;
            Id = _id;
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
            if(isBlocked)
            {
                Console.WriteLine("Счет заблокирован, его невозможно пополнить");
            }
            else
            {
                Balance += amount;
                Console.WriteLine($"На счет добавлено {amount}, текущий баланс: {Balance}");
            }
        }
        public void WithdrawMoney(int amount)
        {
            if (isBlocked)
            {
                Console.WriteLine("Счет заблокирован, невозможно снять деньги");
            }
            else
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
    }

    public sealed class DefaultAccount : Client, IAccount
    {
        public DefaultAccount(string _FIO, string _address, int _balance, int _id)
        {
            FIO = _FIO;
            Address = _address;
            Balance = _balance;
            Id = _id;
        }

        public override void GetAccountType()
        {
            Console.WriteLine($"{AccountType.Стандартный} счет");
        }

        public void DepositMoney(int amount)
        {
            if (isBlocked)
            {
                Console.WriteLine("Счет заблокирован, его невозможно пополнить");
            }
            else
            {
                Balance += amount;
                Console.WriteLine($"На счет добавлено {amount}, текущий баланс: {Balance}");
            }
        }

        public void WithdrawMoney(int amount)
        {
            if (isBlocked)
            {
                Console.WriteLine("Счет заблокирован, невозможно снять деньги");
            }
            else
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
            Bank bank1 = new Bank();
            SavingsAccount newAcc = new SavingsAccount("Иван", "БГТУ", 100, 52);
            bank1.AddClient(newAcc);
            

            Console.WriteLine(newAcc.Info());
            newAcc.GetAccountType();
            newAcc.DepositMoney(100);
            newAcc.BlockAccount();
            newAcc.WithdrawMoney(20);
            newAcc.UnblockAccount();
            newAcc.WithdrawMoney(200);

            DefaultAccount newacc2 = new DefaultAccount("Иван", "БГТУ", 100, 52);
            bank1.AddClient(newacc2);
            DefaultAccount newacc3 = new DefaultAccount("Вадим", "БГТУ", 50, 78);
            bank1.AddClient(newacc3);

            newacc2.GetAccountType();
            newacc2.DepositMoney(100);
            newacc2.WithdrawMoney(20);

            IAccount test = new DefaultAccount("Андрей", "БГУиР", 20, 24);
            if (newAcc is IAccount)
            {
                Console.WriteLine("1 тест");
            }
            if (newacc2 is IAccount)
            {
                Console.WriteLine("2 тест");
            }
            if (test is IAccount)
            {
                Console.WriteLine("3 тест");
            }
            if ((newAcc as Client) != null)
            {
                Console.WriteLine("newAcc преобразован в Client");
            }

            var test1 = new DefaultAccount("Иван", "БГТУ", 100, 52);
            var test2 = new SavingsAccount("Андрей", "БГУиР", 20, 89);
            var test3 = new DefaultAccount("Пи", "Десткое масло", 52, 34);

            var Clients = new Client[3];
            var Printer = new Printer();

            Clients[0] = test1;
            Clients[1] = test2;
            Clients[2] = test3;

            foreach (Client client in Clients)
            {
                Printer.IAmPrinting(client);
            }

            IAccount test4 = new SavingsAccount("Иван", "БГТУ", 100, 52);
            test4.GetAccountType();
            Console.WriteLine("Банк:");
            bank1.PrintBankInfo();
            bank1.SortClientsByBalance();
            Console.WriteLine("Банк:");
            bank1.PrintBankInfo();
        }
    }

}