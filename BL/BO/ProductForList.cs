using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class ProductForList
{
    /// <summary>
    ///property id for ProductForList
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    ///property ProductName for ProductForList
    /// </summary>
    public string? ProductName { get; set; }
    /// <summary>
    ///property ProductPrice for ProductForList
    /// </summary>
    public double ProductPrice { get; set; }
    /// <summary>
    ///property Category for ProductForList
    /// </summary>
    public ECategory Category { get; set; }
    /// <summary>
    ///property Parve for ProductForList
    /// </summary>
    public bool Parve { get; set; }
    /// <summary>
    /// function "to string" for  print the ProductForList
    /// </summary>
    public override string ToString() => $@"
            ProductItem ID: {ID}, 
            Product Name: {ProductName}, 
            Product Price: {ProductPrice},
    	    Category: {Category},
            Parve: {Parve}
            ";
}
