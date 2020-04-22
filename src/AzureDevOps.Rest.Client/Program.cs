﻿

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

            var clusterApiUrl = "https://aks-we-dem-rgp-aks-we-demo--c5b22a-8ecb8e33.hcp.westeurope.azmk8s.io";
            var cert = "LS0tLS1CRUdJTiBDRVJUSUZJQ0FURS0tLS0tCk1JSUV5akNDQXJLZ0F3SUJBZ0lSQU55YytyTkJNVWx1K2hMNjRTSVJzNFF3RFFZSktvWklodmNOQVFFTEJRQXcKRFRFTE1Ba0dBMVVFQXhNQ1kyRXdJQmNOTWpBd05ESXlNVGMwTWpNNFdoZ1BNakExTURBME1qSXhOelV5TXpoYQpNQTB4Q3pBSkJnTlZCQU1UQW1OaE1JSUNJakFOQmdrcWhraUc5dzBCQVFFRkFBT0NBZzhBTUlJQ0NnS0NBZ0VBCjBNVndYQW85ckZvUVRWTGtIeDZ1QzZvT0xMUml6VlJwTUFIVnJWK0VUNjJCUnh0MEo2bUhwbnd2dUFELzlDSDEKeFYzNFhkQ05zZUtIRytiWnk3UTl5cERJUFQvczNPeFQxQ3FQNWVjMFRhYWdQeDhIZjFtYTdZOWlETUhpVU1sVAprOEw3angyblhrMVZ1ZEg0TEVqOVdQQlVSS1RhTjR3RlJiY2JNTnZUVkwrZWVxdlp3dmFVUlROQThEUmhYSUpkCjZHVXNrRm1qeDE3NlFJWTJLMEdNQ09ZUUMvRnRpUzRnYTZabW9ZdkJ2RXgzSGdHa0dZUDhlKzNIL2tJV1dqME4KMjBvS213ZENKWTBnMWR2RTNvcHJrcjl1a3V4QVNIVHRzcmUxV3hyTG9RT0FsZHVqYkMxcGVubDNXU2ZobjVaWApyYXl0NnNDbHQ2NlhvdmsvelRRWWhXM00xWXJoMVp4MktGNUtKT1NvZktZMm5sUTNjNHdOUGkzNEZDcHBMQUExCnltc245ZEdZTE16akpXdnRXUi9JVUgzODhaVkV2OXhybkJwNVJUZ0Q1SytuemFCcWZrS1F1Sk9kTGh3SWdBZHAKT1dVaVNuZ3RNZVhNdTFDdzE5blJ5ait0MDBsVnFwTUllVGN3VzZLVFpRK2xoZWhzanZqaGIrWjN1Rlk0RERGOAp2VGVkUXZ2TnUwSGdudTlrWFR1YjdxcUhGb0FmWm0yVXpESEdna0NHamhnWGVLUGJZdFhRTytIQ1l6eWNwR2xZClNQOVNOSlJ1dWIrc3I1YzZVM0tRZmZIaVZFbnVaSm02WWIzUjU3OFd2cFh2dG5ZTjJmK1kvWlp0T0Y5UUl6WVQKNHZFVVVsZjJBUno4R2U2OXkxWVFwZUhVdU4zcndnZFZTd3ZSQXppTVFvc0NBd0VBQWFNak1DRXdEZ1lEVlIwUApBUUgvQkFRREFnS2tNQThHQTFVZEV3RUIvd1FGTUFNQkFmOHdEUVlKS29aSWh2Y05BUUVMQlFBRGdnSUJBSFk2CkRCRFBnRm9nZVJSb2duUTd2NUFWbENJaGtEK2VTbjY5K0tGODMwRjE0djU3Z1ROYUh4WlpaZ2xXcm1vZk14bkwKTGRsMWpLeTN5SFFGTE9lZHlPTUJvTWlPcGNzUWhVak00YTZ4RVptZXBtU1doQmkwOTFjajZBQzkyS29XMXZVQwo2aVRSTEpKWlNIRDZMTjc2QUlpc1VaREJKWURWd3czeW15L2hObXBiV0M5b25ZM3Z3TWJVOTBMNUlNRk83YVFLCjVRUHZDaHdTYWh0RXIxdjNxY0ppNWZ0ZG1XeUlMTjByNm1DVXhjUFYzYUprUnNocUVnLzdDM201N21nMnorYzAKOG9qK2ZZZmRPVHI4NFNncEt6K2tFbWVZLys5Z2s2SzZ4SnRGOC83bW5pM1EySzZ6OFpjNlJoMk5HSzQ0ZWZpRgpmVWtBWm8wQytOVjMvckRiUVdZbWYyeU9Dbjl0SC9LOENSRjhsNkE2bWhRdFlQRE9MM1p2aDNTY3ZmT2pwSDErClJuUHkxZ2RrdGtBZC9sNVhKOUpVUEVWaS9ndHhLcGdMd2ppVS9TUlF3OUg1REY4cGppTitib044WWVudjUxWmYKM3Zld0RZQ3J4ekxMTkZLTVAvZ1poNWtXdm9oblFOTFhwTS83RVZNMHhlYTJMZXVPUGpBbjVjK05IVUxQS2N0UwpxSnkyWU8ybzY1QUpiMVNDYlFTK0tiMU4zcFVnbGp1ZEhnaTllbVByUCtBZldkTmoxQXlZMms0K3RMZndmSVBOClo1ZlFSaytaMGx0VUlxK2tYeGhYbzZybDNjMWlrdkVFRHRCOUx5SWlCWDExVzQwM0I0Mkg4eDVoTVRaejBycUUKeGJjRkR3N3JJemgzbzczTGQzeFRORVRqNytrb3pDU08xK1NrZW9VMwotLS0tLUVORCBDRVJUSUZJQ0FURS0tLS0tCg==";
            var token = "ZXlKaGJHY2lPaUpTVXpJMU5pSXNJbXRwWkNJNklpSjkuZXlKcGMzTWlPaUpyZFdKbGNtNWxkR1Z6TDNObGNuWnBZMlZoWTJOdmRXNTBJaXdpYTNWaVpYSnVaWFJsY3k1cGJ5OXpaWEoyYVdObFlXTmpiM1Z1ZEM5dVlXMWxjM0JoWTJVaU9pSmtaV1poZFd4MElpd2lhM1ZpWlhKdVpYUmxjeTVwYnk5elpYSjJhV05sWVdOamIzVnVkQzl6WldOeVpYUXVibUZ0WlNJNkltRjZkWEpsWkdWMmIzQnpZMlF0ZEc5clpXNHROMjF1ZUhRaUxDSnJkV0psY201bGRHVnpMbWx2TDNObGNuWnBZMlZoWTJOdmRXNTBMM05sY25acFkyVXRZV05qYjNWdWRDNXVZVzFsSWpvaVlYcDFjbVZrWlhadmNITmpaQ0lzSW10MVltVnlibVYwWlhNdWFXOHZjMlZ5ZG1salpXRmpZMjkxYm5RdmMyVnlkbWxqWlMxaFkyTnZkVzUwTG5WcFpDSTZJakUwTlRsaE5UbG1MVGt4WVdVdE5HRTROQzFpTlRaaExUQTRORE13T1dJeE1EZ3haaUlzSW5OMVlpSTZJbk41YzNSbGJUcHpaWEoyYVdObFlXTmpiM1Z1ZERwa1pXWmhkV3gwT21GNmRYSmxaR1YyYjNCelkyUWlmUS5uSzVGaG92MEIxYnA0WWdkSEdBbWw1Z0F3UTNOM2FyakNZMEFvN3dfajZRTU1TaDZKakFXNnBPbUh4U0RPT0RSTUNCRlRBZXF0ektNRE5xTEJDd19DSUVIcmRvaElCRjI5ZWd5VU1OSS1mRkJkTWtGNV9kdm0tWUF6ay1EMDZoQ2pJZXRhWHM0WTB1TmZESXRyX240QlcyT2ZUWnlLcFZFTUFOV0tpVC1YWlFva0hmNmt2S21Eb19XNTYxcGZEX3I2NXFZQ1otLTc0LVZ0QlVuZTNwUnBDcEl5cmdnYkdJU2NfUmhYeXFMZXdPcExTZnl0eUEtcjNuZHVHMTR5SFN0R04zR293dU1LVWR0MW9TejFMVk5saExrV1Bqa2NJbkh1RWo1dTJ5ZjBMSk1mMEZTU3VUUGxUZXB2bEJVaEd2cWJrZktlcFU3d3Jhb2VjV3ZXS1dqMXhDZUVWQjV5ZVJfSlJzbXNmT1dmc3pvaU1EbGM0UVNvWTNzS3VLd0FGVlNISjE5QzFpa01HVVU4eEJfVmZrNUk0OUstLUJ1c1hEZXotM242QXJEakNOUS1MaERfbEM4RURsS3h0RExGajRnQVhyUDJJUTZPNWtmOE1tbTl4Yjk0WmNpQ19IU3QtSC1QSWcyUzNfUkFIbDBWUDlieU9aRDcxcmQzVFc1aTVxYnNuMDVES0xpOUY3dUszT0RaQ2NZZkdSbUUxb3pvakE4djdMQmtDRGJwMW8zQ1lyVW92ZDZobXh2WHplZUpSOVlqNS15bGJqYU0ydGlKSkR5TTdhLWRIYS03bDVfWFlDbE0tZmFVRlFkM3NnX3g5YXRaNW15V1Zma1JORXdDaWtjRm9ZcGFUVzViSnhDVEFsMjNyX2RDX1ZZYi1pSUdNTGZYQVdXOUN2YmJIbw==";

            var connection = await covid.CreateKubernetesEndpointAsync(
                new Guid("e688ce1c-4243-4cb4-8120-5e269502a25a"), 
                projectName, 
                "AKSCOnnection1112", 
                "Some description",
                clusterApiUrl,
                cert, 
                token);

            //await covid.CreateEnvironmentAsync(projectName, "covidenv", "somedescription");
            //var endpoints = await covid.ListEndpointsAsync(projectName);
            //var epTypes = await covid.GetEndpointTypesAsync();
            //var projects = await covid.GetProjectsAsync();

            await covid.GetEnvAsync(projectName);
        }
    }
}