using Trial.Server.Services;
using Trial.Server.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.Configure<DataBaseSettings>(
    builder.Configuration.GetSection("TrialProjectDatabaseConfiguration"));

builder.Services.AddSingleton<COAService>();
builder.Services.AddSingleton<TBService>();

builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = null;
});

var app = builder.Build();

app.UseCors("AllowAll");

var tbGroup = app.MapGroup("/tb");
tbGroup.MapGet("/", async (TBService tbService) =>
{
    var tbs = await tbService.GetAsync();
    return Results.Ok(tbs);
});

tbGroup.MapPost("/", async (TBService tbService, TrialBalance newTB) =>
{
    await tbService.CreateAsync(newTB);
    return Results.Ok(newTB);
});

tbGroup.MapGet("/{id:length(24)}", async (string id, TBService tbService) =>
{
    var toa = await tbService.GetAsync(id);
    return toa is null ? Results.NotFound() : Results.Ok(toa);
});


var coaGroup = app.MapGroup("/coa");
coaGroup.MapGet("/", async (COAService coaService) =>
{
    var coas = await coaService.GetAsync();
    return Results.Ok(coas);
});

coaGroup.MapPost("/", async (COAService coaService, ChartOfAccount newCOA) =>
{
    await coaService.CreateAsync(newCOA);
    return Results.Ok(newCOA);
});

coaGroup.MapGet("/{id:length(24)}", async (string id, COAService coaService) =>
{
    var coa = await coaService.GetAsync(id);
    return coa is null ? Results.NotFound() : Results.Ok(coa);
});


app.Run();
