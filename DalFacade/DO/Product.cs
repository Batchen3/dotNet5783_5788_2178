//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace DO;

public struct Product
{
    /// <summary>
    /// property Id for Product
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// property Name for Product
    /// </summary>
    public string Name { get; set; }
    /// <summary>
            /// property Price for Product
            /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// property Category for Product
    /// </summary>
    public ECategory Category { get; set; }//enum category
    /// <summary>
    /// property InStock for Product
    /// </summary>
    public int InStock { get; set; }
    /// <summary>
    /// property Parve for Product
    /// </summary>
    public int Parve { get; set; }
    /// <summary>
    /// function toString for print product
    /// </summary>
    public override string ToString() => $@"
            Product ID: {Id}, 
            Name: {Name}, 
            category: {Category},
    	    Price: {Price},
    	    Amount in stock: {InStock},
            Parve:{Parve}
            ";

}
