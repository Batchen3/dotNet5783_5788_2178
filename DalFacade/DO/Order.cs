//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace DO;

public struct Order
{
    /// <summary>
    /// property Id for Order
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// property Customer name for Order
    /// </summary>
    public string CustomerName { get; set; }
    /// <summary>
    /// property Customer Email for Order
    /// </summary>
    public string CustomerEmail { get; set; }
    /// <summary>
    /// property Customer address for Order
    /// </summary>
    public string CustomerAddress { get; set; }
    /// <summary>
    /// property order date for Order
    /// </summary>
    public System.DateTime OrderDate { get; set; }
    /// <summary>
    /// property ship date for Order
    /// </summary>
    public System.DateTime ShipDate { get; set; }//arrive to the delivery
    /// <summary>
    /// property delivery date for Order
    /// </summary>
    public System.DateTime Delivery { get; set; } //arrive to the customer
    /// <summary>
    /// function toString to print Order
    /// </summary>

    public override string ToString() => $@"
            Product ID: {Id}, 
            Customer Name: {CustomerName},
    	    Customer Email: {CustomerEmail},
    	    CustomerAddress: {CustomerAddress},
    	    Order Date: {OrderDate},
    	    Ship Date: {ShipDate},
    	    Delivery: {Delivery}
 ";
}
