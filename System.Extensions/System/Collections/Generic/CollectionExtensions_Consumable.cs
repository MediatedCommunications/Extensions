using System.Linq;

namespace System.Collections.Generic {
    public static class CollectionExtensions_Consumable {

        private static bool Condition_Default<T>(T _) {
            return true;
        }

        public static IEnumerable<T> GetConsumingEnumerableFirst<T>(this ICollection<T>? This) {
            if(This is { } V1) {
                while(V1.FirstOrDefault() is { } Item) {
                    V1.Remove(Item);

                    yield return Item;
                    
                }
            }
        }

        public static IEnumerable<T> GetConsumingEnumerableLast<T>(this ICollection<T>? This) {
            if (This is { } V1) {
                while (V1.LastOrDefault() is { } Item) {
                    V1.Remove(Item);

                    yield return Item;

                }
            }
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
                var Index = 0;

                while (Index < V1.Count) {
                    var tret = V1[Index];
                    if (MyCondition(tret)) {
                        V1.RemoveAt(Index);
                        yield return tret;
                    } else {
                        Index += 1;
                    }
                }

            }
        }

        public static IEnumerable<T> GetConsumingEnumerableEnd<T>(this List<T>? This) {

            if (This is { } V1) {
                while(V1.Count > 0) {
                    var RemoveAt = V1.Count - 1;
                    
                    var tret = V1[RemoveAt];
                    
                    V1.RemoveAt(RemoveAt);
                    yield return tret;

                }

            }
        }

        public static IEnumerable<T> GetConsumingEnumerableLast<T>(this List<T>? This, Func<T, bool>? Condition = default) {
            var MyCondition = Condition ?? Condition_Default<T>;

            if (This is { } V1) {
                var Index = V1.Count - 1;
                while(Index >= 0) {
                    
                    var tret = V1[Index];
                    
                    if (MyCondition(tret)) {
                        V1.RemoveAt(Index);
                        yield return tret;
                    }

                    Index -= 1;
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
