using CQRSMediatR.Features.Products.Commands.Create;
using CQRSMediatR.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CQRSMediatR.Features.Products.Commands.Update
{
	public class UpdateProductCommandHandler(AppDbContext context) : IRequestHandler<UpdateProductCommand>
	{
		public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
		{
			var product = await context.Products.FindAsync(new object[] { request.Id }, cancellationToken);
			if (product == null)
			{
				// Throwing an exception if the product does not exist
				throw new KeyNotFoundException($"Product with ID {request.Id} was not found.");
			}

			// Update the product properties
			product.Name = request.Name;
			product.Description = request.Description;
			product.Price = request.Price;

			await context.SaveChangesAsync(cancellationToken);
		

	}
	}
}
