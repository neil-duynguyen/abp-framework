using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ImportSample.VehicleDriveTrains
{
    public abstract class VehicleDriveTrainManagerBase : DomainService
    {
        protected IVehicleDriveTrainRepository _vehicleDriveTrainRepository;

        public VehicleDriveTrainManagerBase(IVehicleDriveTrainRepository vehicleDriveTrainRepository)
        {
            _vehicleDriveTrainRepository = vehicleDriveTrainRepository;
        }

        public virtual async Task<VehicleDriveTrain> CreateAsync(
        string enName, string name, string slug, string description, bool isVerified)
        {
            Check.NotNullOrWhiteSpace(enName, nameof(enName));
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(slug, nameof(slug));
            Check.NotNullOrWhiteSpace(description, nameof(description));

            var vehicleDriveTrain = new VehicleDriveTrain(
             GuidGenerator.Create(),
             enName, name, slug, description, isVerified
             );

            return await _vehicleDriveTrainRepository.InsertAsync(vehicleDriveTrain);
        }

        public virtual async Task<VehicleDriveTrain> UpdateAsync(
            Guid id,
            string enName, string name, string slug, string description, bool isVerified, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(enName, nameof(enName));
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(slug, nameof(slug));
            Check.NotNullOrWhiteSpace(description, nameof(description));

            var vehicleDriveTrain = await _vehicleDriveTrainRepository.GetAsync(id);

            vehicleDriveTrain.EnName = enName;
            vehicleDriveTrain.Name = name;
            vehicleDriveTrain.Slug = slug;
            vehicleDriveTrain.Description = description;
            vehicleDriveTrain.IsVerified = isVerified;

            vehicleDriveTrain.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _vehicleDriveTrainRepository.UpdateAsync(vehicleDriveTrain);
        }

    }
}