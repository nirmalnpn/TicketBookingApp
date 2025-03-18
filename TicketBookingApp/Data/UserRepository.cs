using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;
using TicketBookingApp.Models;

namespace TicketBookingApp.Data
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqlConnection Connection => new SqlConnection(_connectionString);

        public async Task<User> GetUserByUsername(string username)
        {
            using var db = Connection;
            var sql = "SELECT * FROM Users WHERE Username = @Username";
            return await db.QuerySingleOrDefaultAsync<User>(sql, new { Username = username });
        }

        public async Task<int> AddUser(User user)
        {
            using var db = Connection;
            var sql = @"INSERT INTO Users (Id, FullName, Username, Password, Email, Address, PhoneNumber, NationalId, Role)
                        VALUES (@Id, @FullName, @Username, @Password, @Email, @Address, @PhoneNumber, @NationalId, @Role)";
            return await db.ExecuteAsync(sql, user);
        }
    }
}
