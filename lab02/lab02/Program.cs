using System;
using System.Collections.Specialized;
namespace ConsoleApplication
{
    public partial class Time
    {
        public readonly int ID;

        private int hours;
        private int minutes;
        private int seconds;

        private const int MAX_HOURS_VALUE = 23;
        private const int MAX_MINUTES_VALUE = 59;
        private const int MAX_SECONDS_VALUE = 59;

        private static int ObjectsCounter ;
        public int hh
        {
            get
            {
                return hours;
            }
            set
            {
                if (value >= 0 && value <= MAX_HOURS_VALUE)
                {
                    this.hours = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Неверное значение часа");
                }
            }
        }

        public int mm
        {
            get
            {
                return minutes;
            }
            set
            {
                if(value >= 0 && value <= MAX_MINUTES_VALUE)
                {
                    this.minutes = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Неверное значение минут");
                }
            }
        }

        public int ss
        {
            get
            {
                return seconds;
            }
            set
            {
                if(value >= 0 && value <= MAX_SECONDS_VALUE)
                {
                    this.seconds = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Неверное значение секунд");
                }
            }
        }

        public Time()
        {
            this.hh = 0;
            this.mm = 0;
            this.ss = 0;
            ID = GetHashCode();
            ObjectsCounter++;
        }

        public Time(int hours, int minutes, int seconds)
        {
            this.hh = hours;
            this.mm = minutes;
            this.ss = seconds;
            ID = GetHashCode();
            ObjectsCounter++;
        }

        public Time(int hours = 8)
        {
            this.hh = hours;
            this.mm = minutes;
            this.ss = seconds;
            ID = GetHashCode();
            ObjectsCounter++;
        }

        public static bool IsDay(int hour, out string message)
        {
            bool isDayBool = hour >= 12 && hour <= 18;
            message = isDayBool ? "День" : "Не день";
            return isDayBool;
        }

        public static void PrintInfo()
        {
            Console.WriteLine($"Общее количество объектов класса: {ObjectsCounter}");
        }

        public override int GetHashCode() => HashCode.Combine(hh, mm, ss);
        //https://ru.stackoverflow.com/questions/198504/gethashcode-%D0%B2%D1%8B%D1%87%D0%B8%D1%81%D0%BB%D0%B5%D0%BD%D0%B8%D0%B5-%D1%85%D1%8D%D1%88

        public override bool Equals(object? obj)
        {
            if (obj is Time EqTime)
            {
                return hh == EqTime.hh && mm == EqTime.mm && ss == EqTime.ss; 
            }
            return false;
        }

        public override string ToString()
        {
            return ($"{hh}:{mm}:{ss}");
        }

        public static Time CreateEqTime()
        {
            return new Time(true);
        }
        private Time(bool createCommand)
        {
            if(createCommand)
            {
                Random random = new Random();
                int number = random.Next(0, 24);
                hh = number;
                mm = number;
                ss = number;
            }
            ID = GetHashCode();
            ObjectsCounter++;
        }
    }

    public partial class Time
    {
        public string DayTime(int hour)
        {
            if(hour >= 0 && hour < 6)
            {
                return "Ночь";
            }
            else if(hour >= 6 && hour < 12)
            {
                return "Утро";
            }
            else if(hour >= 12 && hour < 18)
            {
                return "День";
            }
            else
            {
                return "Вечер";
            }
        }
    }

    class lab02
    {
        [STAThread]
        static void Main(string[] args)
        {
            Time[] times = new Time[10]
            {
                new Time (12, 24, 56),
                new Time (15, 11, 23),
                new Time (9, 45, 11),
                new Time (23, 14, 15),
                new Time (8, 14, 48),
                new Time (4, 12, 11),
                new Time (21, 13, 14),
                new Time (2, 45, 11),
                new Time (16, 30, 00),
                new Time (9, 10, 41)
            };
            Console.Write("Введите число часов: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            for(int i = 0; i < times.Length; i++)
            {
                if (times[i].hh == choice)
                {
                    Console.WriteLine(times[i].ToString());
                }
            }

            Console.WriteLine("Ночь: ");
            for(int i = 0; i < times.Length; i++)
            {
                if (times[i].DayTime(times[i].hh) == "Ночь")
                {
                    Console.WriteLine(times[i].ToString());
                }
            }

            Console.WriteLine("Утро: ");
            for (int i = 0; i < times.Length; i++)
            {
                if (times[i].DayTime(times[i].hh) == "Утро")
                {
                    Console.WriteLine(times[i].ToString());
                }
            }

            Console.WriteLine("День: ");
            for (int i = 0; i < times.Length; i++)
            {
                if (times[i].DayTime(times[i].hh) == "День")
                {
                    Console.WriteLine(times[i].ToString());
                }
            }

            Console.WriteLine("Вечер: ");
            for (int i = 0; i < times.Length; i++)
            {
                if (times[i].DayTime(times[i].hh) == "Вечер")
                {
                    Console.WriteLine(times[i].ToString());
                }
            }

            Time Eq = Time.CreateEqTime();
            Console.WriteLine(Eq.ToString());

            var anonimusTime = new { hh = 12, mm = 45, ss = 13 };
            Console.Write($"{anonimusTime.hh}:");
            Console.Write($"{anonimusTime.mm}:");
            Console.Write(anonimusTime.ss);
        }
    }
}