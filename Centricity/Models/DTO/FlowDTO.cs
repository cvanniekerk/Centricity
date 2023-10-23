namespace Centricity.Models.DTO
{
    public class FlowDTO
    {
        public string Name { get; set; }
        public ICollection<StepDTO> Steps { get; set; }
    }
}
