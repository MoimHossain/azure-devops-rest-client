namespace AzureDevOps.Rest.Client
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class JobRequestCollection
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("value")]
        public JobRequest[] Value { get; set; }
    }

    public partial class JobRequest
    {
        [JsonProperty("requestId")]
        public long RequestId { get; set; }

        [JsonProperty("queueTime")]
        public DateTimeOffset QueueTime { get; set; }

        [JsonProperty("assignTime")]
        public DateTimeOffset AssignTime { get; set; }

        [JsonProperty("receiveTime")]
        public DateTimeOffset ReceiveTime { get; set; }

        [JsonProperty("finishTime")]
        public DateTimeOffset FinishTime { get; set; }


        [JsonProperty("serviceOwner")]
        public Guid ServiceOwner { get; set; }

        [JsonProperty("hostId")]
        public Guid HostId { get; set; }

        [JsonProperty("scopeId")]
        public Guid ScopeId { get; set; }



        [JsonProperty("planId")]
        public Guid PlanId { get; set; }

        [JsonProperty("jobId")]
        public Guid JobId { get; set; }

        [JsonProperty("demands")]
        public string[] Demands { get; set; }

        [JsonProperty("reservedAgent")]
        public ReservedAgent ReservedAgent { get; set; }

        [JsonProperty("definition")]
        public Definition Definition { get; set; }


        [JsonProperty("data")]
        public Data Data { get; set; }

        [JsonProperty("poolId")]
        public long PoolId { get; set; }

        [JsonProperty("agentSpecification")]
        public AgentSpecification AgentSpecification { get; set; }

        [JsonProperty("orchestrationId")]
        public string OrchestrationId { get; set; }

        [JsonProperty("matchesAllAgentsInPool")]
        public bool MatchesAllAgentsInPool { get; set; }

        [JsonProperty("priority")]
        public long Priority { get; set; }

        public override string ToString()
        {
            return $"{this.QueueTime} - {this.Definition?.Name}";
        }
    }

    public partial class Definition
    {

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class AgentSpecification
    {
        [JsonProperty("VMImage")]
        public string VmImage { get; set; }
    }

    public partial class ReservedAgent
    {
        [JsonProperty("_links")]
        public Links Links { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("version")]
        public Version Version { get; set; }

        [JsonProperty("osDescription")]
        public string OsDescription { get; set; }

        [JsonProperty("enabled")]
        public bool Enabled { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
    }
}
