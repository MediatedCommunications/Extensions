using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Events.Async {
    public delegate Task AsyncDelegate<TSender, TArgs>(TSender sender, TArgs args);
}
