using CQRSMediatR.Features.Products.DTOs;
using MediatR;

namespace CQRSMediatR.Features.Products.Queries.Get
{
	public record GetProductQuery(Guid Id) : IRequest<ProductDto>;

	/*
	   This record query will have a GUID parameter which will be passed on by the client. 
	  This ID will be used to query for products in the database.

	 */
}
