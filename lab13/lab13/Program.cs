using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text.Json;
using System.Xml.Serialization;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
using System.Text.Json.Serialization;

namespace lab13
{
    public interface IAccount
    {
        void GetAccountType();
        void DepositMoney(int amount);
        void WithdrawMoney(int amount);
    }

    [Serializable]
    [XmlRoot]
    public class SavingsAccount : IAccount
    {
        [XmlElement] public string Name { get; set; }
        [XmlElement] public string SecondName { get; set; }
        [XmlElement] public string Address { get; set; }
        [XmlIgnore] public int Balance { get; set; }

        public SavingsAccount() { }
        public SavingsAccount(string _name, string _secondName, string _address, int _balance)
        {
            Name = _name;
            SecondName = _secondName;
            Address = _address;
            Balance = _balance;
        }
        public virtual string Info()
        {
            return ($"Имя: {Name}, Фамилия: {SecondName}, Адрес: {Address}, Баланс: {Balance}");
        }

        public override string ToString()
        {
            return Info();
        }
        public void GetAccountType()
        {
            Console.WriteLine("Сберегательный счет");
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

    public interface ISerialize
    {
        void Serialize<T>(T obj, string fileName);
        T Deserialize<T>(string fileName);
    }

    public class BinSerializer : ISerialize
    {
        public void Serialize<T>(T obj, string fileName)
        {
#pragma warning disable SYSLIB0011 // Тип или член устарел
            BinaryFormatter bf = new BinaryFormatter();
#pragma warning restore SYSLIB0011 // Тип или член устарел
            using (Stream fStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                bf.Serialize(fStream, obj);
            }
        }

        public T Deserialize<T>(string fileName)
        {
#pragma warning disable SYSLIB0011 // Тип или член устарел
            BinaryFormatter bf = new BinaryFormatter();
#pragma warning restore SYSLIB0011 // Тип или член устарел
            using (Stream fStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                return (T)bf.Deserialize(fStream);
            }
        }
    }

    public class SoapSerializer : ISerialize
    {
        public void Serialize<T>(T obj, string fileName)
        {
            SoapFormatter soapFormatter = new SoapFormatter();
            using (Stream fStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                soapFormatter.Serialize(fStream, obj);
            }

        }
        public T Deserialize<T>(string fileName)
        {
            SoapFormatter soapFormatter = new SoapFormatter();
            using (Stream fStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read))
            {
                return (T)soapFormatter.Deserialize(fStream);
            }
        }
    }

    public class XMLSerializer : ISerialize
    {
        public void Serialize<T>(T obj, string fileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (Stream fStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                xmlSerializer.Serialize(fStream, obj);
            }
        }

        public T Deserialize<T>(string fileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (Stream fStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read))
            {
                return (T)xmlSerializer.Deserialize(fStream);
            }
        }
    }

    public class JSONSerializer : ISerialize
    {
        public void Serialize<T>(T obj, string fileName)
        {
            using (Stream fStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                JsonSerializer.Serialize(fStream, obj);
            }
        }

        public T Deserialize<T>(string fileName)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (Stream fStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Read))
            {
                return JsonSerializer.Deserialize<T>(fStream);
            }
        }
        class Program
        {
            static void Main(string[] args)
            {
                SavingsAccount client = new SavingsAccount("Герман", "Статько", "БГТУ", 22);
                string xmlPath = "XML.xml";
                string soapPath = "SOAP.xml";
                string binPath = "BIN.bin";
                string jsonPath = "JSON.json";
                try
                {
                    ISerialize xmlSerialize = new XMLSerializer();
                    xmlSerialize.Serialize(client, xmlPath);
                    ISerialize soapSerialize = new SoapSerializer();
                    soapSerialize.Serialize(client, soapPath);
                    ISerialize binSerialize = new BinSerializer();
                    binSerialize.Serialize(client, binPath);
                    ISerialize jsonSerialize = new JSONSerializer();
                    jsonSerialize.Serialize(client, jsonPath);

                    SavingsAccount test1;
                    test1 = xmlSerialize.Deserialize<SavingsAccount>(xmlPath);
                    Console.WriteLine(test1.ToString());
                    test1 = soapSerialize.Deserialize<SavingsAccount>(soapPath);
                    Console.WriteLine(test1.ToString());
                    test1 = binSerialize.Deserialize<SavingsAccount>(binPath);
                    Console.WriteLine(test1.ToString());
                    test1 = jsonSerialize.Deserialize<SavingsAccount>(jsonPath);
                    Console.WriteLine(test1.ToString());

                    Console.WriteLine();

                    List<SavingsAccount> list = new List<SavingsAccount>();
                    List<SavingsAccount> deserializeList = new List<SavingsAccount>();
                    SavingsAccount fst = new SavingsAccount("Герман", "Статько", "БГТУ", 12);
                    SavingsAccount snd = new SavingsAccount("Иван", "Иванов", "БГТУ", 42);
                    SavingsAccount trd = new SavingsAccount("Вадим", "Иванов", "БГТУ", 32);
                    list.Add(fst);
                    list.Add(snd);
                    list.Add(trd);
                    jsonSerialize.Serialize(list, "JSON2.json");
                    deserializeList = jsonSerialize.Deserialize<List<SavingsAccount>>("JSON2.json");
                    foreach (var item in deserializeList)
                    {
                        Console.WriteLine(item.ToString());
                    }

                    XElement clients = new XElement("Clients",
                new XElement("Client",
                    new XElement("Name", fst.Name),
                    new XElement("SecondName", fst.SecondName),
                    new XElement("Address", fst.Address),
                    new XElement("Balance", fst.Balance)
                ),
                new XElement("Client",
                    new XElement("Name", snd.Name),
                    new XElement("SecondName", snd.SecondName),
                    new XElement("Address", snd.Address),
                    new XElement("Balance", snd.Balance)
                ),
                new XElement("Client",
                    new XElement("Name", trd.Name),
                    new XElement("SecondName", trd.SecondName),
                    new XElement("Address", trd.Address),
                    new XElement("Balance", trd.Balance)
                )
            );
                    string xmlPath2 = "clients.xml";
                    clients.Save(xmlPath2);

                    var richClients = from _client in clients.Descendants("Client")
                                      where (int)_client.Element("Balance") > 30
                                      select new
                                      {
                                          Name = _client.Element("Name").Value,
                                          SecondName = _client.Element("SecondName").Value,
                                          Balance = _client.Element("Balance").Value
                                      };

                    Console.WriteLine("\nКлиенты с балансом > 30:");
                    foreach (var _client in richClients)
                    {
                        Console.WriteLine($"Name: {client.Name}, SecondName: {client.SecondName}, Balance: {client.Balance}");
                    }

                    XPathDocument document = new XPathDocument(xmlPath2);
                    XPathNavigator navigator = document.CreateNavigator();

                    XPathNodeIterator iterator = navigator.Select("//Client/Name");
                    Console.WriteLine("\nИмена всех клиентов:");
                    while (iterator.MoveNext())
                    {
                        Console.WriteLine(iterator.Current.Value);
                    }

                    iterator = navigator.Select("//Client[SecondName='Статько']");
                    Console.WriteLine("\nИнформация о клиентах с фамилией 'Статько':");
                    while (iterator.MoveNext())
                    {
                        Console.WriteLine(iterator.Current.Value);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}