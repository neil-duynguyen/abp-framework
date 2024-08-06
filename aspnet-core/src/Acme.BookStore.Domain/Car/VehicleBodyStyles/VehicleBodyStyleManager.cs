using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ImportSample.VehicleBodyStyles
{
    public abstract class VehicleBodyStyleManagerBase : DomainService
    {
        protected IVehicleBodyStyleRepository _vehicleBodyStyleRepository;

        public VehicleBodyStyleManagerBase(IVehicleBodyStyleRepository vehicleBodyStyleRepository)
        {
            _vehicleBodyStyleRepository = vehicleBodyStyleRepository;
        }

        public virtual async Task<VehicleBodyStyle> CreateAsync(
        string enName, string name, string slug, string description, bool isVerified)
        {
            Check.NotNullOrWhiteSpace(enName, nameof(enName));
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(slug, nameof(slug));
            Check.NotNullOrWhiteSpace(description, nameof(description));

            var vehicleBodyStyle = new VehicleBodyStyle(
             GuidGenerator.Create(),
             enName, name, slug, description, isVerified
             );

            return await _vehicleBodyStyleRepository.InsertAsync(vehicleBodyStyle);
        }

        public virtual async Task<VehicleBodyStyle> UpdateAsync(
            Guid id,
            string enName, string name, string slug, string description, bool isVerified, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(enName, nameof(enName));
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(slug, nameof(slug));
            Check.NotNullOrWhiteSpace(description, nameof(description));

            var vehicleBodyStyle = await _vehicleBodyStyleRepository.GetAsync(id);

            vehicleBodyStyle.EnName = enName;
            vehicleBodyStyle.Name = name;
            vehicleBodyStyle.Slug = slug;
            vehicleBodyStyle.Description = description;
            vehicleBodyStyle.IsVerified = isVerified;

            vehicleBodyStyle.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _vehicleBodyStyleRepository.UpdateAsync(vehicleBodyStyle);
        }

    }
}