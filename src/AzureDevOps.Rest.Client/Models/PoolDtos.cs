

namespace AzureDevOps.Rest.Client
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class AgentPoolCollection
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("value")]
        public AgentPool[] Value { get; set; }
    }

    public partial class AgentPool
    {
        [JsonProperty("createdOn")]
        public DateTimeOffset CreatedOn { get; set; }

        [JsonProperty("autoProvision")]
        public bool AutoProvision { get; set; }

        [JsonProperty("autoUpdate")]
        public bool AutoUpdate { get; set; }


        [JsonProperty("agentCloudId")]
        public long? AgentCloudId { get; set; }

        [JsonProperty("createdBy")]
        public CreatedBy CreatedBy { get; set; }

        [JsonProperty("owner")]
        public CreatedBy Owner { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("scope")]
        public Guid Scope { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isHosted")]
        public bool IsHosted { get; set; }

        [JsonProperty("poolType")]
        public string PoolType { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("isLegacy")]
        public bool IsLegacy { get; set; }

        public override string ToString()
        {
            return $"{this.Name} ({this.Id})";
        }
    }




}
