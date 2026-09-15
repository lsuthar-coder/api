]using Microsoft.AspNetCore.Mvc;
using Trial.Server.Services;
using Trial.Server.Models;
namespace Trial.Server.Controllers
{
    [Route("/coa")]
    [ApiController]
    public class COAController : Controller
    {
        private readonly COAService service;
        public COAController(COAService _service)
        {
            service = _service;
        }
        [HttpGet]
        public ActionResult<Task<List<ChartOfAccount>>> GetCOAs()
        {
            return service.GetAsync();
        }
        [HttpPost]
        public ActionResult<Task> CreateCOA(ChartOfAccount coa)
        {
            return service.CreateAsync(coa);
        }
    }
}
