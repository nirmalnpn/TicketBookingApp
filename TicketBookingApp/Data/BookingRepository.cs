using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketBookingApp.Models;

namespace TicketBookingApp.Data
{
    public class BookingRepository
    {
        private readonly string _connectionString;

        public BookingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private SqlConnection Connection => new SqlConnection(_connectionString);

        public async Task<int> AddBooking(Booking booking)
        {
            using var db = Connection;
            var sql = @"INSERT INTO Bookings (UserId, AdventureId, BookingDate)
                        VALUES (@UserId, @AdventureId, @BookingDate)";
            return await db.ExecuteAsync(sql, booking);
        }

        public async Task<IEnumerable<Booking>> GetBookingsByUserId(Guid userId)
        {
            using var db = Connection;
            var sql = "SELECT * FROM Bookings WHERE UserId = @UserId";
            return await db.QueryAsync<Booking>(sql, new { UserId = userId });
        }
    }
}
