using System;
using System.Threading;

namespace CommandMemento.Commands
{
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
}