using System;
using System.Threading;

namespace CommandMemento.Commands
{
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
}