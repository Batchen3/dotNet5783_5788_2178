using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    internal static  class Simulator
    {
        private static BO.EStatus _prev;
        private static BO.EStatus _next;
        private static Thread thread;
        private static volatile bool _shouldStop;
        private static event EventHandler stopEvent;
        private static event EventHandler updateProcessEvent;

        // private static event EventHandler stopEvent;
        public static void run()
        {
            new Thread(הזמנה בחירת תהליך).Start();
           
            בחירת הזמנה השהיה סטטוס
                3דק לפי ההגרלה ;
            
           
        }
        public static void stop()
        {
            _shouldStop = true;
        }
    }
}
