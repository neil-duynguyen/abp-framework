using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ImportSample.VehicleYearModels
{
    public abstract class VehicleYearModelManagerBase : DomainService
    {
        protected IVehicleYearModelRepository _vehicleYearModelRepository;

        public VehicleYearModelManagerBase(IVehicleYearModelRepository vehicleYearModelRepository)
        {
            _vehicleYearModelRepository = vehicleYearModelRepository;
        }

        public virtual async Task<VehicleYearModel> CreateAsync(
        Guid vehicleModelId, string name, string slug, string description, int year, bool isVerified)
        {
            Check.NotNull(vehicleModelId, nameof(vehicleModelId));
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(slug, nameof(slug));
            Check.NotNullOrWhiteSpace(description, nameof(description));

            var vehicleYearModel = new VehicleYearModel(
             GuidGenerator.Create(),
             vehicleModelId, name, slug, description, year, isVerified
             );

            return await _vehicleYearModelRepository.InsertAsync(vehicleYearModel);
        }

        public virtual async Task<VehicleYearModel> UpdateAsync(
            Guid id,
            Guid vehicleModelId, string name, string slug, string description, int year, bool isVerified, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNull(vehicleModelId, nameof(vehicleModelId));
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(slug, nameof(slug));
            Check.NotNullOrWhiteSpace(description, nameof(description));

            var vehicleYearModel = await _vehicleYearModelRepository.GetAsync(id);

            vehicleYearModel.VehicleModelId = vehicleModelId;
            vehicleYearModel.Name = name;
            vehicleYearModel.Slug = slug;
            vehicleYearModel.Description = description;
            vehicleYearModel.Year = year;
            vehicleYearModel.IsVerified = isVerified;

            vehicleYearModel.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _vehicleYearModelRepository.UpdateAsync(vehicleYearModel);
        }

    }
}