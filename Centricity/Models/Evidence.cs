using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centricity.Models
{
    public enum EvidenceType
    {
        TextInput = 1,
        Checkbox = 2
    }

    public class Evidence
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public EvidenceType EvidenceType { get; set; }

        public int StepId { get; set; }
        [ForeignKey("StepId")]
        public virtual Step Step { get; set; } 

    }
}
