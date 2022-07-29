namespace System {
    public static class RetryExtensions {
        public static T OnFailure<T>(this T This, RetryFailureResult Value) where T : RetryBase {
            return This with {
                OnFailure = Value
            };
        }

        public static T MaxAttemptsIs<T>(this T This, int Value) where T : RetryBase {
            return This with {
                MaxAttempts = Value
            };
        }

    }


}
