using Microsoft.AspNetCore.Mvc;
using Trial.Server.Models;
using Trial.Server.Services;

namespace Trial.Server.Controllers
{
    [Route("/coa")]
    public class COAController : BaseApiController<ChartOfAccount>
    {
        public COAController(GenericMongoDb<ChartOfAccount> database) : base(database)
        {
        }
    }
}
