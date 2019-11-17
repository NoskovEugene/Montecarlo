namespace Montecarlomethod.Commands
{
    public interface ICommand
    {
        public Message Execute(string Parameter);
    }
}