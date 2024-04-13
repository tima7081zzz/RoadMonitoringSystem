namespace Hub.Models
{
    public class ProcessedAgentData
    {
        public string RoadState { get; set; } = string.Empty;
        public AgentData AgentData { get; set; } = null!;
    }
}