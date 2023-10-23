using Centricity.Data;
using Centricity.Models;
using Centricity.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Centricity.API
{
    [CORS]
    [Route("api/[controller]")]
    [ApiController]
    public class FlowsController : ControllerBase
    {
        private readonly CentricityContext _context;

        public FlowsController(CentricityContext context)
        {
            _context = context;            
        }

        // GET: api/Flows
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Flow>>> GetFlow()
        {
          if (_context.Flow == null)
          {
              return NotFound();
          }
            var a = await _context.Flow.ToListAsync();
            return a;
        }

        // GET: api/Flows/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Flow>> GetFlow(int id)
        {
          if (_context.Flow == null)
          {
              return NotFound();
          }
            var flow = await _context.Flow.FindAsync(id);

            if (flow == null)
            {
                return NotFound();
            }

            return flow;
        }

        // PUT: api/Flows/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlow(int id, Flow flow)
        {
            if (id != flow.Id)
            {
                return BadRequest();
            }

            _context.Entry(flow).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlowExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Flows
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPost]
        public async Task<ActionResult<Flow>> PostFlow(Flow flow)
        {
            if (_context.Flow == null)
            {
                return Problem("Entity set 'CentricityContext.Flow' is null.");
            }
            
            _context.Flow.Add(flow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlow", new { id = flow.Id }, flow);
        }*/

        [HttpPost]
        public async Task<ActionResult<Flow>> PostFlow(FlowDTO flowDTO)
        {
            if (_context.Flow == null)
            {
                return Problem("Entity set 'CentricityContext.Flow' is null.");
            }

            Flow flow = new Flow();
            flow.Name = flowDTO.Name;

            flow.Steps = new List<Step>();

            foreach (var stepDTO in flowDTO.Steps)
            {
                Step step = new Step();
                step.Name = stepDTO.Name;
                step.Ordinal = stepDTO.Ordinal;

                step.Evidence = new List<Evidence>();
                step.Transitions = new List<Transition>();

                foreach (var evidenceDTO in stepDTO.Evidence)
                {
                    Evidence evidence = new Evidence();
                    evidence.Name = evidenceDTO.Name;
                    evidence.EvidenceType = evidenceDTO.EvidenceType;
                    
                    step.Evidence.Add(evidence);
                }

                foreach (var transitionDTO in stepDTO.Transitions)
                {
                    Transition transition = new Transition();
                    transition.Name = transitionDTO.Name;
                    transition.IsEnd = transitionDTO.IsEnd;

                    step.Transitions.Add(transition);
                }

                flow.Steps.Add(step);
            }

            _context.Flow.Add(flow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFlow", new { id = flow.Id }, flow);
        }

        // DELETE: api/Flows/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlow(int id)
        {
            if (_context.Flow == null)
            {
                return NotFound();
            }
            var flow = await _context.Flow.FindAsync(id);
            if (flow == null)
            {
                return NotFound();
            }

            _context.Flow.Remove(flow);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FlowExists(int id)
        {
            return (_context.Flow?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
