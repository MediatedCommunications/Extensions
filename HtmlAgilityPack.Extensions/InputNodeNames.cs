using System;
using System.Collections.Immutable;

namespace HtmlAgilityPack {
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
