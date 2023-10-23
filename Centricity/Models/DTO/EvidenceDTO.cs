namespace Centricity.Models.DTO
{
    public class EvidenceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EvidenceType EvidenceType { get; set; }
        public string? Value { get; set; }
        
    }
}
