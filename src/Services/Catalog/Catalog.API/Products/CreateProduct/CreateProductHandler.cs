using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name,List<string> Category, string Description,string ImageFile, decimal Price)
                :ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command ,CancellationToken token)
        {
            // create Prod entity from command object
            Product product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            //Save to DB 
            //return CreateProductResult
            return new CreateProductResult(Guid.NewGuid());
        }
    }
}
