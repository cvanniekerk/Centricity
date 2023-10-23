using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centricity.Models
{
    public class Transition
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsEnd { get; set; }

        public int? StepToId { get; set; }
        [ForeignKey("StepToId")]
        public virtual Step? StepTo { get; set; }

        public int StepId { get; set; }
        [ForeignKey("StepId")]
        public virtual Step Step {  get; set; }
    }
}
