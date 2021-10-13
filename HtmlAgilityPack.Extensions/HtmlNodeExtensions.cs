using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlAgilityPack {
    public static class HtmlNodeExtensions {

        private static bool Include_Default(InputNode Node) {
            return true;
        }

        private static StringComparer GetComparer(StringComparer? Comparer) {
            var ret = Comparer ?? StringComparer.InvariantCultureIgnoreCase;

            return ret;
        }

        public static Dictionary<string, InputNode> InputNodeDictionary(this HtmlNode This, IEnumerable<string> Include, StringComparer? Comparer = default) {
            Comparer ??= GetComparer(Comparer);
            var Values = Include.ToImmutableHashSet(Comparer);

            var ret = InputNodeDictionary(This, x => Values.Contains(x.Name), Comparer);

            return ret;
        }

        public static Dictionary<string, InputNode> InputNodeDictionary(this HtmlNode This, Func<InputNode, bool>? Include = default, StringComparer? Comparer = default) {
            var Should_Include = Include ?? Include_Default;

            var MyComparer = GetComparer(Comparer);

            var ret = new Dictionary<string, InputNode>(MyComparer);

            foreach (var item in This.InputNodeList()) {
                if (Should_Include(item)) {
                    ret[item.Name] = item;
                }
            }

            return ret;
        }

        public static List<InputNode> InputNodeList(this HtmlNode This) {
            var ret = new List<InputNode>();

            var Nodes = This.SelectNodes(@"//input");
            foreach (var node in Nodes) {
                var Name = new[] {
                        node.Attributes["Name"]?.Value,
                        node.Attributes["Id"]?.Value,
                    }.WhereIsNotBlank().Coalesce();

                var Value = new[] {
                        node.Attributes["Value"]?.Value,
                        node.InnerText
                    }.WhereIsNotBlank().Coalesce();

                var tret = new InputNode(node, Name, Value);
                ret.Add(tret);
            }

            return ret;

        }
    }

    public record InputNode : DisplayRecord {
        public InputNode(HtmlNode Node, string Name, string Value) {
            this.Node = Node;
            this.Name= Name;
            this.Value = Value;
        }

        public HtmlNode Node { get; init; }
        public string Name { get; init; } = string.Empty;
        public string Value { get; init; } = string.Empty;

        public override DisplayBuilder GetDebuggerDisplayBuilder(DisplayBuilder Builder) {
            return base.GetDebuggerDisplayBuilder(Builder)
                .Data.AddNameValue(Name, Value)
                ;
        }

    }

    public static class InputNodeNames {
        public static ImmutableHashSet<string> AspNet { get; }

        public static string EventTarget { get; } = "__EVENTTARGET";
        public static string EventArgument { get; } = "__EVENTARGUMENT";
        public static string EventValidation { get; } = "__EVENTVALIDATION";
        public static string ViewState { get; } = "__VIEWSTATE";
        public static string ViewStateGenerator { get; } = "__VIEWSTATEGENERATOR";

        static InputNodeNames() {
            AspNet = new[] {
                EventTarget, EventArgument, EventValidation,
                ViewState, ViewStateGenerator
            }.ToImmutableHashSet(StringComparer.InvariantCultureIgnoreCase);

        }

    }


}
