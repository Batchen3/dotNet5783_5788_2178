﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DalApi;

namespace BlImplementation;

internal class BlOrder:IOrder
{
    private IDal dalList = new Dal.DalList();
}