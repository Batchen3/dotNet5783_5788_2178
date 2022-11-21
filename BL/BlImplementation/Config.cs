using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

internal static class Config
{
    //private static int _cartId=45550;
    //public static int CartId
    //   {
    //	get { return CartId; }
    //	set { CartId = value; }
    //}

    private static int _orderItemId = 37555;
    public static int OrderItemId
    {
        get { return OrderItemId++; }
        set { OrderItemId = value; }
    }
    private static int _productForListId = 78777;
    public static int ProductForListId
    {
        get { return ProductForListId++; }
        set { ProductForListId = value; }
    }

    private static int _productItemId = 78777;
    public static int ProductItemId
    {
        get { return ProductItemId++; }
        set { ProductItemId = value; }
    }
    private static int _orderForListId = 78777;
    public static int OrderForListId
    {
        get { return OrderForListId++; }
        set { OrderForListId = value; }
    }

}
