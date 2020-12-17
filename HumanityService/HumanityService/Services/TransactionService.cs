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

        public TransactionService(ITransactionStore transactionStore)
        {
            _transactionStore = transactionStore;
            //_notificationService = notificationService;
            //_matchingService = matchingService;
        }

        public Task<Campaign> FindMatch(string username, string type, string category)
        {
            throw new NotImplementedException();
            //call matching service with some parameters
            //what should we return (we have delivery demands and campaigns)
        }

        public async Task<string> AnswerCampaign(string campaignId, AnswerCampaignRequest request)
        {
            // What if one of them failed? You have to delete all of them
            var campaign = await _transactionStore.GetCampaign(campaignId);

            Process process = new Process
            {
                CampaignId = campaignId,
                Status = "Pending",
                TimeCreated = UnixTimeSeconds(),
                TimePickedUp = 0L,
                TimeCompleted = 0L,
                DeliveryCode = ""
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

        public async Task<string> AnswerDeliveryDemand(string deliveryDemandId, AnswerDeliveryDemandRequest request)
        {
            //Update DeliveryDemand
            var deliveryDemand = await _transactionStore.GetDeliveryDemand(deliveryDemandId);
            deliveryDemand.Status = "InProgress";
            await _transactionStore.UpdateDeliveryDemand(deliveryDemand);

            //Create delivery code
            var deliveryCode = CreateDeliveryCode();

            //Update Process with delivery code
            var process = await _transactionStore.GetProcess(deliveryDemand.ProcessId);
            process.Status = "InProgress";
            process.DeliveryCode = deliveryCode;
            await _transactionStore.UpdateProcess(process);

            //add contribution to the delivery demand for the deliverer
            Contribution deliveryContribution = new Contribution
            {
                ProcessId = deliveryDemand.ProcessId,
                DeliveryDemandId = deliveryDemandId,
                DeliveryCode = deliveryCode,
                Type = "Delivery",
                Username = request.Username,
                Status = "InProgress",
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
            donorContribution.Status = "InProgress";
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
                    deliveryDemand.TimeCompleted = UnixTimeSeconds();
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
                    deliveryContribution.TimeCompleted = UnixTimeSeconds();
                    await _transactionStore.UpdateContribution(deliveryContribution);

                    //update campaign
                    var campaign = await _transactionStore.GetCampaign(request.CampaignId);
                    campaign.CurrentState++;
                    await _transactionStore.UpdateCampaign(campaign);
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public Task ApproveContribution(string contributionId)
        {
            throw new NotImplementedException();
            //update contribution
            //Used with volunteering contributions
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
                Status = "Active",
                CurrentState = 0,
                TimeCreated = UnixTimeSeconds(),
                TimeCompleted = 0, 
                Description = request.Description
            };

            var campaignId = await _transactionStore.AddCampaign(campaign);
            return campaignId; 
        }

        public async Task CancelCampaign(string campaignId)
        {
            //cancel campaign
            var campaign = await _transactionStore.GetCampaign(campaignId);
            campaign.Status = "Inactive";
            campaign.TimeCompleted = UnixTimeSeconds();
            await _transactionStore.UpdateCampaign(campaign);

            //cancel all ongoing processes
            var getProcessesResult = await _transactionStore.GetProcesses(campaignId);
            foreach (var process in getProcessesResult.Processes)
            {
                //Only cancel processes that haven't been accepted yet
                if(process.Status == "Pending")
                {
                    process.Status = "Cancelled";
                    await _transactionStore.UpdateProcess(process);

                    //cancel contributions
                    var getContributionsRequest = new GetContributionsRequest
                    {
                        ProcessId = process.Id
                    };
                    var getContributionsResult = await _transactionStore.GetContributions(getContributionsRequest);

                    foreach (var contribution in getContributionsResult.Contributions)
                    {
                        contribution.Status = "Cancelled";
                        await _transactionStore.UpdateContribution(contribution);
                    }

                    //cancel delivery demands
                    var getDeliveryDemandsRequest = new GetDeliveryDemandsRequest
                    {
                        ProcessId = process.Id
                    };
                    var getDeliveryDemandsResult = await _transactionStore.GetDeliveryDemands(getDeliveryDemandsRequest);

                    foreach (var deliveryDemand in getDeliveryDemandsResult.DeliveryDemands)
                    {
                        deliveryDemand.Status = "Cancelled";
                        await _transactionStore.UpdateDeliveryDemand(deliveryDemand);
                    }
                }
            }
        }

        public async Task CancelContribution(string contributionId)
        {
            var contribution = await _transactionStore.GetContribution(contributionId);

            //If donation or volunteering
            if (contribution.Type == "Delivery")
            {
                // Cancel the delivery contribution
                contribution.Status = "Cancelled";
                await _transactionStore.UpdateContribution(contribution);

                // Set the process to pending
                var process = await _transactionStore.GetProcess(contribution.ProcessId);
                process.Status = "Pending";
                await _transactionStore.UpdateProcess(process);

                // Set the donor's contribution to Pending
                var getContributionsRequest = new GetContributionsRequest
                {
                    ProcessId = contribution.ProcessId,
                    Type = "Donation"
                };
                var getContributionsResult = await _transactionStore.GetContributions(getContributionsRequest);
                var donorContribution = getContributionsResult.Contributions[0];
                donorContribution.Status = "Pending";
                await _transactionStore.UpdateContribution(donorContribution);

                // Set the delivery demand to Pending
                var getDeliveryDemandsRequest = new GetDeliveryDemandsRequest
                {
                    ProcessId = contribution.ProcessId
                };
                var getDeliveryDemandsResult = await _transactionStore.GetDeliveryDemands(getDeliveryDemandsRequest);
                var deliveryDemand = getDeliveryDemandsResult.DeliveryDemands[0];
                deliveryDemand.Status = "Pending";
                await _transactionStore.UpdateDeliveryDemand(deliveryDemand);
            }
            else //If Donation or volunteering
            {
                // Cancel the delivery contribution
                contribution.Status = "Cancelled";
                await _transactionStore.UpdateContribution(contribution);

                var process = await _transactionStore.GetProcess(contribution.ProcessId);
                process.Status = "Cancelled";
                await _transactionStore.UpdateProcess(process);

                var getContributionsRequest = new GetContributionsRequest
                {
                    ProcessId = contribution.ProcessId,
                    Type = "Delivery"
                };
                var getContributionsResult = await _transactionStore.GetContributions(getContributionsRequest);
                var donorContribution = getContributionsResult.Contributions[0];
                donorContribution.Status = "Cancelled";
                await _transactionStore.UpdateContribution(donorContribution);

                var getDeliveryDemandsRequest = new GetDeliveryDemandsRequest
                {
                    ProcessId = contribution.ProcessId
                };
                var getDeliveryDemandsResult = await _transactionStore.GetDeliveryDemands(getDeliveryDemandsRequest);
                var deliveryDemand = getDeliveryDemandsResult.DeliveryDemands[0];
                deliveryDemand.Status = "Cancelled";
                await _transactionStore.UpdateDeliveryDemand(deliveryDemand);
            }
        }

        public async Task EditCampaign(string campaignId, EditCampaignRequest request)
        {
            var campaign = await _transactionStore.GetCampaign(campaignId);
            campaign.Name = request.CampaignName;
            campaign.Type = request.Type;
            campaign.Category = request.Category;
            campaign.Target = request.Target;
            campaign.Status = request.Status;
            campaign.Description = request.Description;
            await _transactionStore.UpdateCampaign(campaign);
        }

        public Task EditContribution(string contributionId, EditContributionRequest request)
        {
            //Do we need it? Just cancel the one you already have
            throw new NotImplementedException(); 
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
