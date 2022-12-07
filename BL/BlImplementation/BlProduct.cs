using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;


namespace BlImplementation;

internal class BlProduct : BlApi.IProduct
{
    private IDal dalList = new Dal.DalList();

    public IEnumerable<BO.ProductForList> GetAll(Func<DO.Product, bool>? func = null)//manager:get all products as product-for-list
    {
        IEnumerable<DO.Product> listOfProducts = dalList.Product.GetAll(func);
        List<BO.ProductForList> productForList = new List<BO.ProductForList> { };
        foreach (var product in listOfProducts)//change on product to product-for-list
        {
            BO.ProductForList productForListAdd = new BO.ProductForList { ID = product.Id,Parve= product.Parve == 1 ? true : false, ProductPrice=product.Price, ProductName = product.Name, Category = (BO.ECategory)product.Category };
            productForList.Add(productForListAdd);
        }
        return productForList;
    }
    public IEnumerable<BO.ProductItem> GetCatalog()//customer:get all products as product-item
    {
        IEnumerable<DO.Product> listOfProducts = dalList.Product.GetAll();
        List<BO.ProductItem> productItem = new List<BO.ProductItem> { };
        foreach (var product in listOfProducts)//change on product to product-item
        {
            BO.ProductItem productItemAdd = new BO.ProductItem { ID = product.Id, Parve = product.Parve == 1 ? true : false, ProductPrice = product.Price, ProductName = product.Name, Category = (BO.ECategory)product.Category, available = product.InStock > 0 ? true : false, AmountInCart = 0 };
            productItem.Add(productItemAdd);
        }
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
    public void Add(BO.Product p)//to add product
    {
        if (p.ID <= 0 || p.Name == "" || p.InStock < 0 || p.Price <= 0 || (p.Parve != 0 && p.Parve != 1))//check if the parameters are valid
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
        foreach (var orderItem in AllOrderItems)//check if it is possible to delete order
        {
            if (id == orderItem.ProductID)
            {
                throw new BO.ProductInOrderException();
            }
        }
        try
        {
            dalList.Product.Delete(id);
        }
        catch (NoSuchObjectException ex)
        {
            throw new BO.DalException(ex);
        }

    }
    public void Update(BO.Product p)//update a product
    {
        if (p.ID <= 0 || p.Name == "" || p.InStock < 0 || p.Price <= 0 || (p.Parve != 0 && p.Parve != 1))//check if the parameters are valid
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

    public IEnumerable<BO.ProductForList> GetByCategory(BO.ECategory category)//update a product
    {
        IEnumerable<BO.ProductForList> productList = GetAll();
        List<BO.ProductForList> productListByCategory=new List<BO.ProductForList>();
        foreach (var item in productList)
        {
            if(item.Category == category)
                productListByCategory.Add(item);
        }
        return productListByCategory;
    }

}