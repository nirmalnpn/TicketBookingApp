using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using TicketBookingApp.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace TicketBookingApp.Data
{
    public class AdventureRepository
    {
        private readonly string _connectionString;

        public AdventureRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection Connection => new SqlConnection(_connectionString);

        //  Get All Adventures
        public async Task<IEnumerable<Adventure>> GetAllAdventures()
        {
            using var db = Connection;
            const string sql = "SELECT * FROM Adventures";
            return await db.QueryAsync<Adventure>(sql);
        }

        //  Get Adventure by ID
        public async Task<Adventure> GetAdventureById(Guid id)
        {
            using var db = Connection;
            const string sql = "SELECT * FROM Adventures WHERE Id = @Id";
            return await db.QueryFirstOrDefaultAsync<Adventure>(sql, new { Id = id });
        }

        //  Add New Adventure
        public async Task<int> AddAdventure(Adventure adventure)
        {
            using var db = Connection;
            const string sql = @"
                INSERT INTO Adventures (Id, Name, Description, Location, Duration, Price, ImageUrl)
                VALUES (@Id, @Name, @Description, @Location, @Duration, @Price, @ImageUrl)";

            return await db.ExecuteAsync(sql, new
            {
                adventure.Id,
                adventure.Name,
                adventure.Description,
                adventure.Location,
                adventure.Duration,
                adventure.Price,
                adventure.ImageUrl
            });
        }

        //  Update Adventure
        public async Task<int> UpdateAdventure(Adventure adventure)
        {
            using var db = Connection;
            const string sql = @"
                UPDATE Adventures 
                SET Name = @Name, Description = @Description, Location = @Location, 
                    Duration = @Duration, Price = @Price, ImageUrl = @ImageUrl
                WHERE Id = @Id";

            return await db.ExecuteAsync(sql, new
            {
                adventure.Id,
                adventure.Name,
                adventure.Description,
                adventure.Location,
                adventure.Duration,
                adventure.Price,
                adventure.ImageUrl
            });
        }

        //  Delete Adventure
        public async Task<int> DeleteAdventure(Guid id)
        {
            using var db = Connection;
            const string sql = "DELETE FROM Adventures WHERE Id = @Id";
            return await db.ExecuteAsync(sql, new { Id = id });
        }
    }
}
