using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface IOrderItem: ICrud<OrderItem>
    {
        public OrderItem readByOrderAndProduct(int idOrder, int idProduct);
        public IEnumerable<OrderItem> readByOrder(int idOrder);
    }
}
