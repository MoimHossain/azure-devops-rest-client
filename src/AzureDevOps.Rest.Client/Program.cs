

using System;
using System.Threading.Tasks;

namespace AzureDevOps.Rest.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().Wait();

            Console.ReadLine();
        }

        private static async Task RunAsync()
        {
            var adoUrl = Environment.GetEnvironmentVariable("AZDO_ORG_SERVICE_URL");
            var pat = Environment.GetEnvironmentVariable("AZDO_PERSONAL_ACCESS_TOKEN");
            var projectName = "Azure-K8S-Demo"; // id: "e688ce1c-4243-4cb4-8120-5e269502a25a"

            var covid = new CovidClient(adoUrl, pat);

            var clusterApiUrl = "https://aks-we-dem-rgp-aks-we-demo--c5b22a-bbe88c04.hcp.westeurope.azmk8s.io";
            var cert = "LS0tLS1CRUdJTiBDRVJUSUZJQ0FURS0tLS0tCk1JSUV5akNDQXJLZ0F3SUJBZ0lSQUw3SmFGYVNyTE11ZElqd29kU1poYTh3RFFZSktvWklodmNOQVFFTEJRQXcKRFRFTE1Ba0dBMVVFQXhNQ1kyRXdJQmNOTWpBd05ESXpNRFl3TVRNM1doZ1BNakExTURBME1qTXdOakV4TXpkYQpNQTB4Q3pBSkJnTlZCQU1UQW1OaE1JSUNJakFOQmdrcWhraUc5dzBCQVFFRkFBT0NBZzhBTUlJQ0NnS0NBZ0VBCnZkcVFQTVdtWU9nMFBvaW5qdnBXemh6a3c1eHJCc0xOeVVycXhjQ0hUQ0lQQlc2T0FKYTQzdmZIUHZ3azIrL0MKcTU5REtHeXhmQldxTnluclM3Mm1vOElmYjRDN2pKTDl5NmJqMDh5Sjk2MmVZUno5MXEvbVoyYnpNSUdqeXZndgpuUkE3S1hHVlpGSW1hRU12RXh5TFhJT1RZQXRkUFBJcC9OU1VVdFVhWExCd3Fra0YxWkd6WmMwaFV2UmFOV0ZlCnpXcS9zemUrVmMrU1RNSFNoWCtTcTVCNVpQbGVGVjJHQ3o4UVNHU0YrY2hSRHR6UVk0WnAzdS9Odm1HU2JJaDYKSVBtNU84dnNIYkdMN2hPd2owRWN0VXI5TVRCTEpEaWdMVXRSbUZ6bksrT1RnTFhrQlJMcUhub2VCOC95YWt1Nwp5SmZBYmZhZWhibzZ2eWY4T1NxZU4wUXg0akM0L01ITTIwMExZL2k1a2Vnb24vQ1BuemhaeE9EZVBuWVRhZEZyCnJJUTdXYmhBcmlPeHNrNGJrOFJUUjhhUVZ3MmhwNDM0Ym1PWmRMUUE5elpFVThoSkxLQXpNYWxubEM4bGpNcEEKczROUFluN3RDZk1XNU1WanhYcGltb2E2OVBHVjZsUFdYbHdZMjAvQVROYVZscUxPY2VNWG03U3p6S2xWYy9ncgpkdms4UlFWVmtlbzJpMFZsOWVUWnNzUkljdHZrUlBsVGJhNXBxSHlTQzNaOFppQ2tveWRKOUJiaGIzTnZ6TWd6CnRZVGVBNnJqTWZ2SjArLzVtbmZhWk4rNDVOT3Z4bmFCbmM2NE5xN3RDMXJpemJ0b0pWZGM2YXJCdDZQNFU4VzgKVUJjbWhESVUyeXhKelJMQVFhNG5iYk00V1A5M1V0enRpaTMzb0RabzhZVUNBd0VBQWFNak1DRXdEZ1lEVlIwUApBUUgvQkFRREFnS2tNQThHQTFVZEV3RUIvd1FGTUFNQkFmOHdEUVlKS29aSWh2Y05BUUVMQlFBRGdnSUJBSENrCjJrOHQ0dUdLRzF0REJCZHNBbWltaGxxVW16T2N2bjByWmZZZm9GWmt2MHhGbEQxSExzZmE4V0dlamFZK3d0bkwKVStGTXJLZTgwN1RXWS9NVmZRcjlkQnVWcGRlQ1ZROWgyMG80RDZOUlZJcTRtVEE1Mjh0azFTZnJBTHRadjlmTgpWKzVJTkttRFBCVmw1ZHdJSUU0Wk00U2VXSW5YQmdRTGpSSUEvUjdxS1c2K2RtUGpCVWFWZW9nQnl1akJSWHdUClRjM2ZlK2RLNlhlRTVjWHFIREtaeGV2ZkhEdzA1cThIa25KMGpLVzlJWGpTd215MjFnUjNwVzZPTHk1Z1UvcHYKZkVFb3ZHVEx4aWtteGFIajFTcjdFLzZ0YmhDVk9FSmc3M01QdmpYRmlDbmtONUxtK1VRS2N4UFZzdDJGM0RkaQpqN212L25mcEx2aGduMGl3MzFhaGpzMlBxanRoTnA0a1ZPTWM5L1p4aXpEL2tkN2hPclhnMm9JSGdEVmtTcExBCmNlUG5uS0xIMGJZWHJLVktOOVBrdWVaNUUzOHIrazl4bTFYMHpvSWpXdGlPTzNMVUswV3Z5MmlPYUFXM0UxMGkKUDRsTm1EZ2ppYVlMTHBoV0wwZXZMWkswR2o3TjdyeG5ieDNWY3liTExFL2xmNFBFSGVieWZHcDBYZ1UwUkZxdQoxQWNzMWY1Zkk1d3AyN1laNy9PajBWS2w1MmVHR0xQemxQRUQrcXQ1SzFMdWJYSnRuVG5BNnY3a3E5alM0Kys2CmZsalZGZUgzNk80UWlQTmN0aG90UjAxL1lqbXEvN3ZoVmpaS0hjaUFkenc1WjBmcURxZzA5ZFNuVFZ3U2RNV3EKQTNQQ3BiZWVRYm9adHhNNTZOdlRXSlc4RkRBNThIY3VtKzZXVG1UaAotLS0tLUVORCBDRVJUSUZJQ0FURS0tLS0tCg==";
            var token = "ZXlKaGJHY2lPaUpTVXpJMU5pSXNJbXRwWkNJNklpSjkuZXlKcGMzTWlPaUpyZFdKbGNtNWxkR1Z6TDNObGNuWnBZMlZoWTJOdmRXNTBJaXdpYTNWaVpYSnVaWFJsY3k1cGJ5OXpaWEoyYVdObFlXTmpiM1Z1ZEM5dVlXMWxjM0JoWTJVaU9pSmtaV1poZFd4MElpd2lhM1ZpWlhKdVpYUmxjeTVwYnk5elpYSjJhV05sWVdOamIzVnVkQzl6WldOeVpYUXVibUZ0WlNJNkltRjZkWEpsWkdWMmIzQnpZMlF0ZEc5clpXNHRlR053ZG1zaUxDSnJkV0psY201bGRHVnpMbWx2TDNObGNuWnBZMlZoWTJOdmRXNTBMM05sY25acFkyVXRZV05qYjNWdWRDNXVZVzFsSWpvaVlYcDFjbVZrWlhadmNITmpaQ0lzSW10MVltVnlibVYwWlhNdWFXOHZjMlZ5ZG1salpXRmpZMjkxYm5RdmMyVnlkbWxqWlMxaFkyTnZkVzUwTG5WcFpDSTZJalU1WVRnd016Z3hMV00yWkRNdE5HUTRPUzA1TXpBekxUTmtORFF6WVRGbE0ySTBZaUlzSW5OMVlpSTZJbk41YzNSbGJUcHpaWEoyYVdObFlXTmpiM1Z1ZERwa1pXWmhkV3gwT21GNmRYSmxaR1YyYjNCelkyUWlmUS5iWTRWQm8ya0NTeDgtWFZEcjVfRGlIcmVDMHE1RTFSSG9JcUJyd19YR1BYVy1wTFphd1N5MTY3YXpZV3Rac3pyNl93dmhTNjZHdExyZkNRREtrRWN4VmVBaVQ4cDRFREFZcGNMeWl4NGdxQXpWWl9vc29oQTk5RzJ1a2hLZTk0clZESnNOeGduLTNOdjEyaUV2VXZudm5EUjlWTWFzUWVLV3VmajF0NU9adWRzUmE2V1pSUC0yeG8tZ3hVN0ZmNzZVZlYtdFBhYkpraTg2M3ZudEt0dGdDWl9Ta285QlZlVTR4WkZRQXRtQWRSX2gwZW1malRkYThfYy1XMlRDVFpyRTRuT1Z5d0ctRkNwdDI4WmdpX1lkVkFvQ1FhajJubzB5c2k1SU8yWXRWNVA2Qk10bE5zTUlRd1pSTk5NQ2dMUTNmemQzNHNKcENVNFlmSkhsQnZqUFhrVHp6djhkNUhUVGw0WUszY0g4MC1tSWN5eHZoWFF2eXRYd3M0SWVKdVFUWWEtVnhfRmI1OXFNQmVkTHF5OXpzX1UwWjliM3NMMXMwVy03UEEwV1VBSHJxOGxJMGRJVThrV1ZYUDBJQzZ5S1R3NFZPVVlGejB4RzJVNjBLQ19fNzNTTTk5Q0I3R2xiWUw4bGxXSzlyT1YtNDhlNGtiYm1GMkFJR0t3T0dUamhsMXNmSXJMUlkyZE9SaFlORTlSczRmSWtVQnE5RmxIX3c1ZDVENFdFMGlMQmZHcWcwVnlFZ2pGVjRDcTFYdWJfcW1uUUJfeFlPeGNwUnJmeXdzRmQ0YnI1LUhYTHg1Yndaa2phVVFfOWwxXzNPSlJHbGh3dHF5QnJJZjY4LVVyQ3JxSTRxekFRcmtQOWJiVUdYNTlwekF4dFUwUkh1TW5NM2JybTJWZnhVSQ==";

            var endpoint = await covid.CreateKubernetesEndpointAsync(
                new Guid("e688ce1c-4243-4cb4-8120-5e269502a25a"),
                projectName,
                "Kubernetes-Cluster-Environment-Connection",
                "Some description",
                clusterApiUrl,
                cert,
                token);

            var environment = await covid
                .CreateEnvironmentAsync(projectName, 
                "Kubernetes-Cluster-Environment", "Kubernetes-Cluster-Environment");


            var response = await covid
                .CreateKubernetesResourceAsync(
                projectName, environment.Id, endpoint.Id, 
                "default", "aks-we-demo-apr1");
            
            //var endpoints = await covid.ListEndpointsAsync(projectName);
            //var epTypes = await covid.GetEndpointTypesAsync();
            //var projects = await covid.GetProjectsAsync();

            await covid.GetEnvAsync(projectName);
        }
    }
}
