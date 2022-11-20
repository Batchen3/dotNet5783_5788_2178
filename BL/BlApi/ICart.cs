﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BO;
namespace BlApi;

public interface ICart
{
    public Cart Add(Cart c, int id);
    public Cart Update(Cart c, int id,int amount);
    public void SaveCart(Cart c);

}
