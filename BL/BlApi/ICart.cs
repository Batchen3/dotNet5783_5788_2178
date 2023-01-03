using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BO;
namespace BlApi;
/// <summary>
/// the interface has 3 functions and BlCart doing them
/// </summary>
public interface ICart
{
    public static BO.Cart cart = new BO.Cart { CustomerName = "aaa", CustomerEmail = "aaa@gmail.com", CustomerAddress = "aaa", Items = new List<BO.OrderItem?>() };
    /// <summary>
    /// function add: to add new product to cart
    /// recive id of product and cart and add the product to the cart
    /// return update cart
    /// </summary>
    public Cart Add(Cart c, int id);
    /// <summary>
    /// function update: update the cart
    /// recive cart ,id of product,new amount and update the cart
    /// return update cart
    /// </summary>
    public Cart Update(Cart c, int id,int amount);
    /// <summary>
    /// function save cart :save all the cart
    /// recive cart and check if all the params are valid and save this
    /// </summary>
    public void SaveCart(Cart c);

}
