﻿using System.Collections.Generic;

namespace System
{
    public static class StringExtensions_Trim {
        public static IEnumerable<string> Trim(this IEnumerable<string?>? This) {
            if(This is { })
            {
                foreach (var item in This.EmptyIfNull<string?>())
                {
                    var ret = item.Coalesce().Trim();

                    yield return ret;
                }
            }

        }


    }

}
