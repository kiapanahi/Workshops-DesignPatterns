namespace CommandMemento.Commands
{
    internal interface ICommand
    {
        void Execute();
        void Undo();
    }
}