using Store.Domain.Commands.Intefaces;

namespace Store.Domain.Handlers.Intefaces;

public interface IHandler<T> where T : ICommand
{
    ICommandResult Handle(T command);
}
