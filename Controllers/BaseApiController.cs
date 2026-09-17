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

        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody] TEntity entity)
        {
            await Database.CreateAsync(entity);
            return Ok();
        }
    }
}
