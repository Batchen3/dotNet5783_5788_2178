using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;

using DalApi;

namespace BlImplementation;

internal class BlOrder : BlApi.IOrder
{//לבדוק תקינות של תאריכים
    private IDal dalList = new Dal.DalList();

    public IEnumerable<BO.OrderForList> GetOrders()
    {
        double sum = 0;
        IEnumerable<DO.Order> allOrders = dalList.Order.GetAll();//from dal
        List<BO.OrderForList> orders = new List<BO.OrderForList>();
        foreach (var item in allOrders)
        {
            //try&catch from "readByOrder"
            IEnumerable<DO.OrderItem> orderItemsById = dalList.OrderItem.readByOrder(item._id);//bring all orderitems according to orderId
            foreach (var orderItem in orderItemsById)
                sum += orderItem._price * orderItem._amount;//calculate the price 
            orders.Add(new BO.OrderForList { ID = 0, CustomerName = item._customerName, AmountOfItems = orderItemsById.Count(), OrderStatus = calculateStatus(item), TotalPrice = sum });
        }
        return orders;
    }
    public BO.EStatus calculateStatus(DO.Order order)//calculate status for order
    {
        DateTime today = DateTime.Now;
        if (order._delivery.CompareTo(today) < 0)//if the delivery date already was
            return BO.EStatus.arrived;
        if (order._shipDate.CompareTo(today) < 0)//if the ship date already was
            return BO.EStatus.sent;
        return BO.EStatus.confirmed;
    }
    public BO.Order GetDetailsOfOrder(int id)
    {
        if (id > 0)//check if id is valid
        {
            try
            {
                double sum = 0;
                DO.Order orderFromDal = dalList.Order.Get(id);//get order by id

                IEnumerable<DO.OrderItem> orderItemsById = dalList.OrderItem.readByOrder(orderFromDal._id);//bring all orderitems according to orderId
                List<BO.OrderItem> orderItemsList = new List<BO.OrderItem>();
                foreach (var orderItem in orderItemsById)//create for all DO.Orderitem BO.OrderItem and insert it to a list
                {
                    sum += orderItem._price * orderItem._amount;//calculate the price 
                    DO.Product product = dalList.Product.Get(orderItem._productID);//get a product by id-product
                    orderItemsList.Add(new BO.OrderItem { ID = 0, ProductID = orderItem._productID, ProductPrice = orderItem._price, ProductName = product._name, AmountsItems = orderItem._amount, TotalPriceOfItems = orderItem._amount * orderItem._price });//create BO.orderItem

                }
                BO.Order order = new BO.Order { ID = orderFromDal._id, CustomerName = orderFromDal._customerName, CustomerEmail = orderFromDal._customerEmail, CustomerAddress = orderFromDal._customerAddress, Delivery = orderFromDal._delivery, ShipDate = orderFromDal._shipDate, OrderDate = orderFromDal._orderDate, OrderStatus = calculateStatus(orderFromDal), TotalPrice = sum, Items = orderItemsList };//create BO.Order
                return order;
            }
            catch (NoSuchObjectException ex)
            {
                throw new BO.DalException(ex);
            }
        }
        else
            throw new BO.NotValidException();

    }
    public BO.Order UpdateSentOrder(int id)
    {
        try
        {
            DO.Order order = dalList.Order.Get(id);//get order by id
            if (order._shipDate == DateTime.MinValue || order._shipDate.CompareTo(DateTime.Now) > 0)//check that the date of ship didn't past
            {
                order._shipDate = DateTime.Now;//update ship date
                dalList.Order.Update(order);//update order
                BO.Order updatedOrder = GetDetailsOfOrder(id);//get the update order from the dal
                return updatedOrder;
            }
            else
                throw new BO.DateWasException();

        }
        catch (NoSuchObjectException ex)
        {
            throw new BO.DalException(ex);
        }

    }
    public BO.Order UpdateArrivedOrder(int id)
    {
        try
        {
            DO.Order order = dalList.Order.Get(id);//get order by id
            if (order._delivery == DateTime.MinValue || order._delivery.CompareTo(DateTime.Now) > 0)//check that the date of delivery didn't past
            {
                order._delivery = DateTime.Now;//update delivery date
                dalList.Order.Update(order);//update order
                BO.Order updatedOrder = GetDetailsOfOrder(id);//get the update order from the dal
                return updatedOrder;
            }
            else
                throw new BO.DateWasException();
        }
        catch (NoSuchObjectException ex)
        {
            throw new BO.DalException(ex);
        }
    }
    //public BO.Order UpdateOrder(int id)
    //{

    //}
}
