namespace Catalog.API.Products.GetProductByCategory;

internal record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;

internal record GetProductsByCategoryResult(IEnumerable<Product> Products);

internal class GetProductsByCategoryQueryHandler : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    private readonly IDocumentSession _session;
    private readonly ILogger<GetProductsByCategoryQuery> _logger;
    public GetProductsByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductsByCategoryQuery> logger)
    {
        _session = session;
        _logger = logger;
    }
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        _logger.LogInformation("GetProductsByCategoryQueryHandler.Handle called with {@Query}.", query);

        var products = await _session.Query<Product>()
                                    .Where(p => p.Categories.Contains(query.Category))
                                    .ToListAsync(cancellationToken);

        return new GetProductsByCategoryResult(products);
    }
}
