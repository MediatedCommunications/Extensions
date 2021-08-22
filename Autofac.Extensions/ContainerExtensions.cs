using System;

namespace Autofac
{
    public static class ContainerExtensions {

        public static object? SafeResolve(this IContainer This, Type T) {
            var ret = default(object?);
            try {
                ret = This.TryResolve(T);
            } catch(Exception ex) {
                ex.Ignore();
            }
            return ret;
        }

        public static T? SafeResolve<T>(this IContainer This) 
            where T: class
            {
            var ret = default(T?);
            try {
                ret = This.TryResolve<T>();
            } catch(Exception ex) {
                ex.Ignore();
            }

            return ret;
        }


            public static object? TryResolve(this IContainer This, Type T) {
            var ret = default(object?);
            if(This.TryResolve(T, out var Instance)) {
                ret = Instance;
            }

            return ret;
        }

        public static T? TryResolve<T>(this IContainer This)
            where T:class {
            var ret = default(T?);

            if(This.TryResolve<T>(out var Instance)) {
                ret = Instance;
            }

            return ret;
        }
    }

}
