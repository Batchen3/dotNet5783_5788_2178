using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BO;

public class ProductItem
{
    public int ID { get; set; }
    public string ProductName { get; set; }
    public double ProductPrice { get; set; }
    public ECategory Category { get; set; }
    public bool available { get; set; }
    public int AmountInCart { get; set; }
    public override string ToString() => $@"
            ProductItem ID: {ID}, 
            Product Name: {ProductName}, 
            Product Price: {ProductPrice},
    	    Category: {Category},
    	    available: {available},
            Amount In Cart: {AmountInCart}
            ";
}
