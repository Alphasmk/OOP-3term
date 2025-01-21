using System.Text;

public partial class Time
{
    public readonly int ID;

    private int hours;
    private int minutes;
    private int seconds;

    private const int MAX_HOURS_VALUE = 23;
    private const int MAX_MINUTES_VALUE = 59;
    private const int MAX_SECONDS_VALUE = 59;

    private static int ObjectsCounter;
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
            if (value >= 0 && value <= MAX_MINUTES_VALUE)
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
            if (value >= 0 && value <= MAX_SECONDS_VALUE)
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
        if (createCommand)
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
        if (hour >= 0 && hour < 6)
        {
            return "Ночь";
        }
        else if (hour >= 6 && hour < 12)
        {
            return "Утро";
        }
        else if (hour >= 12 && hour < 18)
        {
            return "День";
        }
        else
        {
            return "Вечер";
        }
    }

    public static bool operator >(Time a, Time b)
    {
        if (a.hh > b.hh)
        {
            return true;
        }
        else if(a.hh < b.hh)
        {
            return false;
        }
        else if(a.mm > b.mm)
        {
            return true;
        }
        else if(a.mm < b.mm)
        {
            return false;
        }
        else if(a.ss > b.ss)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool operator <(Time a, Time b)
    {
        if (a.hh > b.hh)
        {
            return false;
        }
        else if (a.hh < b.hh)
        {
            return true;
        }
        else if (a.mm > b.mm)
        {
            return false;
        }
        else if (a.mm < b.mm)
        {
            return true;
        }
        else if (a.ss > b.ss)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

class lab10
{
    static void Main( string[] args )
    {
        //(1)
        string[] Months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        //1
        int n = 5;
        var Months1 = Months.Where(month => month.Length == n);
        foreach(string month in Months1)
        {
            Console.WriteLine(month);
        }
        Console.WriteLine();
        //2
        var Months2 = Months.Where(month => month == "June" || month == "July" || month == "August" || month == "January" || month == "February" || month == "December");
        foreach (string month in Months2)
        {
            Console.WriteLine(month);
        }

        //3
        Console.WriteLine();
        var Months3 = Months.OrderBy(month => month);
        foreach (string month in Months3)
        {
            Console.WriteLine(month);
        }

        //4
        Console.WriteLine();
        var Months4 = Months.Where(month => month.Contains("u") && month.Length >= 4);
        foreach (string month in Months4)
        {
            Console.WriteLine(month);
        }
        //(2)
        //1
        List<Time> timeList = new List<Time>();
        timeList.Add(new Time(10, 12, 14));
        timeList.Add(new Time(11, 13, 15));
        timeList.Add(new Time(14, 14, 16));
        timeList.Add(new Time(13, 15, 17));
        timeList.Add(new Time(15, 16, 18));
        timeList.Add(new Time(16, 17, 19));
        timeList.Add(new Time(17, 17, 20));
        timeList.Add(new Time(19, 19, 21));
        timeList.Add(new Time(19, 20, 22));
        timeList.Add(new Time(20, 21, 23));
        int h = 12;
        var Time1 = timeList.Where(time => time.hh == h);
        foreach(Time time in Time1)
        {
            Console.WriteLine(time.ToString());
        }

        //2
        var Time2 = timeList.OrderBy(time => time.DayTime(time.hh));
        foreach(Time time in Time2)
        {
            Console.WriteLine(time.DayTime(time.hh));
            Console.WriteLine(time.ToString());
        }

        //3
        var Time3 = timeList.FirstOrDefault(time => time.hh == time.mm);
        Console.WriteLine(Time3);

        //4
        Console.WriteLine();
        var Time4 = timeList.OrderBy(time => time.hh).ThenBy(time => time.mm).ThenBy(time => time.ss);
        foreach (Time time in Time4)
        {
            Console.WriteLine(time.ToString());
        }

        //(4)
        var Time5 = timeList.Where(time => time.hh == time.mm).Select(p => new { hours = p.hh, minutes = p.ss, seconds = p.mm}).OrderBy(p => p.seconds).GroupBy(p => p.minutes).ToList();

        foreach(var group in Time5)
        {
            Console.WriteLine($"Минуты: {group.Key}");
            foreach(var time in group)
            {
                Console.WriteLine($"Часы: {time.hours}, Секунды: {time.seconds}");
            }
        }
        //(5)
        int[] key = { 0, 1, 2};
        var joinType = Time4.Join(key, time => time.hh, k => k,
            (time, k) => new
            {
                Hours = time.hh,
                Minutes = time.mm,
                Seconds = time.ss,
                Key = k
            });
    }
}