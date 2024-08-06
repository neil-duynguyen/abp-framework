using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ImportSample.Districts
{
    public abstract class DistrictManagerBase : DomainService
    {
        protected IDistrictRepository _districtRepository;

        public DistrictManagerBase(IDistrictRepository districtRepository)
        {
            _districtRepository = districtRepository;
        }

        public virtual async Task<District> CreateAsync(
        Guid provinceCityId, string name, string slug, string description)
        {
            Check.NotNull(provinceCityId, nameof(provinceCityId));
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(slug, nameof(slug));
            Check.NotNullOrWhiteSpace(description, nameof(description));

            var district = new District(
             GuidGenerator.Create(),
             provinceCityId, name, slug, description
             );

            return await _districtRepository.InsertAsync(district);
        }

        public virtual async Task<District> UpdateAsync(
            Guid id,
            Guid provinceCityId, string name, string slug, string description, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNull(provinceCityId, nameof(provinceCityId));
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(slug, nameof(slug));
            Check.NotNullOrWhiteSpace(description, nameof(description));

            var district = await _districtRepository.GetAsync(id);

            district.ProvinceCityId = provinceCityId;
            district.Name = name;
            district.Slug = slug;
            district.Description = description;

            district.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _districtRepository.UpdateAsync(district);
        }

    }
}