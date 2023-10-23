using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centricity.Models
{
    public class JobStepTransition
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsEnd { get; set; }

        [ForeignKey("JobStepToId")]
        public virtual JobStep? JobStepTo { get; set; }

        public virtual JobStep JobStep { get; set; }
    }   
}
