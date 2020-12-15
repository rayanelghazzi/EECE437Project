using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HumanityService.DataContracts;
using HumanityService.DataContracts.Requests;
using HumanityService.DataContracts.Results;
using HumanityService.Services.Interfaces;
using HumanityService.Stores.Interfaces;

namespace HumanityService.Services
{
    public class TransactionService : ITransactionService
    {

        private readonly ITransactionStore _transactionStore;
        private readonly INotificationService _notificationService;
        private readonly IMatchingService _matchingService;

        public TransactionService(ITransactionStore transactionStore, INotificationService notificationService, IMatchingService matchingService)
        {
            _transactionStore = transactionStore;
            _notificationService = notificationService;
            _matchingService = matchingService;
        }

        public Task<Campaign> FindMatch(string username, string type, string category)
        {
            throw new NotImplementedException();
            //call matching service with some parameters
            //what should we return (we have delivery demands and campaigns)
        }

        public async Task<string> AnswerCampaign(AnswerCampaignRequest request)
        {
            var campaign = await _transactionStore.GetCampaign(request.CampaignId);

            Process process = new Process
            {
                CampaignId = request.CampaignId,
                Status = "InProgress",
                TimeCreated = UnixTimeSeconds(),
                TimePickedUp = 0L,
                TimeCompleted = 0L,
                DeliveryCode = Guid.NewGuid().ToString()
            };
            var processId = await _transactionStore.AddProcess(process);

            Contribution contribution = new Contribution
            {
                ProcessId = processId,
                Type = campaign.Type,
                Username = request.Username,
                Status = "Pending",
                TimeWindowStart = request.TimeWindowStart,
                TimeWindowEnd = request.TimeWindowEnd,
                OtherInfo = request.OtherInfo, 
                TimeCreated = UnixTimeSeconds(),
                TimeCompleted = 0
            };
            var contributionId = await _transactionStore.AddContribution(contribution);

            DeliveryDemand deliveryDemand = new DeliveryDemand
            {
                ProcessId = processId,
                CampaignName = campaign.Name,
                PickupUsername = request.Username,
                DestinationUsername = campaign.Username, 
                Status = "Pending",
                TimeWindowStart = request.TimeWindowStart,
                TimeWindowEnd = request.TimeWindowEnd,
                OtherInfo = request.OtherInfo,
                TimeCreated = UnixTimeSeconds(),
                TimeCompleted = 0
            };
            await _transactionStore.AddDeliveryDemand(deliveryDemand);

            return contributionId; 
        }

        public async Task<string> AnswerDeliveryDemand(AnswerDeliveryDemandRequest request)
        {
            //Update DeliveryDemand
            var deliveryDemand = await _transactionStore.GetDeliveryDemand(request.DeliveryDemandId);
            deliveryDemand.Status = "InProgress";
            await _transactionStore.UpdateDeliveryDemand(deliveryDemand);

            //Create delivery code
            var deliveryCode = CreateDeliveryCode();

            //Update Process with delivery code
            var process = await _transactionStore.GetProcess(deliveryDemand.ProcessId);
            process.DeliveryCode = deliveryCode;
            await _transactionStore.UpdateProcess(process);

            //add contribution to the delivery demand for the deliverer
            Contribution deliveryContribution = new Contribution
            {
                ProcessId = deliveryDemand.ProcessId,
                DeliveryDemandId = deliveryDemand.Id,
                DeliveryCode = deliveryCode,
                Type = "Delivery",
                Username = request.Username,
                Status = "Accepted",
                TimeWindowStart = request.TimeWindowStart,
                TimeWindowEnd = request.TimeWindowEnd,
                OtherInfo = request.OtherInfo,
                TimeCreated = UnixTimeSeconds(),
                TimeCompleted = 0
            };

            var deliveryContributionId = await _transactionStore.AddContribution(deliveryContribution);

            //Update donor's contribution
            var getContributionsRequest = new GetContributionsRequest
            {
                ProcessId = deliveryDemand.ProcessId,
                Type = "Donation"
            };

            var getContributionsResult = await _transactionStore.GetContributions(getContributionsRequest);
            var donorContribution = getContributionsResult.Contributions[0];
            donorContribution.Status = "Accepted";
            await _transactionStore.UpdateContribution(donorContribution);

            return deliveryContributionId;
        }

        public async Task<bool> ValidateDelivery(ValidateDeliveryRequest request) //CONSIDER REFACTORING
        {
            if (request.ValidationType == "Pickup") //Pickup validation
            {
                var donorContribution = await _transactionStore.GetContribution(request.ContributionId);
                var process = await _transactionStore.GetProcess(donorContribution.ProcessId);

                if(process.DeliveryCode == request.DeliveryCode)
                {
                    //update donor's contribution
                    donorContribution.Status = "Completed";
                    donorContribution.TimeCompleted = UnixTimeSeconds();
                    await _transactionStore.UpdateContribution(donorContribution);

                    //update process
                    process.Status = "PickedUp";
                    process.TimePickedUp = UnixTimeSeconds();
                    await _transactionStore.UpdateProcess(process);

                    //update deliveryDemand
                    var getDeliveryDemandRequest = new GetDeliveryDemandsRequest
                    {
                        ProcessId = process.Id
                    };
                    var getDeliveryDemandsResult = await _transactionStore.GetDeliveryDemands(getDeliveryDemandRequest);
                    var deliveryDemand = getDeliveryDemandsResult.DeliveryDemands[0];
                    deliveryDemand.Status = "PickedUp";
                    await _transactionStore.UpdateDeliveryDemand(deliveryDemand);

                    //update deliverer's contribution
                    var getContributionRequest = new GetContributionsRequest
                    {
                        ProcessId = process.Id,
                        Type = "Delivery"
                    };
                    var deliveryContributions = await _transactionStore.GetContributions(getContributionRequest);
                    var deliveryContribution = deliveryContributions.Contributions[0];
                    deliveryContribution.Status = "PickedUp";
                    await _transactionStore.UpdateContribution(deliveryContribution);

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else //Destination validation
            {
                //The process for validating on destination is different
                //The ngo enters into the campaign page and enters the code (instead of having to open the specific process

                var getProcessesResult = await _transactionStore.GetProcesses(request.CampaignId);
                var process = getProcessesResult.Processes.Find(request => request.DeliveryCode == request.DeliveryCode);
                if (process != null)
                {
                    //update process
                    process.Status = "Completed";
                    process.TimeCompleted = UnixTimeSeconds();
                    await _transactionStore.UpdateProcess(process);

                    //update delivery demand
                    var getDeliveryDemandRequest = new GetDeliveryDemandsRequest
                    {
                        ProcessId = process.Id
                    };
                    var getDeliveryDemandsResult = await _transactionStore.GetDeliveryDemands(getDeliveryDemandRequest);
                    var deliveryDemand = getDeliveryDemandsResult.DeliveryDemands[0];
                    deliveryDemand.Status = "Completed";
                    await _transactionStore.UpdateDeliveryDemand(deliveryDemand);

                    //update deliverer's contribution
                    var getContributionRequest = new GetContributionsRequest
                    {
                        ProcessId = process.Id,
                        Type = "Delivery"
                    };
                    var deliveryContributions = await _transactionStore.GetContributions(getContributionRequest);
                    var deliveryContribution = deliveryContributions.Contributions[0];
                    deliveryContribution.Status = "Completed";
                    await _transactionStore.UpdateContribution(deliveryContribution);

                    //update campaign
                    var campaign = await _transactionStore.GetCampaign(request.CampaignId);
                    campaign.CurrentState++;

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Task ApproveContribution(string contributionId)//
        {
            throw new NotImplementedException();
            //update campaign
            //
        }

        public async Task<string> CreateCampaign(CreateCampaignRequest request)
        {
            Campaign campaign = new Campaign
            {
                Name = request.Name,
                Username = request.NgoUsername,
                NgoName = request.NgoName,
                Type = request.Type,
                Category = request.Category,
                Target = request.Target,
                CurrentState = 0,
                TimeCreated = UnixTimeSeconds(),
                TimeCompleted = 0, 
                Description = request.Description
            };

            var campaignId = await _transactionStore.AddCampaign(campaign);
            return campaignId; 
        }

        public async Task DeleteCampaign(string campaignId)
        {
            await _transactionStore.DeleteCampaign(campaignId);
        }

        public async Task DeleteContribution(string contributionId)
        {
            //If donation or volunteering delete the process (process, contributions, deliveryDemand)
            //
            //If delivery Demand update donation to pending and update delivery demand to
        }

        public Task DeleteDeliveryDemand(string deliveryDemandId)
        {
            throw new NotImplementedException();
            //Might need it if NGOs have the ability to change all these requirements
        }

        public Task EditCampaign(EditCampaignRequest request)
        {
            throw new NotImplementedException();
            // How do we only allow the right user to edit his camapaign
        }

        public Task EditContribution(EditContributionRequest request)
        {
            throw new NotImplementedException();
            //edit the contribution
            //edit delivery demand if needed
        }


        public async Task<Campaign> GetCampaign(string campaignId)
        {
            var campaign = await _transactionStore.GetCampaign(campaignId);
            return campaign;
        }

        public async Task<GetCampaignsResult> GetCampaigns(GetCampaignsRequest request)
        {
            var getCampaignsResult = await _transactionStore.GetCampaigns(request);
            return getCampaignsResult;
        }

        public async Task<Contribution> GetContribution(string contributionId)
        {
            var contribution = await _transactionStore.GetContribution(contributionId);
            return contribution;
        }

        public async Task<GetContributionsResult> GetContributions(GetContributionsRequest request)
        {
            var getContributionsResult = await _transactionStore.GetContributions(request);
            return getContributionsResult;
        }

        public async Task<DeliveryDemand> GetDeliveryDemand(string deliveryDemandId)
        {
            var deliveryDemand = await _transactionStore.GetDeliveryDemand(deliveryDemandId);
            return deliveryDemand;
        }

        public async Task<GetDeliveryDemandsResult> GetDeliveryDemands(GetDeliveryDemandsRequest request)
        {
            var getDeliveryDemandsResult = await _transactionStore.GetDeliveryDemands(request);
            return getDeliveryDemandsResult;
        }

        public async Task<Process> GetProcess(string processId)
        {
            var process = await _transactionStore.GetProcess(processId);
            return process;
        }

        public async Task<GetProcessesResult> GetProcesses(string campaignId)
        {
            var getProcessesResult = await _transactionStore.GetProcesses(campaignId);
            return getProcessesResult;
        }

        private long UnixTimeSeconds()
        {
            return DateTimeOffset.UtcNow.ToUnixTimeSeconds();
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
