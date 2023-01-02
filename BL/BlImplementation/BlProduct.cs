using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using DalApi;


namespace BlImplementation;

internal class BlProduct : BlApi.IProduct
{
    private IDal dalList = DalApi.Factory.Get();

    public IEnumerable<BO.ProductForList> GetAll(Func<DO.Product, bool>? func = null)//manager:get all products as product-for-list
    {
        IEnumerable<DO.Product> listOfProducts = dalList.Product.GetAll(func);
        List<BO.ProductForList> productForList = new List<BO.ProductForList> { };
        listOfProducts.ToList().ForEach(product => productForList.Add(new BO.ProductForList { ID = product.Id, Parve = product.Parve == 1 ? true : false, ProductPrice = product.Price, ProductName = product.Name, Category = (BO.ECategory)product.Category }));
        //foreach (var product in listOfProducts)//change on product to product-for-list
        //{
        //    BO.ProductForList productForListAdd = new BO.ProductForList { ID = product.Id,Parve= product.Parve == 1 ? true : false, ProductPrice=product.Price, ProductName = product.Name, Category = (BO.ECategory)product.Category };
        //    productForList.Add(productForListAdd);
        //}
        return productForList;
    }

    public IEnumerable<BO.ProductItem> GetCatalog(Func<DO.Product, bool>? func = null)//customer:get all products as product-item
    {
        IEnumerable<DO.Product> listOfProducts = dalList.Product.GetAll(func);
        List<BO.ProductItem> productItem = new List<BO.ProductItem> { };
        listOfProducts.ToList().ForEach(product => productItem.Add(new BO.ProductItem { ID = product.Id, Parve = product.Parve == 1 ? true : false, ProductPrice = product.Price, ProductName = product.Name, Category = (BO.ECategory)product.Category, available = product.InStock > 0 ? true : false, AmountInCart = 0 }));
        //foreach (var product in listOfProducts)//change on product to product-item
        //{
        //    BO.ProductItem productItemAdd = new BO.ProductItem { ID = product.Id, Parve = product.Parve == 1 ? true : false, ProductPrice = product.Price, ProductName = product.Name, Category = (BO.ECategory)product.Category, available = product.InStock > 0 ? true : false, AmountInCart = 0 };
        //    productItem.Add(productItemAdd);
        //}
        return productItem;
    }
    public BO.Product Get(int id)//to get product by id
    {
        if (id > 0)//check if id valid
        {
            try
            {
                DO.Product product = dalList.Product.Get(id);//get the product from dal
                BO.Product newProduct = new BO.Product { ID = product.Id, Name = product.Name, Price = product.Price, Category = (BO.ECategory)product.Category, InStock = product.InStock, Parve = product.Parve };//create BO.Product from info of DO.Product
                return newProduct;
            }
            catch (NoSuchObjectException e)
            {
                throw new BO.DalException(e);
            }
        }
        else
        {
            throw new BO.NotValidException();
        }

    }
    public BO.ProductItem Get(int id, BO.Cart cart)
    {
        if (id > 0)
        {
            try
            {
                DO.Product product = dalList.Product.Get(id);//get the product from dal
                int amount = 0;
                amount = (from item in cart.Items
                          where item.ID == id
                          select item.AmountsItems).FirstOrDefault();

                //foreach (var item in cart.Items)
                //    if (item.ProductID == id)
                //        amount = item.AmountsItems;
                if (amount == 0)
                    throw new BO.ObjectNotInCartException();
                BO.ProductItem newProductItem = new BO.ProductItem { ID = product.Id, AmountInCart = amount, ProductName = product.Name, available = product.InStock > 0 ? true : false, Category = (BO.ECategory)product.Category, Parve = product.Parve == 1 ? true : false, ProductPrice = product.Price };
                return newProductItem;
            }
            catch (NoSuchObjectException e)
            {
                throw new BO.DalException(e);
            }
        }
        else
        {
            throw new BO.NotValidException();
        }
    }

    public void Add(BO.Product p)//to add product
    {
        if (p.ID <= 0 || p.Name == null || p.InStock < 0 || p.Price <= 0 || (p.Parve != 0 && p.Parve != 1))//check if the parameters are valid
            throw new BO.NotValidException();
        try
        {
            dalList.Product.Add(new DO.Product { Id = p.ID, Name = p.Name, InStock = p.InStock, Category = (DO.ECategory)p.Category, Parve = p.Parve, Price = p.Price });
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
    public void Delete(int id)//delete certain product
    {
        IEnumerable<DO.OrderItem> AllOrderItems = dalList.OrderItem.GetAll();
        DO.OrderItem? orderItem = AllOrderItems.First(item => id == item.ProductID);
        if (orderItem is null)
        {
            try
            {
                dalList.Product.Delete(id);
            }
            catch (NoSuchObjectException ex)
            {
                throw new BO.DalException(ex);
            }
        }  
        else
            throw new BO.ProductInOrderException();
    }
    public void Update(BO.Product p)//update a product
    {
        if (p.ID <= 0 || p.Name == null || p.InStock < 0 || p.Price <= 0 || (p.Parve != 0 && p.Parve != 1))//check if the parameters are valid
            throw new BO.NotValidException();
        DO.Product newProduct = new DO.Product { Id = p.ID, Name = p.Name, Price = p.Price, Category = (DO.ECategory)p.Category, InStock = p.InStock, Parve = p.Parve };//create the product in order to update
        try
        {
            dalList.Product.Update(newProduct);
        }
        catch (NoSuchObjectException ex)
        {
            throw new BO.DalException(ex);
        }

    }

    public IEnumerable<BO.ProductForList> GetByCategoryAdmin(BO.ECategory category)
    {
        return GetAll(item => item.Category == (DO.ECategory)category);
    }

    public IEnumerable<BO.ProductItem> GetByCategoryForOrder(BO.ECategory category)
    {
        return GetCatalog(item => item.Category == (DO.ECategory)category);
    }
}