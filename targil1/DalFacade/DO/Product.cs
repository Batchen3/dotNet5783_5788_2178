//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace DO;

public struct Product
{

    public int id { get; set; }
    public string name { get; set; }
    public double price { get; set; }
    public ECategory category { get; set; }//enum category
    public int inStock { get; set; }
    public int parve { get; set; }

    public override string ToString() => $@"
            Product ID:{id}: 
            Name:{name}:, 
            category:{category},
    	    Price: {price},
    	     Amount in stock: {inStock},
             Parve:{parve}
            ";

}
