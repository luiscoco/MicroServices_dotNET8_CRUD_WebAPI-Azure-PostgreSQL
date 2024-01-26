using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AzureMySQLWebAPI.Data;
using AzureMySQLWebAPI.Models;
using Npgsql;
using System;
using System.Threading.Tasks;

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
await InitializeProcedureAsync(connectionString); // Create the procedure if it doesn't exist

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

// Procedure initialization logic
async Task InitializeProcedureAsync(string connectionString)
{
    using (var connection = new NpgsqlConnection(connectionString))
    {
        await connection.OpenAsync();
        string createProcedureCommand = @"
            CREATE OR REPLACE PROCEDURE addnewitem(itemName TEXT)
            LANGUAGE plpgsql
            AS $$
            BEGIN
                INSERT INTO Items (name) VALUES (itemName);
            END;
            $$;
        ";

        using (var command = new NpgsqlCommand(createProcedureCommand, connection))
        {
            try
            {
                await command.ExecuteNonQueryAsync();
                Console.WriteLine("Procedure addnewitem created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error creating procedure: " + ex.Message);
            }
        }
    }
}
