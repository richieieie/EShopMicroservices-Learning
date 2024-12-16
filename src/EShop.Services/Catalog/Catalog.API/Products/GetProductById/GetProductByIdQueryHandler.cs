
using Catalog.API.Exceptions;

namespace Catalog.API.Products.GetProductById;

internal record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

internal record GetProductByIdResult(Product Product);

internal class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    private readonly IDocumentSession _session;
    private readonly ILogger<GetProductByIdQueryHandler> _logger;
    public GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger)
    {
        _session = session;
        _logger = logger;
    }
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetProductByIdQueryHandler.Handle called with {@Query}.", query);

        var product = await _session.LoadAsync<Product>(query.Id, cancellationToken) ?? throw new ProductNotFoundException();

        return new GetProductByIdResult(product);
    }
}
