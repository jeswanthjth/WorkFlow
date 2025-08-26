using WorkFlow;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WorkFlow.Model;

namespace WorkFlow.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class workController : ControllerBase
    {
        private static List<WorkInfo> works = new List<WorkInfo>();

        // GET: api/work
        [HttpGet]
        public ActionResult<IEnumerable<WorkInfo>> GetAll()
        {
            return Ok(works);
        }

        // GET: api/work/{id}
        [HttpGet("{id}")]
        public ActionResult<WorkInfo> GetById(int id)
        {
            var work = works.FirstOrDefault(c => c.Id == id);
            if (work == null) return NotFound();
            return Ok(work);
        }

        // POST: api/work
        [HttpPost]
        public ActionResult<WorkInfo> Create(WorkInfo work)
        {
            work.Id = works.Count > 0 ? works.Max(c => c.Id) + 1 : 1;
            works.Add(work);
            return CreatedAtAction(nameof(GetById), new { id = work.Id }, work);
        }

        // PUT: api/work/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, WorkInfo updatedwork)
        {
            var work = works.FirstOrDefault(c => c.Id == id);
            if (work == null) return NotFound();

            work.Employee = updatedwork.Employee;
            work.Role = updatedwork.Role;
            work.Reporting = updatedwork.Reporting;

            return NoContent();
        }

        // DELETE: api/work/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var work = works.FirstOrDefault(c => c.Id == id);
            if (work == null) return NotFound();

            works.Remove(work);
            return NoContent();
        }
    }
}