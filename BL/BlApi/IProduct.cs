using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;


public interface IProduct
{
    public IEnumerable<BO.ProductForList> GetAll();
    public IEnumerable<BO.ProductItem> GetCatalog();
    public BO.Product Get(int id);
    public void Add(BO.Product p);
    public void Delete(int id);
    public void Update(BO.Product p);

}
