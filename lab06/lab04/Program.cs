using System;
using System.Diagnostics;
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
            for (int i = 0; i < persons.Count; i++)
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
            foreach (Client client in persons)
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
        private int balance;
        public int Balance
        {
            get => balance;
            set
            {
                if(value < 0)
                {
                    throw new MinusBalanceEx(value); 
                }
            }
        }
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
        public SavingsAccount(string _FIO, string _address, int _balance, int _id)
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
            if (isBlocked)
            {
                throw new BlockedEx(isBlocked);
            }
            Balance += amount;
            Console.WriteLine($"На счет добавлено {amount}, текущий баланс: {Balance}");
        }
        public void WithdrawMoney(int amount)
        {
            if (isBlocked)
            {
                throw new BlockedEx(isBlocked);
            }
            if (Balance == amount)
            {
                Balance = 0;
                Console.WriteLine($"Со счета снято {amount}, текущий баланс: {Balance}");
            }
            else
            {
                throw new SavingsEx(amount);
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
                throw new BlockedEx(isBlocked);
            }
            Balance += amount;
            Console.WriteLine($"На счет добавлено {amount}, текущий баланс: {Balance}");
        }

        public void WithdrawMoney(int amount)
        {
            if (isBlocked)
            {
                throw new BlockedEx(isBlocked);
            }
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

    public class MinusBalanceEx : Exception
    {
        public int Balance { get; }
        public MinusBalanceEx(int _balance) : base("Отрицательный баланс, невозможно снять деньги")
        {
            Balance = _balance;
        }
    }

    public class BlockedEx : Exception
    {
        public bool isBlocked { get; }
        public BlockedEx(bool _isBlocked) : base("Счет заблокирован")
        {
            isBlocked = _isBlocked;
        }
    }

    public class SavingsEx : Exception
    {
        public int Balance { get; }
        public SavingsEx(int balance) : base("Вы можете снять со сберегательного счета только всю сумму")
        {
            Balance = balance;
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
        static void Main(string[] args)
        {
            Bank bank1 = new Bank();
            SavingsAccount newAcc = new SavingsAccount("Иван", "БГТУ", 100, 52);
            bank1.AddClient(newAcc);

            try
            {
                newAcc.DepositMoney(100);
                newAcc.BlockAccount();
                newAcc.WithdrawMoney(20);
            }
            catch (SavingsEx ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message} - Баланс: {ex.Balance}");
            }
            catch (BlockedEx ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message} - Заблокирован: {ex.isBlocked}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Неизвестная ошибка: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Обработка завершена.");
            }

            try
            {
                // Проверка на отрицательный баланс при инициализации
                var test1 = new DefaultAccount("Иван", "БГТУ", -15, 52);
            }
            catch (MinusBalanceEx ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message} - Баланс: {ex.Balance}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании аккаунта: {ex.Message}");
            }
            SavingsAccount newAcc2 = new SavingsAccount("Иван", "БГТУ", 100, 52);
            Debug.Assert(false, "Проверка Assert");
        }
    }

}