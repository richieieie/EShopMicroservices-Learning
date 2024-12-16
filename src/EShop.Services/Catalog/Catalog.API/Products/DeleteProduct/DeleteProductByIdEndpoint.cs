namespace Catalog.API.Products.DeleteProduct;

internal record DeleteProductByIdResponse();

public class DeleteProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id:guid}", async (Guid id, ISender sender) =>
        {
            var command = new DeleteProductByIdCommand(id);

            var result = await sender.Send(command);

            var response = result.Adapt<DeleteProductByIdResponse>();

            return Results.NoContent();
        })
        .WithName("DeleteProductById")
        .Produces<DeleteProductByIdResponse>(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound)
        .WithSummary("Get Product By Id")
        .WithDescription("Get Product By Id");
    }
}
