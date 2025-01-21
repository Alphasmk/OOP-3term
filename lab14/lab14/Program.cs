using System;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace lab14
{
    public static class Controller
    {
        public static void GetProc(string logPath)
        {
            Process[] allProc = Process.GetProcesses();
            int i = 1;
            using (StreamWriter file = new StreamWriter(logPath))
            {
                foreach (Process proc in allProc)
                {
                    file.WriteLine("Процесс " + i + ":");
                    file.WriteLine("Имя: " + proc.ProcessName);
                    file.WriteLine("ID: " + proc.Id);
                    file.WriteLine("Приоритет " + proc.BasePriority);
                    file.WriteLine("Объем памяти: " + proc.PagedMemorySize64);
                    file.WriteLine("Объем виртуальной памяти: " + proc.VirtualMemorySize64);
                    try
                    {
                        file.WriteLine("Сколько времени использовал процессор: " + proc.TotalProcessorTime);
                    }
                    catch (Exception ex)
                    {
                        file.WriteLine("Сколько времени использовал процессор: Доступ запрещен или процесс завершен.");
                    }

                    try
                    {
                        file.WriteLine("Время запуска: " + proc.StartTime);
                    }
                    catch (Exception ex)
                    {
                        file.WriteLine("Время запуска: Доступ запрещен или процесс завершен.");
                    }
                    file.Write("\n");
                    i++;
                }
            }
        }
    }

    class Program
    {
        public static Mutex mutex = new Mutex();
        public static Mutex mutex2 = new Mutex();
        public static int n;
        private static bool isEvenTurn = true;

        static void Main(string[] args)
        {
            TimerCallback tm = new TimerCallback(printTime);
            Timer timer = new Timer(tm, 0, 0, 8000);
            Controller.GetProc("log.txt");
            Console.WriteLine("Имя домена: \n" + AppDomain.CurrentDomain.FriendlyName);
            Console.WriteLine("Конфигурация: \n" + AppDomain.CurrentDomain.SetupInformation);
            Assembly[] assembly;
            assembly = AppDomain.CurrentDomain.GetAssemblies();
            Console.WriteLine("Сборки: ");
            foreach (Assembly a in assembly)
            {
                Console.WriteLine(a.FullName);
            }
            //AppDomain domain = AppDomain.CreateDomain("Test");
            //string assemblyPath = "E:\\unic\\ООП\\lab14\\lab14\\bin\\Debug\\net8.0\\lab14.dll";
            //try
            //{
            //    domain.Load(AssemblyName.GetAssemblyName(assemblyPath));
            //    AppDomain.Unload(domain);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Ошибка при загрузке сборки: " + ex.Message);
            //}
            //Console.WriteLine("Новый домен выгружен.");
            Thread test1 = new Thread(sec) { Name = "testThread" };
            test1.Start();
            test1.Join();
            Console.WriteLine("Введите число n: ");
            n = int.Parse(Console.ReadLine());

            Thread evenThread = new Thread(PrintEvenNumbers);
            Thread oddThread = new Thread(PrintOddNumbers);

            evenThread.Priority = ThreadPriority.Highest;

            evenThread.Start();
            oddThread.Start();
            evenThread.Join();
            oddThread.Join();
            //isEvenTurn = false;
            //Thread evenThreadfst = new Thread(PrintEvenNumbersFst);
            //Thread oddThreadsnd = new Thread(PrintOddNumbersSnd);

            //evenThreadfst.Priority = ThreadPriority.Highest;


            //evenThreadfst.Start();
            //oddThreadsnd.Start();
            //evenThreadfst.Join();
            //oddThreadsnd.Join();
        }
        public static void printTime(object obj)
        {
            DateTime now = DateTime.Now;
            string formattedTime = now.ToString("HH:mm:ss");
            Console.WriteLine("\nТекущее время: " + formattedTime);
        }

        public static void sec()
        {
            Console.WriteLine(Thread.CurrentThread.Name);
            Console.WriteLine(Thread.CurrentThread.Priority);
            Console.WriteLine(Thread.CurrentThread.ThreadState);
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Введите число: ");
            int number = int.Parse(Console.ReadLine());
            using (StreamWriter file = new StreamWriter("sec.txt"))
            {
                for (int i = 0; i < number; i++)
                {
                    if (IsPrime(i))
                    {
                        file.WriteLine(i);
                        Console.WriteLine(i);
                        Thread.Sleep(100);
                    }
                }
            }
        }

        //gpt функция ->
        public static bool IsPrime(int num)
        {
            // Проверка на числа меньше 2
            if (num < 2) return false;

            // Проверка делителей от 2 до квадратного корня из num
            for (int i = 2; i * i <= num; i++)
            {
                if (num % i == 0)
                    return false;
            }

            return true;
        }

        private static void PrintEvenNumbers()
        {
            for (int i = 0; i <= n; i += 2)
            {
                mutex.WaitOne();
                Console.WriteLine($"Четное: {i}");
                using (StreamWriter file = new StreamWriter("numbers.txt", true))
                {
                    file.WriteLine(i);
                }
                isEvenTurn = false;
                mutex.ReleaseMutex();

                while (!isEvenTurn) { Thread.Sleep(100); }
            }
        }

        private static void PrintEvenNumbersFst()
        {
            mutex2.WaitOne();
            for (int i = 0; i <= n; i += 2)
            {
                Console.WriteLine($"Четное: {i}");
                using (StreamWriter file = new StreamWriter("numbers.txt", true))
                {
                    file.WriteLine(i);
                }
                Thread.Sleep(100);
            }
            mutex2.ReleaseMutex();
            isEvenTurn = false;
        }

        private static void PrintOddNumbers()
        {
            for (int i = 1; i <= n; i += 2)
            {
                mutex.WaitOne();
                Console.WriteLine($"Нечетное: {i}");
                using (StreamWriter file = new StreamWriter("numbers.txt", true))
                {
                    file.WriteLine(i);
                }
                isEvenTurn = true;
                mutex.ReleaseMutex();

                while (isEvenTurn) { Thread.Sleep(100); }
            }
        }

        private static void PrintOddNumbersSnd()
        {
            mutex2.WaitOne();
            for (int i = 1; i <= n; i += 2)
            {
                Console.WriteLine($"Нечетное: {i}");
                using (StreamWriter file = new StreamWriter("numbers.txt", true))
                {
                    file.WriteLine(i);
                }
                Thread.Sleep(100);
            }
            mutex2.ReleaseMutex();
            isEvenTurn = true;
        }
    }

}