using System.ComponentModel.DataAnnotations;

namespace Centricity.Models
{
    public class Flow
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Step> Steps { get; set; }
    }
}
