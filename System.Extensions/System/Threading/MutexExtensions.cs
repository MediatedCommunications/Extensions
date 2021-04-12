namespace System.Threading {
    public static class MutexExtensions {

        public static bool Acquire(this Mutex This) {
            var ret = false;
            
            try {
                ret = This.WaitOne(0);
            
            } catch(AbandonedMutexException ex) {
                ex.Ignore();

                ret = true;
            } catch (Exception ex) {
                ex.Ignore();
            }

            return ret;
        }

    }
}
