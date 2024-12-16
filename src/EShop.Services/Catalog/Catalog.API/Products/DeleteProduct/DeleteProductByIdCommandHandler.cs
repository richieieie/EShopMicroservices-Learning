namespace Catalog.API.Products.DeleteProduct;

internal record DeleteProductByIdCommand(Guid id)
    : ICommand<DeleteProductByIdResult>;

internal record DeleteProductByIdResult(bool Success);

internal class DeleteProductByIdCommandHandler
    : ICommandHandler<DeleteProductByIdCommand, DeleteProductByIdResult>
{
    private readonly IDocumentSession _session;
    private readonly ILogger<DeleteProductByIdCommandHandler> _logger;

    public DeleteProductByIdCommandHandler(IDocumentSession session, ILogger<DeleteProductByIdCommandHandler> logger)
    {
        _session = session;
        _logger = logger;
    }

    public async Task<DeleteProductByIdResult> Handle(DeleteProductByIdCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Deleting product with id: {@Command}", command);

        _session.Delete<Product>(command.id);
        await _session.SaveChangesAsync(cancellationToken);

        return new DeleteProductByIdResult(true);
    }
}
