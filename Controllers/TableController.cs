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
        [Route("gettablebyid/{tableId}")]
        public async Task<ActionResult<TableInfoAllDTO>> GetTableById(int tableId)
        {
            var table = await _tableService.GetTableByIdAsync(tableId);

            return Ok(table);
        }

        [HttpPost]
        [Route("AddTable")]
        public async Task<ActionResult> AddTable(TableDTO tableDTO)
        {
            try
            {
                await _tableService.AddTableAsync(tableDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok("perfect! well done, table has been added.");
        }

        [HttpPut]
        [Route("/UpdateTable/{tableId}")]
        public async Task<ActionResult> UpdateTable(int tableId, TableDTO tableDTO)
        {
            try
            {
                await _tableService.UpdateTableAsync(tableId, tableDTO);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return Ok("table has been updated");
        }

        [HttpDelete]
        [Route("DeleteTable/{tableId}")]
        public async Task<ActionResult> DeleteTable(int tableId)
        {
            try
            {
                await _tableService.DeleteTableAsync(tableId);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound($"{ex.Message}");
            }

            return Ok("Good job, table has been removed...");
        }
    }
}
