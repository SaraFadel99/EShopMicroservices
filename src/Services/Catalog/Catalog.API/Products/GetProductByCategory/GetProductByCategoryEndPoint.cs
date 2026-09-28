
using Catalog.API.Products.GetProducts;

namespace Catalog.API.Products.GetProductByCategory
{
    public record GetProductByCategoryRequest(string Category);
    public record GetProductByCategoryResponse(IEnumerable<Product> Products);
    public class GetProductByCategoryEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapGet("/products/category/{category}", async (string category, ISender sender) => 
           {
               var result = await sender.Send(new GetProductByCategoryQuery(category));
               var res = result.Adapt<GetProductByCategoryResponse>();
               return Results.Ok(res);
           })
             .WithName("GetProductByCategory")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products By Category")
            .WithDescription("Get Products By Category");
        }
    }
}
