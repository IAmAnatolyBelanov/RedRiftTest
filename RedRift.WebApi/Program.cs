using Microsoft.AspNetCore.Diagnostics;

using RedRift.BusinessLogic;
using RedRift.Common.Exceptions;
using RedRift.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ILobbyBusinessLogic, LobbyBusinessLogic>();
builder.Services.AddScoped<IDataBusinessLogic, DataBusinessLogic>();
builder.Services.AddScoped<IGameBusinessLogic, GameBusinessLogic>();
builder.Services.AddScoped<IPlayerNotificationBusinessLogic, PlayerNotificationBusinessLogic>();

builder.Services.AddScoped<RedRiftContext>();

var app = builder.Build();

app.UseExceptionHandler(handler =>
{
    handler.Run(async context =>
    {
        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
        context.Response.ContentType = "text/plain";

        if (exceptionHandlerPathFeature.Error is UserException)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(exceptionHandlerPathFeature.Error.Message);
        }
        else
        {
            // TODO - log error
            await context.Response.WriteAsync("Something went wrong");
        }
    });
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
