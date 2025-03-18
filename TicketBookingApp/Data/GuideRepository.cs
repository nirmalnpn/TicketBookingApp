using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using TicketBookingApp.Models;

namespace TicketBookingApp.Data
{
    public class GuideRepository
    {
        private readonly string _connectionString;

        public GuideRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Method to get all guides
        public async Task<IEnumerable<Guide>> GetAllGuides()
        {
            using var db = new SqlConnection(_connectionString);
            var sql = "SELECT * FROM Guides";
            return await db.QueryAsync<Guide>(sql);
        }

        // Method to get a guide by ID
        public async Task<Guide> GetGuideById(Guid id)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = "SELECT * FROM Guides WHERE Id = @Id";
            return await db.QueryFirstOrDefaultAsync<Guide>(sql, new { Id = id });
        }

        // Method to add a new guide
        public async Task<int> AddGuide(Guide guide)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"INSERT INTO Guides (FullName, Bio, ImageUrl, AdventureId)
                        VALUES (@FullName, @Bio, @ImageUrl, @AdventureId)";
            return await db.ExecuteAsync(sql, guide);
        }

        // Method to update an existing guide
        public async Task<int> UpdateGuide(Guide guide)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = @"UPDATE Guides 
                        SET FullName = @FullName, Bio = @Bio, ImageUrl = @ImageUrl, AdventureId = @AdventureId
                        WHERE Id = @Id";
            return await db.ExecuteAsync(sql, guide);
        }

        // Method to delete a guide by ID
        public async Task<int> DeleteGuide(Guid id)
        {
            using var db = new SqlConnection(_connectionString);
            var sql = "DELETE FROM Guides WHERE Id = @Id";
            return await db.ExecuteAsync(sql, new { Id = id });
        }

        internal async Task<IEnumerable<GuideRepository>> GetGuidesByAdventureId(Guid adventureId)
        {
            throw new NotImplementedException();
        }

    }
}
