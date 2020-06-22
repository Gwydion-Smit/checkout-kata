using System;

namespace checkout_kata.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Basket basket1 = new Basket();
            string item = "";

            System.Console.WriteLine("Welcome to the checkout, please scan your items: (type quit to stop scanning)");
            do
            {
                item = System.Console.ReadLine();
                if (item != "quit")
                {
                    basket1.Scan(item);
                    System.Console.WriteLine("enter your next item:");
                }
            } while (item != "quit");

            System.Console.WriteLine("Your total is: {0}", basket1.GetTotal());
            System.Console.ReadKey();

        }
    }
}
