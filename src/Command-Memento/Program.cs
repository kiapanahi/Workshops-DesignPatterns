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
            cts.CancelAfter(350);
            var company = new FanapPlus(cts.Token);
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

            _hiringManager = new HiringProcedureManager(cancellation);
        }

        public void Hire(Person person)
        {
            _hiringManager.StartHiringProcess(person);
            Console.WriteLine(new string('=', 20));
        }
    }

    internal class HiringProcedureManager
    {
        private readonly CancellationToken _cancellation;

        public HiringProcedureManager(CancellationToken cancellation)
        {
            _cancellation = cancellation;
        }

        public void StartHiringProcess(Person person)
        {
            var undoList = new Stack<ICommand>();

            var procedure = new List<ICommand>
            {
                new CreateProfileCommand(person, _cancellation),
                new CreateEmailCommand(person, _cancellation),
                new AssemblePcCommand(person, _cancellation),
                new ConnectToNetworkCommand(person, _cancellation),
                new IntroduceToOthersCommand(person, _cancellation)
            };

            foreach (var step in procedure)
            {
                if (!_cancellation.IsCancellationRequested)
                {
                    step.Execute();
                    undoList.Push(step);
                }
                else
                {
                    break;
                }
            }

            if (_cancellation.IsCancellationRequested)
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