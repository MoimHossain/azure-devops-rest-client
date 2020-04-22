

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AzureDevOps.Rest.Client
{
    public class CovidClient
    {
        private string adoUrl;
        private string pat;
        private static HttpClient client = new HttpClient();
        public CovidClient(string adoUrl, string pat)
        {
            this.adoUrl = adoUrl;
            this.pat = pat;
        }

        public async Task<Endpoint> CreateKubernetesEndpointAsync(
            Guid projectId, string projectName, 
            string name, string description, 
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
                    name,
                    owner = "library",
                    type = "kubernetes",
                    url = clusterApiUri,
                    description = description,
                    serviceEndpointProjectReferences = new List<Object>
                    {
                        new
                        {
                            description = description,
                            name =  name,
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

        public async Task<string> CreateEnvironmentAsync(
            string project, string name, string description)
        {
            var envs = await GetAzureDevOpsDefaultUri()
                .PostRestAsync(
                $"{project}/_apis/distributedtask/environments?api-version=5.1-preview.1",
                new
                {
                    name = name,
                    description = description
                },
                await GetBearerTokenAsync());

            return envs;
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

        public async Task<string> GetProjectsAsync()
        {
            var path = "_apis/projects?stateFilter=All&api-version=1.0";
            var projects = await GetAzureDevOpsDefaultUri()
                .GetRestJsonAsync(path, await GetBearerTokenAsync());

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
