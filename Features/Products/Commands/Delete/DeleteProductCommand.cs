using MediatR;

namespace CQRSMediatR.Features.Products.Commands.Delete
{
	public record DeleteProductCommand(Guid Id) : IRequest;

	/*The Delete Handler would take in the ID, 
	 * fetch the record from the database, and try to remove it.
	 * If the product with the mentioned ID is not found, it simply exits out of the handler code.
    */

}
