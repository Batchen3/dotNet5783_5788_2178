//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace DO;

public struct Product
{

    public int _id { get; set; }
    public string _name { get; set; }
    public double _price { get; set; }
    public ECategory _category { get; set; }//enum category
    public int _inStock { get; set; }
    public int _parve { get; set; }

    public override string ToString() => $@"
            Product ID: {_id}, 
            Name: {_name}, 
            category: {_category},
    	    Price: {_price},
    	    Amount in stock: {_inStock},
            Parve:{_parve}
            ";

}
