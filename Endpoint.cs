namespace VerticalSliceArchitecture;

public class Endpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/todos", 
                (Model model, CancellationToken cancellationToken) =>
                {
                    Console.WriteLine(model.Foo);
                    return Results.Ok();
                })
            .Produces(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithTags(nameof(Model));
    }
}
