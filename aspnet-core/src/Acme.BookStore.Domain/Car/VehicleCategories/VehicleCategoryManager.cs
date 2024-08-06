using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ImportSample.VehicleCategories
{
    public abstract class VehicleCategoryManagerBase : DomainService
    {
        protected IVehicleCategoryRepository _vehicleCategoryRepository;

        public VehicleCategoryManagerBase(IVehicleCategoryRepository vehicleCategoryRepository)
        {
            _vehicleCategoryRepository = vehicleCategoryRepository;
        }

        public virtual async Task<VehicleCategory> CreateAsync(
        string name, string description, string slug, bool isVerified)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(description, nameof(description));
            Check.NotNullOrWhiteSpace(slug, nameof(slug));

            var vehicleCategory = new VehicleCategory(
             GuidGenerator.Create(),
             name, description, slug, isVerified
             );

            return await _vehicleCategoryRepository.InsertAsync(vehicleCategory);
        }

        public virtual async Task<VehicleCategory> UpdateAsync(
            Guid id,
            string name, string description, string slug, bool isVerified, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(description, nameof(description));
            Check.NotNullOrWhiteSpace(slug, nameof(slug));

            var vehicleCategory = await _vehicleCategoryRepository.GetAsync(id);

            vehicleCategory.Name = name;
            vehicleCategory.Description = description;
            vehicleCategory.Slug = slug;
            vehicleCategory.IsVerified = isVerified;

            vehicleCategory.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _vehicleCategoryRepository.UpdateAsync(vehicleCategory);
        }

    }
}