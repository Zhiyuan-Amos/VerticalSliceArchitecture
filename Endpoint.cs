using Microsoft.AspNetCore.Http.HttpResults;

namespace VerticalSliceArchitecture;

public class Endpoint : IEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/one",
                [EndpointSummary("Summary")]
                [EndpointDescription("Description")]
                Results<Ok, BadRequest> (Model model) =>
                {
                    Console.WriteLine(model.Foo);
                    return TypedResults.Ok();
                }).AddEndpointFilter<ValidationFilter>();
    }

    public class ValidationFilter(ILoggerFactory loggerFactory) : IEndpointFilter
    {
        private readonly ILogger _logger = loggerFactory.CreateLogger<ValidationFilter>();

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext efiContext,
            EndpointFilterDelegate next)
        {
            var model = efiContext.GetArgument<Model>(0);

            if (string.IsNullOrEmpty(model.Foo))
            {
                _logger.LogInformation("boo!");
                return TypedResults.BadRequest("Foo should not be empty");
            }

            return await next(efiContext);
        }
    }
}