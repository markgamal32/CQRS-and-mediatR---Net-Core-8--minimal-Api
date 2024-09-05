using CQRSMediatR.Features.Products.DTOs;
using CQRSMediatR.Persistence;
using MediatR;

namespace CQRSMediatR.Features.Products.Queries.Get
{
	public class GetProductQueryHandler(AppDbContext context)
	: IRequestHandler<GetProductQuery, ProductDto?>
	{
		public async Task<ProductDto?> Handle(GetProductQuery request, CancellationToken cancellationToken)
		{
			var product = await context.Products.FindAsync(request.Id);
			if (product == null)
			{
				return null;
			}
			return new ProductDto(product.Id, product.Name, product.Description, product.Price);
		}
	}

	/*
	 * In the Handle method, we will use the ID to get the product from the database.
	 * If the result is empty, null is returned, indicating a not found result.
	 * Otherwise, we project the product data to a ProductDto object and return it.
	 */

}
