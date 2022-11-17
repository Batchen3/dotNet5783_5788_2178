using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Order
{
    public int ID { get; set; }
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public EStatus OrderStatus { get; set; }
    public System.DateTime OrderDate { get; set; }
    public System.DateTime ShipDate { get; set; }//arrive to the delivery
    public System.DateTime Delivery { get; set; } //arrive to the customer
    public List<OrderItem> Items { get; set; }
    public double TotalPrice { get; set; }
    public override string ToString() => $@"
            Product ID: {ID}, 
            Customer Name: {CustomerName},
    	    Customer Email: {CustomerEmail},
    	    CustomerAddress: {CustomerAddress},
            Status: {OrderStatus}
    	    Order Date: {OrderDate},
    	    Ship Date: {ShipDate},
    	    Delivery: {Delivery},
            Items: {Items},
            TotalPrice: {TotalPrice}
 ";
}
