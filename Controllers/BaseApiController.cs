using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trial.Server.Models;
using Trial.Server.Services;

namespace Trial.Server.Controllers
{
    [ApiController]
    public abstract class BaseApiController<TEntity> : ControllerBase where TEntity : class, IBaseEntity
    {
        protected readonly GenericMongoDb<TEntity> Database;

        protected BaseApiController(GenericMongoDb<TEntity> database)
        {
            Database = database;
        }

        [HttpGet]
        public virtual async Task<ActionResult<List<TEntity>>> GetAll()
        {
            var result = await Database.GetAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> GetById(string id)
        {
            var entity = await Database.GetAsync(id);
            if (entity == null)
            {
                return NotFound(new { message = $"Entity with id '{id}' not found." });
            }
            return Ok(entity);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody] TEntity entity)
        {
            if (entity == null)
            {
                return BadRequest(new { message = "Entity data is required." });
            }
            await Database.CreateAsync(entity);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(string id, [FromBody] TEntity entity)
        {
            if (entity == null)
            {
                return BadRequest(new { message = "Entity data is required." });
            }

            var existing = await Database.GetAsync(id);
            if (existing == null)
            {
                return NotFound(new { message = $"Entity with id '{id}' not found." });
            }

            entity.Id = id;
            await Database.UpdateAsync(id, entity);
            return Ok(entity);
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(string id)
        {
            var existing = await Database.GetAsync(id);
            if (existing == null)
            {
                return NotFound(new { message = $"Entity with id '{id}' not found." });
            }

            await Database.RemoveAsync(id);
            return NoContent();
        }
    }
}
