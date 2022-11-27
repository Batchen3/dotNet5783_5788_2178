using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;
/// <summary>
/// the interface has 3 propertis and Bl doing them
/// </summary>
public interface IBl
{
    /// <summary>
    /// property Product from IProduct  with get method
    /// </summary>
    public IProduct Product { get; }
    /// <summary>
    /// property Order from IOrder  with get method
    /// </summary>
    public IOrder Order { get; }
    /// <summary>
    /// property Cart from ICart  with get method
    /// </summary>
    public ICart Cart { get; }
}
