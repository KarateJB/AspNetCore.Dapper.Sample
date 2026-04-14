using System;
using System.Threading.Tasks;
using AspNetCore.Dapper.WebApi.Models.DAO;
using AspNetCore.Dapper.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Dapper.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FolkController : ControllerBase
    {
        private readonly FolkDalService _folkDalService;

        public FolkController(FolkDalService folkDalService)
        {
            _folkDalService = folkDalService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Folk folk)
        {
            try
            {
                var result = await _folkDalService.CreateAsync(folk);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _folkDalService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, Folk folk)
        {
            try
            {
                if (id != folk.Id)
                {
                    return BadRequest("ID mismatch");
                }
                await _folkDalService.UpdateAsync(folk);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Find(Guid id)
        {
            try
            {
                var result = await _folkDalService.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
