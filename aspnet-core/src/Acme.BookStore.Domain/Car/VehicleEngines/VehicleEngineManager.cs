using ImportSample.VehicleEngines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ImportSample.VehicleEngines
{
    public abstract class VehicleEngineManagerBase : DomainService
    {
        protected IVehicleEngineRepository _vehicleEngineRepository;

        public VehicleEngineManagerBase(IVehicleEngineRepository vehicleEngineRepository)
        {
            _vehicleEngineRepository = vehicleEngineRepository;
        }

        public virtual async Task<VehicleEngine> CreateAsync(
        Guid? vehicleBrandId, string label, int horsePower, int torque, string slug, string description, VehicleEngineType type, bool isVerified)
        {
            Check.NotNullOrWhiteSpace(label, nameof(label));
            Check.NotNullOrWhiteSpace(slug, nameof(slug));
            Check.NotNullOrWhiteSpace(description, nameof(description));
            Check.NotNull(type, nameof(type));

            var vehicleEngine = new VehicleEngine(
             GuidGenerator.Create(),
             vehicleBrandId, label, horsePower, torque, slug, description, type, isVerified
             );

            return await _vehicleEngineRepository.InsertAsync(vehicleEngine);
        }

        public virtual async Task<VehicleEngine> UpdateAsync(
            Guid id,
            Guid? vehicleBrandId, string label, int horsePower, int torque, string slug, string description, VehicleEngineType type, bool isVerified, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(label, nameof(label));
            Check.NotNullOrWhiteSpace(slug, nameof(slug));
            Check.NotNullOrWhiteSpace(description, nameof(description));
            Check.NotNull(type, nameof(type));

            var vehicleEngine = await _vehicleEngineRepository.GetAsync(id);

            vehicleEngine.VehicleBrandId = vehicleBrandId;
            vehicleEngine.Label = label;
            vehicleEngine.HorsePower = horsePower;
            vehicleEngine.Torque = torque;
            vehicleEngine.Slug = slug;
            vehicleEngine.Description = description;
            vehicleEngine.Type = type;
            vehicleEngine.IsVerified = isVerified;

            vehicleEngine.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _vehicleEngineRepository.UpdateAsync(vehicleEngine);
        }

    }
}