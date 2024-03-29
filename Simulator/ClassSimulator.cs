﻿using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NameSpaceSimulator;
public delegate void StopEvent();
public delegate void UpdateProcessEvent(BO.EStatus prev, BO.EStatus next);
public delegate void StartProcessEvent(BO.Order order, int index);
public static class ClassSimulator
{
    private static BO.EStatus _prev;
    private static BO.EStatus _next;
    private static volatile bool _shouldStop = false;
    public static event StopEvent? stopEvent = null;
    public static event UpdateProcessEvent? updateProcessEvent = null;
    public static event StartProcessEvent? startProcessEvent = null;
    public static void run()
    {
        _shouldStop = false;
        Thread? _ourThread = new Thread(() =>
        {
            Random random = new Random();
            BlApi.IBl bl = BlApi.Factory.Get();
            while (!_shouldStop)
            {
                int? id = bl.Order.ChooseOrderToHandler();
                if (id == null)
                {
                    stop();                  
                }
                else
                {
                    try
                    {
                    BO.Order order = bl.Order.GetDetailsOfOrder((int)id);
                        _prev = order.OrderStatus;
                        int index = random.Next(4000, 9000);
                        if (startProcessEvent != null)
                            startProcessEvent(order, index);
                        Thread.Sleep(index);
                        if (order.ShipDate == null)
                            order = bl.Order.UpdateSentOrder((int)id);
                        else
                            order = bl.Order.UpdateArrivedOrder((int)id);
                        _next = order.OrderStatus;
                        if (updateProcessEvent != null)
                            updateProcessEvent(_prev, _next);
                    }
                    catch (DalException ex)
                    {
                       throw ex;
                    }
                }
            }
            if (stopEvent != null)
                stopEvent();
        });
        _ourThread.Start();
    }
    public static void stop()
    {
        _shouldStop = true;
    }

}