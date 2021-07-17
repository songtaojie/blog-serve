using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(typeof(IAutoMapper).IsAssignableFrom(typeof(UserInfo)));
            Console.ReadKey();
        }
    }

    interface IAutoMapper { }
    class UserInfo : IAutoMapper
    { }
        
}
