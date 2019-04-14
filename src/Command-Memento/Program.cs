using System;
using System.Collections.Generic;
using System.Threading;

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

    internal interface ICommand
    {
        void Execute();
        void Undo();
    }

    internal class CreateProfileCommand : ICommand
    {
        private readonly CancellationToken _cancellation;
        private readonly Person _person;

        public CreateProfileCommand(Person person, CancellationToken cancellation)
        {
            _person = person;
            _cancellation = cancellation;
        }

        public void Execute()
        {
            if (_cancellation.IsCancellationRequested)
            {
                return;
            }

            Thread.Sleep(100);

            Console.WriteLine(
                $"{nameof(CreateProfileCommand)}::{nameof(Execute)} => creating profile for {_person.Name}");
        }

        public void Undo()
        {
            Console.WriteLine($"{nameof(CreateProfileCommand)}::{nameof(Undo)} => Deleting profile of {_person.Name}");
        }
    }

    internal class CreateEmailCommand : ICommand
    {
        private readonly CancellationToken _cancellation;
        private readonly Person _person;

        public CreateEmailCommand(Person person, CancellationToken cancellation)
        {
            _person = person;
            _cancellation = cancellation;
        }

        public void Execute()
        {
            if (_cancellation.IsCancellationRequested)
            {
                return;
            }

            Thread.Sleep(100);
            Console.WriteLine($"{nameof(CreateEmailCommand)}::{nameof(Execute)} => creating email for {_person.Email}");
        }

        public void Undo()
        {
            Console.WriteLine($"{nameof(CreateEmailCommand)}::{nameof(Undo)} => Deleting email of {_person.Email}");
        }
    }

    internal class AssemblePcCommand : ICommand
    {
        private readonly CancellationToken _cancellation;
        private readonly Person _person;

        public AssemblePcCommand(Person person, CancellationToken cancellation)
        {
            _person = person;
            _cancellation = cancellation;
        }

        public void Execute()
        {
            if (_cancellation.IsCancellationRequested)
            {
                return;
            }

            Thread.Sleep(100);
            Console.WriteLine(
                $"{nameof(AssemblePcCommand)}::{nameof(Execute)} => Ordering a laptop for {_person.Name}");
        }

        public void Undo()
        {
            Console.WriteLine(
                $"{nameof(AssemblePcCommand)}::{nameof(Undo)} => Putting {_person.Name}'s laptop in storage");
        }
    }

    internal class ConnectToNetworkCommand : ICommand
    {
        private readonly CancellationToken _cancellation;
        private readonly string _ip;
        private readonly Person _person;

        public ConnectToNetworkCommand(Person person, CancellationToken cancellation)
        {
            _person = person;
            _cancellation = cancellation;
            var rnd = new Random();
            _ip = $"{rnd.Next(255)}.{rnd.Next(255)}.{rnd.Next(255)}.{rnd.Next(255)}";
        }

        public void Execute()
        {
            if (_cancellation.IsCancellationRequested)
            {
                return;
            }

            Thread.Sleep(100);
            Console.WriteLine(
                $"{nameof(ConnectToNetworkCommand)}::{nameof(Execute)} => setting IP: {_ip} for {_person.Name}");
        }

        public void Undo()
        {
            Console.WriteLine(
                $"{nameof(ConnectToNetworkCommand)}::{nameof(Undo)} => revoking {_person.Name}'s access to IP: {_ip}");
        }
    }

    internal class IntroduceToOthersCommand : ICommand
    {
        private readonly CancellationToken _cancellation;
        private readonly Person _person;

        public IntroduceToOthersCommand(Person person, CancellationToken cancellation)
        {
            _person = person;
            _cancellation = cancellation;
        }

        public void Execute()
        {
            if (_cancellation.IsCancellationRequested)
            {
                return;
            }

            Thread.Sleep(100);
            Console.WriteLine(
                $"{nameof(IntroduceToOthersCommand)}::{nameof(Execute)} => Introducing {_person.Name} to Tech1W");
        }

        public void Undo()
        {
            Console.WriteLine(
                $"{nameof(IntroduceToOthersCommand)}::{nameof(Undo)} => Saying that {_person.Name} was just another guest!");
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