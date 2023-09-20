using APICatalogo.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var config = builder.Configuration;

builder.Services.AddControllers().AddJsonOptions(options => 
                                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

#region Container de conexão com o banco de dados
builder.Services.AddDbContext<APICatalogoContext>(options => 
                                                    options.UseMySql(config.GetConnectionString("DefaultConnection"), 
                                                    ServerVersion.AutoDetect(config.GetConnectionString("DefaultConnection"))));
#endregion

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
