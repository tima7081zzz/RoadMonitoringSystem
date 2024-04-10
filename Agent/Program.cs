using Agent.DI;
using Common.DI;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCommon();
builder.Services.AddAgent();

var app = builder.Build();

app.Run();