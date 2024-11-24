using Microsoft.VisualBasic.FileIO;
using System;
namespace Lab08
{
    public delegate void DeleteAction();
    public delegate void MutateAction();
    class Programmer
    {
        public string Name { get; set; }
        public Programmer(string _name) => Name = _name;
    }

    class ProgrammerController
    {
        public List<Programmer> persons;
        public ProgrammerController() => persons = new List<Programmer>();

        public event DeleteAction? Delete;
        public event MutateAction? Mutate;

        public void DeleteRandomElement()
        {
            if (persons.Count > 0)
            {
                Random random = new Random();
                int removeIndex = random.Next(persons.Count);
                Console.WriteLine("\nМутация удаления случайного элемента прошла успешно");
                persons.RemoveAt( removeIndex );
            }
            else
            {
                Console.WriteLine("Мутация удаления случайного элемента невозможна");
            }
        }

        private void RandomSwap()
        {
            if (persons.Count >= 2)
            {
                Random random = new Random();
                int index1, index2;
                do
                {
                    index1 = random.Next(persons.Count);
                    index2 = random.Next(persons.Count);
                } while (index1 == index2);
                Programmer temp;
                temp = persons[index1];
                persons[index1] = persons[index2];
                persons[index2] = temp;
                Console.WriteLine("\nМутация обмена прошла успешно");
            }
            else
            {
                Console.WriteLine("Мутация обмена невозможна");
            }
        }

        private void RandomChange()
        {
            if(persons.Count >= 2)
            {
                Random random = new Random();
                persons[random.Next(persons.Count)].Name = "моргенштерн";
                Console.WriteLine("\nМутация случайной смены прошла успешно");
            }
            else
            {
                Console.WriteLine("Мутация случайной смены невозможна");
            }
        }
        
        private void SVO()
        {
            if(persons.Count > 0)
            {
                foreach(Programmer programmer in persons)
                {
                    programmer.Name = "ZV";
                }
                Console.WriteLine("\nZV мутация прошла успешно");
            }
            else
            {
                Console.WriteLine("О НЕТ А КАК ЖЕ ZOV...");
            }
        }

        public void RandomMutate()
        {
            Random random = new Random();
            int a = random.Next(1, 4);
            switch(a)
            {
                case 1:
                    RandomSwap();
                    break;
                case 2:
                    RandomChange();
                    break;
                case 3:
                    SVO();
                    break;
            }
        }

        
        public void MutateUpdate()
        {
            if(Mutate != null)
            {
                Mutate.Invoke();
            }
            else
            {
                Console.WriteLine("У тебя событие пустое еблан");
            }
        }

        public void DeleteUpdate()
        {
            if (Delete != null)
            {
                Delete.Invoke();
            }
            else
            {
                Console.WriteLine("У тебя событие пустое еблан");
            }
        }

        public void Print()
        {
            foreach (Programmer person in persons)
            {
                Console.Write(person.Name);
                Console.Write(" ");
            }
            Console.Write("\n");
        }
    }

    class lab08
    {
        static void Main(string[] args)
        {
            Programmer p1 = new Programmer("Иван");
            Programmer p2 = new Programmer("Андрей");
            Programmer p3 = new Programmer("Вадим");
            Programmer p4 = new Programmer("Матвей");
            
            ProgrammerController controller = new ProgrammerController();
            
            controller.persons.Add(p1);
            controller.persons.Add(p2);
            controller.persons.Add(p3);
            controller.persons.Add(p4);
            controller.Print();
            controller.Delete += controller.DeleteRandomElement;
            controller.DeleteUpdate();
            controller.Print();
            controller.Mutate += controller.RandomMutate;
            controller.MutateUpdate();
            controller.Print();
            controller.Mutate += controller.RandomMutate;
            controller.MutateUpdate();
            controller.Print();
        }
    }
}