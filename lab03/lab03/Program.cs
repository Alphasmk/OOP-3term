using System.Text;

namespace ConsoleApplication
{
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

        private void Append(string add)
        {
            str += add;
        }

        public int Length()
        {
            return str.Length;
        }

        public char this[int index]
        {
            get
            {
                if (index < 0 || index >= str.Length)
                {
                    throw new IndexOutOfRangeException("Индекс вне допустимого диапазона.");
                }
                return str[index];
            }
        }

        private int MaxWordLength()
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return 0;
            }

            int counter = 0, bufCounter = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (!(Convert.ToInt32(str[i]) >= 33 && Convert.ToInt32(str[i]) <= 40))
                {
                    bufCounter++;
                }
                if (str[i] == ' ')
                {
                    bufCounter = 0;
                }
                if (bufCounter > counter)
                {
                    counter = bufCounter;
                }
            }
            return counter;
        }

        public static bool operator <(NewStr a, NewStr b)
        {
            return a.MaxWordLength() < b.MaxWordLength();
        }

        public static bool operator >(NewStr a, NewStr b)
        {
            return a.MaxWordLength() > b.MaxWordLength();
        }

        public static NewStr operator *(NewStr a, char newChar)
        {
            string ModStr = new string(newChar, a.Length());
            return new NewStr(ModStr);
        }

        public void AddNum(int num)
        {
            string strNum = Convert.ToString(num);
            Append(strNum);
        }

        public void Print()
        {
            Console.WriteLine(str);
        }

        public void DeleteLastChar()
        {
            str = str.Remove(Length() - 1);
        }
        public string DeletePunctuationChar()
        {
            StringBuilder newString = new StringBuilder();
            for (int i = 0; i < Length(); i++)
            {
                if (!(str[i] == ',' || str[i] == '.' || str[i] == '?' || str[i] == '!'))
                {
                    newString.Append(str[i]);
                }
            }
            str = newString.ToString();
            return str;
        }

        public class Production
        {
            private readonly int ID;
            private readonly string? Org;
            public Production()
            {
                ID = 777;
                Org = "Brawl Stars";
            }

            public Production(int EntID, string? EntOrg)
            {
                ID = EntID;
                Org = EntOrg;
            }

            public void Print()
            {
                Console.WriteLine($"ID: {ID}, Организация: {Org}");
            }
        }

        public class Developer
        {
            private readonly int ID;
            private string? department;
            private string? FIO;

            public Developer()
            {
                ID = 777;
                department = "БГТУ"; 
                FIO = "Статько Герман Вячеславович";
            }

            public Developer(int EntID, string EntDep, string EntFIO)
            {
                ID = EntID;
                department = EntDep;
                FIO = EntFIO;
            }

            public void Print()
            {
                Console.WriteLine($"ID: {ID}, Отдел: {department}, ФИО: {FIO}");
            }
        }
    }

    public static class NewStrExtension
    {
        public static bool ServiceCharCheck(this NewStr str)
        {
            bool isFound = false;
            for (int i = 0; i < str.Length(); i++)
            {
                if (Convert.ToInt32(str[i]) >= 21 && Convert.ToInt32(str[i]) <= 47 || Convert.ToInt32(str[i]) >= 58 && Convert.ToInt32(str[i]) <= 64 || Convert.ToInt32(str[i]) >= 91 && Convert.ToInt32(str[i]) <= 96 || Convert.ToInt32(str[i]) >= 123 && Convert.ToInt32(str[i]) <= 126)
                {
                    isFound = true;
                }
            }

            return isFound;
        }
    }
    static class StatisticOperation
    {
        public static int CharSum(NewStr a)
        {
            int sum = 0;
            for (int i = 0; i < a.Length(); i++)
            {
                sum += Convert.ToInt32(a[i]);
            }
            return sum;
        }

        public static int CharDiff(NewStr a)
        {
            int min = a[0];
            int max = a[0];
            for(int i = 1; i < a.Length() ; i++)
            {
                if (a[i] > max)
                {
                    max = a[i];
                }
                if (a[i] < min)
                {
                    min = a[i];
                }
            }
            return max - min;
        }

        public static int LetterCount(NewStr a)
        {
            int counter = 0;
            for(int i = 0; i < a.Length(); i++)
            {
                if (Convert.ToInt32(a[i]) >= 65 && Convert.ToInt32(a[i]) <= 90 || Convert.ToInt32(a[i]) >= 97 && Convert.ToInt32(a[i]) <= 122 || Convert.ToInt32(a[i]) >= 192 && Convert.ToInt32(a[i]) <= 255)
                {
                    counter++;
                }
            }
            return counter;
        }
    }

    class Lab03
    {
        static void Main(string[] args)
        {
            NewStr myStr = new NewStr("Hello World!");
            NewStr myStr2 = new NewStr("HelloWorldazazazazaza");
            NewStr myStr3 = new NewStr("Hello, world!");
            Console.WriteLine(myStr.Length());
            Console.WriteLine(myStr[6]);
            Console.WriteLine(myStr > myStr2);
            myStr.AddNum(5);
            myStr.Print();
            myStr.DeleteLastChar();
            myStr.Print();
            myStr = myStr * 'l';
            myStr.Print();
            Console.WriteLine(myStr2.ServiceCharCheck());
            myStr3.DeletePunctuationChar();
            myStr3.Print();
            NewStr.Production prod = new NewStr.Production(125, "БГТУ");
            prod.Print();
            NewStr.Developer dev = new NewStr.Developer(128, "Кибербезопасность", "Андрей Андреев");
            dev.Print();
            Console.WriteLine(StatisticOperation.LetterCount(myStr));
        }
    }
}
