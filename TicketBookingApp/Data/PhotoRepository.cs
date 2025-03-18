using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketBookingApp.Models;

namespace TicketBookingApp.Data
{
    public class PhotoRepository
    {
        private readonly string _connectionString;

        public PhotoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqlConnection Connection => new SqlConnection(_connectionString);

        public async Task<int> AddPhoto(Photo photo)
        {
            using var db = Connection;
            var sql = @"INSERT INTO Photos (Id, AdventureId, FileName, FilePath)
                        VALUES (@Id, @AdventureId, @FileName, @FilePath)";
            return await db.ExecuteAsync(sql, photo);
        }

        public async Task<IEnumerable<Photo>> GetPhotosByAdventureId(Guid adventureId)
        {
            using var db = Connection;
            var sql = "SELECT * FROM Photos WHERE AdventureId = @AdventureId";
            return await db.QueryAsync<Photo>(sql, new { AdventureId = adventureId });
        }
    }
}
