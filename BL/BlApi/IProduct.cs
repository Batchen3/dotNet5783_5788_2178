using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

/// <summary>
/// the interface has 3 functions and BlProduct doing them
/// </summary>
public interface IProduct
{
    /// <summary>
    /// function Get All for manager
    /// return all product from dal in IEnumerable<BO.ProductForList>
    /// </summary>
    public IEnumerable<BO.ProductForList> GetAll(Func<DO.Product, bool>? func = null);
    /// <summary>
    /// function Get Catalog for customer
    /// return all product for catalog in IEnumerable<BO.ProductItem>
    /// </summary>
   // public IEnumerable<BO.ProductItem> GetCatalog();
    /// <summary>
    /// function Get
    /// recive id
    /// return product from dal
    /// </summary>
    public BO.Product Get(int id);


    /// <summary>
    /// function Get
    /// recive id
    /// return product-item from dal
    /// </summary>
    public BO.ProductItem Get(int id, BO.Cart cart);

    /// <summary>
    /// function add 
    /// recive product and add it to all products
    /// </summary>
    public void Add(BO.Product p);
    /// <summary>
    /// function to delete product
    /// recive id of product and delete it
    /// </summary>
    /// <param name="id"></param>
    public void Delete(int id);
    /// <summary>
    /// function update
    /// recive product with their change  and update the product
    /// </summary>
    public void Update(BO.Product p);

    public IEnumerable<BO.ProductForList> GetByCategory(BO.ECategory category);
}
