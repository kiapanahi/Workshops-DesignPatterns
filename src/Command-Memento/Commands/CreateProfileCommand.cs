using System;
using System.Threading;

namespace CommandMemento.Commands
{
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
}