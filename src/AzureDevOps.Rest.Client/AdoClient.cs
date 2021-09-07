

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AzureDevOps.Rest.Client
{
    public class AdoClient
    {
        private string adoUrl;
        private string pat;
        private static HttpClient client = new HttpClient();
        public AdoClient(string adoUrl, string pat)
        {
            this.adoUrl = adoUrl;
            this.pat = pat;
        }

        


        public async Task<MsVssBuildWebRunArtifactsDataProvider> GetConsumedArtifactInfoAsync(
            string projectName, long runId)
        {
            var jrs = await GetAzureDevOpsDefaultUri()
                .GetRestAsync<RunArtifactsRootObject>(
                $"{GetOrganizationName()}/{projectName}/_build/results?buildId={runId}&view=artifacts&pathAsName=false&__rt=fps&__ver=2",
                new Action<HttpClient>(httpClient => 
                {
                    var credentials =
                    Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(
                        string.Format("{0}:{1}", "", this.pat)));
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));

                    httpClient.DefaultRequestHeaders.TryAddWithoutValidation("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/93.0.4577.63 Safari/537.36 Edg/93.0.961.38");
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                }));

            return jrs.Fps.DataProviders.Data.MsVssBuildWebRunConsumedArtifactsDataProvider;
        }

        public async Task<string> GetPipelineRunAsync(
            string projectName, long pipelineId, long runId)
        {
            var jrs = await GetAzureDevOpsDefaultUri()
                .GetRestJsonAsync(
                $"{GetOrganizationName()}/{projectName}/_apis/pipelines/{pipelineId}/runs/{runId}?api-version=6.1-preview.1",
                await GetBearerTokenAsync());

            return jrs;
        }


        public async Task<PipelineRunCollection> ListPipelineRunesAsync(string projectName, long pipelineId)
        {
            var jrs = await GetAzureDevOpsDefaultUri()
                .GetRestAsync<PipelineRunCollection>(
                $"{GetOrganizationName()}/{projectName}/_apis/pipelines/{pipelineId}/runs?api-version=6.1-preview.1",
                await GetBearerTokenAsync());

            return jrs;
        }

        public async Task<VstsPipelineCollection> ListPipelinesAsync(string projectName)
        {
            var jrs = await GetAzureDevOpsDefaultUri()
                .GetRestAsync<VstsPipelineCollection>(
                $"{GetOrganizationName()}/{projectName}/_apis/pipelines?api-version=6.1-preview.1",
                await GetBearerTokenAsync());

            return jrs;
        }

        public async Task<TfsRunData> ListRunsAsync(string projectName)
        {
            var jrs = await GetAzureDevOpsDefaultUri()
                .GetRestAsync<RunRootObject>(
                $"{GetOrganizationName()}/{projectName}/_build?definitionId=95&__rt=fps&__ver=2",
                await GetBearerTokenAsync());

            return jrs.Fps.DataProviders.Data;
        }

        public async Task<string> ListProjectsAsync()
        {
            var jrs = await GetAzureDevOpsDefaultUri()
                .GetRestJsonAsync(
                $"{GetOrganizationName()}/_apis/projects?api-version=6.1-preview.4",
                await GetBearerTokenAsync());

            return jrs;
        }

        public async Task<JobRequestCollection> ListJobRequestsAsync(long poolId)
        {            
            var jrs = await GetAzureDevOpsDefaultUri()
                .GetRestAsync<JobRequestCollection>(
                $"_apis/distributedtask/pools/{poolId}/jobrequests?api-version=5.1-preview.1&",
                await GetBearerTokenAsync());

            return jrs;
        }

        public async Task<AgentPool> GetPoolAsync(long poolId)
        {
            var pool = await GetAzureDevOpsDefaultUri()
                .GetRestAsync<AgentPool>(
                $"_apis/distributedtask/pools/{poolId}?api-version=5.1",
                await GetBearerTokenAsync());

            return pool;
        }

        public async Task<AgentPoolCollection> ListPoolAsync()
        {
            var pools = await GetAzureDevOpsDefaultUri()
                .GetRestAsync<AgentPoolCollection>(
                $"_apis/distributedtask/pools?api-version=5.1",
                await GetBearerTokenAsync());
                        
            return pools;
        }


        public async Task<string> CreateAcrConnectionAsync(
            string projectName, string acrName, string name, string description,
            string subscriptionId, string subscriptionName, string resourceGroup,
            string clientId, string secret, string tenantId)
        {
            var response = await GetAzureDevOpsDefaultUri()
                .PostRestAsync(
                $"{projectName}/_apis/serviceendpoint/endpoints?api-version=5.1-preview.2",
                new
                {
                    name,
                    description,
                    type = "dockerregistry",
                    url = $"https://{acrName}.azurecr.io",
                    isShared = false,
                    owner = "library",
                    data = new
                    {
                        registryId = $"/subscriptions/{subscriptionId}/resourceGroups/{resourceGroup}/providers/Microsoft.ContainerRegistry/registries/{acrName}",
                        registrytype = "ACR",
                        subscriptionId,
                        subscriptionName
                    },
                    authorization = new
                    {
                        scheme = "ServicePrincipal",
                        parameters = new
                        {
                            loginServer = $"{acrName}.azurecr.io",
                            servicePrincipalId = clientId,
                            tenantId,
                            serviceprincipalkey = secret
                        }
                    }
                },
                await GetBearerTokenAsync());
            return response;
        }

        public async Task<string> CreateApprovalPolicyAsync(
            string projectName, Guid groupId, long envId, 
            string instruction = "Please approve the Deployment")
        {
            var response = await GetAzureDevOpsDefaultUri()
                .PostRestAsync(
                $"{projectName}/_apis/pipelines/checks/configurations?api-version=5.2-preview.1",
                new
                {
                    timeout = 43200,
                    type = new
                    {                                   
                        name = "Approval"
                    },
                    settings = new
                    {
                        executionOrder = 1,
                        instructions = instruction,
                        blockedApprovers = new List<object> { },
                        minRequiredApprovers = 0,
                        requesterCannotBeApprover = false,
                        approvers = new List<object>
                        {
                            new 
                            {
			                    id = groupId
                            }
                        }
                    },
                    resource = new
                    {
                        type = "environment",
                        id = envId.ToString()
                    }
                }, await GetBearerTokenAsync());
            return response;
        }

        public async Task<IEnumerable<Group>> ListGroupsAsync()
        {
            var groups = await GetAzureDevOpsVsspUri()
                .GetRestAsync<GroupCollection>(
                $"_apis/graph/groups?api-version=5.1-preview.1",
                await GetBearerTokenAsync());
            return groups.Value;
        }

        public async Task<string> CreateKubernetesResourceAsync(
            string projectName, long environmentId, Guid endpointId,
            string kubernetesNamespace, string kubernetesClusterName)
        {
            var link = await GetAzureDevOpsDefaultUri()
                            .PostRestAsync(
                            $"{projectName}/_apis/distributedtask/environments/{environmentId}/providers/kubernetes?api-version=5.0-preview.1",
                            new
                            {
                                name = kubernetesNamespace,
                                @namespace = kubernetesNamespace,
                                clusterName = kubernetesClusterName,
                                serviceEndpointId = endpointId
                            },
                            await GetBearerTokenAsync());
            return link;
        }

        public async Task<Endpoint> CreateKubernetesEndpointAsync(
            Guid projectId, string projectName,
            string endpointName, string endpointDescription,
            string clusterApiUri,
            string serviceAccountCertificate, string apiToken)
        {
            var ep = await GetAzureDevOpsDefaultUri()
                .PostRestAsync<Endpoint>(
                $"{projectName}/_apis/serviceendpoint/endpoints?api-version=6.0-preview.4",
                new
                {
                    authorization = new
                    {
                        parameters = new
                        {
                            serviceAccountCertificate,
                            isCreatedFromSecretYaml = true,
                            apitoken = apiToken
                        },
                        scheme = "Token"
                    },
                    data = new
                    {
                        authorizationType = "ServiceAccount"
                    },
                    isShared = false,
                    name = endpointName,
                    owner = "library",
                    type = "kubernetes",
                    url = clusterApiUri,
                    description = endpointDescription,
                    serviceEndpointProjectReferences = new List<Object>
                    {
                        new
                        {
                            description = endpointDescription,
                            name =  endpointName,
                            projectReference = new
                            {
                                id =  projectId,
                                name =  projectName
                            }
                        }
                    }
                },
                await GetBearerTokenAsync());
            return ep;
        }

        public async Task<PipelineEnvironment> CreateEnvironmentAsync(
            string project, string envName, string envDesc)
        {
            var env = await GetAzureDevOpsDefaultUri()
                .PostRestAsync<PipelineEnvironment>(
                $"{project}/_apis/distributedtask/environments?api-version=5.1-preview.1",
                new
                {
                    name = envName,
                    description = envDesc
                },
                await GetBearerTokenAsync());

            return env;
        }

        public async Task<EndpointCollection> ListEndpointsAsync(string project)
        {
            var path = $"{project}/_apis/serviceendpoint/endpoints?api-version=5.1-preview.2";
            var types = await GetAzureDevOpsDefaultUri()
                .GetRestAsync<EndpointCollection>(path, await GetBearerTokenAsync());

            return types;
        }

        public async Task<string> GetEndpointTypesAsync()
        {
            var path = "_apis/serviceendpoint/types?api-version=5.1-preview.1";
            var types = await GetAzureDevOpsDefaultUri()
                .GetRestJsonAsync(path, await GetBearerTokenAsync());

            return types;
        }

        public async Task<ProjectCollection> GetProjectsAsync()
        {
            var path = "_apis/projects?stateFilter=All&api-version=1.0";
            var projects = await GetAzureDevOpsDefaultUri()
                .GetRestAsync<ProjectCollection>(path, await GetBearerTokenAsync());

            return projects;
        }

        public async Task<PipelineEnvironmentCollection> GetEnvAsync(string project)
        {
            var envs = await GetAzureDevOpsDefaultUri()
                .GetRestAsync<PipelineEnvironmentCollection>(
                $"{project}/_apis/distributedtask/environments",
                await GetBearerTokenAsync());

            return envs;
        }


        #region Helper methods

        private string GetOrganizationName()
        {
            return GetAzureDevOpsDefaultUri().AbsolutePath.Replace("/", string.Empty);
        }

        private Uri GetAzureDevOpsVsspUri()
        {
            var organizationName = GetOrganizationName();
            return new Uri($"https://vssps.dev.azure.com/{organizationName}/");
        }

        private Uri GetAzureDevOpsDefaultUri()
        {
            return new Uri(this.adoUrl);
        }

        private async Task<Action<HttpClient>> GetBearerTokenAsync()
        {
            await Task.Delay(0);
            return new Action<HttpClient>((httpClient) =>
            {
                var credentials =
                Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(
                    string.Format("{0}:{1}", "", this.pat)));
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
            });
        }
        #endregion
    }
}
