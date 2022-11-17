using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;
using BO;

public interface IProduct
{
    public IEnumerable<ProductForList> GetAll();
    public IEnumerable<ProductItem> GetCatalog();


}
