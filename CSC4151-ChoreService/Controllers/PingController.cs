using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CSC4151_ChoreService.Controllers
{
    [Route("Ping")]
    [ApiController]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public string Ping()
        {
            return "Chore Pong";
        }
    }
}