using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test
{
    public class SortOrderType<T>
    {
        public Func<T, object> Selector { get; private set; }

        public SortOrderType(Func<T, object> selector)
        {
            Selector = selector;
        }
    }
}
