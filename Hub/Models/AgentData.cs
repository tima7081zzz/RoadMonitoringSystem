using System;

namespace Hub.Models
{
    public class AgentData
    {
        public Accelerometer? Accelerometer { get; set; }
        public Gps? Gps { get; set; }
        public DateTime Timestamp { get; set; }
        public int UserId { get; set; }
    }
}