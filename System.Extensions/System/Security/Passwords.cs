using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Security {
    public static class Passwords {

        public static string Default { get; }

        static Passwords() {
            Default = $@"ChangeMe13579!";
        }

    }
}
