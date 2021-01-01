using HumanityService.DataContracts.Requests;
using HumanityService.Stores.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HumanityService.DataContracts.CompositeDesignPattern
{
    public class Process : IComponent
    {
        public string Id { get; set; }
        public string CampaignId { get; set; }
        public string Status { get; set; }
        public long TimeCreated { get; set; }
        public long TimePickedUp { get; set; }
        public long TimeCompleted { get; set; }
        public string DeliveryCode { get; set; }

        ITransactionStore _transactionStore;
        private readonly List<IComponent> components = new List<IComponent>();

        public Process()
        {
        }

        public Process(string campaignId, string campaignName, string campaignUsername, string campaignType, AnswerCampaignRequest request)
        {
            //Process constructor
            Id = Utils.CreateId();
            CampaignId = campaignId;
            //Initially, a process' status is: Pending
            Status = "Pending";
            TimeCreated = Utils.UnixTimeSeconds();
            TimePickedUp = 0L;
            TimeCompleted = 0L;
            DeliveryCode = "";
            //A new contribution availabilty gets posted
            Contribution contribution = new Contribution(Id, campaignType, request);
            AddComponent(contribution);

            //A process automatically intiates a new delivery demand if the campaign is a physical donation.
            if (campaignType == "Donation")
            {
                DeliveryDemand deliveryDemand = new DeliveryDemand(Id, campaignName, campaignUsername, request);
                AddComponent(deliveryDemand);
            }
        }

        public async Task ApproveContribution() //For volunteer. Once a contribution is assigned, its status is updated to: InProgress.
        {
            Status = "InProgress";
            await Update();

            Contribution contribution = (Contribution)components.Find(x => x is Contribution);
            await contribution.SetStatusInProgress();
        }

        public async Task ValidateContribution() //For volunteer. Once a contribution is completed, its status is updated to: Completed.
        {
            Status = "Completed";
            await Update();

            Contribution contribution = (Contribution)components.Find(x => x is Contribution);
            await contribution.SetStatusCompleted();
        }


        public async Task AnswerDeliveryDemand(string deliveryDemandId, AnswerDeliveryDemandRequest request)
        //For delivery courier. Once a delivery demand is answered, its status is updated to: InProgress.
        {
            Status = "InProgress";
            // Delivery Code is auto-generated for later valdation
            var deliveryCode = CreateDeliveryCode();
            DeliveryCode = deliveryCode;
            await Update();

            DeliveryDemand deliveryDemand = (DeliveryDemand)components.Find(x => x.Id == deliveryDemandId);
            await deliveryDemand.AnswerDeliveryDemand(deliveryCode, request);

            Contribution contribution = (Contribution)components.Find(x => x is Contribution);
            await contribution.SetStatusInProgress();
        }

        public async Task ValidateDelivery(string pickupOrDestination)
        // Function to update delivery status and validate with delivery courier at once.
        {
            if(pickupOrDestination == "Pickup")
            {
                Status = "PickedUp";
                TimePickedUp = Utils.UnixTimeSeconds();
                await Update();

                DeliveryDemand deliveryDemand = (DeliveryDemand)components.Find(x => x is DeliveryDemand && x.Status != "Cancelled");
                await deliveryDemand.ValidatePickup();

                Contribution contribution = (Contribution)components.Find(x => x is Contribution);
                await contribution.SetStatusPickedUp();
                await contribution.SetTimeCompleted();
            }
            else
            {
                Status = "Completed";
                TimeCompleted= Utils.UnixTimeSeconds();
                await Update();

                DeliveryDemand deliveryDemand = (DeliveryDemand)components.Find(x => x is DeliveryDemand && x.Status != "Cancelled");
                await deliveryDemand.ValidateDestination();

                Contribution contribution = (Contribution)components.Find(x => x is Contribution);
                await contribution.SetStatusCompleted();
            }
        }

        public async Task Cancel()
        {
            // Only cancel processes that aren't ongoing (e.g delivery demand not accepted yet)
            if(Status == "Pending")
            {
                Status = "Cancelled";
                await Update();

                foreach (var component in components)
                {
                    await component.Cancel();
                }
            }
        }

        public async Task CancelDeliveryContribution()
        // Once a delivery contribution (answer) is canceled, its status is back to: Pending and its reset.
        {
            Status = "Pending";
            await Update();

            DeliveryDemand deliveryDemand = (DeliveryDemand)components.Find(x => x is DeliveryDemand);
            await deliveryDemand.Cancel();
            await deliveryDemand.Reset();

            Contribution contribution = (Contribution)components.Find(x => x is Contribution);
            await contribution.Reset();
        }

        public async Task Save()
        {
            await _transactionStore.AddProcess(this);
            foreach (var component in components)
            {
                await component.Save();
            }
        }

        public async Task Update()
        {
            await _transactionStore.UpdateProcess(this);
        }

        public void AddComponent(IComponent component)
        {
            components.Add(component);
        }

        public void SetStore(ITransactionStore transactionStore)
        {
            _transactionStore = transactionStore;
            foreach (var component in components)
            {
                component.SetStore(transactionStore);
            }
        }

        private string CreateDeliveryCode()
        {
            // ex output: "230798"
            // The code is simple to facilitate typing for donors
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < 6; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
    }
}
