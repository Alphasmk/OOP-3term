using System;
using System.Collections.Generic;
using System.IO;

public interface ICollectionOperations<T>
{
    void Add(T item);
    void Remove(T item);
    void PrintAll();
    void SaveToFile(string filename);
    void ReadFromFile(string filename);
}
public class CollectionType<T> : ICollectionOperations<T> where T : class
{
    private List<T> items = new List<T>();

    public void Add(T item)
    {
        items.Add(item);
    }

    public void Remove(T item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
        }
        else
        {
            Console.WriteLine("Элемент не найден в коллекции.");
        }
    }

    public void PrintAll()
    {
        foreach (var item in items)
        {
            Console.WriteLine(item.ToString());
        }
    }

    public string ToString(List<T> listlist)
    {
        string result = "";
        foreach(T t in listlist)
        {
            result += t.ToString() + "\n";
        }
        return result;
    }
    public void SaveToFile(string filename)
    {
        if(!File.Exists(filename))
        {
            File.Create(filename);
        }
        File.WriteAllText(filename, ToString(items));
    }

    public void ReadFromFile(string filename)
    {
        if(!File.Exists(filename))
        {
            throw new FileNotFoundException("Файл не найден");
        }
        string result = File.ReadAllText(filename);
        Console.WriteLine(result);
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

public class NewStr
{
    private string str;

    public NewStr()
    {
        str = "";
    }

    public NewStr(string init)
    {
        str = init;
    }

    public int Length()
    {
        return str.Length;
    }

    public override string ToString()
    {
        return str;
    }
}

public class CollectionTypeSec<T> : ICollectionOperations<T> where T : Client
{
    private List<T> items = new List<T>();

    public void Add(T item)
    {
        items.Add(item);
    }

    public void Remove(T item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
        }
        else
        {
            Console.WriteLine("Элемент не найден в коллекции.");
        }
    }
    public void PrintAll()
    {
        foreach (var item in items)
        {
            Console.WriteLine(item.ToString());
        }
    }

    public string ToString(List<T> listlist)
    {
        string result = "";
        foreach (T t in listlist)
        {
            result += t.ToString() + "\n";
        }
        return result;
    }
    public void SaveToFile(string filename)
    {
        if (!File.Exists(filename))
        {
            File.Create(filename);
        }
        File.WriteAllText(filename, ToString(items));
    }

    public void ReadFromFile(string filename)
    {
        if (!File.Exists(filename))
        {
            throw new FileNotFoundException("Файл не найден");
        }
        string result = File.ReadAllText(filename);
        Console.WriteLine(result);
    }
}
class Program
{
    static void Main()
    {
        try
        {
            CollectionType<NewStr> collection = new CollectionType<NewStr>();

            collection.Add(new NewStr("Hello"));
            collection.Add(new NewStr("World"));

            collection.PrintAll();

            collection.SaveToFile("test.txt");
            collection.ReadFromFile("test.txt");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при обработке обобщения");
        }
        finally
        {
            Console.WriteLine("Программа выполнена");
        }

    }
};
