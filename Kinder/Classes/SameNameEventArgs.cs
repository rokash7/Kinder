using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kinder.Classes
{
    public class InvalidEventArgs<T1, T2> : EventArgs
    {
        public T1 Obj;
        public T2 InvalidObj;

        public InvalidEventArgs(T1 obj, T2 invalidObj)
        {
            Obj = obj;
            InvalidObj = invalidObj;
        }
    }
}
