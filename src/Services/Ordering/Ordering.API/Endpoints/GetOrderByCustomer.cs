
using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints
{
    public record GetOrderByCustomerResponse(IEnumerable<OrderDto> Orders);
    public class GetOrderByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("orders/customer/{customerId}", async (Guid customerId, ISender sender) =>
            {
                var command = new GetOrdersByCustomerQuery(customerId);

                var result = sender.Send(command);

                var response = result.Adapt<GetOrderByCustomerResponse>();

                return Results.Ok(response);
            })
            .WithName("GetOrderByCustomer")
           .Produces<GetOrderByCustomerResponse>(StatusCodes.Status200OK)
           .ProducesProblem(StatusCodes.Status400BadRequest)
           .ProducesProblem(StatusCodes.Status404NotFound)
           .WithSummary("Get Order By Customer")
           .WithDescription("Get Order By Customer");
        }
    }
}
