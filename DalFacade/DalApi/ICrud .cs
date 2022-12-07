using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi;
/// <summary>
/// interface that declare crud: Add,Delete,Update,Get
/// </summary>
public interface ICrud<T>
{
    /// <summary>
    /// function that recive value and add it to the collection
    /// </summary>
    public int Add(T value);
    /// <summary>
    /// function that recive id and delete from the collection
    /// </summary>
    public void Delete(int id);
    /// <summary>
    /// function that recive value and update it
    /// </summary>
    public void Update(T value);
    /// <summary>
    /// function that recive id and get the value from the collection
    /// </summary>
    public T Get(int id);
    //public T Get(Func<T,bool>? func = null);


    /// <summary>
    /// function that return all the collection
    /// </summary>
    /// 
    public IEnumerable<T> GetAll(Func<T, bool>? func = null);
  


    //public IEnumerable<T> GetAll();
}

