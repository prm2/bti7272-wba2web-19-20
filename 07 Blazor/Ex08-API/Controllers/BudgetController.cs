using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ex08_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Ex08_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly DataService _data;

        public BudgetController(DataService data)
        {
            _data = data;
        }

        // GET: api/Budget/2017
        [HttpGet("{year}", Name = "Get")]
        public async Task<Budget> Get(int year)
        {
            return await _data.Budget(year);
        }


    }
}
