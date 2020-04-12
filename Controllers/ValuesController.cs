using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCore.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetCore.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly AppDataContext _context;

        public ValuesController(AppDataContext context)
        {
            _context = context;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetValues()
        {
            var returnData = await _context.Values.ToListAsync();
            return Ok(returnData);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValueById(int id)
        {
            var returnData = await _context.Values.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(returnData);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}