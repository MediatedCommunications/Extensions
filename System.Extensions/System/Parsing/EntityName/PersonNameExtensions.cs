namespace System {
    public static class PersonNameExtensions {
        public static string MiddleString(this PersonName This) {
            var ret = This.Middle.JoinSpace();

            return ret;
        }
    }

}
