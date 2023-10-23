using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centricity.Models
{
    public class Job
    {
        [Key]
        public int Id { get; set; }        

        public virtual Flow Flow { get; set; }     
        
        public virtual ICollection<JobStep> JobSteps { get; set; }

        public DateTime? CompletedOn { get; set; }
    }
}
