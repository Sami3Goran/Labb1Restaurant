using Labb1Restaurant.Models;
using Labb1Restaurant.Models.DTOs.Table;

namespace Labb1Restaurant.Services.IServices
{
    public interface ITableService
    {
        Task<IEnumerable<TableInfoAllDTO>> GetAllTablesAsync(); 
        Task<TableInfoAllDTO> GetTableByIdAsync(int tableId); 

        Task AddTableAsync(TableDTO tableDTO); 

        Task UpdateTableAsync(int tableId, TableDTO tableDTO);

        Task DeleteTableAsync(int tableId);
    }
}
