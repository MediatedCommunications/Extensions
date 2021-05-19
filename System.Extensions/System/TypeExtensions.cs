using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System {
    public static class TypeExtensions {
        public static HashSet<Type> AllBaseTypes(this Type? Type) {
            var ret = new HashSet<Type>();
            if (Type is { }) {

                ret.Add(Type);

                {
                    var Interfaces = Type.GetInterfaces();

                    foreach (var Interface in Interfaces) {
                        foreach (var ChildType in AllBaseTypes(Interface)) {
                            ret.Add(ChildType);
                        }
                    }

                }

                {
                    var BaseType = Type.BaseType;
                    foreach (var ChildType in AllBaseTypes(BaseType)) {
                        ret.Add(ChildType);
                    }
                }

            }

            return ret;
        }

    }
}
