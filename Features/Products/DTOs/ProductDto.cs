namespace CQRSMediatR.Features.Products.DTOs
{
	public record ProductDto(Guid Id, string Name, string Description, decimal Price);
	/* Quick Tip: It’s recommended to use records to define Data Transfer Objects,
	   as they are Immutable by default! 
	   Used only to transfer states , and nothing else 
	   One-Way Flow 
	*/


}
