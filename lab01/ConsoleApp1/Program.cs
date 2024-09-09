using System;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            //1

            //a.
            bool a = false;
            byte b = 122; // максимальное - 255, минимальное - 0
            sbyte c = 100; // максимальное - 127, минимальное - -128
            char d = 'a'; // символьный тип
            decimal e = 4500000000000000000; // 28-29 знаков
            double f = 1230000000000000; // 15-17 знаков
            float g = 1.0f; // 6 - 9 знаков
            int h = -155; // целочисленный тип
            uint i = 155; // unsigned int;
            long j = -56000000000; // от −9 223 372 036 854 775 808 до 9 223 372 036 854 775 807
            ulong k = 56000000000; // unsigned long
            short l = -15000; // от -32 768 до 32 767
            ushort m = 15000; // unsigned short
            Console.WriteLine($"bool: {a}, byte: {b}, sbyte: {c}, char: {d}, decimal: {e}, double: {f}, float: {g}, int: {h}, uint: {i}, long: {j}, ulong: {k}, short: {l}, ushort: {m}");

            //b.
            int x1 = 12000;
            long y1 = 160000;
            y1 = x1; // преобразуем int в long неявно(без потери точности)
            long x2 = -56000000000;
            float y2 = 1.0f;
            y2 = x2; // преобразуем long в float неявно(без потери точности)
            byte x3 = 123;
            decimal y3 = 4500000000000000000;
            y3 = x3; // преобразуем byte в decimal неявно(без потери точности)
            long x4 = -56000000000;
            double y4 = 1230000000000000;
            y4 = x4; // преобразуем long в double неявно(без потери точности)
            float x5 = 1.0f;
            double y5 = 1230000000000000;
            y5 = x5; // преобразуем float в double неявно(без потери точности)

            int x6 = 10;
            int y6 = 15;
            byte z6 = (byte)(x6 + y6); // преобразуем сумму x6 и y6 из int в byte(потеря точности или данных)
            double x7 = 1230000000000000;
            short x8 = (short)(x7); // преобразуем x7 из double в short неявно(потеря точности или данных)
            byte x9 = (byte)(x7); // преобразуем x7 из double в byte неявно(потеря точности или данных)
            ulong x10 = (ulong)(x7); // преобразуем x7 из double в ulong неявно(потеря точности или данных)
            uint x11 = (uint)(x7); // преобразуем x7 из double в uint неявно(потеря точности или данных)

            // double x12 = 150000000000;
            // int y12 = Convert.ToInt32(x12); Unhandled exception. System.OverflowException: Value was either too large or too small for an Int32.

            double x13 = 15000;
            int y13 = Convert.ToInt32(x13);
            Console.WriteLine(y13);

            //c
            object boxedNumber = 42; // Упаковка
            int unboxedNumber = (int)boxedNumber; // Распаковка

            object boxedChar = 'c'; // Упаковка
            char unboxedChar = (char)(boxedChar); // Распаковка

            //int test = 10;
            //object boxedTest = test;

            //double unboxedTest = (double)boxedTest; Unhandled exception. System.InvalidCastException: Unable to cast object of type 'System.Int32' to type 'System.Double'
            // Возникает исключение при попытке распаковать в неверный тип

            //d
            var isItInt = 5;
            Console.WriteLine(isItInt.GetType()); // Результат - System.Int32

            var isItChar = 'c';
            Console.WriteLine(isItChar.GetType()); // Результат - System.Char
            //Фактически, var - это аналогия auto в C++
            //Компилятор автоматически определяет тип переменной из правой части ее объявления, что позволяет сделать код более читаемым и удобным в понимании

            //e
            int test1; // Не допускает null
            int? test2; // Допускает null

            // test1 = null; Не удается преобразовать значение NULL в "int", поскольку этот тип значений не допускает значение NULL
            test2 = null;

            // Nullble переменная - переменная, которой можно присвоить значение null(она может быть пустой)

            //f
            var varTest = 15;
            // varTest = "hello world!"; CS0029 Не удается неявно преобразовать тип "string" в "int".


            //Причины ошибки заключается в том, что, при присвоении var переменной значения, она становится статически типизированной. И после этого,
            //если попытаться присвоить ей значение другого типа данных - появится ошибка, так как, в данном случае, переменная varTest уже определяется как Int.

            //2
            //a
            string str1 = "Hello world!";
            string str2 = "Hello world!";
            string str3 = "Hello!";

            bool isEqual1 = str1 == str2; // true
            Console.WriteLine(isEqual1);
            bool isEqual2 = str1 == str3; // false
            Console.WriteLine(isEqual2);

            bool isEqual3 = str1.Equals(str2); // true
            Console.WriteLine(isEqual3);
            bool isEqual4 = str1.Equals(str3); // false
            Console.WriteLine(isEqual4);

            //b
            string stroke1 = "a";
            string stroke2 = "b";
            string stroke3 = "c";
            string resultStroke = stroke1 + stroke2 + stroke3; // сцепление
            Console.WriteLine(resultStroke);
            string stroke4 = String.Copy(stroke1); // копирование
            Console.WriteLine(stroke4);
            string stroke5 = "Hello world!";
            string stroke6 = stroke5.Substring(6); // выделение подстроки
            Console.WriteLine(stroke6);
            string stroke7 = "London is a capital of Great Britain";
            string[] words = stroke7.Split(' '); // разделение строки на слова
            for (int q = 0; q < words.Length; q++)
            {
                Console.WriteLine($"{q + 1}: <{words[q]}>"); // интерполирование строки
            }
            string stroke8 = "beautiful ";
            stroke5 = stroke5.Insert(6, stroke8); // вставка подстроки
            Console.WriteLine(stroke5);
            stroke5 = stroke5.Remove(5, 10); // удаление заданной подстроки
            Console.WriteLine(stroke5);

            //c
            string stroke9 = "";
            string? stroke10 = null;
            Console.WriteLine($"stroke 9 is null or empty: {string.IsNullOrEmpty(stroke9)}");
            Console.WriteLine($"stroke 10 is null or empty: {string.IsNullOrEmpty(stroke10)}");
            Console.WriteLine($"stroke 5 is null or empty: {string.IsNullOrEmpty(stroke5)}");
            //К примеру, еще можно проверить строку на пустоту или состоит ли она только из пробелов
            string stroke11 = " ";
            Console.WriteLine($"stroke 9 is null or white space: {string.IsNullOrWhiteSpace(stroke11)}");
            Console.WriteLine($"stroke 11 is null or white space: {string.IsNullOrWhiteSpace(stroke11)}");
            Console.WriteLine($"stroke 5 is null or white space: {string.IsNullOrWhiteSpace(stroke5)}");

            //d
            StringBuilder sb = new StringBuilder();
            sb.Append("Hello ");
            sb.Append("world!");
            Console.WriteLine(sb.ToString());
            sb.Remove(6, 6);
            Console.WriteLine(sb.ToString());
            sb.Insert(0, "Say ");
            sb.Insert(10, "World!");
            Console.WriteLine(sb.ToString());

            //3
            //a
            int[,] mas = new int[5, 5];
            Random rand = new Random();

            for(int s = 0; s < 5; s++)
            {
                for(int n = 0; n < 5; n++)
                {
                    mas[s, n] = rand.Next(1, 20);
                    Console.Write("{0}\t", mas[s, n]);
                }
                Console.WriteLine();
            }

            //b
            string[] masStr = { "Hello", "World!" };
            for(int s = 0; s < 2; s++)
            {
                Console.WriteLine(masStr[s]);
            }
            Console.WriteLine("Какое слово по номеру заменить(Длина массива - 2)? ");
            int choice = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите слово: ");
            string? word = Console.ReadLine();
            masStr[choice - 1] = word;
            for (int s = 0; s < 2; s++)
            {
                Console.WriteLine(masStr[s]);
            }

            //c
            int[][] steppedArray = new int[3][];
            steppedArray[0] = new int[2];
            steppedArray[1] = new int[3];
            steppedArray[2] = new int[4];
            Console.WriteLine("Введите значения первой строки: ");
            for (int s = 0; s < 2; s++)
            {
                Console.Write($"Элемент {s + 1}: ");
                steppedArray[0][s] = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine();

            for (int s = 0; s < 3; s++)
            {
                Console.Write($"Элемент {s + 1}: ");
                steppedArray[1][s] = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine();

            for (int s = 0; s < 4; s++)
            {
                Console.Write($"Элемент {s + 1}: ");
                steppedArray[2][s] = Convert.ToInt32(Console.ReadLine());
            }
            Console.WriteLine();

            for (int s = 0; s < steppedArray.Length; s++)
            {
                for (int n = 0; n < steppedArray[s].Length; n++)
                {
                    Console.Write(steppedArray[s][n] + " ");
                }
                Console.WriteLine();
            }

            //d
            var varMas = new[] { 1, 2, 3 }; // неявно типизированная переменная для хранения массива
            var varStr = "Hello world"; // неявно типизированная переменная для хранения строки
   
            //4
            //a
            (int, string, char, int, ulong) cort = (14, "Андрей Андреевич Андреев", 'A', 24, 9);
            //b
            string vyvod1 = $"Возраст: {cort.Item1}, ФИО: {cort.Item2}, Буква класса: {cort.Item3}, Школа: {cort.Item4}, Средний балл: {cort.Item5}";
            Console.WriteLine(vyvod1);
            string vyvod2 = $"Возраст: {cort.Item1}, Средний балл: {cort.Item5}";
            Console.WriteLine(vyvod2);

            //c
            var (age, fio, letter, school, avg) = cort; // Распаковка в отдельные переменные
            Console.WriteLine($"Возраст: {age}, ФИО: {fio}, Буква класса: {letter}, Школа: {school}, Средний балл: {avg}");
            // Также, к примеру, можно распаковать кортеж с использованием символа нижнего подчеркивания(_), чтобы пропустить присваивание значения переменной
            // var (age, _, letter, school, _) = cort;
            (int, string, char, int, ulong) cort1 = (14, "Андрей Андреевич Андреев", 'A', 24, 9);
            (int, string, char, int, ulong) cort2 = (14, "Андрей Андреевич Андреев", 'B', 12, 10);
            bool isEqual = cort == cort1; // true
            Console.WriteLine(isEqual);
            isEqual = cort == cort2; // false
            Console.WriteLine(isEqual);

            //5
            int[] funcMas = { 4, 8, 9, 1, 2, 3, 4, 7, 5, 6, 7 };
            string funcStr = "Hello world!";
            (int, int, int, char) funcResult = GetCort(funcMas, funcStr);
            Console.WriteLine($"Максимальное: {funcResult.Item1}, Минимальное: {funcResult.Item2}, Сумма: {funcResult.Item3}, Первая буква: {funcResult.Item4}");

            //6
            CheckedTest();
            UncheckedTest();
        }

        //5 функция
        public static (int Max, int Min, int Sum, char FirstLetter) GetCort(int[] mas, string str)
        {
            int size = mas.Length;
            int min = mas[0], max = mas[0], sum = mas[0];
            for (int i = 1; i < size; i++)
            {
                if (mas[i] > max)
                {
                    max = mas[i];
                }

                if (mas[i] < min)
                {
                    min = mas[i];
                }

                sum += mas[i];
            }

            char fst = str[0];
            return (max, min, sum, fst);
        }

        public static int CheckedTest() // Функция выдает исключение о переполнении: Unhandled exception. System.OverflowException: Arithmetic operation resulted in an overflow.
        {
            int maxInt = int.MaxValue;
            checked
            {
                maxInt += 10;
            }
            return maxInt;
        }

        public static int UncheckedTest() // Данная функция не выдает исключение, потому что оператор unchecked отключается проверку на переполнение типа.
        {
            int maxInt = int.MaxValue;
            unchecked
            {
                maxInt += 10;
            }
            return maxInt;
        }
    }

}
