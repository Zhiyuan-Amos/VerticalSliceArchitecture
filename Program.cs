using System.Reflection;
using VerticalSliceArchitecture;

var appAssembly = Assembly.GetExecutingAssembly();
var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureFeatures(builder.Configuration, appAssembly);

var app = builder.Build();
app.RegisterEndpoints(appAssembly);
app.Run();
