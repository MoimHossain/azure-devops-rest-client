using System;
using System.Collections.Generic;

using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AzureDevOps.Rest.Client
{


    public  class RunArtifactsRootObject
    {
        [JsonProperty("fps")]
        public RunArtifactsWrapper Fps { get; set; }
    }

    public  class RunArtifactsWrapper
    {
        [JsonProperty("dataProviders")]
        public ConsumedArtifactDataProviderCollection DataProviders { get; set; }
    }

    public  class ConsumedArtifactDataProviderCollection
    {
        [JsonProperty("data")]
        public ConsumedArtifactData Data { get; set; }
    }

    public  class ConsumedArtifactData
    {
        [JsonProperty("ms.vss-build-web.run-artifacts-data-provider")]
        public MsVssBuildWebRunArtifactsDataProvider MsVssBuildWebRunArtifactsDataProvider { get; set; }

        [JsonProperty("ms.vss-build-web.run-consumed-artifacts-data-provider")]
        public MsVssBuildWebRunArtifactsDataProvider MsVssBuildWebRunConsumedArtifactsDataProvider { get; set; }
    }

    public  class MsVssBuildWebRunArtifactsDataProvider
    {
        [JsonProperty("buildId")]
        public long BuildId { get; set; }

        [JsonProperty("buildNumber")]
        public string BuildNumber { get; set; }

        [JsonProperty("definitionId")]
        public long DefinitionId { get; set; }

        [JsonProperty("definitionName")]
        public string DefinitionName { get; set; }

        [JsonProperty("consumedSources", NullValueHandling = NullValueHandling.Ignore)]
        public ArtifactConsumptionSource[] ConsumedSources { get; set; }
    }

    public  class ArtifactConsumptionSource
    {
        [JsonProperty("artifactCategory")]
        public string ArtifactCategory { get; set; }

        [JsonProperty("artifactType")]
        public string ArtifactType { get; set; }

        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("versionName")]
        public string VersionName { get; set; }

        [JsonProperty("versionId")]        
        public long VersionId { get; set; }

        [JsonProperty("consumedArtifacts")]
        public ConsumedArtifact[] ConsumedArtifacts { get; set; }
    }

    public  class ConsumedArtifact
    {
        [JsonProperty("artifactName")]
        public string ArtifactName { get; set; }
    }



    public  class PipelineRunCollection
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("value")]
        public PipelineRun[] Value { get; set; }
    }

    public  class PipelineRun
    {

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("result")]
        public string Result { get; set; }

        [JsonProperty("createdDate")]
        public DateTimeOffset CreatedDate { get; set; }

        [JsonProperty("finishedDate")]
        public DateTimeOffset FinishedDate { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }





    public  class VstsPipelineCollection
    {
        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("value")]
        public VstsPipeline[] Value { get; set; }
    }

    public  class VstsPipeline
    {

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("revision")]
        public long Revision { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("folder")]
        public string Folder { get; set; }
    }


    public  class RunRootObject
    {
        [JsonProperty("fps")]
        public RunWrapper Fps { get; set; }
    }

    public  class RunWrapper
    {
        [JsonProperty("dataProviders")]
        public RunDataProviders DataProviders { get; set; }
    }

    public  class RunDataProviders
    {
        [JsonProperty("data")]
        public TfsRunData Data { get; set; }
    }

    public  class TfsRunData
    {
        [JsonProperty("ms.vss-build-web.runs-data-provider")]
        public MsVssBuildWebRunsDataProvider MsVssBuildWebRunsDataProvider { get; set; }

        [JsonProperty("ms.vss-build-web.pipeline-details-data-provider")]
        public MsVssBuildWebPipelineDetailsDataProvider MsVssBuildWebPipelineDetailsDataProvider { get; set; }
    }

    public  class MsVssBuildWebPipelineDetailsDataProvider
    {
        [JsonProperty("definitionName")]
        public string DefinitionName { get; set; }

        [JsonProperty("definitionId")]
        public long DefinitionId { get; set; }

        [JsonProperty("definitionPath")]
        public string DefinitionPath { get; set; }

        [JsonProperty("definitionUri")]
        public string DefinitionUri { get; set; }

        [JsonProperty("definitionQueueStatus")]
        public long DefinitionQueueStatus { get; set; }

        [JsonProperty("definitionProcessType")]
        public long DefinitionProcessType { get; set; }

        [JsonProperty("isDraft")]
        public bool IsDraft { get; set; }

        [JsonProperty("serviceConnection")]
        public ServiceConnection ServiceConnection { get; set; }

        [JsonProperty("definition")]
        public MsVssBuildWebPipelineDetailsDataProviderDefinition Definition { get; set; }
    }

    public  class MsVssBuildWebPipelineDetailsDataProviderDefinition
    {
        [JsonProperty("triggers")]
        public Trigger[] Triggers { get; set; }

        [JsonProperty("jobAuthorizationScope")]
        public long JobAuthorizationScope { get; set; }

        [JsonProperty("jobTimeoutInMinutes")]
        public long JobTimeoutInMinutes { get; set; }

        [JsonProperty("jobCancelTimeoutInMinutes")]
        public long JobCancelTimeoutInMinutes { get; set; }

        [JsonProperty("process")]
        public Process Process { get; set; }

        [JsonProperty("repository")]
        public DefinitionRepository Repository { get; set; }

        [JsonProperty("quality")]
        public long Quality { get; set; }

        [JsonProperty("authoredBy")]
        public AuthoredBy AuthoredBy { get; set; }

        [JsonProperty("drafts")]
        public object[] Drafts { get; set; }

        [JsonProperty("queue")]
        public Queue Queue { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("queueStatus")]
        public long QueueStatus { get; set; }

        [JsonProperty("revision")]
        public long Revision { get; set; }

        [JsonProperty("createdDate")]
        public string CreatedDate { get; set; }

        [JsonProperty("project")]
        public VstsProject Project { get; set; }
    }

    public  class AuthoredBy
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("uniqueName")]
        public string UniqueName { get; set; }

        [JsonProperty("imageUrl")]
        public Uri ImageUrl { get; set; }

        [JsonProperty("descriptor")]
        public string Descriptor { get; set; }
    }



    public  class Process
    {
        [JsonProperty("yamlFilename")]
        public string YamlFilename { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }
    }

    public  class VstsProject
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("state")]
        public long State { get; set; }

        [JsonProperty("revision")]
        public long Revision { get; set; }

        [JsonProperty("visibility")]
        public long Visibility { get; set; }

        [JsonProperty("lastUpdateTime")]
        public string LastUpdateTime { get; set; }
    }

    public  class Queue
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("pool")]
        public Pool Pool { get; set; }
    }

    public  class Pool
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isHosted")]
        public bool IsHosted { get; set; }
    }

    public  class DefinitionRepository
    {
        [JsonProperty("properties")]
        public Properties Properties { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("defaultBranch")]
        public string DefaultBranch { get; set; }
    }

    public  class Properties
    {
        [JsonProperty("cloneUrl")]
        public Uri CloneUrl { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("defaultBranch")]
        public string DefaultBranch { get; set; }

        [JsonProperty("isFork")]
        public string IsFork { get; set; }

        [JsonProperty("safeRepository")]
        public Guid SafeRepository { get; set; }
    }

    public  class Trigger
    {
        [JsonProperty("branchFilters")]
        public object[] BranchFilters { get; set; }

        [JsonProperty("pathFilters")]
        public object[] PathFilters { get; set; }

        [JsonProperty("settingsSourceType")]
        public long SettingsSourceType { get; set; }

        [JsonProperty("batchChanges")]
        public bool BatchChanges { get; set; }

        [JsonProperty("maxConcurrentBuildsPerBranch")]
        public long MaxConcurrentBuildsPerBranch { get; set; }

        [JsonProperty("triggerType")]
        public long TriggerType { get; set; }
    }

    public  class ServiceConnection
    {
        [JsonProperty("connectionId")]
        public object ConnectionId { get; set; }

        [JsonProperty("isAppConnection")]
        public bool IsAppConnection { get; set; }

        [JsonProperty("login")]
        public object Login { get; set; }

        [JsonProperty("nonce")]
        public object Nonce { get; set; }

        [JsonProperty("autoUpgradeToAppConnection")]
        public bool AutoUpgradeToAppConnection { get; set; }

        [JsonProperty("appHelpLink")]
        public object AppHelpLink { get; set; }

        [JsonProperty("appName")]
        public object AppName { get; set; }
    }

    public  class MsVssBuildWebRunsDataProvider
    {
        [JsonProperty("runs")]
        public RunElement[] Runs { get; set; }
    }

    public  class RunElement
    {
        [JsonProperty("stages")]
        public Stage[] Stages { get; set; }

        [JsonProperty("run")]
        public RunInfo Run { get; set; }

        [JsonProperty("sourceVersionMessage")]
        public string SourceVersionMessage { get; set; }
    }

    public  class RunInfo
    {
        [JsonProperty("retentionLeases")]
        public RetentionLease[] RetentionLeases { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("buildNumber")]
        public string BuildNumber { get; set; }

        [JsonProperty("sourceBranch")]
        public string SourceBranch { get; set; }

        [JsonProperty("sourceVersion")]
        public string SourceVersion { get; set; }

        [JsonProperty("repository")]
        public RunRepository Repository { get; set; }

        [JsonProperty("queueTime")]
        public string QueueTime { get; set; }

        [JsonProperty("startTime")]
        public string StartTime { get; set; }

        [JsonProperty("finishTime")]
        public string FinishTime { get; set; }

        [JsonProperty("reason")]
        public long Reason { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("result")]
        public long Result { get; set; }

        [JsonProperty("definition")]
        public RunDefinition Definition { get; set; }

        [JsonProperty("triggerInfo")]
        public TriggerInfo TriggerInfo { get; set; }

        [JsonProperty("requestedFor")]
        public AuthoredBy RequestedFor { get; set; }

        [JsonProperty("requestedById")]
        public Guid RequestedById { get; set; }

        [JsonProperty("triggeringRepository")]
        public TriggeringRepository TriggeringRepository { get; set; }
    }

    public  class RunDefinition
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("queueStatus")]
        public long QueueStatus { get; set; }
    }


    public  class RunRepository
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("checkoutSubmodules")]
        public bool CheckoutSubmodules { get; set; }
    }

    public  class RetentionLease
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("leaseType")]
        public string LeaseType { get; set; }

        [JsonProperty("ownerDisplayName")]
        public string OwnerDisplayName { get; set; }

        [JsonProperty("validUntil")]
        public string ValidUntil { get; set; }
    }

    public  class TriggerInfo
    {
        [JsonProperty("ci.sourceBranch")]
        public string CiSourceBranch { get; set; }

        [JsonProperty("ci.sourceSha")]
        public string CiSourceSha { get; set; }

        [JsonProperty("ci.message")]
        public string CiMessage { get; set; }

        [JsonProperty("ci.triggerRepository")]
        public Guid CiTriggerRepository { get; set; }
    }

    public  class TriggeringRepository
    {
        [JsonProperty("alias")]
        public string Alias { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("refName")]
        public string RefName { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("projectId")]
        public Guid ProjectId { get; set; }
    }

    public  class Stage
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("state")]
        public long State { get; set; }

        [JsonProperty("result")]
        public long Result { get; set; }

        [JsonProperty("startTime")]
        public string StartTime { get; set; }

        [JsonProperty("finishTime")]
        public string FinishTime { get; set; }

        [JsonProperty("order")]
        public long Order { get; set; }

        [JsonProperty("checkpoint")]
        public Checkpoint Checkpoint { get; set; }

        [JsonProperty("stateData")]
        public StateData StateData { get; set; }
    }

    public  class Checkpoint
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("state")]
        public long State { get; set; }

        [JsonProperty("result")]
        public long Result { get; set; }

        [JsonProperty("startTime")]
        public string StartTime { get; set; }

        [JsonProperty("finishTime")]
        public string FinishTime { get; set; }

        [JsonProperty("parentId")]
        public Guid ParentId { get; set; }
    }

    public  class StateData
    {
        [JsonProperty("pendingDependencies")]
        public bool PendingDependencies { get; set; }

        [JsonProperty("pendingChecks")]
        public bool PendingChecks { get; set; }
    }
}

