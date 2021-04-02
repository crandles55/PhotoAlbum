using System;

namespace PhotoAlbum
{
    public interface IConsoleWrapper
    {
        void WriteLine(string message);
        void Write(string message);
        string ReadLine();
    }
    
    public class ConsoleWrapper : IConsoleWrapper
    {
        public ConsoleWrapper()
        {
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Write(string message)
        {
            Console.Write(message);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }
    }
}
