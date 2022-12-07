using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    /// <summary>
    /// interface IOrderItem that DalOrderItem do it
    /// </summary>
    public interface IOrderItem: ICrud<OrderItem>
    {
        /// <summary>
        /// function read By Order And Product
        /// recive ids of product and order
        /// return the sutiable orderItem
        /// </summary>
       // public OrderItem readByOrderAndProduct(int idOrder, int idProduct);
        /// <summary>
        /// function read By Order
        /// recive id of order
        /// return the sutiable orderItems
        /// </summary>
       // public IEnumerable<OrderItem> readByOrder(int idOrder);
    }
}
