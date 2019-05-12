using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Iterator
{
    internal interface IIterator<T> where T : class
    {
        bool HasNext();
        T Next();
    }


    internal class Program
    {
        private static void Main()
        {
            var collection = new SentMessageCollection();
            collection.Initialize(1000);

            //foreach (var item in collection)
            //{
            //    Console.WriteLine(item);
            //}

            //get all telegrams
            var actualTelegram = collection.List.Count(a => a.Operator == MobileOperator.Telegram);
            var count = 0;
            var telegramIterator = new TelegramIterator(collection);
            while (telegramIterator.HasNext())
            {
                var item = telegramIterator.Next();
                Console.WriteLine(item);
                count++;
            }
            Console.WriteLine($"actual:{actualTelegram} - iterator:{count}");

            //get all for acc-2
            var itr = new AccountFilterIterator(collection);

            //get all for date 2019-05-09
        }
    }

    internal class AccountFilterIterator : IIterator<SentMessage> {

        private readonly SentMessageCollection _collection;

        private int index = 0;
        // Where(w=>w.Target == "acc-2")
        public AccountFilterIterator(SentMessageCollection collection)
        {
            _collection = collection;
        }
        public bool HasNext()
        {
            if (index > _collection.List.Count) return false;

            // check for acc-2 in the remaining of items;
            return true;
        }

        public SentMessage Next()
        {
            var result = _collection.List[index];
            index++;
            return result;
        }
    }

    internal class TelegramIterator : IIterator<SentMessage>
    {
        private readonly SentMessageCollection _collection;
        private int _index;

        public TelegramIterator(SentMessageCollection collection)
        {
            _collection = collection;
        }


        public bool HasNext()
        {
            return _collection
                .List
                .SkipWhile((m, i) => i < _index)
                .Any(a => a.Operator == MobileOperator.Telegram);
        }

        public SentMessage Next()
        {
            while (_index < _collection.List.Count && _collection.List[_index].Operator != MobileOperator.Telegram)
            {
                _index++;
            }
            var res = _collection.List[_index];
            _index++;
            return res;
        }
    }

    internal class SentMessageCollection
    {
        public IList<SentMessage> List { get; private set; }

        public SentMessageCollection()
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