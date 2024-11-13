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

        public async Task AddTableAsync(TableDTO table)
        {
            if (table == null)
            {
                throw new ArgumentNullException(nameof(table), "Table cannot be null.");
            }

            var tableNew = new Table
            {
                TableNumber = table.TableNumber,
                TableSeats = table.TableSeats
            };

            await _tableRepository.AddTableAsync(tableNew);
        }

        public async Task DeleteTableAsync(int tableId)
        {
            var table = await _tableRepository.GetTableByIdAsync(tableId);
            try
            {
                await _tableRepository.DeleteTableAsync(table);
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
                Id = t.Id,
                TableNumber = t.TableNumber,
                TableSeats = t.TableSeats,
            }).ToList();
        }

        public async Task<IEnumerable<TableInfoAllDTO>> GetAvailableTablesAsync(int guestAttending)
        {
            var freeTable = await _tableRepository.GetAvailableTablesAsync(guestAttending);
            return freeTable.Select(t => new TableInfoAllDTO
            {
                Id = t.Id,
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
                Id = existingTable.Id,
                TableNumber = existingTable.TableNumber,
                TableSeats = existingTable.TableSeats
            };
        }

        public async Task UpdateTableAsync(int tableId, TableDTO table)
        {
            var tableUp = await _tableRepository.GetTableByIdAsync(tableId);
            
            if (tableUp == null)
            {
                throw new InvalidOperationException($"Couldnt find table with ID:{tableId}");
            }

            tableUp.TableNumber = table.TableNumber;
            tableUp.TableSeats = table.TableSeats;

            await _tableRepository.UpdateTableAsync(tableUp);
        }
    }
}
