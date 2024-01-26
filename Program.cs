using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AzureMySQLWebAPI.Data;
using AzureMySQLWebAPI.Models;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register your repository
builder.Services.AddScoped<ItemRepository>(serviceProvider =>
    new ItemRepository(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Initialize the database and create the table if it doesn't exist
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
await InitializeDatabaseAsync(connectionString);

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

// Database initialization logic
async Task InitializeDatabaseAsync(string connectionString)
{
    using (var connection = new NpgsqlConnection(connectionString))
    {
        await connection.OpenAsync();
        string createTableCommand = @"
            CREATE TABLE IF NOT EXISTS public.Items
            (
                id SERIAL PRIMARY KEY,
                name VARCHAR(255) NOT NULL
                -- Add other column definitions as needed
            );
        ";

        using (var command = new NpgsqlCommand(createTableCommand, connection))
        {
            await command.ExecuteNonQueryAsync();
        }
    }
}
