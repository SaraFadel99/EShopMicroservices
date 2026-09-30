
namespace Catalog.API.Products.CreateProduct
{

    public record CreateProductRequest 
    {
        public Guid ID { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public List<string> Category { get; init; }
        public string ImageFile { get; init; }
        public decimal Price { get; init; }

    }
    public record CreateProductResponse(Guid Id);
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products",
                async (CreateProductRequest request ,ISender sender) => 
                {
                    var command = request.Adapt<CreateProductCommand>();
                    var result = await sender.Send(command);
                    var responseMap = result.Adapt<CreateProductResponse>();
                    return Results.Created($"/products/{responseMap.Id}", responseMap);
                })
                .WithName("CreateProduct")
                .Produces<CreateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("Create Product")
                .WithDescription("Create Product");
       
        }
    }
}
