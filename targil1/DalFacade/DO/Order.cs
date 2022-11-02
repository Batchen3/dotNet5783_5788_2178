//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace DO;

public struct Order
{
    public int _id { get; set; }
    public string _customerName { get; set; }
    public string _customerEmail { get; set; }
    public string _customerAddress { get; set; }
    public System.DateTime _orderDate { get; set; }
    public System.DateTime _shipDate { get; set; }//arrive to the delivery
    public System.DateTime _delivery { get; set; } //arrive to the customer

    public override string ToString() => $@"
            Product ID{_id}, 
             Customer Name:{_customerName}
    	     Customer Email: {_customerEmail}
    	    CustomerAddress: {_customerAddress}
    	    Order Date: {_orderDate}
    	    Ship Date: {_shipDate}
    	    Delivery: {_delivery}
`
            ";
}
