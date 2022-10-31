//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System;
using static Dal.DataSource;

namespace Dal;

public class DalProduct
{
    public int create(DO.Product product)
    {
        if (config.curentIndexProduct == arrayProduct.Length - 1)
            throw new Exception("array is full");
        for (int i = 0; i < config.curentIndexProduct; i++)
        {
            if (arrayProduct[i].productId == product.productId)
                throw new Exception("productId already exists");
        }
        arrayProduct[config.curentIndexProduct++] = product;
            return product.productId;
        
    }

    public DO.Product read(int productId)
    {
        for (int i = 0; i < config.curentIndexProduct; i++)
        {
            if (arrayProduct[i].productId == productId)
                return arrayProduct[i];
        }
        throw new Exception("Product not found");
    }

    public void delete(int productId)
    {
        for (int i = 0; i < config.curentIndexProduct; i++)
        {
            if (arrayProduct[i].productId == productId)
                arrayProduct[i] = arrayProduct[config.curentIndexProduct--];
        }
    }
    public void update(DO.Product product)
    {
        bool found = false;
        int i;
        for (i = 0; i < config.curentIndexProduct && found == false; i++)
        {
            if (arrayProduct[i].productId == product.productId)
            {
                arrayProduct[i] = product;
                found = true;
            }
        }
        if (i == config.curentIndexProduct)
            throw new Exception("product not found");
    }

    public DO.Product[] readAll()
    {
        DO.Product[] allProducts = new DO.Product[config.curentIndexProduct];
        for (int i = 0; i < config.curentIndexProduct; i++)
        {
            allProducts[i] =arrayProduct[i];
        }
            return allProducts;
    }
}
