using BuildingBlocks.Exceptions;

namespace Ordering.Application.Exceptions
{
    public class OrderNotFoundException : NotFoundException
    {
        //ToDo check why I should implment its cnst on inhertance
        public OrderNotFoundException(Guid id) : base("Order", id)
        {
        }
    }
}
