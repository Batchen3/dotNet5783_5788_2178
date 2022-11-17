using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class ProductForList
{
    public int ID { get; set; }
    public string ProductName { get; set; }
    public double ProductPrice { get; set; }
    public ECategory Category { get; set; }
    public override string ToString() => $@"
            ProductItem ID: {ID}, 
            Product Name: {ProductName}, 
            Product Price: {ProductPrice},
    	    Category: {Category}
            ";
}
