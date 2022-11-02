//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace DO;

public struct Order
{
    public int id { get; set; }
    public string customerName { get; set; }
    public string customerEmail { get; set; }
    public string customerAddress { get; set; }
    public System.DateTime orderDate { get; set; }
    public System.DateTime shipDate { get; set; }//נמסר למשלוחן
    public System.DateTime delivery { get; set; } //הגיע לללקוח

    public override string ToString() => $@"
            Product ID{id}, 
             Customer Name:{customerName}
    	     Customer Email: {customerEmail}
    	    CustomerAddress: {customerAddress}
    	    Order Date: {orderDate}
    	    Ship Date: {shipDate}
    	    Delivery: {delivery}
`
            ";
}
