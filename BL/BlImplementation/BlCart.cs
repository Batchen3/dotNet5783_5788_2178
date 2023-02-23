using System.Net.Mail;
using BlApi;
using DalApi;
using System.Runtime.CompilerServices;

namespace BlImplementation;

internal class BlCart : ICart
{
    private IDal? dal = DalApi.Factory.Get();

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Cart Add(BO.Cart c, int id)//add item to cart
    {
        try
        {
            DO.Product product;
            lock (dal)
            {
                product = dal?.Product.Get(id) ?? throw new NullException();//get product
            }
            if (product.InStock <= 0)//Checking if in stock
                throw new BO.OutOfStockException();
            bool exist = false;
            var orderItem = (from item in c.Items
                             where item.ProductID == id
                             select item).FirstOrDefault();
            if (orderItem != null)
            {
                exist = true;
                orderItem.AmountsItems++;
                orderItem.TotalPriceOfItems += product.Price;
                c.TotalPrice += product.Price;
            }
            if (!exist)//if not exist in cart
            {
                BO.OrderItem newOrderItem = new BO.OrderItem { ID = 0, ProductID = product.Id, ProductName = product.Name, ProductPrice = product.Price, AmountsItems = 1, TotalPriceOfItems = product.Price };
                c.TotalPrice += product.Price;
                c.Items?.Add(newOrderItem);
            }
            return c;
        }
        catch (NoSuchObjectException e)
        {
            throw new BO.DalException(e);
        }
        catch (NullException ex)
        {
            throw new BO.DalException(ex);
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Cart Update(BO.Cart c, int id, int amount)//update product
    {
        BO.OrderItem orderItemToDelete = new BO.OrderItem();
        BO.OrderItem? orderItem;
        orderItem = (from item in c.Items
                     where item.ProductID == id
                     select item).FirstOrDefault();
        if (orderItem?.ProductID != 0)
        {
            try
            {
                if (amount > orderItem?.AmountsItems)//add to amount
                {
                    DO.Product product;
                    lock (dal)
                    {
                        product = dal?.Product.Get(id) ?? throw new NullException();
                    }
                    if (product.InStock < amount - orderItem.AmountsItems)//check if in stock
                        throw new BO.OutOfStockException();
                    orderItem.TotalPriceOfItems = amount * orderItem.ProductPrice;
                    c.TotalPrice = c.TotalPrice + orderItem.ProductPrice * (amount - orderItem.AmountsItems);
                    orderItem.AmountsItems = amount;
                }
                else if (amount < orderItem?.AmountsItems && amount != 0)//remove from amount
                {
                    orderItem.TotalPriceOfItems = amount * orderItem.ProductPrice;
                    c.TotalPrice = c.TotalPrice - orderItem.ProductPrice * (orderItem.AmountsItems - amount);
                    orderItem.AmountsItems = amount;
                }
                else if (amount == 0)//delete the item
                {
                    c.TotalPrice -= orderItem?.AmountsItems * orderItem?.ProductPrice ?? throw new NullException();
                    //c.Items.Remove(orderItem);
                    orderItemToDelete = orderItem ?? throw new NullException();
                }
            }
            catch (NullException ex)
            {
                throw new BO.DalException(ex);
            }
        }
        else
        {
            throw new BO.ObjectNotInCartException();
        }
        //foreach (var orderItem in c.Items)//exist in cart
        //    if (orderItem.ProductID == id)
        //    {
        //        exist = true;
        //        if (amount > orderItem.AmountsItems)//add to amount
        //        {
        //            DO.Product product = dal.Product.Get(id);
        //            if (product.InStock < amount - orderItem.AmountsItems)//check if in stock
        //                throw new BO.OutOfStockException();
        //            orderItem.TotalPriceOfItems = amount * orderItem.ProductPrice;
        //            c.TotalPrice = c.TotalPrice + orderItem.ProductPrice * (amount - orderItem.AmountsItems);
        //            orderItem.AmountsItems = amount;
        //        }
        //        else if (amount < orderItem.AmountsItems && amount != 0)//remove from amount
        //        {
        //            orderItem.TotalPriceOfItems = amount * orderItem.ProductPrice;
        //            c.TotalPrice = c.TotalPrice - orderItem.ProductPrice * (orderItem.AmountsItems - amount);
        //            orderItem.AmountsItems = amount;
        //        }
        //        else if (amount == 0)//delete the item
        //        {
        //            c.TotalPrice -= orderItem.AmountsItems * orderItem.ProductPrice;
        //            //c.Items.Remove(orderItem);
        //            orderItemToDelete = orderItem;
        //        }
        //    }
        if (amount == 0)
            c.Items?.Remove(orderItemToDelete);
        //if (!exist)
        //    throw new BO.ObjectNotFoundException();
        return c;
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void SaveCart(BO.Cart c)
    {
        if (c.CustomerName == null || c.CustomerName == "" || c.CustomerAddress == null)//check if the params are valid
            throw new BO.NotValidException();
        if (c.CustomerEmail != null)
        {
            try
            {
                var addr = new MailAddress(c.CustomerEmail);
            }
            catch
            {
                throw new BO.NotValidException();
            }
        }
        DO.Product product = new DO.Product();

        c.Items?.ToList().ForEach(orderItem =>
        {
            try
            {
                lock (dal)
                {
                    product = dal?.Product.Get(orderItem?.ProductID ?? throw new NullException()) ?? throw new NullException();
                }
                if (orderItem.AmountsItems <= 0)
                    throw new BO.NotValidException();
                if (orderItem.AmountsItems > product.InStock)
                    throw new BO.OutOfStockException();
            }
            catch (NoSuchObjectException e)
            {
                throw new BO.DalException(e);
            }
            catch (NullException ex)
            {
                throw new BO.DalException(ex);
            }
        });
        //create new order
        DO.Order order = new DO.Order { CustomerName = c.CustomerName, CustomerAddress = c.CustomerAddress, CustomerEmail = c.CustomerEmail, OrderDate = DateTime.Now, ShipDate = null, Delivery = null };
        try
        {
            int idOrder;
            lock (dal)
            {
                idOrder = dal?.Order.Add(order) ?? throw new NullException(); ;
            }
            c.Items?.ToList().ForEach(orderItem =>
            {
                DO.OrderItem orderItemAdd = new DO.OrderItem { ProductID = orderItem?.ProductID ?? throw new NullException(), Price = orderItem.ProductPrice, Amount = orderItem.AmountsItems, OrderID = idOrder };
                lock (dal)
                {
                    dal.OrderItem.Add(orderItemAdd);
                    product = dal.Product.Get(orderItem.ProductID);
                    product.InStock -= orderItem.AmountsItems;
                    dal.Product.Update(product);
                }
            });  
        }
        catch (FullListException ex)
        {
            throw new BO.DalException(ex);
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
