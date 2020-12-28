using HumanityService.DataContracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HumanityService.Client
{
    public partial class Dashboard : Form
    {
        private static Campaign selectedCampaign { get; set; }

        private HumanityServiceClient client;
        public Dashboard()
        {
            InitializeComponent();
            client = new HumanityServiceClient();
            treeView.Focus();
            DeliveryCodeLabel.Hide();
            DeliveryCodeTextBox.Hide();
            ValidateDeliveryButton.Hide();
        }

        private async void treeView_Enter(object sender, EventArgs e)
        {
            treeView.Nodes.Clear();
            var username = Properties.Settings.Default["Username"].ToString();
            var getCampaignsResult = await client.GetCampaigns(username);
            foreach (var campaign in getCampaignsResult.Campaigns)
            {
                TreeNode nodeLevel1 = treeView.Nodes.Add(campaign.Name);
                nodeLevel1.Tag = campaign;

                var getProcessesResult = await client.GetProcesses(campaign.Id);
                foreach (var process in getProcessesResult.Processes)
                {
                    TreeNode nodeLevel2 = nodeLevel1.Nodes.Add("Process (" + process.Status + ")");

                    var getDeliveryDemandsResult = await client.GetDeliveryDemands(process.Id);
                    if (getDeliveryDemandsResult.DeliveryDemands.Count != 0)
                    {
                        nodeLevel2.Nodes.Add("Delivery Demand (" + process.Status + ")");
                    }
                    var getContributionsResult = await client.GetContributions(processId: process.Id);
                    foreach (var contribution in getContributionsResult.Contributions)
                    {
                        if (contribution.Type != "Delivery")
                        {
                            nodeLevel2.Nodes.Add("Contribution (" + contribution.Status + ")");
                        }
                        else
                        {
                            nodeLevel2.Nodes[0].Nodes.Add("Contribution (" + contribution.Status + ")");
                        }
                    }
                }
            }
        }

        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (treeView.SelectedNode.Tag != null)
            {
                selectedCampaign = ((Campaign)treeView.SelectedNode.Tag);
                DeliveryCodeLabel.Show();
                DeliveryCodeTextBox.Show();
                ValidateDeliveryButton.Show();
            }
            else
            {
                DeliveryCodeLabel.Hide();
                DeliveryCodeTextBox.Hide();
                ValidateDeliveryButton.Hide();
            }
        }

        private async void ValidateDeliveryButton_Click(object sender, EventArgs e)
        {
            var validateDeliveryRequest = new ValidateDeliveryRequest
            {
                ValidationType = "Destination",
                CampaignId = selectedCampaign.Id,
                DeliveryCode = DeliveryCodeTextBox.Text
            };

            var result = await client.ValidateDelivery(validateDeliveryRequest);
            string message;
            if (result.IsValid)
            {
                message = "Delivery Validated!";
                treeView.Focus();
            }
            else
            {
                message = "Wrong Delivery Code.";
            }
            MessageBox.Show(message);
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            treeView.Focus();
        }

        private void Signout_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["Username"] = "";
            Properties.Settings.Default.Save();
            WelcomeScreen welcomeScreen = new WelcomeScreen();
            this.Hide();
            welcomeScreen.Show();
        }
    }
}
