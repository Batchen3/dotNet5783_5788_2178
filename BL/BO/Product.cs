using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Product
{
    /// <summary>
    ///property id for Product
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    ///property name for Product
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    ///property Price for Product
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    ///property Category for Product
    /// </summary>
    public ECategory Category { get; set; }//enum category
    /// <summary>
    ///property InStock for Product
    /// </summary>
    public int InStock { get; set; }
    /// <summary>
    ///property Parve for Product
    /// </summary>
    public int Parve { get; set; }
    /// <summary>
    /// function "to string" for  print the Product
    /// </summary>
    public override string ToString() => $@"
            Product ID: {ID}, 
            Name: {Name}, 
            category: {Category},
    	    Price: {Price},
    	    Amount in stock: {InStock},
            Parve:{Parve}
            ";

}

