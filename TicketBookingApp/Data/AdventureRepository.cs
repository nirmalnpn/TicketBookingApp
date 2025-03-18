using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketBookingApp.Models;

namespace TicketBookingApp.Data
{
    public class AdventureRepository
    {
        private readonly string _connectionString;

        public AdventureRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqlConnection Connection => new SqlConnection(_connectionString);

        public async Task<IEnumerable<Adventure>> GetAllAdventures()
        {
            using var db = Connection;
            var sql = "SELECT * FROM Adventures";
            return await db.QueryAsync<Adventure>(sql);
        }

        public async Task<Adventure> GetAdventureById(Guid id)
        {
            using var db = Connection;
            var sql = "SELECT * FROM Adventures WHERE Id = @Id";
            return await db.QuerySingleOrDefaultAsync<Adventure>(sql, new { Id = id });
        }
    }
}
