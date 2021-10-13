using System.Collections;
using System.Collections.Generic;

namespace System
{
    public abstract record ListParser<T> : DefaultClassParser<LinkedList<T>>, IEnumerable<T> {

        public override LinkedList<T> GetValue() {
            return GetValue(new LinkedList<T>());
        }

        public IEnumerator<T> GetEnumerator() {
            return GetValue().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }


}
