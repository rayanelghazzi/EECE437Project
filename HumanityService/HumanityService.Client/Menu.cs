using HumanityService.DataContracts;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace HumanityService.Client
{
    
    public partial class Menu : Form
    {
        private static List<Panel> panels = new List<Panel>();
        private static Campaign matchedCampaign;
        private static DeliveryDemand matchedDeliveryDemand;
        private static string volunteeringTag;
        private static Dictionary<string,string> transportationType = new Dictionary<string, string>();

        private HumanityServiceClient client;
        public Menu()
        {
            client = new HumanityServiceClient();
            InitializeComponent();
            transportationType.Add("Car", "driving-car");
            transportationType.Add("Walking", "cycling-regular");
            transportationType.Add("Cycling", "foot-walking");
        }

        private void Navigate(Panel destinationPanel)
        {
            foreach(var panel in panels)
            {
                if (panel == destinationPanel) panel.Show();
                else panel.Hide();
            }
        }

        private void SignoutButton_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["Username"] = "";
            Properties.Settings.Default.Save();
            WelcomeScreen welcomeScreen = new WelcomeScreen();
            this.Hide();
            welcomeScreen.Show();
        }

        private async void FindCampaignMatchAsync(object sender, EventArgs e)
        {
            var tag = ((Button)sender).Tag.ToString();
            var matchCampaignRequest = new MatchCampaignRequest
            {
                Type = "Donation",
                Category = tag
            };
            matchedCampaign = await client.MatchCampaign(matchCampaignRequest);
            DonationCampaignMatchPanel_CampaignNameLabel.Text = matchedCampaign.Name;
            Navigate(DonationCampaignMatchPanel);
        }

        private async void AnswerDonationCampaignAsync(object sender, EventArgs e)
        {
            var timeWindowStart = ToUnixTime(DonationInfoPanel_DatePicker.Value, DonationInfoPanel_TimePicker1.Value);
            var timeWindowEnd = ToUnixTime(DonationInfoPanel_DatePicker.Value, DonationInfoPanel_TimePicker2.Value);
            var answerCampaignRequest = new AnswerCampaignRequest
            {
                Username = Properties.Settings.Default["Username"].ToString(),
                TimeWindowStart = timeWindowStart,
                TimeWindowEnd = timeWindowEnd,
                OtherInfo = DonationInfoPanel_OtherInfo.Text
            };
            await client.AnswerCampaign(matchedCampaign.Id, answerCampaignRequest);
            Navigate(MenuPanel);
        }

        private async void DeliveryInfoPanel_FindDeliveryDemandMatch_Click(object sender, EventArgs e)
        {
            var timeWindowStart = ToUnixTime(DeliveryInfoPanel_DatePicker.Value, DeliveryInfoPanel_TimePicker1.Value);
            var timeWindowEnd = ToUnixTime(DeliveryInfoPanel_DatePicker.Value, DeliveryInfoPanel_TimePicker2.Value);
            var location = new Location
            {
                Longitude = Convert.ToDouble(DeliveryInfoPanel_Longitude.Text),
                Latitude = Convert.ToDouble(DeliveryInfoPanel_Latitude.Text)
            };
            var matchDeliveryDemandRequest = new MatchDeliveryDemandRequest
            {
                TimeWindowStart = timeWindowStart,
                TimeWindowEnd = timeWindowEnd,
                DelivererLocation = location,
                TransportationType = transportationType[DeliveryInfoPanel_TransportationType.Text]
            };
            matchedDeliveryDemand = await client.MatchDeliveryDemand(matchDeliveryDemandRequest);
            DeliveryDemandMatchPanel_CampaignNameLabel.Text = matchedDeliveryDemand.CampaignName; 
            Navigate(DeliveryDemandMatchPanel);
        }

        private async void DeliveryDemandMatchPanel_AnswerCampaignButton_Click(object sender, EventArgs e)
        {
            var timeWindowStart = ToUnixTime(DeliveryInfoPanel_DatePicker.Value, DeliveryInfoPanel_TimePicker1.Value);
            var timeWindowEnd = ToUnixTime(DeliveryInfoPanel_DatePicker.Value, DeliveryInfoPanel_TimePicker2.Value);
            var answerDeliveryDemandRequest = new AnswerDeliveryDemandRequest
            {
                Username = Properties.Settings.Default["Username"].ToString(),
                TimeWindowStart = timeWindowStart,
                TimeWindowEnd = timeWindowEnd
            };
            await client.AnswerDeliveryDemand(matchedDeliveryDemand.Id, answerDeliveryDemandRequest);
            Navigate(MenuPanel);
        }

        private async void VolunteeringInfoPanel_FindMatch_Click(object sender, EventArgs e)
        {
            var location = new Location
            {
                Longitude = Convert.ToDouble(VolunteeringInfoPanel_Longitude.Text),
                Latitude = Convert.ToDouble(VolunteeringInfoPanel_Latitude.Text)
            };
            var matchCampaignRequest = new MatchCampaignRequest
            {
                Type = "Volunteering",
                Category = volunteeringTag,
                TransportationType = transportationType[VolunteeringInfoPanel_TransportationType.Text],
                Location = location
            };
            matchedCampaign = await client.MatchCampaign(matchCampaignRequest);
            VolunteeringCampaignMatchPanel_CampaignNameLabel.Text = matchedCampaign.Name;
            Navigate(VolunteeringCampaignMatchPanel);
        }

        private async void VolunteeringCampaignMatchPanel_ContributeButton_Click(object sender, EventArgs e)
        {
            var answerCampaignRequest = new AnswerCampaignRequest
            {
                Username = Properties.Settings.Default["Username"].ToString(),
                OtherInfo = VolunteeringInfoPanel_OtherInfo.Text
            };
            await client.AnswerCampaign(matchedCampaign.Id, answerCampaignRequest);
            Navigate(MenuPanel);
        }

        private void BackToMenu_Click(object sender, EventArgs e)
        {
            Navigate(MenuPanel);
        }

        private void BackToCampaignMatch_Click(object sender, EventArgs e)
        {
            Navigate(DonationCampaignMatchPanel);
        }

        private void BackToDonations_Click(object sender, EventArgs e)
        {
            Navigate(DonationsPanel);
        }

        private void DonateButton_Click(object sender, EventArgs e)
        {
            Navigate(DonationsPanel);
        }

        private void VolunteerButton_Click(object sender, EventArgs e)
        {
            Navigate(VolunteeringPanel);
        }

        private void MyContributionsButton_Click(object sender, EventArgs e)
        {
            Navigate(MyContributionsPanel);
        }

        private void VolunteeringChoiceButton(object sender, EventArgs e)
        {
            volunteeringTag = ((Button)sender).Tag.ToString();
            VolunteeringInfoPanel_Title.Text = "Help us find a good " + volunteeringTag + " volunteering job for you.";
            Navigate(VolunteeringInfoPanel);
        }

        private void DonationCampaignMatchPanel_ContributeButton_Click(object sender, EventArgs e)
        {
            Navigate(DonationInfoPanel);
        }

        private void FindDeliveryDemandMatch_Click(object sender, EventArgs e)
        {
            Navigate(DeliveryInfoPanel);
        }

        private void BackToVolunteering_Click(object sender, EventArgs e)
        {
            Navigate(VolunteeringPanel);
        }

        private void VolunteeringInfoPanel_BackButton_Click(object sender, EventArgs e)
        {
            Navigate(VolunteeringPanel);
        }

        private void BackToVolunteeringInfo_Click(object sender, EventArgs e)
        {
            Navigate(VolunteeringInfoPanel);
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            panels.Add(DonationCampaignMatchPanel);
            panels.Add(DeliveryDemandMatchPanel);
            panels.Add(DeliveryInfoPanel);
            panels.Add(DonationInfoPanel);
            panels.Add(DonationsPanel);
            panels.Add(MenuPanel);
            panels.Add(VolunteeringPanel);
            panels.Add(MyContributionsPanel);
            panels.Add(VolunteeringCampaignMatchPanel);
            panels.Add(VolunteeringInfoPanel);
            Navigate(MenuPanel);
        }

        private void DonationInfoPanel_Paint(object sender, PaintEventArgs e)
        {
            DonationInfoPanel_TimePicker1.Format = DateTimePickerFormat.Custom;
            DonationInfoPanel_TimePicker1.CustomFormat = "HH : mm";
            DonationInfoPanel_TimePicker2.Format = DateTimePickerFormat.Custom;
            DonationInfoPanel_TimePicker2.CustomFormat = "HH : mm";
        }

        private void DeliveryInfoPanel_Paint(object sender, PaintEventArgs e)
        {
            DeliveryInfoPanel_TimePicker1.Format = DateTimePickerFormat.Custom;
            DeliveryInfoPanel_TimePicker1.CustomFormat = "HH : mm";
            DeliveryInfoPanel_TimePicker2.Format = DateTimePickerFormat.Custom;
            DeliveryInfoPanel_TimePicker2.CustomFormat = "HH : mm";
        }

        private long ToUnixTime(DateTime YYYYMMDD, DateTime HHmm)
        {
            var dateTime = new DateTime(YYYYMMDD.Year, YYYYMMDD.Month, YYYYMMDD.Day, HHmm.Hour, HHmm.Minute, 0);
            return ((DateTimeOffset)dateTime).ToUnixTimeSeconds();
        }
    }
}
