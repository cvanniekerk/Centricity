using Centricity.Data;
using Centricity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Centricity.API
{
    [CORS]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly CentricityContext _context;

        public JobController(CentricityContext context)
        {
            _context = context;
        }

        [HttpGet("{flowId}")]
        public async Task<ActionResult<Job>> CreateJobForFlow(int flowId)
        {
            var flow = _context.Flow
                        .Where(f => f.Id == flowId)
                        .Include(f => f.Steps)                        
                        .FirstOrDefault();            

            if (flow == null)
            {
                return BadRequest();
            }

            var job = new Job();

            job.Flow = flow;            
            job.JobSteps = new List<JobStep>();

            foreach (var step in flow.Steps)
            {
                var jobStep = new JobStep();
                jobStep.Step = step;                

                jobStep.Evidence = new List<JobStepEvidence>();
                jobStep.Transitions = new List<JobStepTransition>();

                var e = _context.Evidence.Where(e => e.StepId == step.Id);

                foreach (var evidence in e)
                {
                    jobStep.Evidence.Add(new JobStepEvidence()
                    {
                        EvidenceType = evidence.EvidenceType,
                        Name = evidence.Name
                    });
                }

                var t = _context.Transition.Where(t => t.StepId == step.Id);

                foreach (var transition in t)
                {
                    var jobStepTransition = new JobStepTransition();

                    jobStepTransition.Name = transition.Name;
                    jobStepTransition.IsEnd = transition.IsEnd;

                    jobStepTransition.JobStep = new JobStep();
                    jobStepTransition.JobStep.Id = transition.Step.Id;

                    if (transition.StepTo != null)
                    {
                        jobStepTransition.JobStepTo = new JobStep();
                        jobStepTransition.JobStepTo.Id = transition.StepTo.Id;
                    }
                    
                    jobStep.Transitions.Add(jobStepTransition);
                }

                job.JobSteps.Add(jobStep);
            }

            _context.Job.Add(job);

            await _context.SaveChangesAsync();

            return CreatedAtAction("GetJob", job);
        }

        [HttpGet("{jobId}"), ActionName("complete")]
        public async Task<ActionResult<Job>> CompleteJob(int jobId)
        {
            var job = _context.Job.Where(j => j.Id == jobId).FirstOrDefault();

            if (job == null)
            {
                return BadRequest();
            }

            job.CompletedOn = DateTime.Now;

            await _context.SaveChangesAsync();
            return Ok(job);
        }

        [HttpGet, ActionName("history")]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobHistory()
        {
            if (_context.Job == null)
            {
                return NotFound();
            }

            var jobs = _context.Job
                        .OrderByDescending(j => j.Id)
                        .Include(j => j.Flow)
                        .ToListAsync();

            return await jobs;
        }

        [HttpGet("{jobId}"), ActionName("history")]
        public async Task<ActionResult<Job>> GetJobHistory(int jobId)
        {
            if (_context.Job == null)
            {
                return NotFound();
            }

            var job = _context.Job
                        .Where(j => j.Id == jobId)
                        .Include(j => j.Flow)
                        .Include(j => j.JobSteps)
                        .ThenInclude(j => j.Step)
                        .Include(j => j.JobSteps)
                        .ThenInclude(j => j.Evidence)                        
                        .SingleOrDefault();

            if (job == null )
            {
                return NotFound();
            }
            else
            {
                return Ok(job);
            }            
        }

    }

    
}
