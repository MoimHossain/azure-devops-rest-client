

using k8s;
using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace AzureDevOps.Rest.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var rootCommand = new RootCommand(description: "REST Client for Azure DevOps.");            
            
            rootCommand.AddOption(new Option(
              aliases: new string[] { "--uri", "-u" },
              description: "The Organization URL.", argumentType: typeof(string)));
            rootCommand.AddOption(new Option(
              aliases: new string[] { "--credentials", "-c" },
              description: "The PAT.", argumentType: typeof(string)));
            rootCommand.AddOption(new Option(
                aliases: new string[] { "--project", "-p" }, 
                description: "The name or ID of project.", argumentType: typeof(string)));
            rootCommand.AddOption(new Option(
                aliases: new string[] { "--verb", "-v" },
                description: "verb.", argumentType: typeof(ActionVerbs)));
  
            rootCommand.Handler =
              CommandHandler.Create<string, string, string, ActionVerbs>(RunAsync);
            await rootCommand.InvokeAsync(args);
        }

        private static async Task RunAsync(
            string uri, 
            string credentials, 
            string project, 
            ActionVerbs verb)
        {            
            var ado = new AdoClient(uri, credentials);

            if (verb == ActionVerbs.Create)
            {
                Console.WriteLine(await ado.CreateProjectAsync(project));
            }
            else if(verb == ActionVerbs.Delete)
            {
                var all = await ado.GetProjectsAsync();
                var dp = all.Value.FirstOrDefault(p => p.Name.Equals(project, StringComparison.InvariantCultureIgnoreCase));
                if(dp != null )
                {
                    Console.WriteLine(await ado.DeleteProjectAsync(dp.Id.ToString()));
                }
            }
        }
    }
}
