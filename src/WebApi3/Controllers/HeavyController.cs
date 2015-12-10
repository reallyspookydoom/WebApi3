using Microsoft.AspNet.Mvc;
using System;
using System.Threading.Tasks;

namespace WebApi3.Controllers
{
    [Route("api/[controller]")]
    public class HeavyController
    {
        [HttpGet]
        public async Task<DateTime> DoHeavyWork()
        {
            await Task.Delay(5000);
            return DateTime.Now;
        }
    }
}
