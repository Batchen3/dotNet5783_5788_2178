//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace DO;

public struct OrderItem
{
    public int id { get; set; }
    public int productID { get; set; }
    public int orderID { get; set; }
    public double price { get; set; }
    public int amount { get; set; }

    public override string ToString() => $@"
            ID:{id}
            Product ID:{productID}, 
             Order ID :{orderID}
    	     Price: {price}
    	     Amount in order: {amount}
            ";

}
