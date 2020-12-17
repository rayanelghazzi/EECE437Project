using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HumanityService.DataContracts;
using HumanityService.DataContracts.Requests;
using HumanityService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HumanityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet("campaigns/{campaignId}")]
        public async Task<IActionResult> GetCampaign(string campaignId)
        {
            var campaign = await _transactionService.GetCampaign(campaignId);
            return Ok(campaign);
        }

        [HttpGet("campaigns")]
        public async Task<IActionResult> GetCampaigns([FromBody] GetCampaignsRequest request)
        {
            var campaigns = await _transactionService.GetCampaigns(request);
            return Ok(campaigns);
        }

        //[HttpGet("campaigns/find-match")]
        //public async Task<IActionResult> FindMatchingCampaign()
        //{
            
        //}

        [HttpPost("campaigns")]
        public async Task<IActionResult> CreateCampaign([FromBody] CreateCampaignRequest request)
        {
            await _transactionService.CreateCampaign(request);
            return Ok();
        }

        [HttpPost("campaigns/answer-campaign/{campaignId}")]
        public async Task<IActionResult> AnswerCampaign(string campaignId, [FromBody] AnswerCampaignRequest request)
        {
            //TO SECURE
            await _transactionService.AnswerCampaign(campaignId, request);
            return Ok();
        }

        [HttpPut("campaigns/{campaignId}")]
        public async Task<IActionResult> EditCampaign(string campaignId, [FromBody] EditCampaignRequest request)
        {
            //TO SECURE
            await _transactionService.EditCampaign(campaignId, request);
            return Ok();
        }

        [HttpDelete("campaigns/{campaignId}")]
        public async Task<IActionResult> CancelCampaign(string campaignId)
        {
            //TO SECURE
            await _transactionService.CancelCampaign(campaignId);
            return Ok();
        }

        [HttpGet("delivery-demands/{deliveryDemandId}")]
        public async Task<IActionResult> GetDeliveryDemand(string deliveryDemandId)
        {
            var deliveryDemand = await _transactionService.GetDeliveryDemand(deliveryDemandId);
            return Ok(deliveryDemand);
        }

        [HttpGet("delivery-demands")]
        public async Task<IActionResult> GetDeliveryDemands([FromBody] GetDeliveryDemandsRequest request)
        {
            var deliveryDemands = await _transactionService.GetDeliveryDemands(request);
            return Ok(deliveryDemands);
        }

        [HttpPost("delivery-demands/answer-delivery-demand/{deliveryDemandId}")]
        public async Task<IActionResult> AnswerDeliveryDemand(string deliveryDemandId, [FromBody] AnswerDeliveryDemandRequest request)
        {
            //TO SECURE
            await _transactionService.AnswerDeliveryDemand(deliveryDemandId, request);
            return Ok();
        }

        [HttpGet("contributions/{contributionId}")]
        public async Task<IActionResult> GetContribution(string contributionId)
        {
            var contribution = await _transactionService.GetContribution(contributionId);
            return Ok(contribution);
        }

        [HttpGet("contributions")]
        public async Task<IActionResult> GetContributions([FromBody] GetContributionsRequest request)
        {
            var contributions = await _transactionService.GetContributions(request);
            return Ok(contributions);
        }

        [HttpPost("contributions/{contributionId}")]
        public async Task<IActionResult> ApproveContribution(string contributionId)
        {
            //See if still needed
            await _transactionService.ApproveContribution(contributionId);
            return Ok(); 
        }

        [HttpDelete("contributions/{contributionId}")]
        public async Task<IActionResult> CancelContribution(string contributionId)
        {
            //TO SECURE
            await _transactionService.CancelContribution(contributionId);
            return Ok();
        }

        [HttpGet("processes/{processId}")]
        public async Task<IActionResult> GetProcess(string processId)
        {
            var process = await _transactionService.GetProcess(processId);
            return Ok(process);
        }

        [HttpGet("processes")]
        public async Task<IActionResult> GetProcesses(GetProcessesRequest request)
        {
            var processes = await _transactionService.GetProcesses(request.CampaignId);
            return Ok(processes);
        }

        [HttpPost("validate-delivery")]
        public async Task<IActionResult> ValidateDelivery([FromBody] ValidateDeliveryRequest request)
        {
            //TO SECURE
            await _transactionService.ValidateDelivery(request);
            return Ok();
        }


    }
}
