namespace System {
    public static class Versions {
        public static Version Zero2 { get; }
        public static Version Zero3 { get; }
        public static Version Zero4 { get; }

        public static Version Min { get; } 
        public static Version Max { get; } 

        static Versions() {
            Zero2 = new Version(0, 0);
            Zero3 = new Version(0, 0, 0);
            Zero4 = new Version(0, 0, 0, 0);

            Min = Zero2;
            Max = new Version(int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue);
        }

    }

}
