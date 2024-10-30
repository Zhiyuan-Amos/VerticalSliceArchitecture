using System.Reflection;
using Scalar.AspNetCore;
using VerticalSliceArchitecture;

var appAssembly = Assembly.GetExecutingAssembly();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();
app.MapOpenApi();
app.MapScalarApiReference();
app.RegisterEndpoints(appAssembly);
app.Run();
