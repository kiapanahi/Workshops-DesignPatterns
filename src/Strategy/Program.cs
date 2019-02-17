using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Strategy
{
    class Program
    {
        private static PaymentService service = new PaymentService();
        static void Main(string[] args)
        {

            var s = args[0];

            var t =(LoggerType)Enum.Parse(typeof(LoggerType),s);

            service.ConfigureService(t);

            foreach (var i in Enumerable.Range(1, 10))
            {
                service.ChargeCustomer($"charing constumer... {i}")
                .GetAwaiter()
                .GetResult();
            }

            Console.ReadLine();
        }
    }

    enum LoggerType
    {
        KafkaLogger,
        ConsoleLogger
    }
    class PaymentService
    {
        private ILogBehaviour _logger;

        public void ConfigureService(LoggerType loggerType)
        {
            switch (loggerType)
            {
                case LoggerType.ConsoleLogger:
                    this._logger = new ConsoleLogger();
                    break;

                case LoggerType.KafkaLogger:
                    this._logger = new KafkaLogger();
                    break;
            }
        }

        public async Task ChargeCustomer(string message)
        {
            _logger.Log("log: charging customer");
            Console.WriteLine($"charging customer... => {message}");
            await Task.CompletedTask;
        }
    }

    interface ILogBehaviour
    {
        void Log(string message);
    }

    class KafkaLogger : ILogBehaviour
    {
        public void Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{nameof(KafkaLogger)}: logging into kafka: {message}");
            Console.ResetColor();
        }
    }

    class ConsoleLogger : ILogBehaviour
    {
        public void Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{nameof(ConsoleLogger)}: logging into Console: {message}");
            Console.ResetColor();
        }
    }
}
