using System;

namespace MediaTagger.Core
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
