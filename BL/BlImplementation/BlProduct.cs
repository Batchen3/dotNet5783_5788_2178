﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;
using System.Runtime.CompilerServices;

namespace BlImplementation;

internal class BlProduct : BlApi.IProduct
{
    private IDal? dal = DalApi.Factory.Get();

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductForList> GetAll(Func<DO.Product, bool>? func = null)//manager:get all products as product-for-list
    {
        try
        {
            IEnumerable<DO.Product> listOfProducts;
           lock(dal) listOfProducts = dal?.Product.GetAll(func) ?? throw new NullException();
            List<BO.ProductForList> productForList = new List<BO.ProductForList> { };
            listOfProducts.ToList().ForEach(product => productForList.Add(new BO.ProductForList { ID = product.Id, Parve = product.Parve, ProductPrice = product.Price, ProductName = product.Name, Category = (BO.ECategory)product.Category }));
            return productForList.OrderBy(product=>product.ProductName);
        }
        catch (NullException ex)
        {
            throw new DalException(ex);
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductItem> GetCatalog(Func<DO.Product, bool>? func = null)//customer:get all products as product-item
    {
        try
        {
            IEnumerable<DO.Product> listOfProducts;
           lock(dal) listOfProducts = dal?.Product.GetAll(func) ?? throw new NullException();
            List<BO.ProductItem> productItem = new List<BO.ProductItem> { };
            listOfProducts.ToList().ForEach(product => productItem.Add(new BO.ProductItem { ID = product.Id, Parve = product.Parve, ProductPrice = product.Price, ProductName = product.Name, Category = (BO.ECategory)product.Category, available = product.InStock > 0 ? true : false, AmountInCart = 0 }));
            return productItem.OrderBy(product => product.ProductName);
        }
        catch (NullException ex)
        {
            throw new DalException(ex);
        }

    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.Product Get(int id)//to get product by id
    {
        if (id > 0)//check if id valid
        {
            try
            {
                DO.Product product;
               lock(dal) product = dal?.Product.Get(id)??throw new NullException();//get the product from dal
                BO.Product newProduct = new BO.Product { ID = product.Id, Name = product.Name, Price = product.Price, Category = (BO.ECategory)product.Category, InStock = product.InStock, Parve = product.Parve };//create BO.Product from info of DO.Product
                return newProduct;
            }
            catch (NoSuchObjectException e)
            {
                throw new BO.DalException(e);
            }
            catch (NullException ex)
            {
                throw new DalException(ex);
            }
        }
        else
        {
            throw new BO.NotValidException();
        }

    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public BO.ProductItem Get(int id, BO.Cart cart)
    {
        if (id > 0)
        {
            try
            {
                DO.Product product;
              lock(dal)product = dal?.Product.Get(id) ?? throw new NullException();//get the product from dal
                int amount = 0;
                amount = (from item in cart.Items
                          let idInCart=item.ID
                          where idInCart == id
                          select item.AmountsItems).FirstOrDefault();
                if (amount == 0)
                    throw new BO.ObjectNotInCartException();
                BO.ProductItem newProductItem = new BO.ProductItem { ID = product.Id, AmountInCart = amount, ProductName = product.Name, available = product.InStock > 0 ? true : false, Category = (BO.ECategory)product.Category, Parve = product.Parve, ProductPrice = product.Price };
                return newProductItem;
            }
            catch (NoSuchObjectException e)
            {
                throw new BO.DalException(e);
            }
            catch (NullException ex)
            {
                throw new DalException(ex);
            }
        }
        else
        {
            throw new BO.NotValidException();
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Add(BO.Product p)//to add product
    {
        if (p.ID <= 0 || p.Name == null || p.InStock < 0 || p.Price <= 0)//check if the parameters are valid
            throw new BO.NotValidException();
        try
        {
           lock(dal) dal?.Product.Add(new DO.Product { Id = p.ID, Name = p.Name, InStock = p.InStock, Category = (DO.ECategory)p.Category, Parve = p.Parve, Price = p.Price });
        }
        catch (ExistException ex)
        {
            throw new BO.DalException(ex);
        }
        catch (FullListException ex)
        {
            throw new BO.DalException(ex);
        }

    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)//delete certain product
    {
        DO.OrderItem? orderItem;
        try
        {
            IEnumerable<DO.OrderItem> AllOrderItems;
           lock(dal) AllOrderItems = dal?.OrderItem.GetAll() ?? throw new NullException();
            orderItem = AllOrderItems.First(item => id == item.ProductID);
        }
        catch (NullException ex)
        {
            throw new DalException(ex);
        }
        if (orderItem is null)
        {
            try
            {
               lock(dal) dal?.Product.Delete(id);
            }
            catch (NoSuchObjectException ex)
            {
                throw new BO.DalException(ex);
            }
        }
        else
            throw new BO.ProductInOrderException();
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(BO.Product p)//update a product
    {
        if (p.ID <= 0 || p.Name == null || p.InStock < 0 || p.Price <= 0)//check if the parameters are valid
            throw new BO.NotValidException();
        DO.Product newProduct = new DO.Product { Id = p.ID, Name = p.Name, Price = p.Price, Category = (DO.ECategory)p.Category, InStock = p.InStock, Parve = p.Parve };//create the product in order to update
        try
        {
           lock(dal) dal?.Product.Update(newProduct);
        }
        catch (NoSuchObjectException ex)
        {
            throw new BO.DalException(ex);
        }

    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductForList> GetByCategoryAdmin(BO.ECategory category)
    {
        return GetAll(item => item.Category == (DO.ECategory)category);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<BO.ProductItem> GetByCategoryForOrder(BO.ECategory category)
    {
        return GetCatalog(item => item.Category == (DO.ECategory)category);
    }
}