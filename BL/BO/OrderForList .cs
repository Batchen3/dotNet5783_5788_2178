using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderForList
{
    public int ID { get; set; }
    public string CustomerName { get; set; }
    public EStatus OrderStatus { get; set; }
    public int AmountOfItems { get; set; }
    public double TotalPrice { get; set; }
    public override string ToString() => $@"
            ProductItem ID: {ID}, 
            Customer Name: {CustomerName}, 
            OrderStatus: {OrderStatus},
    	    Amount Of Items: {AmountOfItems},
    	    Total Price: {TotalPrice},
            ";
}
