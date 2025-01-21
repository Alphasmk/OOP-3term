using System.Reflection;

namespace lab11
{
    public class Test
    {
        public string name;
        public Test(string _name) => name = _name;

        public void PrintTest(string hello)
        {
            Console.WriteLine("Hello world!");
        }
    }
    static class Reflector
    {
        public static void GetAssemblyName(Type type)
        {
            Console.WriteLine($"Имя сборки: {type.Assembly}");
        }

        public static void GetPublicConstructors(Type type)
        {
            ConstructorInfo[] constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            if (constructors.Length != 0)
            {
                Console.WriteLine($"Конструкторы класса {type.Name}:");
                foreach (ConstructorInfo constructor in constructors)
                {
                    Console.WriteLine(constructor);
                }
            }
            else
            {
                Console.WriteLine("Публичных конструкторов нет");
            }
        }

        public static IEnumerable<string> GetPublicMethods(Type type)
        {
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            Console.WriteLine($"Публичные методы класса {type.Name}:");
            foreach (MethodInfo method in methods)
            {
                yield return method.Name;
            }
        }

        public static IEnumerable<string> GetFieldsAndProperties(Type type)
        {
            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            Console.WriteLine($"Поля и свойства методы класса {type.Name}:");

            foreach (FieldInfo field in fields)
            {
                yield return field.Name;
            }

            foreach (PropertyInfo property in properties)
            {
                yield return property.Name;
            }
        }

        public static IEnumerable<string> GetInterfaces(Type type)
        {
            Type[] interfaces = type.GetInterfaces();
            Console.WriteLine($"Все реализованные интерфейсы класса {type.Name}:");
            foreach (Type interfaceType in interfaces)
            {
                yield return interfaceType.Name;
            }
        }

        public static void GetMethodsWithSpecifiedParameters(Type type, string parameter)
        {
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            Console.WriteLine($"Методы с заданными параметрами класса {type.Name}:");
            foreach (MethodInfo method in methods)
            {
                ParameterInfo[] parameters = method.GetParameters();
                foreach(ParameterInfo param in parameters)
                {
                    if(param.Name == parameter)
                    {
                        Console.WriteLine(method.Name);
                    }
                }
            }
        }

        public static object Invoke(object obj, string methodName)
        {
            string[] lines = File.ReadAllLines("in.txt");
            if (lines.Length < 2)
            {
                throw new ArgumentException("Файл должен содержать как минимум две строки: имя класса и имя метода.");
            }

            string className = lines[0];
            string methodToInvoke = lines[1];

            Type type = Type.GetType(className);
            if (type == null)
            {
                throw new ArgumentException($"Класс '{className}' не найден.");
            }

            MethodInfo methodInfo = type.GetMethod(methodToInvoke);
            if (methodInfo == null)
            {
                throw new ArgumentException($"Метод '{methodToInvoke}' не найден в классе '{className}'.");
            }

            ParameterInfo[] parameters = methodInfo.GetParameters();
            object[] generatedParameters = GenerateParameters(parameters);
            return methodInfo.Invoke(obj, generatedParameters);
        }

        private static object[] GenerateParameters(ParameterInfo[] parameters)
        {
            return parameters.Select(param => GenerateValue(param.ParameterType)).ToArray();
        }

        private static object GenerateValue(Type type)
        {
            if (type == typeof(int))
                return new Random().Next(1, 100);
            if (type == typeof(string))
                return "Test";
            if (type == typeof(bool))
                return new Random().Next(0, 2) == 0;
            if (type == typeof(double))
                return new Random().NextDouble() * 100;
            throw new Exception($"{type.Name} не поддерживается.");
        }

        public static T Create<T>(params object[] args)
        {
            Type type = typeof(T);

            ConstructorInfo constructor = type.GetConstructor(args.Select(arg => arg.GetType()).ToArray());
            if (constructor == null)
            {
                throw new Exception($"конструктор для {type.Name} не найден.");
            }
            else
            {
                Console.WriteLine("Норм создали объект");
            }
            return (T)constructor.Invoke(args);
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            
            Reflector.GetAssemblyName(typeof(Program));
            Reflector.GetPublicConstructors(typeof(Test));
            foreach(string name in Reflector.GetPublicMethods(typeof(Test)))
            {
                Console.WriteLine(name);
            } 

            foreach(string value in Reflector.GetFieldsAndProperties(typeof(Test)))
            {
                Console.WriteLine(value);
            }

            Reflector.GetMethodsWithSpecifiedParameters(typeof(Test), "hello");
            try
            {
                Test test = new Test("hello");
                Reflector.Invoke(test, "PrintTest");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            try
            {
                Test test1 = Reflector.Create<Test>("hello");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}