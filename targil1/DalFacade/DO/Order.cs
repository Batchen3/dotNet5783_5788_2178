//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace Do
{
    public struct Order
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public System.DateTime OrderDate { get; set; }
        public System.DateTime ShipDate { get; set; }//נמסר למשלוחן
        public System.DateTime Delivery { get; set; } //הגיע לללקוח

        public override string ToString() => $@"
            Product ID{ID}, 
             Customer Name:{CustomerName}
    	     Customer Email: {CustomerEmail}
    	    CustomerAddress: {CustomerAddress}
    	    Order Date: {OrderDate}
    	    Ship Date: {ShipDate}
    	    Delivery: {Delivery}
`
            ";
    }
}
