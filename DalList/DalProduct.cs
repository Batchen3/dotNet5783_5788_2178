//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using DO;
using System;
using System.Linq;

namespace Dal;
using static Dal.DataSource;
using DalApi;



internal class DalProduct : IProduct
{
    [MethodImpl(MethodImplOptions.Synchronized)]
    public int Add(Product value)
    {
        if (s_listProduct.Count == NUMPRODUCTS)
            throw new FullListException();
        else
        {
            try
            {
                Product product = Get(value.Id);
                throw new ExistException();
            }
            catch (NoSuchObjectException)
            {
                s_listProduct.Add(value);
                return value.Id;
            }
        }
       
    }//add product to arr

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Product Get(int id)
    {
        var product = (from item in s_listProduct
                           where item.Id == id
                           select item).FirstOrDefault();
            if(product.Id==0)
                throw new NoSuchObjectException();
            return product;

    }//read the product according id

    [MethodImpl(MethodImplOptions.Synchronized)]
    public IEnumerable<Product> GetAll(Func<Product, bool>? func = null)
    {
        return (func == null) ? s_listProduct : s_listProduct.Where(func);
    }//read all the products by condition or not

    [MethodImpl(MethodImplOptions.Synchronized)]
    public Product Get(Predicate<Product> func)
    {
        return s_listProduct.Find(func);
    }//get by condition

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Update(Product value)
    {
        try
        {
            Product product = Get(value.Id);
            var index = s_listProduct.IndexOf(product);
            s_listProduct[index] = value;
        }
       catch (NoSuchObjectException ex)
        {
            throw ex;
        }
    }//update a product 

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Delete(int id)
    {
        try
        {
            Product product = Get(id);
            s_listProduct.Remove(product); 
        }
        catch (NoSuchObjectException ex)
        {
            throw ex;
        }
    }//delete a product according id

}
