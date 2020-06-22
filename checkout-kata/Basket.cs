using System;
using System.Collections.Generic;

namespace checkout_kata
{
    public class Basket
    {
        private const string prices_file_name = @"..\..\..\..\checkout-kata\prices.txt"; //These two could make obvious crashes if files don't exist in that exact location
        private const string discounts_file_name = @"..\..\..\..\checkout-kata\discounts.txt";

        private Dictionary<string, int> basket_items = new Dictionary<string, int>(); //stores basket items in name:quantity pair
        private Dictionary<string, Tuple<int, int>> discounts = new Dictionary<string, Tuple<int, int>>(); //stores discounts in name:(quantity:new price) pair
        private Dictionary<string, int> prices = new Dictionary<string, int>(); //stores prices in name:price pair

        private int total = 0;
        public Basket()
        {
            LoadPrices();
            LoadDiscounts();
        }
        private void LoadPrices() //loads prices from the file and adds them to the dictionary
        {
            System.IO.StreamReader prices_file = new System.IO.StreamReader(prices_file_name); //assumes the file is of a csv format, and thus parses it as such
            string[] current_iteminfo = { };

            while (!prices_file.EndOfStream)
            {
                current_iteminfo = prices_file.ReadLine().Split(',');
                prices.Add(current_iteminfo[0], Int32.Parse(current_iteminfo[1]));
            }
        }
        private void LoadDiscounts() //loads discounts from teh discounts file and adds them to the dictionary
        {
            System.IO.StreamReader discounts_file = new System.IO.StreamReader(discounts_file_name); //assumes the file is of a csv format
            string[] current_discountinfo = { };

            while (!discounts_file.EndOfStream)
            {
                current_discountinfo = discounts_file.ReadLine().Split(',');
                discounts.Add(current_discountinfo[0], new Tuple<int, int>(Int32.Parse(current_discountinfo[1]), Int32.Parse(current_discountinfo[2])));
            }
        }
        public void Scan(string item_name) //Scans the item passed to it and if it's valid adds it to the basket
        {
            if (prices.ContainsKey(item_name))
            {
                if (basket_items.ContainsKey(item_name)) //If item type is already in basket increment its quantity, else add it to the dictionary
                {
                    basket_items[item_name] += 1;
                }
                else
                {
                    basket_items.Add(item_name, 1);
                }
            }
            else
            {
                System.Console.WriteLine("Invalid item scanned");
            }
        }
        public void Total() //calculates the total value of the basket
        {
            int quotient, remainder;
            foreach (var item in basket_items) //goes through item types in the basket
            {
                if (discounts.ContainsKey(item.Key)) //if item has a discount then apply it
                {
                    quotient = Math.DivRem(item.Value, discounts[item.Key].Item1, out remainder);
                    total += quotient * discounts[item.Key].Item2 + prices[item.Key] * remainder;
                }
                else
                {
                    total += prices[item.Key] * item.Value;
                }
            }
        }
        public int GetTotal() //Calculates basket total and then returns it
        {
            Total();
            return total;
        }
    }

}
