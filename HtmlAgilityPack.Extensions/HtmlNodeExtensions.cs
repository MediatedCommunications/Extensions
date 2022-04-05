using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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


}
