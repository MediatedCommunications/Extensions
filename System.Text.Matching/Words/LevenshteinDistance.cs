﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Text.Matching {
    public static class LevenshteinDistance {
        public static int Compute<T>(IList<T> s, IList<T> t, IComparer<T> Comparer) {
            
            var n = s.Count;
            var m = t.Count;
            var d = new int[n + 1, m + 1];

            // Step 1
            if (n == 0) {
                return m;
            }

            if (m == 0) {
                return n;
            }

            // Step 2
            for (var i = 0; i <= n; d[i, 0] = i++) {
            }

            for (var j = 0; j <= m; d[0, j] = j++) {
            }

            // Step 3
            for (var i = 1; i <= n; i++) {
                //Step 4
                for (var j = 1; j <= m; j++) {
                    // Step 5

                    var Match = Comparer.Compare(t[j - 1], s[i - 1]) == 0;

                    var cost = Match ? 0 : 1;

                    // Step 6
                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }
            // Step 7
            return d[n, m];
        }
    }
}
