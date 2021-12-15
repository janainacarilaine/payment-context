using Shared.Commands;

namespace Shared.Handlers
{
    public interface ICommandHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);
    }
}
