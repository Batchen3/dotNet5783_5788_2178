using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;
/// <summary>
/// the interface has 3 propertis and DalList doing them
/// </summary>
public interface IDal
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
    /// property OrderItem from IOrderItem  with get method
    /// </summary>
    public IOrderItem OrderItem { get; }
}
