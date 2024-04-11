using Microsoft.AspNetCore.Builder;
using Store.DI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddStore();

var app = builder.Build();

app.Run();