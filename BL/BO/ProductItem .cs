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
    /// <summary>
    ///property id for ProductItem
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    ///property ProductName for ProductItem
    /// </summary>
    public string ProductName { get; set; }
    /// <summary>
    ///property ProductPrice for ProductItem
    /// </summary>
    public double ProductPrice { get; set; }
    /// <summary>
    ///property Category for ProductItem
    /// </summary>
    public ECategory Category { get; set; }
    /// <summary>
    ///property parve for ProductItem
    /// </summary>
    public bool Parve { get; set; }
    /// <summary>
    ///property available for ProductItem
    /// </summary>
    public bool available { get; set; }
    /// <summary>
    ///property AmountInCart for ProductItem
    /// </summary>
    public int AmountInCart { get; set; }
    /// <summary>
    /// function "to string" for  print the ProductItem
    /// </summary>
    public override string ToString() => $@"
            ProductItem ID: {ID}, 
            Product Name: {ProductName}, 
            Product Price: {ProductPrice},
    	    Category: {Category},
    	    available: {available},
            Amount In Cart: {AmountInCart},
            Parve: {Parve}
            ";
}
