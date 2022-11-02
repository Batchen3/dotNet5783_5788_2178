//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace DO;

public struct OrderItem
{
    public int _id { get; set; }
    public int _productID { get; set; }
    public int _orderID { get; set; }
    public double _price { get; set; }
    public int _amount { get; set; }

    public override string ToString() => $@"
            ID:{_id}
            Product ID:{_productID}, 
             Order ID :{_orderID}
    	     Price: {_price}
    	     Amount in order: {_amount}
            ";

}
