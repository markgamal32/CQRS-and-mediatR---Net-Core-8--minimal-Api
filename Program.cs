using CQRSMediatR.Features.Products.Commands.Create;
using CQRSMediatR.Features.Products.Commands.Delete;
using CQRSMediatR.Features.Products.Queries.Get;
using CQRSMediatR.Features.Products.Queries.List;
using CQRSMediatR.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Registering the EF Core Database Context
builder.Services.AddDbContext<AppDbContext>();
//Registering MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Minimal API Endpoints
/*The important thing to note here is that we will be using the ISender interface from MediatR to send the commands/queries to its registered handlers.
 * Alternatively, you can also use the IMediator interface,
 * but the ISender interface is far more lightweight,
 * and you don’t always need the fully blown up IMediator interface.
 * I would use the IMediator interface if my service/endpoint wants to perform more than simple request-response messaging, like notifications, etc.*/
app.MapGet("/products/{id:guid}", async (Guid id, ISender mediatr) =>
{
	var product = await mediatr.Send(new GetProductQuery(id));
	if (product == null) return Results.NotFound();
	return Results.Ok(product);
});

app.MapGet("/products", async (ISender mediatr) =>
{
	var products = await mediatr.Send(new ListProductsQuery());
	return Results.Ok(products);
});

app.MapPost("/products", async (CreateProductCommand command, ISender mediatr) =>
{
	var productId = await mediatr.Send(command);
	if (Guid.Empty == productId) return Results.BadRequest();
	return Results.Created($"/products/{productId}", new { id = productId });
});

app.MapDelete("/products/{id:guid}", async (Guid id, ISender mediatr) =>
{
	await mediatr.Send(new DeleteProductCommand(id));
	return Results.NoContent();
});


// The line app.UseHttpsRedirection();
// in ASP.NET Core is used to ensure that requests made to the server are redirected from HTTP to HTTPS.
//When to Use It:
//This is typically used in production environments where HTTPS is configured with a valid SSL/TLS certificate.
//In development, if you're not using HTTPS locally, it can be skipped
app.UseHttpsRedirection();
app.Run();


