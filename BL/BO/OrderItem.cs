using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderItem
{
    public int ID { get; set; }
    public int ProductID { get; set; }
    public string ProductName { get; set; }
    public double ProductPrice { get; set; }
    public int AmountsItems { get; set; }
    public double TotalPriceOfItems { get; set; }

    public override string ToString() => $@"
            ID: {ID},
            Product ID: {ProductID}, 
            Product Name: {ProductName},
    	    Product Price: {ProductPrice},
    	    Amounts Items: {AmountsItems},
            Total Price Of Items: {TotalPriceOfItems}
            ";
}
