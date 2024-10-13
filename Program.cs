using System.Reflection;
using VerticalSliceArchitecture;

var appAssembly = Assembly.GetExecutingAssembly();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.ConfigureFeatures(builder.Configuration, appAssembly);

var app = builder.Build();
app.MapOpenApi();
app.RegisterEndpoints(appAssembly);
app.Run();
