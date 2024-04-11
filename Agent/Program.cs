using Agent.DI;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAgent();

var app = builder.Build();

app.Run();