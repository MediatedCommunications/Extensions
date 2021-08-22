using System;
using System.Linq;
using System.Text;

namespace System.Threading
{

    public static class Invoke
    {
        public static Invoker_Action Sta(Action Action) { 
            return Sta(x => Action());
        }

        public static Invoker_Func<T> Sta<T>(Func<T> Action)
        {
            return Sta(x => Action());
        }

        public static Invoker_Action Mta(Action Action)
        {
            return Sta(x => Action());
        }

        public static Invoker_Func<T> Mta<T>(Func<T> Action)
        {
            return Sta(x => Action());
        }


        public static Invoker_Action Sta(Action<CancellationToken>? Action = default)
        {
            var ret = new Invoker_Action()
            {
                ApartmentState = ApartmentState.STA,
            };
            ret = ret with
            {
                Action = Action ?? ret.Action
            };

            return ret;
        }

        public static Invoker_Func<T> Sta<T>(Func<CancellationToken, T>? Action = default)
        {
            var ret = new Invoker_Func<T>()
            {
                ApartmentState = ApartmentState.STA,
            };
            ret = ret with
            {
                Action = Action ?? ret.Action
            };

            return ret;
        }

        public static Invoker_Action Mta(Action<CancellationToken>? Action = default)
        {
            var ret = new Invoker_Action()
            {
                ApartmentState = ApartmentState.MTA,
            };
            ret = ret with
            {
                Action = Action ?? ret.Action
            };

            return ret;
        }

        public static Invoker_Func<T> Mta<T>(Func<CancellationToken, T>? Action = default)
        {
            var ret = new Invoker_Func<T>()
            {
                ApartmentState = ApartmentState.MTA,
            };
            ret = ret with
            {
                Action = Action ?? ret.Action
            };

            return ret;
        }

    }

}
