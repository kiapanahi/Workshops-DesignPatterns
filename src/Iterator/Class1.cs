using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Iterator.DotNetImpl
{
    internal class SentMessageCollectionEnumerable: IEnumerator<SentMessage>
    {
        public IList<SentMessage> List { get; private set; }

        public SentMessageCollectionEnumerable()
        {
            List = new List<SentMessage>();
        }

        public void Initialize(int count)
        {
            var rnd = new Random();

            DateTime GetRandomDate()
            {
                var start = new DateTime(2019, 5, 1);
                var range = (DateTime.Today - start).Days;
                return start.AddDays(rnd.Next(range));
            }

            var targets = Enumerable.Range(1, 10).Select(s => $"acc-{s}").ToList();

            foreach (var i in Enumerable.Range(1, count))
            {
                List.Add(new SentMessage
                {
                    Operator = (MobileOperator)rnd.Next(1, 4),
                    SendDate = GetRandomDate(),
                    Target = targets[rnd.Next(targets.Count)],
                    Text = $"text {Guid.NewGuid():N}"
                });
            }
        }

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public SentMessage Current { get; }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    internal class SentMessage
    {
        public DateTime SendDate { get; set; }
        public MobileOperator Operator { get; set; }
        public string Target { get; set; }
        public string Text { get; set; }

        public override string ToString()
        {
            return $"{SendDate:s} - TO: {Target}[{Operator.ToString()}] => <{Text}>";
        }
    }

    internal enum MobileOperator
    {
        Imi = 1,
        Pardis = 2,
        Telegram = 3,
        Irancell = 4
    }
}