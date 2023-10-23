using System.ComponentModel.DataAnnotations;

namespace Centricity.Models
{
    public class JobStepEvidence
    {
        [Key]
        public int Id { get; set; }

        public JobStep JobStep { get; set; }

        public EvidenceType EvidenceType { get; set; }

        public string Name { get; set; }

        public string? Value { get; set; }

        
    }
}
