using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;

namespace System.Net.Http.Message.Modifiers
{
    public abstract record SetHeaderModifier : MessageModifier {
        public string Name { get; init; }
        public string? Value { get; init; }

        public SetHeaderModifier(string Name, string? Value, bool? Enabled = default) : base(Enabled) {
            this.Name = Name;
            this.Value = Value;
        }

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Id.Add(Name)
                .Data.Add(Value)
                ;
        }

    }

    public abstract record SetHeadersModifier : MessageModifier {
        public ImmutableList<KeyValuePair<string, string?>> Values { get; init; }
        public bool RemoveFirst { get; init; }

        public SetHeadersModifier(string Name, string? Value, bool? RemoveFirst = default, bool? Enabled = default) : base(Enabled) {
            this.Values = new[] {
                KeyValuePair.Create(Name, Value)
            }.ToImmutableList();

            this.RemoveFirst = RemoveFirst ?? true;

        }

        public SetHeadersModifier(IEnumerable<KeyValuePair<string, string?>> Values, bool? RemoveFirst = default, bool? Enabled = default) : base(Enabled) {
            this.Values = Values.ToImmutableList();
            this.RemoveFirst = RemoveFirst ?? true;
        }

    }

}
