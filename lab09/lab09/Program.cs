using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace lab09
{
    public class Game
    {
        public string Name { get; set; }
        public int Year { get; set; }

        public Game(string name, int year)
        {
            Name = name;
            Year = year;
        }

        public override string ToString()
        {
            return $"Игра: {Name}, Год: {Year}";
        }
    }

    public class GameCollection : IEnumerable<Game>
    {
        private BlockingCollection<Game> games = new BlockingCollection<Game>();
        public void Add(Game game)
        {
            games.Add(game);
        }

        public bool DeleteGame(Game game)
        {
            return games.TryTake(out game);
        }

        public string FindGameByYear(int year)
        {
            foreach(Game _game in games)
            {
                if(_game.Year == year)
                {
                    return _game.Name;
                }
            }
            return "Игра не найдена";
        }

        public void PrintGames()
        {
            foreach (Game _game in games)
            {
                Console.WriteLine(_game.ToString());
            }
        }

        public IEnumerator<Game> GetEnumerator()
        {
            return games.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    class Program
    {
        static void Main(String[] args)
        {
        }
    }
}