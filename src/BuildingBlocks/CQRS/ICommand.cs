using MediatR;

namespace BuildingBlocks;

public interface ICommand
    : IRequest<Unit>
{
}

public interface ICommand<out TResponse>
    : IRequest<TResponse>
{
}

