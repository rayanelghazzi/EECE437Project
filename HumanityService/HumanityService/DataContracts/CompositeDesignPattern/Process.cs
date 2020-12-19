
using HumanityService.DataContracts.Requests;
using HumanityService.Stores.Interfaces;
using Microsoft.Extensions.DependencyInjection;
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
        List<IComponent> components = new List<IComponent>();

        public Process()
        {
        }

        public Process(string campaignId, string campaignName, string campaignUsername, string campaignType, AnswerCampaignRequest request)
        {
            Id = Utils.CreateId();
            CampaignId = campaignId;
            Status = "Pending";
            TimeCreated = Utils.UnixTimeSeconds();
            TimePickedUp = 0L;
            TimeCompleted = 0L;
            DeliveryCode = "";

            Contribution contribution = new Contribution(Id, campaignType, request);
            AddComponent(contribution);

            //Have to check if volunteering or not
            DeliveryDemand deliveryDemand = new DeliveryDemand(Id, campaignName, campaignUsername, request);
            AddComponent(deliveryDemand);
        }


        public async Task AnswerDeliveryDemand(string deliveryDemandId, AnswerDeliveryDemandRequest request)
        {
            Status = "InProgress";
            var deliveryCode = CreateDeliveryCode();
            DeliveryCode = deliveryCode;
            await Update();

            DeliveryDemand deliveryDemand = (DeliveryDemand)components.Find(x => x.Id == deliveryDemandId);
            await deliveryDemand.AnswerDeliveryDemand(deliveryCode, request);

            Contribution contribution = (Contribution)components.Find(x => x is Contribution);
            await contribution.OnAnswerDeliveryDemand();
        }

        public async Task ValidateDelivery(string pickupOrDestination)
        {
            if(pickupOrDestination == "Pickup")
            {
                Status = "PickedUp";
                TimePickedUp = Utils.UnixTimeSeconds();
                await Update();

                DeliveryDemand deliveryDemand = (DeliveryDemand)components.Find(x => x is DeliveryDemand && x.Status != "Cancelled");
                await deliveryDemand.ValidatePickup();

                Contribution contribution = (Contribution)components.Find(x => x is Contribution);
                await contribution.ValidatePickupDonor();
            }
            else
            {
                Status = "Completed";
                TimeCompleted= Utils.UnixTimeSeconds();
                await Update();

                DeliveryDemand deliveryDemand = (DeliveryDemand)components.Find(x => x is DeliveryDemand && x.Status != "Cancelled");
                await deliveryDemand.ValidateDestination();

                Contribution contribution = (Contribution)components.Find(x => x is Contribution);
                await contribution.ValidateDestinationDonor();
            }
        }

        public async Task Cancel()
        {
            // Only accept processes that aren't ongoing (e.g delivery demand not accepted yet)
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
