using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shared = Models;

namespace Ex08_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ERController : Controller
    {
        private readonly DataService _data;

        public ERController(DataService data)
        {
            _data = data;
        }

        [HttpGet("{year?}")]
        public async Task<IEnumerable<Shared.AccountGroup>> Get(int? year)
        {
            return await _data.ER(year);
        }

        [HttpGet("accounts/{year?}/{id?}")]
        public async Task<IEnumerable<Shared.Account>> Accounts(int? year, string id)
        {
            return await _data.AccountsAsync(year, id);
        }
    }
}