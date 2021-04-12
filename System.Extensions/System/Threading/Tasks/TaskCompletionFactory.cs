namespace System.Threading.Tasks {
    public static class TaskCompletionFactory {

        private const TaskCreationOptions DefaultOptions = TaskCreationOptions.RunContinuationsAsynchronously;

        public static TaskCompletionSource Create() {
            return new TaskCompletionSource(DefaultOptions);
        }

        public static TaskCompletionSource<T> Create<T>() {
            return new TaskCompletionSource<T>(DefaultOptions);
        }



    }
}
