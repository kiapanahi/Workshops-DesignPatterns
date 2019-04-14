using System;
using System.Collections.Generic;
using System.Threading;
using CommandMemento.Commands;

namespace CommandMemento
{
    internal class Program
    {
        private static void Main()
        {
            var cts = new CancellationTokenSource();
            cts.CancelAfter(750);


            ICompany company = new FanapPlus(cts.Token);

            company.Hire(new Person("Kia", "Panahi Rad"));
            company.Hire(new Person("Steve", "Jobs"));
        }
    }

    internal interface ICompany
    {
        void Hire(Person person);
    }

    internal class FanapPlus : ICompany
    {
        private readonly CancellationToken _cancellation;
        private readonly HiringProcedureManager _hiringManager;

        public FanapPlus(CancellationToken cancellation)
        {
            _cancellation = cancellation;
            _hiringManager = new HiringProcedureManager();
        }

        public void Hire(Person person)
        {
            _hiringManager.StartHiringProcess(person, _cancellation);
            Console.WriteLine(new string('=', 50));
        }
    }

    internal class HiringProcedureManager
    {
        public void StartHiringProcess(Person person, CancellationToken cancellation)
        {
            var undoList = new Stack<ICommand>();

            var procedure = new List<ICommand>
            {
                new CreateProfileCommand(person, cancellation),
                new CreateEmailCommand(person, cancellation),
                new AssemblePcCommand(person, cancellation),
                new ConnectToNetworkCommand(person, cancellation),
                new IntroduceToOthersCommand(person, cancellation)
            };

            foreach (var step in procedure)
            {
                if (!cancellation.IsCancellationRequested)
                {
                    step.Execute();
                    undoList.Push(step);
                }
                else
                {
                    break;
                }
            }

            if (cancellation.IsCancellationRequested)
            {
                while (undoList.Count > 0)
                {
                    var s = undoList.Pop();
                    s.Undo();
                }
            }
        }
    }
}