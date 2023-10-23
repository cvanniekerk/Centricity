namespace Centricity.Models.DTO
{
    public class StepDTO
    {
        public string Name { get; set; }
        public int Ordinal { get; set; }       
        public ICollection<EvidenceDTO> Evidence { get; set; }  
        public ICollection<TransitionDTO> Transitions { get; set; }
    }
}
