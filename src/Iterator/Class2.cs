using System.Collections;
using System.Collections.Generic;

namespace Iterator
{
    public class EnumerableSentMessageCollection : 
        IEnumerable<EnumerableSentMessageCollection.SentMessage>,
        IEnumerator<EnumerableSentMessageCollection.SentMessage>
    {
        public IEnumerator<EnumerableSentMessageCollection.SentMessage> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public class SentMessage { }

        public bool MoveNext()
        {
            throw new System.NotImplementedException();
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }

        public SentMessage Current { get; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}