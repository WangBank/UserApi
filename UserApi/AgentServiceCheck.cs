using System;

namespace Users.Api
{
    internal class AgentServiceCheck
    {
        public AgentServiceCheck()
        {
        }

        public TimeSpan DeregisterCriticalServiceAfter { get; set; }
        public TimeSpan Interval { get; set; }
        public string HTTP { get; set; }
    }
}