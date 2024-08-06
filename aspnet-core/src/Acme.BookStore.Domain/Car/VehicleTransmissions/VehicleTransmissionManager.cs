using ImportSample.VehicleTransmissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ImportSample.VehicleTransmissions
{
    public abstract class VehicleTransmissionManagerBase : DomainService
    {
        protected IVehicleTransmissionRepository _vehicleTransmissionRepository;

        public VehicleTransmissionManagerBase(IVehicleTransmissionRepository vehicleTransmissionRepository)
        {
            _vehicleTransmissionRepository = vehicleTransmissionRepository;
        }

        public virtual async Task<VehicleTransmission> CreateAsync(
        string label, int speeds, string description, string slug, VehicleTransmissionType type, bool isVerified)
        {
            Check.NotNullOrWhiteSpace(label, nameof(label));
            Check.NotNullOrWhiteSpace(description, nameof(description));
            Check.NotNullOrWhiteSpace(slug, nameof(slug));
            Check.NotNull(type, nameof(type));

            var vehicleTransmission = new VehicleTransmission(
             GuidGenerator.Create(),
             label, speeds, description, slug, type, isVerified
             );

            return await _vehicleTransmissionRepository.InsertAsync(vehicleTransmission);
        }

        public virtual async Task<VehicleTransmission> UpdateAsync(
            Guid id,
            string label, int speeds, string description, string slug, VehicleTransmissionType type, bool isVerified, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(label, nameof(label));
            Check.NotNullOrWhiteSpace(description, nameof(description));
            Check.NotNullOrWhiteSpace(slug, nameof(slug));
            Check.NotNull(type, nameof(type));

            var vehicleTransmission = await _vehicleTransmissionRepository.GetAsync(id);

            vehicleTransmission.Label = label;
            vehicleTransmission.Speeds = speeds;
            vehicleTransmission.Description = description;
            vehicleTransmission.Slug = slug;
            vehicleTransmission.Type = type;
            vehicleTransmission.IsVerified = isVerified;

            vehicleTransmission.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _vehicleTransmissionRepository.UpdateAsync(vehicleTransmission);
        }

    }
}