//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using Do;
using System;
using static Dal.DataSource;

namespace Dal;

public class DalOrder
{
    public int create(Order o)
    {
        o.ID = Config.IdOrder;
        if (Config.moneOrder > arrayOrder.Length)
            throw new Exception("the orders array is full");
        else
            arrayOrder[Config.moneOrder++] = o;
        return o.ID;
    }//create an order
    public Order read(int id)
    {
        for (int i = 0; i < Config.moneOrder; i++)
        {
            if (arrayOrder[i].ID == id)
                return arrayOrder[i];
        }
        throw new Exception("the order not found");
    }//read order according id 
    public Order[] readAll()
    {
        Order[] tmpOrders = new Order[Config.moneOrder];
        for (int i = 0; i < Config.moneOrder; i++)
        {
            tmpOrders[i] = arrayOrder[i];
        }
        return tmpOrders;
    }//read all the orders
    public void update(Order o)
    {
        int j;
        bool isExist = false;
        for (j = 0; j < Config.moneOrder && !isExist; j++)
        {
            if (arrayOrder[j].ID == o.ID)
                isExist = true;

        }
        if (!isExist)
            throw new Exception("this order is not exist");
        for (int i = 0; i < Config.moneOrder; i++)
        {
            if (arrayOrder[i].ID == o.ID)
                arrayOrder[i] = o;
        }
    }//update the order
    public void delete(int id)
    {
        int j;
        bool isExist = false;
        for (j = 0; j < Config.moneOrder && !isExist; j++)
        {
            if (arrayOrder[j].ID == id)
                isExist = true;

        }
        if (!isExist)
            throw new Exception("this order is not exist");
        for (int i = 0; i < Config.moneOrder; i++)
        {
            if (arrayOrder[i].ID == id)
            {
                if (i == Config.moneOrder)
                    Config.moneOrder--;
                else
                    arrayOrder[i] = arrayOrder[Config.moneOrder--];
            }

        }
    }//delete an order
}
