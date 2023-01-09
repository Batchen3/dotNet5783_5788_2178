using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;

using DalApi;

namespace BlImplementation;

internal class BlOrder : BlApi.IOrder
{
    private IDal? dalList = DalApi.Factory.Get();

    public IEnumerable<BO.OrderForList> GetOrders(Func<DO.Order, bool>? func = null)//get all orders
    {
        try
        {
            double sum;
            IEnumerable<DO.Order> allOrders = dalList?.Order.GetAll(func)??throw new NullException();//from dal
            List<BO.OrderForList> orders = new List<BO.OrderForList>();
            allOrders.ToList().ForEach(item => {
                sum = 0;
                IEnumerable<DO.OrderItem> orderItemsById = dalList.OrderItem.GetAll(element => element.OrderID == item.Id);//bring all orderitems according to orderId
                orderItemsById.ToList().ForEach(orderItem => sum += orderItem.Price * orderItem.Amount);
                orders.Add(new BO.OrderForList { ID = item.Id, CustomerName = item.CustomerName, AmountOfItems = orderItemsById.Count(), OrderStatus = calculateStatus(item), TotalPrice = sum });
            });
            return orders;
        }
        catch (NullException ex)
        {
            throw new BO.DalException(ex);
        }      
    }

    /// <summary>
    /// help function that calculate status
    /// </summary>
    private BO.EStatus calculateStatus(DO.Order order)//calculate status for order
    {
        DateTime today = DateTime.Now;
        if (order.Delivery!=null || order.Delivery?.CompareTo(today) < 0)//if the delivery date already was
            return BO.EStatus.arrived;
        if (order.ShipDate !=null|| order.ShipDate?.CompareTo(today) < 0)//if the ship date already was
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
                DO.Order orderFromDal = dalList?.Order.Get(id) ?? throw new NullException();//get order by id

                IEnumerable<DO.OrderItem> orderItemsById = dalList.OrderItem.GetAll(element => element.OrderID == orderFromDal.Id);//bring all orderitems according to orderId
                List<BO.OrderItem> orderItemsList = new List<BO.OrderItem>();
                orderItemsById.ToList().ForEach(orderItem => {
                    sum += orderItem.Price * orderItem.Amount;
                    DO.Product product = dalList.Product.Get(orderItem.ProductID);
                    orderItemsList.Add(new BO.OrderItem { ID = orderItem.Id, ProductID = orderItem.ProductID, ProductPrice = orderItem.Price, ProductName = product.Name, AmountsItems = orderItem.Amount, TotalPriceOfItems = orderItem.Amount * orderItem.Price });//create BO.orderItem
                });
                BO.Order order = new BO.Order { ID = orderFromDal.Id, CustomerName = orderFromDal.CustomerName, CustomerEmail = orderFromDal.CustomerEmail, CustomerAddress = orderFromDal.CustomerAddress, Delivery = orderFromDal.Delivery, ShipDate = orderFromDal.ShipDate, OrderDate = orderFromDal.OrderDate, OrderStatus = calculateStatus(orderFromDal), TotalPrice = sum, Items = orderItemsList };//create BO.Order
                return order;
            }
            catch (NoSuchObjectException ex)
            {
                throw new BO.DalException(ex);
            }
            catch (NullException ex)
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
            DO.Order order = dalList?.Order.Get(id) ?? throw new NullException();//get order by id
            if (order.ShipDate==null || order.ShipDate?.CompareTo(DateTime.Now) > 0)//check that the date of ship didn't past
            {
                order.ShipDate = DateTime.Now;//update ship date
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
        catch (NullException ex)
        {
            throw new BO.DalException(ex);
        }
    }
    public BO.Order UpdateArrivedOrder(int id)
    {
        try
        {
            DO.Order order = dalList?.Order.Get(id) ?? throw new NullException();//get order by id
            if (order.ShipDate==null || order.ShipDate?.CompareTo(DateTime.Now) > 0)
                throw new BO.DatesNotInCorrectOrderException();
            if (order.Delivery==null || order.Delivery?.CompareTo(DateTime.Now) > 0)//check that the date of delivery didn't past
            {
                order.Delivery = DateTime.Now;//update delivery date
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
        catch (NullException ex)
        {
            throw new BO.DalException(ex);
        }
    }
    //bonuus
    public void UpdateOrder(int idOrder, int idProduct, int amount)
    {
        try
        {
            DO.OrderItem orderItem = dalList?.OrderItem.Get(element => ((element.OrderID == idOrder) && (element.ProductID == idProduct))) ?? throw new NullException();
            DO.Order order = dalList.Order.Get(idOrder);
            if (!(order.ShipDate?.CompareTo(DateTime.Now) > 0 || order.ShipDate==null))
            {
                throw new BO.DateWasException();
            }
            if (amount < 0)
            {
                throw new BO.NotValidException();
            }
            DO.Product product = dalList.Product.Get(idProduct);
            if (orderItem.Amount > amount && amount != 0)
            {
                orderItem.Amount = amount;
                product.InStock -= orderItem.Amount - amount;
                dalList.OrderItem.Update(orderItem);
                dalList.Product.Update(product);
            }
            else if (orderItem.Amount < amount)
            {
                if (product.InStock >= amount - orderItem.Amount)
                {
                    orderItem.Amount = amount;
                    product.InStock += amount - orderItem.Amount;
                    dalList.OrderItem.Update(orderItem);
                    dalList.Product.Update(product);
                }
                else
                {
                    throw new BO.OutOfStockException();
                }
            }
            else if (amount == 0)
            {
                product.InStock += orderItem.Amount;
                dalList.Product.Update(product);
                dalList.OrderItem.Delete(orderItem.Id);
            }
        }
        catch (NoSuchObjectException ex)
        {
            throw new BO.DalException(ex);
        }
        catch (NullException ex)
        {
            throw new BO.DalException(ex);
        }

    }
    public BO.OrderTracking OrderTracking(int orderId)
    {
        try
        {
            DO.Order order = dalList?.Order.Get(orderId) ?? throw new NullException();
            BO.EStatus status = calculateStatus(order);
            List<(DateTime?, string)> newDateAndDescriptionOrder = new List<(DateTime?, string)> { };
            if (status == BO.EStatus.arrived)
            {
                newDateAndDescriptionOrder.Add((order.ShipDate??throw new NullException(), "the order has been confirmed"));
                newDateAndDescriptionOrder.Add((order.ShipDate ?? throw new NullException(), "the order is on the way"));
                newDateAndDescriptionOrder.Add((order.Delivery ?? throw new NullException(), "the order arrived"));
            }
            if (status == BO.EStatus.sent)
            {
                newDateAndDescriptionOrder.Add((order.OrderDate ?? throw new NullException(), "the order has been confirmed"));
                newDateAndDescriptionOrder.Add((order.ShipDate ?? throw new NullException(), "the order is on the way"));
            }
            if (status == BO.EStatus.confirmed)
                newDateAndDescriptionOrder.Add((order.OrderDate ?? throw new NullException(), "the order has been confirmed"));
            BO.OrderTracking orderTracking = new BO.OrderTracking { ID = orderId, OrderStatus = status, DateAndDescriptionOrder = newDateAndDescriptionOrder};
            return orderTracking;
        }
        catch (NoSuchObjectException ex)
        {
            throw new BO.DalException(ex);
        }
        catch (NullException ex)
        {
            throw new BO.DalException(ex);
        }
    }
}

