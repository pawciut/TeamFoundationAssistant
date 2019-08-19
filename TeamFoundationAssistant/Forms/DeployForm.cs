using Microsoft.TeamFoundation.SourceControl.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi;
using Microsoft.TeamFoundation.WorkItemTracking.WebApi.Models;
using Microsoft.VisualStudio.Services.Client;
using Microsoft.VisualStudio.Services.Common;
using Microsoft.VisualStudio.Services.WebApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeamFoundationAssistant.Forms
{
    public partial class DeployForm : Form
    {
        //============= Config [Edit these with your settings] =====================
        //internal const string azureDevOpsOrganizationUrl = "https://dev.azure.com/organization"; //change to the URL of your Azure DevOps account; NOTE: This must use HTTPS
        internal const string vstsCollectioUrl = "http://192.168.222.102/mbdvc/";//"http://192.168.222.102/mbdvc/_projects";
        //"http://myserver:8080/tfs/DefaultCollection";// alternate URL for a TFS collection
        //==========================================================================

        //http://192.168.222.102/mbdvc/_projects

        public DeployForm()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            //Prompt user for credential
            VssConnection connection = new VssConnection(new Uri(vstsCollectioUrl), new VssClientCredentials());


            //var sourceControlServer = connection.GetClient<TfvcHttpClient>();

            //create http client and query for resutls
            WorkItemTrackingHttpClient witClient = connection.GetClient<WorkItemTrackingHttpClient>();
            Wiql query = new Wiql() { Query = "SELECT [Id], [Title], [State] FROM workitems WHERE [Work Item Type] = 'Bug' AND [Assigned To] = @Me" };
            WorkItemQueryResult queryResults = witClient.QueryByWiqlAsync(query).Result;

            //Display reults in console
            if (queryResults == null || queryResults.WorkItems.Count() == 0)
            {
                Console.WriteLine("Query did not find any results");
            }
            else
            {
                foreach (var item in queryResults.WorkItems)
                {
                    Console.WriteLine(item.Id);
                }
            }
        }
    }
}
