﻿namespace System {
    public static class LanguageExtensions {

        public static T With<T>(this T o, params Action<T>[] x) {
            foreach (var item in x) {
                item(o);
            }

            return o;
        }

    }
}
