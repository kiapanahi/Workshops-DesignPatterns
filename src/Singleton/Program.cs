using System;
using System.Threading;

namespace Singleton
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //var instance  = Service.FieldSingleton;
            //var instance = Service.LazyPropertySingleton;

            // Create the worker thread object. This does not start the thread.
            var workerObject = new Worker();
            var workerThread = new Thread(workerObject.DoWork);

            // Start the worker thread.
            workerThread.Start();
            Console.WriteLine("Main thread: starting worker thread...");

            // Loop until the worker thread activates.
            while (!workerThread.IsAlive)
            {
            }

            // Put the main thread to sleep for 500 milliseconds to
            // allow the worker thread to do some work.
            Thread.Sleep(500);

            // Request that the worker thread stop itself.
            workerObject.RequestStop();

            // Use the Thread.Join method to block the current thread 
            // until the object's thread terminates.
            workerThread.Join();
            Console.WriteLine("Main thread: worker thread has terminated.");
        }
    }

    internal class Service
    {
        public static readonly Service FieldSingleton = new Service();

        private static readonly Lazy<Service> Lazy = new Lazy<Service>(() => new Service());

        static Service()
        {
            Console.WriteLine("called static constructor");
        }

        private Service()
        {
            Console.WriteLine("called private constructor");
        }

        public static Service LazyPropertySingleton => Lazy.Value;
    }

    internal class Worker
    {
        // Keyword volatile is used as a hint to the compiler that this data
        // member is accessed by multiple threads.
        private /*volatile*/ bool _shouldStop;

        // This method is called when the thread is started.
        public void DoWork()
        {
            Console.WriteLine("worker started executing DoWork...");
            var work = false;
            while (!_shouldStop)
            {
                work = !work; // simulate some work
            }

            Console.WriteLine("Worker thread: terminating gracefully.");
        }

        public void RequestStop()
        {
            _shouldStop = true;
        }
    }
}