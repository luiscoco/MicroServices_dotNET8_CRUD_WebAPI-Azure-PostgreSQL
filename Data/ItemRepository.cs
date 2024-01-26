using Dapper;
using Npgsql;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AzureMySQLWebAPI.Models;

namespace AzureMySQLWebAPI.Data
{
    public class ItemRepository
    {
        private readonly string _connectionString;

        public ItemRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                return await db.QueryAsync<Item>("SELECT * FROM items");
            }
        }

        // Add method to retrieve a single item by id
        public async Task<Item> GetItemByIdAsync(int id)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                return await db.QueryFirstOrDefaultAsync<Item>("SELECT * FROM items WHERE id = @id", new { id = id });
            }
        }

        // Add method to insert a new item
        public async Task<int> AddItemAsync(Item item)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sql = "INSERT INTO items (name) VALUES (@name) RETURNING id;";
                return await db.ExecuteScalarAsync<int>(sql, item);
            }
        }

        // Add method to update an existing item
        public async Task UpdateItemAsync(Item item)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var sql = "UPDATE items SET name = @name WHERE id = @id";
                await db.ExecuteAsync(sql, item);
            }
        }

        // Add method to delete an item
        public async Task DeleteItemAsync(int id)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                await db.ExecuteAsync("DELETE FROM items WHERE id = @id", new { id = id });
            }
        }

        public async Task AddItemUsingStoredProcedureAsync(Item item)
        {
            using (IDbConnection db = new NpgsqlConnection(_connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("itemName", item.name, DbType.String);

                await db.ExecuteAsync("AddNewItem", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
