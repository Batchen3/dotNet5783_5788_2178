using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public interface ICrud<T>
    {
        //Add,Delete,Update,Get
        public int Add(T value);
        public void Delete(int id);
        public void Update(T p);
        public T Get(int id);
        public IEnumerable<T> GetAll();
    }
}
