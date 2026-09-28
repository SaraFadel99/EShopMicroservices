
namespace Catalog.API.Products.GetProductById
{
    public record GetProductByIdQuery(Guid Id): IQuery<GetProductByIdResult>;
    public record GetProductByIdResult(Product product);
    public class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler>logger) :IQueryHandler<GetProductByIdQuery,GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("GetProductByIdQueryHandler {@request}", query);

           var prod = await session.LoadAsync<Product>(query.Id, cancellationToken);
            if (prod is null) 
            {
                throw new ProductNotFoundException();
            }
            return new GetProductByIdResult(prod);
        }



    }
}
