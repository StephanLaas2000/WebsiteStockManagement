using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebsiteStockManagement.OOP
{
    public class Products : IComparable
    {
        //Creating 3 variables type,price,daterange
        private string type;
        private int price;
        private DateTime dateRange;

        //Creating an empty constructor
        public Products()
        {

        }

        //Creating a construtor with the 3 neccssary variable types inside
        public Products(string type, int price, DateTime dateRange)
        {
            this.Type = type;
            this.Price = price;
            this.DateRange = dateRange;
        }

        //Setting the getters and setters 
        public string Type { get => type; set => type = value; }
        public int Price { get => price; set => price = value; }
        public DateTime DateRange { get => dateRange; set => dateRange = value; }

        //Compares this instance with a specified String object and indicates whether this instance precedes, 
        //follows, or appears in the same position in the sort order as the specified string.
        public int CompareTo(object obj)
        {
            return type.CompareTo(obj.ToString());
        }

        public override string ToString()
        {
            return type;
        }
    }
}
