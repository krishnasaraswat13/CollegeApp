using Microsoft.AspNetCore.Http.HttpResults;
using CollegeApp.Controller;
using System.Text.Json.Serialization;

//var builder = WebApplication.CreateBuilder(args);

//// 1. REGISTER CONTROLLERS: Allows the framework to find your API controllers
//builder.Services.AddControllers();

//// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
//builder.Services.AddEndpointsApiExplorer();


//var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();

//    app.UseSwaggerUI(options =>
//    {
//        options.SwaggerEndpoint("/openapi/v1.json", "v1");
//    });
//}

//// 2. MAP CONTROLLER ROUTES: Exposes controller endpoints to the routing engine
//app.MapControllers();
//app.UseAuthorization();
//app.MapControllers();
//app.Run();


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

