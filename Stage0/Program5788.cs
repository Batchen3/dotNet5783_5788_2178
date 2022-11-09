// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");


namespace Stage0
{
    partial class Program
    {
        public static void Main(string[]args)
        {
            welcome5788();
            welcome2178();
            Console.ReadKey();
        }
        static partial void welcome2178();
        private static void welcome5788()
        {
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}


