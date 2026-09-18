using Microsoft.AspNetCore.Mvc;
using Trial.Server.Models;
using Trial.Server.Services;
using Scalar.AspNetCore;
var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddResponseCaching();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
builder.Services.AddOpenApi();
builder.Services.Configure<DataBaseSettings>(
    builder.Configuration.GetSection("TrialProjectDatabaseConfiguration"));

builder.Services.AddSingleton(typeof(GenericMongoDb<>));

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = null; // defailt behaviour is PascalCase to camelCase
});


//Program.cs maps all class-based controllers using a two-step process: Discovery(via builder.Services.AddControllers()) and
//Execution Routing (via app.MapControllers()).
builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowAll");
app.UseHttpsRedirection();

//app.Use(async (context, next) =>
//{
//    context.Response.GetTypedHeaders().CacheControl =
//        new Microsoft.Net.Http.Headers.CacheControlHeaderValue()
//        {
//            Public = true,
//            MaxAge = TimeSpan.FromSeconds(1000000)
//        };
//    context.Response.Headers[Microsoft.Net.Http.Headers.HeaderNames.Vary] =
//        new string[] { "Accept-Encoding" };

//    await next();
//});

//app.UseResponseCaching();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();
app.MapGet("/health/", (string abc) => TypedResults.Ok("OKS"));

app.Run();
