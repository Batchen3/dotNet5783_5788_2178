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
                if (s_listProduct[j]._id == value._id) 
                    throw new ExistException();
            }
            s_listProduct.Add(value);
        }
        return value._id;
    }//add product to arr

    public Product Get(int id)
    {
        for (int i = 0; i < s_listProduct.Count; i++)
        {
            if (s_listProduct[i]._id == id)
                return s_listProduct[i];
        }
        throw new NoSuchObjectException();
    }//read the product according id

    public IEnumerable<Product> GetAll()
    {
        List<Product> tmpProducts = new List<Product> { };
        for (int i = 0; i < s_listProduct.Count; i++)
        {
            tmpProducts.Add(s_listProduct[i]);
        }
        return tmpProducts;
    }//read all products
    public void Update(Product value)
    {
        int j;
        bool isExist = false;
        for (j = 0; j < s_listProduct.Count && !isExist; j++)
        {
            if (s_listProduct[j]._id == value._id)
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
            if (s_listProduct[j]._id == id)
            {
                isExist = true;
                s_listProduct.Remove(s_listProduct[j]);
            }
        }
        if (!isExist)
            throw new NoSuchObjectException();
    }//delete a product according id

}
