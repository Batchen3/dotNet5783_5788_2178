// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");


namespace Stage0
{
    class Program
    {
        public static void Main(string[]args)
        {
            welcome5788();

            Console.ReadKey();
        }

        private static void welcome5788()
        {
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}


