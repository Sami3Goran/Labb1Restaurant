using Labb1Restaurant.Data.Repos.IRepos;
using Labb1Restaurant.Models;
using Labb1Restaurant.Models.DTOs.Table;
using Labb1Restaurant.Services.IServices;

namespace Labb1Restaurant.Services
{
    public class TableService : ITableService
    {
        private readonly ITableRepository _tableRepository;

        public TableService(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public async Task AddTableAsync(TableDTO tableDTO)
        {
            if (tableDTO == null)
            {
                throw new ArgumentNullException(nameof(tableDTO), "Table cannot be null.");
            }

            var tableNew = new Table
            {
                TableNumber = tableDTO.TableNumber,
                TableSeats = tableDTO.TableSeats,
            };

            await _tableRepository.AddTableAsync(tableNew);
        }

        public async Task DeleteTableAsync(int tableId)
        {
            try
            {
                await _tableRepository.DeleteTableAsync(tableId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed when trying to delete table {ex.Message}");
            }
        }

        public async Task<IEnumerable<TableInfoAllDTO>> GetAllTablesAsync()
        {
            var tableList = await _tableRepository.GetAllTablesAsync();
            return tableList.Select(t => new TableInfoAllDTO
            {
                TableId = t.TableId,
                TableNumber = t.TableNumber,
                TableSeats = t.TableSeats,
            }).ToList();
        }

        public async Task<TableInfoAllDTO> GetTableByIdAsync(int tableId)
        {
            var existingTable = await _tableRepository.GetTableByIdAsync(tableId);

            if (existingTable == null)
            {
                throw new KeyNotFoundException($"Couldn't find table with ID:{tableId}");
            }

            return new TableInfoAllDTO
            {
                TableId = existingTable.TableId,
                TableNumber = existingTable.TableNumber,
                TableSeats = existingTable.TableSeats
            };
        }

        public async Task UpdateTableAsync(int tableId, TableDTO tableDTO)
        {
            var tableUp = await _tableRepository.GetTableByIdAsync(tableId);
            
            if (tableUp == null)
            {
                throw new InvalidOperationException($"Couldnt find table with ID:{tableId}");
            }

            tableUp.TableNumber = tableDTO.TableNumber;
            tableUp.TableSeats = tableDTO.TableSeats;

            await _tableRepository.UpdateTableAsync(tableUp);
        }
    }
}
