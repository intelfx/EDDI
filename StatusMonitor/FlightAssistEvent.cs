using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EddiEvents
{
    class FlightAssistEvent : Event
    {
        public const string NAME = "Flight assist";
        public const string DESCRIPTION = "Triggered when flight assist is toggled";
        public const string SAMPLE = null;
        public static Dictionary<string, string> VARIABLES = new Dictionary<string, string>();

        static FlightAssistEvent()
        {
            VARIABLES.Add("status", "Whether flight assist is on");
        }

        [JsonProperty("status")]
        public bool status { get; private set; }

        public FlightAssistEvent(DateTime timestamp, bool flightAssist) : base(timestamp, NAME)
        {
            this.status = flightAssist;
        }
    }
}
