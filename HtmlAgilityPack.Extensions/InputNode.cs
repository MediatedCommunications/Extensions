using System;
using System.Diagnostics;

namespace HtmlAgilityPack {
    public record InputNode : DisplayRecord {
        public InputNode(HtmlNode Node, string Name, string Value) {
            this.Node = Node;
            this.Name= Name;
            this.Value = Value;
        }

        public HtmlNode Node { get; init; }
        public string Name { get; init; } = Strings.Empty;
        public string Value { get; init; } = Strings.Empty;

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.AddNameValue(Name, Value)
                ;
        }

    }


}
