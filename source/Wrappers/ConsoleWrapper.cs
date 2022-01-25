using HaTeamsWatcher.Interfaces;
using System;

namespace HaTeamsWatcher.Wrappers
{
    public class ConsoleWrapper : IConsole
    {
        public ConsoleKeyInfo ReadKey(bool intercept)
        {
            return Console.ReadKey(intercept);
        }

        public string? ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteLine(string? value)
        {
            Console.WriteLine(value);
        }
    }
}