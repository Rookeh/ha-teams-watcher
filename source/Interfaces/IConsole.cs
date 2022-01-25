using System;

namespace HaTeamsWatcher.Interfaces
{
    public interface IConsole
    {
        ConsoleKeyInfo ReadKey(bool intercept);
        string? ReadLine();
        void WriteLine(string? value);
    }
}