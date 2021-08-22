using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class HashCodeExtensions
    {
        public static void AddRange<T>(this HashCode This, IEnumerable<T>? Items)
        {
            foreach (var item in Items.Coalesce())
            {
                This.Add(item);
            }

        }

    }
}
