//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using DO;
using System;
using static Dal.DataSource;

namespace Dal;

public class DalOrderItem
{


    public int create(DO.OrderItem orderItem)
    {
        orderItem.orderItemId = config.OrderItemId;
        if (config.curentIndexOrderItem == arrayOrderItem.Length-1)
            throw new Exception("array is full");
        arrayOrderItem[config.curentIndexOrderItem++] = orderItem;
            return orderItem.orderId;
    }

    public DO.OrderItem read(int orderItemId)
    {
        for (int i = 0; i < config.curentIndexOrderItem; i++)
        {
            if (arrayOrderItem[i].orderItemId == orderItemId)
                return arrayOrderItem[i];
        }
        throw new Exception("orderItem not found");
    }

    public void delete(int orderItemId)
    {
        for (int i = 0; i < config.curentIndexOrderItem ; i++)
        {
            if (arrayOrderItem[i].orderItemId == orderItemId)
                arrayOrderItem[i]= arrayOrderItem[config.curentIndexOrderItem--];
        }
    }

    public  void update(DO.OrderItem orderItem)
    {
        bool found=false;
        int i;
        for (i = 0; i < config.curentIndexOrderItem&&found==false; i++)
        {
            if (arrayOrderItem[i].orderItemId == orderItem.orderItemId)
            { 
                arrayOrderItem[i] = orderItem;
                found=true; 
            }
        }
        if (i ==config.curentIndexOrderItem)
            throw new Exception("orderItem not found");
    }
    public DO.OrderItem[] readAll()
    {
        DO.OrderItem[] allOrderItems = new DO.OrderItem[config.curentIndexOrderItem];
        for (int i = 0; i < config.curentIndexProduct; i++)
        {
            allOrderItems[i] = arrayOrderItem[i];
        }
        return allOrderItems;
    }


    //function to  read all the orderProducts in specific order
    public int[] ReadAllProductInOrder(int orderId)
    {
        try
        {
         read(orderId);
        }
        catch (Exception)
        {

            throw new Exception("there isnt such a order");
        }
         int[] _arrOrderProductsToReturn = new int[config.OrderItemId];
        for (int i = 0, j = 0; i < config.OrderItemId; i++)
        {
            if (arrayOrderItem[i].orderId == orderId)
            {
                _arrOrderProductsToReturn[j++] = arrayOrderItem[i].orderItemId;
            }

        }
        return _arrOrderProductsToReturn;
    }

    //function to read order product by order id and  product id
    public DO.OrderItem ProductItemByOrderIDProductID(int orderId, int productId)
    {
        try
        {
           read(orderId);
        }
        catch (Exception)
        {

            throw new Exception("there isnt such an order");
        }
        try
        {
          read(productId);
        }
        catch (Exception)
        {

            throw new Exception("there isnt such a product");
        }
        for (int i = 0; i < config.OrderItemId; i++)
        {
            if (arrayOrderItem[i].orderId == orderId && arrayOrderItem[i].itemId == productId)
            {
                return arrayOrderItem[i];
            }

        }
        throw new Exception("item product wasnt found");

    }
}
