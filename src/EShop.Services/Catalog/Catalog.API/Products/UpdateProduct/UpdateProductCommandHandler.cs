
using Catalog.API.Exceptions;

namespace Catalog.API.Products.UpdateProduct;

internal record UpdateProductCommand(Guid Id, string Name, List<string> Categories, string Description, decimal Price, string ImageFile)
    : ICommand<UpdateProductResult>;

internal record UpdateProductResult(Product Product);

internal class UpdateProductCommandHandler
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IDocumentSession _session;
    private readonly ILogger<UpdateProductCommandHandler> _logger;

    public UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
    {
        _session = session;
        _logger = logger;
    }

    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating product {@Command}", command);

        var product = await _session.LoadAsync<Product>(command.Id) ?? throw new ProductNotFoundException();

        product.Name = command.Name;
        product.Categories = command.Categories;
        product.Description = command.Description;
        product.Price = command.Price;
        product.ImageFile = command.ImageFile;
        _session.Update(product);
        await _session.SaveChangesAsync(cancellationToken);

        return new UpdateProductResult(product);
    }
}
