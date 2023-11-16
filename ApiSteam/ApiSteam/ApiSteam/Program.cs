using Microsoft.AspNetCore.Builder;
using ApiSteam.Utils;
var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Services.BuildServiceProvider().GetService<IConfiguration>();
Configuration.OwnedGamesPath = configuration.GetValue<string>("OwnedGamesPath");

builder.Configuration.AddJsonFile("appsettings.json", false, true);
builder.Configuration.AddEnvironmentVariables();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
