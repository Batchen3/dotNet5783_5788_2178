using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;

namespace BlImplementation;

internal class BlProduct : IProduct
{
    private IDal dalList = new Dal.DalList();
    public IEnumerable<BO.ProductForList> GetAll()
    {
        IEnumerable<DO.Product> listOfProducts = dalList.Product.GetAll();
        List<BO.ProductForList> productForList = new List<BO.ProductForList> { };
        foreach (var product in listOfProducts)
        {
            BO.ProductForList productForListAdd = new BO.ProductForList { ID = Config.ProductForListId, ProductPrice = product._price, ProductName = product._name, Category = (BO.ECategory)product._category };
            productForList.Add(productForListAdd);
        }
        return productForList;
    }
    public IEnumerable<BO.ProductItem> GetCatalog()
    {
        IEnumerable<DO.Product> listOfProducts = dalList.Product.GetAll();
        List<BO.ProductItem> productItem = new List<BO.ProductItem> { };
        foreach (var product in listOfProducts)
        {
            BO.ProductItem productItemAdd = new BO.ProductItem { ID = Config.ProductItemId, ProductPrice = product._price, ProductName = product._name, Category = (BO.ECategory)product._category, available = product._inStock > 0 ? true : false };
            //לבנתיים אין קונה לכן הכמות במלאי שווה ל0
            productItemAdd.AmountInCart = 0;
            productItem.Add(productItemAdd);
        }
        return productItem;
    }
    public DO.Product Get(int id)
    {
        if (id > 0)
        {
            try
            {
                DO.Product product = dalList.Product.Get(id);
            }
            catch (NoSuchObjectException e)
            {
                throw new BO.DalException(e);
            }


        }

    }
    public void Add(DO.Product p)
    {

    }
    public void Delete(int id)
    {

    }
    public void Update(DO.Product id)
    {

    }
}
