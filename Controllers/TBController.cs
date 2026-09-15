]using Microsoft.AspNetCore.Mvc;
using Trial.Server.Services;
using Trial.Server.Models;
namespace Trial.Server.Controllers
{
    [Route("/tb")]
    [ApiController]
    public class TBController : Controller
    {
        private readonly TBService service;
        public TBController(TBService _service)
        {
            service = _service;
        }
        [HttpGet]
        public ActionResult<Task<List<TrialBalance>>> GetTBs()
        {
            return service.GetAsync();
        }
        [HttpPost]
        public ActionResult<Task> CreateTB(TrialBalance tb)
        {
            return service.CreateAsync(tb);
        }
    }
}
