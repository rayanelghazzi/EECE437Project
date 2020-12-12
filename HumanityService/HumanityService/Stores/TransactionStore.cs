using AutoMapper;
using Dapper;
using HumanityService.DataContracts;
using HumanityService.DataContracts.Requests;
using HumanityService.Exceptions;
using HumanityService.Stores.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace HumanityService.Stores
{
    public class TransactionStore : ITransactionStore
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly ILocationStore _locationStore;
        private readonly IMapper _mapper;

        public TransactionStore(ISqlConnectionFactory sqlConnectionFactory, ILocationStore locationStore)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _locationStore = locationStore; 

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Campaign, CampaignEntity>();
                cfg.CreateMap<CampaignEntity, Campaign>();
                cfg.CreateMap<Contribution, ContributionEntity>();
                cfg.CreateMap<ContributionEntity, Contribution>();
                cfg.CreateMap<Process, ProcessEntity>();
                cfg.CreateMap<ProcessEntity, Process>();
                cfg.CreateMap<DeliveryDemand, DeliveryDemandEntity>();
                cfg.CreateMap<DeliveryDemandEntity, DeliveryDemand>();
            });
            _mapper = configuration.CreateMapper();
        }

        private static readonly string[] CampaignsTableColumns =
        {
            nameof(CampaignEntity.Id),
            nameof(CampaignEntity.Name),
            nameof(CampaignEntity.Username),
            nameof(CampaignEntity.Status),
            nameof(CampaignEntity.Type),
            nameof(CampaignEntity.Category),
            nameof(CampaignEntity.Target),
            nameof(CampaignEntity.CurrentState),
            nameof(CampaignEntity.TimeCreated),
            nameof(CampaignEntity.TimeCompleted),
            nameof(CampaignEntity.Description)
        };

        private static readonly string[] ContributionsTableColumns =
        {
            nameof(ContributionEntity.Id),
            nameof(ContributionEntity.ProcessId),
            nameof(ContributionEntity.Type),
            nameof(ContributionEntity.Username),
            nameof(ContributionEntity.Status),
            nameof(ContributionEntity.TimeWindowStart),
            nameof(ContributionEntity.TimeWindowEnd),
            nameof(ContributionEntity.TimeCreated),
            nameof(ContributionEntity.TimeCompleted)
        };

        private static readonly string[] ProcessesTableColumns =
        {
            nameof(ProcessEntity.Id),
            nameof(ProcessEntity.CampaignId),
            nameof(ProcessEntity.Status),
            nameof(ProcessEntity.TimeCreated),
            nameof(ProcessEntity.TimeCompleted),
            nameof(ProcessEntity.DeliveryCode)
        };

        private static readonly string[] DeliveryDemandsTableColumns =
        {
            nameof(DeliveryDemandEntity.Id),
            nameof(DeliveryDemandEntity.Name),
            nameof(DeliveryDemandEntity.ProcessId),
            nameof(DeliveryDemandEntity.PickupUsername),
            nameof(DeliveryDemandEntity.DestinationUsername),
            nameof(DeliveryDemandEntity.TimeWindowStart),
            nameof(DeliveryDemandEntity.TimeWindowEnd),
            nameof(DeliveryDemandEntity.Status),
            nameof(DeliveryDemandEntity.ProcessId),
            nameof(DeliveryDemandEntity.TimeCreated),
            nameof(DeliveryDemandEntity.TimeCompleted),
            nameof(DeliveryDemandEntity.Description)
        };

        public async Task AddCampaign(Campaign campaign)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();
            var sql = new QueryBuilder().InsertInto("campaigns", CampaignsTableColumns).Build();
            var campaignEntity = ToCampaignEntity(campaign);
            await connection.ExecuteAsync(sql, campaignEntity);
        }

        public async Task AddDeliveryDemand(DeliveryDemand deliveryDemand)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();
            var sql = new QueryBuilder().InsertInto("delivery-demands", CampaignsTableColumns).Build();
            var deliveryDemandEntity = ToDeliveryDemandEntity(deliveryDemand);
            await connection.ExecuteAsync(sql, deliveryDemandEntity);
        }

        public async Task AddProcess(Process process)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();
            var sql = new QueryBuilder().InsertInto("processes", CampaignsTableColumns).Build();
            var processEntity = ToProcessEntity(process);
            await connection.ExecuteAsync(sql, processEntity);
        }

        public async Task AddContribution(Contribution contribution)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();
            var sql = new QueryBuilder().InsertInto("contributions", CampaignsTableColumns).Build();
            var contributionEntity = ToContributionEntity(contribution);
            await connection.ExecuteAsync(sql, contribution);
        }

        public async Task DeleteCampaign(int campaignId)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();

            var sql = new QueryBuilder().DeleteFrom("campaigns")
                .Where("Id = @CampaignId").Build();

            await connection.ExecuteAsync(sql, new
            {
                CampaignId = campaignId
            });
        }

        public async Task DeleteContribution(int contributionId)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();

            var sql = new QueryBuilder().DeleteFrom("contributions")
                .Where("Id = @ContributionId").Build();

            await connection.ExecuteAsync(sql, new
            {
                ContributionId = contributionId
            });
        }

        public async Task DeleteDeliveryDemand(int deliveryDemandId)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();

            var sql = new QueryBuilder().DeleteFrom("delivery-demands")
                .Where("Id = @DeliveryDemandId").Build();

            await connection.ExecuteAsync(sql, new
            {
                DeliveryDemandId = deliveryDemandId
            });
        }

        public async Task DeleteProcess(int processId)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();

            var sql = new QueryBuilder().DeleteFrom("processes")
                .Where("Id = @ProcessId").Build();

            await connection.ExecuteAsync(sql, new
            {
                ProcessId = processId
            });
        }

        public async Task UpdateCampaign(Campaign campaign)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();

            var sql = new QueryBuilder().Update("campaigns", CampaignsTableColumns)
                .Where($"Id = @Id").Build();


            int rowsAffected = await connection.ExecuteAsync(sql, campaign);
            if (rowsAffected == 0)
            {
                if (!await EntityExists("campaigns", campaign.Id))
                {
                    throw new StorageErrorException($"Campaign entity with Id {campaign.Id} was not found", 404);
                }
                throw new StorageErrorException($"The entity you are trying to update has changed, reload the entity and try again", 412);
            }
        }

        public async Task UpdateContribution(Contribution contribution)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();

            var sql = new QueryBuilder().Update("contributions", ContributionsTableColumns)
                .Where($"Id = @Id").Build();


            int rowsAffected = await connection.ExecuteAsync(sql, contribution);
            if (rowsAffected == 0)
            {
                if (!await EntityExists("contributions", contribution.Id))
                {
                    throw new StorageErrorException($"Contribution entity with Id {contribution.Id} was not found", 404);
                }
                throw new StorageErrorException($"The entity you are trying to update has changed, reload the entity and try again", 412);
            }
        }

        public async Task UpdateDeliveryDemand(DeliveryDemand deliveryDemand)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();

            var sql = new QueryBuilder().Update("delivery-demands", DeliveryDemandsTableColumns)
                .Where($"Id = @Id").Build();


            int rowsAffected = await connection.ExecuteAsync(sql, deliveryDemand);
            if (rowsAffected == 0)
            {
                if (!await EntityExists("delivery-demands", deliveryDemand.Id))
                {
                    throw new StorageErrorException($"DeliveryDemand entity with Id {deliveryDemand.Id} was not found", 404);
                }
                throw new StorageErrorException($"The entity you are trying to update has changed, reload the entity and try again", 412);
            }
        }

        public async Task UpdateProcess(Process process)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();

            var sql = new QueryBuilder().Update("processes", ProcessesTableColumns)
                .Where($"Id = @Id").Build();


            int rowsAffected = await connection.ExecuteAsync(sql, process);
            if (rowsAffected == 0)
            {
                if (!await EntityExists("processes", process.Id))
                {
                    throw new StorageErrorException($"Process entity with Id {process.Id} was not found", 404);
                }
                throw new StorageErrorException($"The entity you are trying to update has changed, reload the entity and try again", 412);
            }
        }

        public async Task<Campaign> GetCampaign(int campaignId)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();

            var sql = new QueryBuilder()
                .SelectColumns("campaigns", CampaignsTableColumns)
                .Where("Id = @Id")
                .Build();

            var campaignEntity = await connection.QueryFirstOrDefaultAsync<CampaignEntity>(sql, new { Id = campaignId });
            var location = await _locationStore.GetLocation(campaignEntity.Username);
            var campaign = ToCampaign(campaignEntity, location);

            if (campaign == null)
            {
                throw new StorageErrorException($"Campaign entity with Id {campaignId} was not found", 404);
            }

            return campaign;
        }

        public async Task<List<Campaign>> GetCampaigns(GetCampaignsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<Contribution> GetContribution(int contributionId)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();

            var sql = new QueryBuilder()
                .SelectColumns("contributions", ContributionsTableColumns)
                .Where("Id = @Id")
                .Build();

            var contributionEntity = await connection.QueryFirstOrDefaultAsync<ContributionEntity>(sql, new { Id = contributionId });
            var location = await _locationStore.GetLocation(contributionEntity.Username);
            var contribution = ToContribution(contributionEntity, location);

            if (contribution == null)
            {
                throw new StorageErrorException($"Contribution entity with Id {contributionId} was not found", 404);
            }

            return contribution;
        }

        public async Task<List<Contribution>> GetContributions(GetContributionsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<DeliveryDemand> GetDeliveryDemand(int deliveryDemandId)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();

            var sql = new QueryBuilder()
                .SelectColumns("deliver-demands", DeliveryDemandsTableColumns)
                .Where("Id = @Id")
                .Build();

            var deliveryDemandEntity = await connection.QueryFirstOrDefaultAsync<DeliveryDemandEntity>(sql, new { Id = deliveryDemandId });
            var pickupLocation = await _locationStore.GetLocation(deliveryDemandEntity.PickupUsername);
            var destinationLocation = await _locationStore.GetLocation(deliveryDemandEntity.DestinationUsername);
            var deliveryDemand = ToDeliveryDemand(deliveryDemandEntity, pickupLocation, destinationLocation);

            if (deliveryDemand == null)
            {
                throw new StorageErrorException($"DeliveryDemand entity with Id {deliveryDemandId} was not found", 404);
            }

            return deliveryDemand;
        }

        public async Task<List<DeliveryDemand>> GetDeliveryDemands(GetDeliveryDemandsRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<Process> GetProcess(int processId)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();

            var sql = new QueryBuilder()
                .SelectColumns("processes", ProcessesTableColumns)
                .Where("Id = @Id")
                .Build();

            var processEntity = await connection.QueryFirstOrDefaultAsync<ProcessEntity>(sql, new { Id = processId });
            var process = ToProcess(processEntity);

            if (process == null)
            {
                throw new StorageErrorException($"Process entity with Id {processId} was not found", 404);
            }

            return process;
        }

        public async Task<List<Campaign>> GetProcesses(int campaignId)
        {
            throw new NotImplementedException();
        }

        private CampaignEntity ToCampaignEntity(Campaign campaign)
        {
            var entity = _mapper.Map<CampaignEntity>(campaign);
            return entity;
        }

        private Campaign ToCampaign(CampaignEntity entity, Location location)
        {
            if (entity == null)
            {
                return null;
            }

            var campaign = _mapper.Map<Campaign>(entity);
            campaign.Location = location;
            return campaign;
        }

        private ContributionEntity ToContributionEntity(Contribution contribution)
        {
            var entity = _mapper.Map<ContributionEntity>(contribution);
            return entity;
        }

        private Contribution ToContribution(ContributionEntity entity, Location location)
        {
            if (entity == null)
            {
                return null;
            }

            var contribution = _mapper.Map<Contribution>(entity);
            contribution.Location = location;
            return contribution;
        }
        private ProcessEntity ToProcessEntity(Process campaign)
        {
            var entity = _mapper.Map<ProcessEntity>(campaign);
            return entity;
        }

        private Process ToProcess(ProcessEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            var process = _mapper.Map<Process>(entity);
            return process;
        }
        private DeliveryDemandEntity ToDeliveryDemandEntity(DeliveryDemand deliveryDemand)
        {
            var entity = _mapper.Map<DeliveryDemandEntity>(deliveryDemand);
            return entity;
        }

        private DeliveryDemand ToDeliveryDemand(DeliveryDemandEntity entity, Location pickupLocation, Location destinationLocation)
        {
            if (entity == null)
            {
                return null;
            }

            var deliveryDemand = _mapper.Map<DeliveryDemand>(entity);
            deliveryDemand.PickupLocation = pickupLocation;
            deliveryDemand.DestinationLocation = destinationLocation;
            return deliveryDemand;
        }

        public async Task<bool> EntityExists(string tableName, int Id)
        {
            using IDbConnection connection = _sqlConnectionFactory.CreateConnection();
            connection.Open();

            var sql = new QueryBuilder()
                .Count(tableName)
                .Where("Id = @Id")
                .Build();

            int count = await connection.QuerySingleAsync<int>(sql, new { Id = Id });
            return count > 0;
        }
    }
}
