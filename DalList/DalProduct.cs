//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using DO;
using System;
using System.Linq;

namespace Dal;
using static Dal.DataSource;
using DalApi;



internal class DalProduct:IProduct
{
    //public void d()
    //{
    //    mm();
    //}
    public int Add(Product value)
    {
        if (s_listProduct.Count == NUMPRODUCTS)
            throw new FullListException();
        else
        {
            for (int j = 0; j < s_listProduct.Count; j++)
            {
                if (s_listProduct[j].Id == value.Id) 
                    throw new ExistException();
            }
            s_listProduct.Add(value);
        }
        return value.Id;
    }//add product to arr

    public Product Get(int id)
    {
        for (int i = 0; i < s_listProduct.Count; i++)
        {
            if (s_listProduct[i].Id == id)
                return s_listProduct[i];
        }
        throw new NoSuchObjectException();
    }//read the product according id

    public IEnumerable<Product> GetAll(Func<Product, bool>? func = null)
    {
        return (func == null) ? s_listProduct : s_listProduct.Where(func);
    }//read all the products by condition or not
    
    public Product Get(Predicate<Product> func)
    {
        return s_listProduct.Find(func);
    }//get by condition
    public void Update(Product value)
    {
        int j;
        bool isExist = false;
        for (j = 0; j < s_listProduct.Count && !isExist; j++)
        {
            if (s_listProduct[j].Id == value.Id)
            {
                isExist = true;
                s_listProduct[j] = value;
            }
        }
        if (!isExist)
            throw new NoSuchObjectException();
    }//update a product 
    public void Delete(int id)
    {
        int j;
        bool isExist = false;
        for (j = 0; j < s_listProduct.Count && !isExist; j++)
        {
            if (s_listProduct[j].Id == id)
            {
                isExist = true;
                s_listProduct.Remove(s_listProduct[j]);
            }
        }
        if (!isExist)
            throw new NoSuchObjectException();
    }//delete a product according id

}
