using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CQRSMediatR.Features.Products.Commands.Create
{
	public record CreateProductCommand(string Name, string Description, decimal Price) : IRequest<Guid>;

	//Firstly, the command itself, which takes in Name, Description, and Price.
	//Note that this Command object is expected to return the newly created product’s ID.


}
