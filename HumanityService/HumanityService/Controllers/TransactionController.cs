using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HumanityService.DataContracts.ContributionDataContracts;
using HumanityService.DataContracts.DemandDataContracts;
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

        [HttpGet("demands/{demandId}")]
        public async Task<IActionResult> GetDemand(string demandId)
        {

        }

        [HttpGet("demands")]
        public async Task<IActionResult> GetDemands([FromBody] GetDemandsRequest request)
        {

        }

        [HttpPost("demands")]
        public async Task<IActionResult> CreateDemand([FromBody] EditDemandRequest request)
        {

        }

        [HttpPost("demands/{demandId}")]
        public async Task<IActionResult> AnswerDemand(string demandId, [FromBody] AnswerDemandRequest request)
        {

        }

        [HttpPut("demands/{demandId}")]
        public async Task<IActionResult> EditDemand(string demandId, [FromBody] EditDemandRequest request)
        {

        }

        [HttpDelete("demands/{demandId}")]
        public async Task<IActionResult> CancelDemand(string demandId)
        {

        }

        [HttpGet("contributions/{contributionId}")]
        public async Task<IActionResult> GetContribution(string contributionId)
        {

        }

        [HttpGet("contributions")]
        public async Task<IActionResult> GetContribution([FromBody] GetContributionsRequest request)
        {

        }

        [HttpPost("contributions/{contributionId}")]
        public async Task<IActionResult> ApproveContribution(string contributionId)
        {

        }

        [HttpPut("contributions/{contributionId}")]
        public async Task<IActionResult> EditContribution(string contributionId, [FromBody] EditContributionRequest request)
        {

        }

        [HttpDelete("contributions/{contributionId}")]
        public async Task<IActionResult> CancelContribution(string contributionId)
        {

        }

        [HttpGet("processes/{processId}")]
        public async Task<IActionResult> GetProcess(string processId)
        {

        }

        [HttpGet("processes/{demandId}")]
        public async Task<IActionResult> GetProcesses(string demandId)
        {

        }

        [HttpPost("delivery")]
        public async Task<IActionResult> ValidateDelivery([FromBody] ValidateDeliveryRequest request)
        {

        }

        [HttpPost("delivery/{contributionId}")]
        public async Task<IActionResult> AcceptDelivery(string contributionId)
        {

        }

    }
}
