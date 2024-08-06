using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ImportSample.VehicleModels
{
    public abstract class VehicleModelManagerBase : DomainService
    {
        protected IVehicleModelRepository _vehicleModelRepository;

        public VehicleModelManagerBase(IVehicleModelRepository vehicleModelRepository)
        {
            _vehicleModelRepository = vehicleModelRepository;
        }

        public virtual async Task<VehicleModel> CreateAsync(
        Guid vehicleBrandId, string name, string description, string slug, bool isVerified)
        {
            Check.NotNull(vehicleBrandId, nameof(vehicleBrandId));
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(description, nameof(description));
            Check.NotNullOrWhiteSpace(slug, nameof(slug));

            var vehicleModel = new VehicleModel(
             GuidGenerator.Create(),
             vehicleBrandId, name, description, slug, isVerified
             );

            return await _vehicleModelRepository.InsertAsync(vehicleModel);
        }

        public virtual async Task<VehicleModel> UpdateAsync(
            Guid id,
            Guid vehicleBrandId, string name, string description, string slug, bool isVerified, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNull(vehicleBrandId, nameof(vehicleBrandId));
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(description, nameof(description));
            Check.NotNullOrWhiteSpace(slug, nameof(slug));

            var vehicleModel = await _vehicleModelRepository.GetAsync(id);

            vehicleModel.VehicleBrandId = vehicleBrandId;
            vehicleModel.Name = name;
            vehicleModel.Description = description;
            vehicleModel.Slug = slug;
            vehicleModel.IsVerified = isVerified;

            vehicleModel.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _vehicleModelRepository.UpdateAsync(vehicleModel);
        }

    }
}