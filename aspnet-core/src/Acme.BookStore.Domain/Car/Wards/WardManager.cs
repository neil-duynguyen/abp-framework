using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ImportSample.Wards
{
    public abstract class WardManagerBase : DomainService
    {
        protected IWardRepository _wardRepository;

        public WardManagerBase(IWardRepository wardRepository)
        {
            _wardRepository = wardRepository;
        }

        public virtual async Task<Ward> CreateAsync(
        Guid districtId, string name, string description, string slug)
        {
            Check.NotNull(districtId, nameof(districtId));
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(description, nameof(description));
            Check.NotNullOrWhiteSpace(slug, nameof(slug));

            var ward = new Ward(
             GuidGenerator.Create(),
             districtId, name, description, slug
             );

            return await _wardRepository.InsertAsync(ward);
        }

        public virtual async Task<Ward> UpdateAsync(
            Guid id,
            Guid districtId, string name, string description, string slug, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNull(districtId, nameof(districtId));
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(description, nameof(description));
            Check.NotNullOrWhiteSpace(slug, nameof(slug));

            var ward = await _wardRepository.GetAsync(id);

            ward.DistrictId = districtId;
            ward.Name = name;
            ward.Description = description;
            ward.Slug = slug;

            ward.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _wardRepository.UpdateAsync(ward);
        }

    }
}