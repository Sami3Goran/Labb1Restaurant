using Labb1Restaurant.Data.Repos.IRepos;
using Labb1Restaurant.Models;
using Microsoft.EntityFrameworkCore;

namespace Labb1Restaurant.Data.Repos
{
    public class TableRepository : ITableRepository
    {
        private readonly Labb1RestaurantContext _context;

        public TableRepository(Labb1RestaurantContext context)
        {
            _context = context;
        }

        public async Task AddTableAsync(Table table)
        {
            await _context.Tables.AddAsync(table);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTableAsync(int tableId)
        {
            var table = await _context.Tables.FindAsync(tableId);

            if (table != null)
            {
                _context.Tables.Remove(table);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Table>> GetAllTablesAsync()
        {
            var tablesList = await _context.Tables.ToListAsync();
            return tablesList;
        }

        public async Task<IEnumerable<Booking>> GetTableBookingConnectionByIdAsync(int tableId)
        {
            var table = await _context.Tables.FindAsync(tableId);

            var bookingsList = await _context.Bookings
                                 .Where(b => b.FK_TableId == tableId)
                                 .ToListAsync();
            return bookingsList;
        }

        public async Task<Table> GetTableByIdAsync(int tableId)
        {
            var table = await _context.Tables.FindAsync(tableId);
            return table;
        }

        public async Task UpdateTableAsync(Table table)
        {
            _context.Tables.Update(table);
            await _context.SaveChangesAsync();
        }
    }
}
