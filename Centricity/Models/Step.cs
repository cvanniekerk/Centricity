using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Centricity.Models
{
    public class Step
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
                
        public int Ordinal { get; set; }

        public int FlowId { get; set; }
        [ForeignKey("FlowId")]
        public virtual Flow Flow { get; set; }

        public virtual ICollection<Evidence> Evidence { get; set; } 

        public virtual ICollection<Transition> Transitions { get; set; }
        
    }
}
