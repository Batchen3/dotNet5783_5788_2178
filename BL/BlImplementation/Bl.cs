using BlApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{
    /// <summary>
    /// doing interface IBl
    /// </summary>
    sealed public class Bl : IBl
    {
        /// <summary>
        /// initialize Cart to do the functions in BlCart
        /// </summary>
        public ICart Cart => new BlCart();
        /// <summary>
        /// initialize Product to do the functions in BlProduct
        /// </summary>
        public IProduct Product => new BlProduct();
        /// <summary>
        /// initialize Order to do the functions in BlOrder
        /// </summary>
        public IOrder Order => new BlOrder();
    }
}
