using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centricity.Models
{
    public class JobStep
    {
        [Key]
        public int Id { get; set; }

        public virtual Step Step { get; set; }

        public virtual Job Job { get; set; }

        public virtual ICollection<JobStepEvidence> Evidence { get; set; }
        
        public virtual ICollection<JobStepTransition> Transitions { get; set; }
    }
}
