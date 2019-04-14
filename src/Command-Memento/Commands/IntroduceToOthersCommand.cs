using System;
using System.Threading;

namespace CommandMemento.Commands
{
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
}