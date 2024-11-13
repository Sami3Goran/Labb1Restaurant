using Labb1Restaurant.Models.DTOs.Table;
using Labb1Restaurant.Services;
using Labb1Restaurant.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Labb1Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;
        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        [Route("GetAllTables")]
        public async Task<ActionResult<IEnumerable<TableInfoAllDTO>>> GetAllTables()
        {
            var tableList = await _tableService.GetAllTablesAsync();

            if (tableList.IsNullOrEmpty())
            {
                return NotFound("There is no tables yet.");
            }

            return Ok(tableList);
        }

        [HttpGet]
        [Route("gettablebyid/{id}")]
        public async Task<ActionResult<TableInfoAllDTO>> GetTableById(int id)
        {
            var table = await _tableService.GetTableByIdAsync(id);

            if (table == null)
            {
                return NotFound("There is no tables with that ID");
            }

            return Ok(table);
        }

        // Get /api/Tables/GetAvailableTables
        [HttpGet]
        [Route("GetAvailableTables/{guestAttending}")]
        public async Task<ActionResult<IEnumerable<TableInfoAllDTO>>> GetAvailableTables(int guestAttending)
        {
            var freeTable = await _tableService.GetAvailableTablesAsync(guestAttending);

            if (freeTable.IsNullOrEmpty())
            {
                return NotFound("No tables found.");
            }

            return Ok(freeTable);
        }

        [HttpPost]
        [Route("AddTable")]
        public async Task<ActionResult> AddTable([FromBody] TableDTO table)
        {
            try
            {
                await _tableService.AddTableAsync(table);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("perfect! well done, table has been added.");
        }

        [HttpPut]
        [Route("UpdateTable/{id}")]
        public async Task<ActionResult> UpdateTable(int id, [FromBody] TableDTO table)
        {
            try
            {
                await _tableService.UpdateTableAsync(id, table);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return Ok("table has been updated");
        }

        [HttpDelete]
        [Route("DeleteTable/{id}")]
        public async Task<ActionResult> DeleteTable(int id)
        {
            try
            {
                await _tableService.DeleteTableAsync(id);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"{ex.Message}");
            }

            return Ok("Good job, table has been removed...");
        }
    }
}
