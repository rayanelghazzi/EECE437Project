using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HumanityService.DataContracts;
using HumanityService.DataContracts.Requests;
using HumanityService.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HumanityService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        //[HttpGet("campaigns/{campaignId}")]
        //public async Task<IActionResult> GetCampaign(string campaignId)
        //{

        //}

        //[HttpGet("campaigns")]
        //public async Task<IActionResult> GetCampaigns([FromBody] GetCampaignsRequest request)
        //{

        //}

        //[HttpPost("campaigns")]
        //public async Task<IActionResult> CreateCampaign([FromBody] CreateCampaignRequest request)
        //{

        //}

        //[HttpPost("campaigns/{campaignId}")]
        //public async Task<IActionResult> AnswerCampaign(string campaignId, [FromBody] AnswerCampaignRequest request)
        //{

        //}

        //[HttpPut("campaigns/{campaignId}")]
        //public async Task<IActionResult> EditCampaign(string campaignId, [FromBody] EditCampaignRequest request)
        //{

        //}

        //[HttpDelete("campaigns/{campaignId}")]
        //public async Task<IActionResult> DeleteCampaign(string campaignId)
        //{

        //}

        //[HttpGet("deliverydemands/{deliveryDemandId}")]
        //public async Task<IActionResult> GetDeliveryDemand(string deliveryDemandId)
        //{

        //}

        //[HttpGet("deliverydemands")]
        //public async Task<IActionResult> GetDeliveryDemands([FromBody] GetDeliveryDemandsRequest request)
        //{

        //}


        //[HttpPost("deliverydemands/{deliveryDemandId}")]
        //public async Task<IActionResult> AnswerDeliveryDemand(string deliveryDemandId, [FromBody] AnswerDeliveryDemandRequest request)
        //{

        //}

        //[HttpPut("deliverydemands/{deliveryDemandId}")]
        //public async Task<IActionResult> EditDeliveryDemand(string deliveryDemandId, [FromBody] EditDeliveryDemandRequest request)
        //{

        //}

        //[HttpDelete("deliverydemands/{deliveryDemandId}")]
        //public async Task<IActionResult> DeleteDeliveryDemand(string deliveryDemandId)
        //{

        //}

        //[HttpGet("contributions/{contributionId}")]
        //public async Task<IActionResult> GetContribution(string contributionId)
        //{

        //}

        //[HttpGet("contributions")]
        //public async Task<IActionResult> GetContribution([FromBody] GetContributionsRequest request)
        //{

        //}

        //[HttpPost("contributions/{contributionId}")]
        //public async Task<IActionResult> ApproveContribution(string contributionId)
        //{

        //}

        //[HttpPut("contributions/{contributionId}")]
        //public async Task<IActionResult> EditContribution(string contributionId, [FromBody] EditContributionRequest request)
        //{

        //}

        //[HttpDelete("contributions/{contributionId}")]
        //public async Task<IActionResult> DeleteContribution(string contributionId)
        //{

        //}

        //[HttpGet("processes/{processId}")]
        //public async Task<IActionResult> GetProcess(string processId)
        //{

        //}

        //[HttpGet("processes/{demandId}")]
        //public async Task<IActionResult> GetProcesses(string campaignId)
        //{

        //}

        //[HttpPost("delivery")]
        //public async Task<IActionResult> ValidateDelivery([FromBody] ValidateDeliveryRequest request)
        //{

        //}

        //[HttpPost("delivery/{contributionId}")]
        //public async Task<IActionResult> AcceptDelivery(string contributionId)
        //{

        //}

    }
}
