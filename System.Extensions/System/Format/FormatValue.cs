using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System {
    public record FormatValue<T> : DisplayRecord {
        public T Value { get; init; }
        public FormatValue(T Value) {
            this.Value = Value;
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.Add($@"{Value}")
                ;
        }
    }

}
