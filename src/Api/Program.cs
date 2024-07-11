using Api.Hubs;
using Api;
using Application;
using Data;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddData(builder.Configuration);
}

var app = builder.Build();
{
    app.UseCors(corsBuilder => corsBuilder
        .WithOrigins("null")
        .AllowAnyHeader()
        .SetIsOriginAllowed((_) => true)
        .AllowAnyMethod()
        .AllowCredentials());

    app.MapHub<ChatHub>("/chatHub");
    app.MapControllers();
    app.Run();
}