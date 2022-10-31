//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System;
using static Dal.DataSource;

namespace Dal;

public class DalOrder
{
   
    public int create(DO.Order order)
    {

        Random random = new Random();
        if (config.curentIndexOrder == arrayOrder.Length - 1)
            throw new Exception("array is full");
        order.orderId = config.OrderId;
        arrayOrder[config.curentIndexOrder++] = order;
        return order.orderId;
    }
    public DO.Order read(int orderId)
    {
        for (int i = 0; i < config.curentIndexOrder; i++)
        {
            if(arrayOrder[i].orderId == orderId)
                return arrayOrder[i];
        }
        throw new Exception("Order not found");
    }

    public void delete(int orderId)
    {
        for (int i = 0; i < config.curentIndexOrder; i++)
        {
            if (arrayOrder[i].orderId == orderId)
            {
                arrayOrder[i] = arrayOrder[config.curentIndexOrder--];
            }
        }
        throw new Exception("Order not found");
    }

    public void update(DO.Order order)
    {
        bool found = false;
        int i;
        for (i = 0; i < config.curentIndexOrder && found == false; i++)
        {
            if (arrayOrder[i].orderId == order.orderId)
            {
                arrayOrder[i] = order;
                found = true;
            }
        }
        if (i == config.curentIndexOrder)
            throw new Exception("order not found");
    }
    public DO.Order[] readAll()
    {
        DO.Order[] allOrders=new DO.Order[config.curentIndexOrder];
        for (int i = 0; i < config.curentIndexOrder; i++)
        {
            allOrders[i] =  arrayOrder[i] ;
        }
        if (allOrders.Length > 0)
            return allOrders;
        throw new Exception("no orders");
    }
}
