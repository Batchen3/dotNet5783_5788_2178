using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using BlApi;
using DalApi;
using DalFacade;
using System.Collections;


namespace BlImplementation;

internal class BlCart : ICart
{
    private IDal? dalList = DalApi.Factory.Get();
    //public static BO.Cart cart = new BO.Cart { CustomerName = "aaa", CustomerEmail = "aaa@gmail.com", CustomerAddress = "aaa", Items = new List<BO.OrderItem?>() };
    public BO.Cart Add(BO.Cart c, int id)//add item to cart
    {
        try
        {
            DO.Product product = dalList?.Product.Get(id) ?? throw new NullException();//get product
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
            //foreach (var orderItem in c.Items)//if it exist in cart
            //    if (orderItem.ProductID == id)
            //    {
            //        exist = true;
            //        orderItem.AmountsItems++;
            //        orderItem.TotalPriceOfItems += product.Price;
            //        c.TotalPrice += product.Price;
            //    }

            if (!exist)//if not exist in cart
            {
                BO.OrderItem newOrderItem = new BO.OrderItem { ID = 0, ProductID = product.Id, ProductName = product.Name, ProductPrice = product.Price, AmountsItems = 1, TotalPriceOfItems = product.Price };
                c.TotalPrice += product.Price;
                c.Items.Add(newOrderItem);
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
                    DO.Product product = dalList?.Product.Get(id) ?? throw new NullException();
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
        //            DO.Product product = dalList.Product.Get(id);
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
            c.Items.Remove(orderItemToDelete);
        //if (!exist)
        //    throw new BO.ObjectNotFoundException();
        return c;
    }
    public void SaveCart(BO.Cart c)
    {
        if (c.CustomerName == null|| c.CustomerName=="" || c.CustomerAddress == null)//check if the params are valid
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

        c.Items.ToList().ForEach(orderItem =>
        {
            try
            {
                product = dalList?.Product.Get(orderItem?.ProductID?? throw new NullException()) ?? throw new NullException();
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

        //foreach (var orderItem in c.Items)//check if the params are valid
        //{
        //    try
        //    {
        //        product = dalList.Product.Get(orderItem.ProductID);
        //        if (orderItem.AmountsItems <= 0)
        //            throw new BO.NotValidException();
        //        if (orderItem.AmountsItems > product.InStock)
        //            throw new BO.OutOfStockException();
        //    }
        //    catch (NoSuchObjectException e)
        //    {
        //        throw new BO.DalException(e);
        //    }
        //}

        //create new order
        DO.Order order = new DO.Order { CustomerName = c.CustomerName, CustomerAddress = c.CustomerAddress, CustomerEmail = c.CustomerEmail, OrderDate = DateTime.Now, ShipDate = null, Delivery = null };
        try
        {
            int idOrder = dalList?.Order.Add(order) ?? throw new NullException(); ;

            c.Items.ToList().ForEach(orderItem =>
            {
                DO.OrderItem orderItemAdd = new DO.OrderItem { ProductID = orderItem?.ProductID?? throw new NullException(), Price = orderItem.ProductPrice, Amount = orderItem.AmountsItems, OrderID = idOrder };
                dalList.OrderItem.Add(orderItemAdd);
                product = dalList.Product.Get(orderItem.ProductID);
                product.InStock -= orderItem.AmountsItems;
                dalList.Product.Update(product);
            });
            //foreach (var orderItem in c.Items)//create items in order
            //{
            //    DO.OrderItem orderItemAdd = new DO.OrderItem { ProductID = orderItem.ProductID, Price = orderItem.ProductPrice, Amount = orderItem.AmountsItems, OrderID = idOrder };
            //    dalList.OrderItem.Add(orderItemAdd);
            //    product = dalList.Product.Get(orderItem.ProductID);
            //    product.InStock -= orderItem.AmountsItems;
            //    dalList.Product.Update(product);
            //}
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
