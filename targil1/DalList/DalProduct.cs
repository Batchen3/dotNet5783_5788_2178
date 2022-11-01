﻿//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using DO;
using System;
namespace Dal;
using static Dal.DataSource;



public class DalProduct
{
    public int create(Product p)
    {
        if (Config.moneProduct == arrayProduct.Length - 1)
            throw new Exception("the products array is full");
        else
        {
            for (int j = 0; j < Config.moneProduct; j++)
            {
                if (arrayOrder[j].ID == p.ID)
                    throw new Exception("the product is alredy exist");
            }
            arrayProduct[Config.moneProduct++] = p;
        }
        return p.ID;
    }//add product to arr
    public Product read(int id)
    {
        for (int i = 0; i < Config.moneProduct; i++)
        {
            if (arrayProduct[i].ID == id)
                return arrayProduct[i];
        }
        throw new Exception("the product not found");
    }//read the product according id

    public Product[] readAll()
    {
        Product[] tmpProducts = new Product[Config.moneProduct];
        for (int i = 0; i < Config.moneProduct; i++)
        {
            tmpProducts[i] = arrayProduct[i];
        }
        return tmpProducts;
    }//read all products
    public void update(Product p)
    {
        int j;
        bool isExist = false;
        for (j = 0; j < Config.moneProduct && !isExist; j++)
        {
            if (arrayProduct[j].ID == p.ID)
                isExist = true;
        }
        if (!isExist)
            throw new Exception("this product is not exist");
        for (int i = 0; i < Config.moneProduct; i++)
        {
            if (arrayProduct[i].ID == p.ID)
                arrayProduct[i] = p;
        }
    }//update a product 
    public void delete(int id)
    {
        int j;
        bool isExist = false;
        for (j = 0; j < Config.moneProduct && !isExist; j++)
        {
            if (arrayProduct[j].ID == id)
                isExist = true;
        }
        if (!isExist)
            throw new Exception("this product is not exist");
        for (int i = 0; i < Config.moneProduct; i++)
        {
            if (arrayProduct[i].ID == id)
            {
                if (i == Config.moneProduct)
                    Config.moneProduct--;
                else
                    arrayProduct[i] = arrayProduct[Config.moneProduct--];
            }

        }
    }//delete a product according id

}
