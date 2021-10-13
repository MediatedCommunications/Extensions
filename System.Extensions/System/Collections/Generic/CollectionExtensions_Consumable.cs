namespace System.Collections.Generic {
    public static class CollectionExtensions_Consumable {

        static bool Condition_Default<T>(T _) {
            return true;
        }

        public static IEnumerable<T> GetConsumingEnumerableFirst<T>(this LinkedList<T>? This) {
            if(This is { } V1) {
                while (V1.First is { } Node) {
                    V1.RemoveFirst();
                    yield return Node.Value;
                }
            }
        }

        public static IEnumerable<T> GetConsumingEnumerableLast<T>(this LinkedList<T>? This) {
            if (This is { } V1) {
                while (V1.Last is { } Node) {
                    V1.RemoveLast();
                    yield return Node.Value;
                }
            }
        }

        public static IEnumerable<T> GetConsumingEnumerableFirst<T>(this LinkedList<T>? This, Func<T, bool> Condition) {
            var MyCondition = Condition ?? Condition_Default<T>;
            
            if(This is { } V1) {
                var Current = V1.First;

                while(Current is { } V2) {
                    var Next = V2.Next;
                
                    if (MyCondition(V2.Value)) {
                        var tret = V2.Value;
                        This.Remove(Current);
                        yield return tret;
                    }

                    Current = Next;
                }
                
            }

        }

        public static IEnumerable<T> GetConsumingEnumerableLast<T>(this LinkedList<T>? This, Func<T, bool> Condition) {
            var MyCondition = Condition ?? Condition_Default<T>;

            if (This is { } V1) {
                var Current = V1.Last;

                while (Current is { } V2) {
                    var Next = V2.Previous;

                    if (MyCondition(V2.Value)) {
                        var tret = V2.Value;
                        This.Remove(Current);
                        yield return tret;
                    }

                    Current = Next;
                }

            }
        }

        public static IEnumerable<T> GetConsumingEnumerableFirst<T>(this List<T>? This, Func<T, bool>? Condition = default) {
            var MyCondition = Condition ?? Condition_Default<T>;

            if (This is { } V1) {

                while (V1.Count > 0) {
                    if (MyCondition(V1[0])) {
                        var tret = V1[0];
                        V1.RemoveAt(0);
                        yield return tret;
                    }
                }

            }
        }

        public static IEnumerable<T> GetConsumingEnumerableLast<T>(this List<T>? This, Func<T, bool>? Condition = default) {
            var MyCondition = Condition ?? Condition_Default<T>;

            if (This is { } V1) {
                for (var i = V1.Count -1; i >= 0; i--) {
                    if (MyCondition(V1[i])) {
                        var tret = V1[i];
                        V1.RemoveAt(i);
                        yield return tret;
                    }
                }
            }
        }

        public static IEnumerable<T> GetConsumingEnumerable<T>(this Stack<T>? This) {
            if(This is { } V1) {
                while(This.TryPop(out var tret)) {
                    yield return tret;
                }
            }
        }

        public static IEnumerable<T> GetConsumingEnumerable<T>(this Queue<T>? This) {
            if (This is { } V1) {
                while (This.TryDequeue(out var tret)) {
                    yield return tret;
                }
            }
        }

    }
}
