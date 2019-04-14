using System;
using System.Threading;

namespace CommandMemento.Commands
{
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
}