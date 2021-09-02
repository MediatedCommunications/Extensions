using System.Collections.Generic;

namespace System
{
    public static class TypeExtensions {
        public static HashSet<Type> AllBaseTypes(this Type? Type) {
            var ret = AllBaseTypesInternal(Type);

            return ret;
        }

        private static HashSet<Type> AllBaseTypesInternal(this Type? Type) {
            var ret = new HashSet<Type>();
            if (Type is { }) {

                ret.Add(Type);

                {
                    var Interfaces = Type.GetInterfaces();

                    foreach (var Interface in Interfaces) {
                        foreach (var ChildType in AllBaseTypesInternal(Interface)) {
                            ret.Add(ChildType);
                        }
                    }

                }

                {
                    var BaseType = Type.BaseType;
                    foreach (var ChildType in AllBaseTypesInternal(BaseType)) {
                        ret.Add(ChildType);
                    }
                }

            }

            return ret;
        }

    }
}
