using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaTagger.Core
{
    public class SortOrder
    {
        public static SortOrder<MediaFile> LastModified(OrderDirection orderDirection)
        {
            return new SortOrder<MediaFile>(
                new SortOrderType<MediaFile>(f => f.LastModified),
                orderDirection);
        }
    }

    public class SortOrder<T>
    {
        public SortOrderType<T> SortOrderType { get; private set; }
        public OrderDirection OrderDirection { get; private set; }

        public SortOrder(SortOrderType<T> sortOrderType, OrderDirection orderDirection)
        {
            SortOrderType = sortOrderType;
            OrderDirection = orderDirection;
        }

        public IEnumerable<T> Evaluate(IEnumerable<T> items)
        {
            if (OrderDirection == OrderDirection.Ascending)
                return items.OrderBy(SortOrderType.Selector);

            return items.OrderByDescending(SortOrderType.Selector);
        }
    }
}
