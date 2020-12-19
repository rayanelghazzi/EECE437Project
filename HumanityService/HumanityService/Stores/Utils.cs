using AutoMapper;
using HumanityService.DataContracts.CompositeDesignPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HumanityService.Stores
{
    public class Utils
    {
        private readonly IMapper _mapper;

        private Utils()
        {
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

        public CampaignEntity ToCampaignEntity(Campaign campaign)
        {
            var entity = _mapper.Map<CampaignEntity>(campaign);
            return entity;
        }

        public Campaign ToCampaign(CampaignEntity entity, Location location)
        {
            if (entity == null)
            {
                return null;
            }

            var campaign = _mapper.Map<Campaign>(entity);
            campaign.Location = location;
            return campaign;
        }

        public ContributionEntity ToContributionEntity(Contribution contribution)
        {
            var entity = _mapper.Map<ContributionEntity>(contribution);
            return entity;
        }

        public Contribution ToContribution(ContributionEntity entity, Location location)
        {
            if (entity == null)
            {
                return null;
            }

            var contribution = _mapper.Map<Contribution>(entity);
            contribution.Location = location;
            return contribution;
        }
        public ProcessEntity ToProcessEntity(Process campaign)
        {
            var entity = _mapper.Map<ProcessEntity>(campaign);
            return entity;
        }

        public Process ToProcess(ProcessEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            var process = _mapper.Map<Process>(entity);
            return process;
        }
        public DeliveryDemandEntity ToDeliveryDemandEntity(DeliveryDemand deliveryDemand)
        {
            var entity = _mapper.Map<DeliveryDemandEntity>(deliveryDemand);
            return entity;
        }

        public DeliveryDemand ToDeliveryDemand(DeliveryDemandEntity entity, Location pickupLocation, Location destinationLocation)
        {
            if (entity == null)
            {
                return null;
            }

            var deliveryDemand = _mapper.Map<DeliveryDemand>(entity);
            deliveryDemand.PickupLocation = pickupLocation;
            return deliveryDemand;
        }

    }
}
