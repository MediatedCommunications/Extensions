namespace System.Threading
{
    public static class ThreadExtensions
    {
        public static bool TrySetName(this Thread This, string Name)
        {
            var ret = false;
            if (This.Name == null)
            {
                This.Name = Name;
                ret = true;
            }

            return ret;
        }
    }

}
