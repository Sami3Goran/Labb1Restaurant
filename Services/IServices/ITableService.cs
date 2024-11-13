using Labb1Restaurant.Models;
using Labb1Restaurant.Models.DTOs.Table;

namespace Labb1Restaurant.Services.IServices
{
    public interface ITableService
    {
        Task<IEnumerable<TableInfoAllDTO>> GetAllTablesAsync(); 
        Task<TableInfoAllDTO> GetTableByIdAsync(int tableId); 

        Task AddTableAsync(TableDTO table);

        Task<IEnumerable<TableInfoAllDTO>> GetAvailableTablesAsync(int guestAttending);

        Task UpdateTableAsync(int tableId, TableDTO table);

        Task DeleteTableAsync(int tableId);
    }
}
