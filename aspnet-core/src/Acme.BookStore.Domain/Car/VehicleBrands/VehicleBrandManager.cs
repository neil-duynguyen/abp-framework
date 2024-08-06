using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ImportSample.VehicleBrands
{
    public abstract class VehicleBrandManagerBase : DomainService
    {
        protected IVehicleBrandRepository _vehicleBrandRepository;

        public VehicleBrandManagerBase(IVehicleBrandRepository vehicleBrandRepository)
        {
            _vehicleBrandRepository = vehicleBrandRepository;
        }

        public virtual async Task<VehicleBrand> CreateAsync(
        string name, string slug, string description, bool isVerified)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(slug, nameof(slug));
            Check.NotNullOrWhiteSpace(description, nameof(description));

            var vehicleBrand = new VehicleBrand(
             GuidGenerator.Create(),
             name, slug, description, isVerified
             );

            return await _vehicleBrandRepository.InsertAsync(vehicleBrand);
        }

        public virtual async Task<VehicleBrand> UpdateAsync(
            Guid id,
            string name, string slug, string description, bool isVerified, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(slug, nameof(slug));
            Check.NotNullOrWhiteSpace(description, nameof(description));

            var vehicleBrand = await _vehicleBrandRepository.GetAsync(id);

            vehicleBrand.Name = name;
            vehicleBrand.Slug = slug;
            vehicleBrand.Description = description;
            vehicleBrand.IsVerified = isVerified;

            vehicleBrand.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _vehicleBrandRepository.UpdateAsync(vehicleBrand);
        }

    }
}