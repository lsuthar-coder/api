using Microsoft.AspNetCore.Mvc;
using Trial.Server.Models;
using Trial.Server.Services;

namespace Trial.Server.Controllers
{
    [Route("/tb")]
    public class TBController : BaseApiController<TrialBalance>
    {
        public TBController(GenericMongoDb<TrialBalance> database) : base(database)
        {
        }
    }
}
