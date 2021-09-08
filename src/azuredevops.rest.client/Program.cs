﻿

using k8s;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AzureDevOps.Rest.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteTestsAsync(args).Wait();
        }

        private static async Task ExecuteTestsAsync(string[] args)
        {
            var adoUrl = Environment.GetEnvironmentVariable("AZDO_ORG_URI");
            var pat = Environment.GetEnvironmentVariable("AZDO_TOKEN");
            var ado = new AdoClient(adoUrl, pat);


            var pipelines = await ado.ListPipelinesAsync("CloudOven");


            var test = await ado.GetBuildChangesAsync("CloudOven", 1412);

            var targetPipeline = pipelines.Value.FirstOrDefault(p => p.Name.Equals("Consumer"));            

            var runs = await ado.ListPipelineRunesAsync("CloudOven", targetPipeline.Id);
            var latestRun = runs.Value.FirstOrDefault();
            // You are already here upuntil now

            // ??
            var consumedArtifacts = await ado.GetConsumedArtifactInfoAsync("CloudOven", latestRun.Id);

            // var jobs =  await ado.ListJobRequestsAsync(9);
        }

        private static async Task ExecuteAsync(string [] args)
        {
            var clusterApiUrl = Environment.GetEnvironmentVariable("AKS_URI");
            var adoUrl = Environment.GetEnvironmentVariable("AZDO_ORG_SERVICE_URL");
            var pat = Environment.GetEnvironmentVariable("AZDO_PERSONAL_ACCESS_TOKEN");
            var adoClient = new AdoClient(adoUrl, pat);
            var groups = await adoClient.ListGroupsAsync();

            var config = KubernetesClientConfiguration.BuildConfigFromConfigFile();
            var client = new Kubernetes(config);

            var accounts = await client
                .ListServiceAccountForAllNamespacesAsync(labelSelector: "purpose=ado-automation");

            foreach (var account in accounts.Items)
            {
                var project = await GetProjectAsync(account.Metadata.Labels["project"], adoClient);
                var secretName = account.Secrets[0].Name;
                var secret = await client
                    .ReadNamespacedSecretAsync(secretName, account.Metadata.NamespaceProperty);

                var endpoint = await adoClient.CreateKubernetesEndpointAsync(
                    project.Id,
                    project.Name,
                    $"Kubernetes-Cluster-Endpoint-{account.Metadata.NamespaceProperty}",
                    $"Service endpoint to the namespace {account.Metadata.NamespaceProperty}",
                    clusterApiUrl,
                    Convert.ToBase64String(secret.Data["ca.crt"]),
                    Convert.ToBase64String(secret.Data["token"]));

                var environment = await adoClient.CreateEnvironmentAsync(project.Name,
                    $"Kubernetes-Environment-{account.Metadata.NamespaceProperty}",
                    $"Environment scoped to the namespace {account.Metadata.NamespaceProperty}");

                await adoClient.CreateKubernetesResourceAsync(project.Name, 
                    environment.Id, endpoint.Id,
                    account.Metadata.NamespaceProperty,
                    account.Metadata.ClusterName);

                var group = groups.FirstOrDefault(g => g.DisplayName
                    .Equals($"[{project.Name}]\\Release Administrators", StringComparison.OrdinalIgnoreCase));
                await adoClient.CreateApprovalPolicyAsync(project.Name, group.OriginId, environment.Id);

                await adoClient.CreateAcrConnectionAsync(project.Name, 
                    Environment.GetEnvironmentVariable("ACRName"), 
                    $"ACR-Connection", "The connection to the ACR",
                    Environment.GetEnvironmentVariable("SubId"),
                    Environment.GetEnvironmentVariable("SubName"),
                    Environment.GetEnvironmentVariable("ResourceGroup"),
                    Environment.GetEnvironmentVariable("ClientId"), 
                    Environment.GetEnvironmentVariable("Secret"),
                    Environment.GetEnvironmentVariable("TenantId"));
            }
        }

        private async static Task<Project> GetProjectAsync(string projectName, AdoClient client)
        {
            var projects = await client.GetProjectsAsync();

            return projects.Value.FirstOrDefault(p => p.Name.Equals(projectName, StringComparison.OrdinalIgnoreCase));
        }

        private static async Task RunAsync()
        {
            var adoUrl = Environment.GetEnvironmentVariable("AZDO_ORG_SERVICE_URL");
            var pat = Environment.GetEnvironmentVariable("AZDO_PERSONAL_ACCESS_TOKEN");
            var projectName = "Azure-K8S-Demo"; 

            var ado = new AdoClient(adoUrl, pat);



            //var groups = await ado.ListGroupsAsync();

            //var group = groups.FirstOrDefault(g => g.DisplayName
            //    .Equals("Kubernetes-Admins", StringComparison.OrdinalIgnoreCase));


            //await ado.CreateApprovalPolicyAsync(projectName, group.OriginId, 15);

            //var endpoint = await ado.CreateKubernetesEndpointAsync(
            //    new Guid("e688ce1c-4243-4cb4-8120-5e269502a25a"),
            //    projectName,
            //    "Kubernetes-Cluster-Environment-Connection",
            //    "Some description",
            //    clusterApiUrl,
            //    cert,
            //    token);

            //var environment = await ado
            //    .CreateEnvironmentAsync(projectName, 
            //    "Kubernetes-Cluster-Environment", "Kubernetes-Cluster-Environment");


            //var response = await ado
            //    .CreateKubernetesResourceAsync(
            //    projectName, environment.Id, endpoint.Id, 
            //    "default", "aks-we-demo-apr1");

            //var endpoints = await ado.ListEndpointsAsync(projectName);
            //var epTypes = await ado.GetEndpointTypesAsync();
            //var projects = await ado.GetProjectsAsync();

            await ado.GetEnvAsync(projectName);
        }
    }
}
