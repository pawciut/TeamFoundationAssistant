using Microsoft.TeamFoundation.Build.WebApi;
using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.Wiki.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.WebApi;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamFoundationAssistant
{
    public class eZJobs
    {
        //============= Config [Edit these with your settings] =====================
        //internal const string azureDevOpsOrganizationUrl = "https://dev.azure.com/organization"; //change to the URL of your Azure DevOps account; NOTE: This must use HTTPS
        internal const string vstsCollectioUrl = "http://192.168.222.102/mbdvc/";//"http://192.168.222.102/mbdvc/_projects";
                                                                                 //http://192.168.222.102/mbdvc/eZJ
                                                                                 //"http://myserver:8080/tfs/DefaultCollection";// alternate URL for a TFS collection
                                                                                 //==========================================================================
        const string ProjectName = "eZJ";

        private VssConnection connection
        {
            get
            {
                return GetConnection();//TODO:Na razie tworzymy nowe ale moze trzymac obiekt?
            }
        }

        //http://192.168.222.102/mbdvc/_projects

        VssConnection GetConnection()
        {
            //Prompt user for credential
            return new VssConnection(new Uri(vstsCollectioUrl), new VssClientCredentials());
        }

        public IEnumerable<WorkItemReference> GetWorkItemsExample()
        {
            //create http client and query for resutls
            WorkItemTrackingHttpClient witClient = connection.GetClient<WorkItemTrackingHttpClient>();
            Wiql query = new Wiql() { Query = "SELECT [Id], [Title], [State] FROM workitems WHERE [Work Item Type] = 'Bug' AND [Assigned To] = @Me" };
            WorkItemQueryResult queryResults = witClient.QueryByWiqlAsync(query).Result;

            //Display reults in console
            if (queryResults == null || queryResults.WorkItems.Count() == 0)
            {
                //Console.WriteLine("Query did not find any results");
            }
            //else
            //{
            //    foreach (var item in queryResults.WorkItems)
            //    {
            //        Console.WriteLine(item.Id);
            //    }
            //}
            return queryResults.WorkItems;
        }


        public IEnumerable<BuildDefinitionReference> GetBuildDefinitions()
        {
            
            var buildClient = connection.GetClient<Microsoft.TeamFoundation.Build.WebApi.BuildHttpClient>();

            var defRes = buildClient.GetDefinitionsAsync(ProjectName).Result;
            if (defRes != null)
            {

            }

            return defRes;
        }

        public IEnumerable<Deployment> GetBuildDeployments(int buildId)
        {

            var buildClient = connection.GetClient<Microsoft.TeamFoundation.Build.WebApi.XamlBuildHttpClient>();

            var defRes = buildClient.GetBuildDeploymentsAsync(ProjectName, buildId).Result;
            if (defRes != null)
            {

            }

            return defRes;
        }

        public void AddToWiki(string pagePath, string pageContent)
        {
            var wikiClient = connection.GetClient<Microsoft.TeamFoundation.Wiki.WebApi.WikiHttpClient>();
            string wikiId = String.Empty;
            string version = String.Empty;
            var wiki = new WikiPageCreateOrUpdateParameters();
            wiki.Content = pageContent;
            var response = wikiClient.CreateOrUpdatePageAsync(wiki, ProjectName, wikiId, pagePath, version);
        }

        public WorkItem GetWIT(int id)
        {
            var witClient = connection.GetClient<Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient>();
            var wit = witClient.GetWorkItemAsync(id).Result;
            return wit;
        }

        public void UpdateWIT(int id, JsonPatchDocument doc)
        {
            //https://developercommunity.visualstudio.com/content/problem/213770/updateworkitemasync-fails-with-you-must-pass-a-val.html
            var witClient = connection.GetClient<Microsoft.TeamFoundation.WorkItemTracking.WebApi.WorkItemTrackingHttpClient>();
            witClient.UpdateWorkItemAsync(doc, id);
        }

        public GitBranchStats GetGitRefs(Guid repositoryId, string branchName)
        {

            var witClient = connection.GetClient<Microsoft.TeamFoundation.SourceControl.WebApi.GitHttpClient>();
            var stats = witClient.GetBranchAsync(repositoryId, branchName).Result;
            return stats;
        }
        public GitBranchStats GetGitRefs(Guid repositoryId, string branchName)
        {

            var witClient = connection.GetClient<Microsoft.TeamFoundation.Build.WebApi.XamlBuildHttpClient>();
            var stats = witClient.GetBuildDeploymentsAsync(repositoryId, branchName).Result;
            Deployment d
            return stats;
        }




    }
}
