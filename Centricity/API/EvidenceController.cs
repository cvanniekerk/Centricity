using Centricity.Data;
using Centricity.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Centricity.API
{
    [CORS]
    [ApiController]
    [Route("api/[controller]")]
    public class EvidenceController : ControllerBase
    {
        private readonly CentricityContext _context;

        public EvidenceController(CentricityContext context)
        {
            _context = context;
        }

        [HttpOptions]
        public async Task<ActionResult> DoNothing()
        {
            Response.Headers.Add("Access-Control-Allow-Headers", "Accept,Content-Type");
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<EvidenceDTO>> UpdateEvidence([FromBody] EvidenceDTO[] evidence)
        {
            foreach (var e in evidence)
            {
                var jobStepEvidence = _context.JobStepEvidence.Where(j => j.Id == e.Id).SingleOrDefault();
                if (jobStepEvidence != null)
                {                    
                    jobStepEvidence.Value = e.Value;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    return BadRequest();
                }
            }
            return Ok(evidence);
        }
    }
}
