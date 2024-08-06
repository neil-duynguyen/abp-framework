using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ImportSample.ProvinceCities
{
    public abstract class ProvinceCityManagerBase : DomainService
    {
        protected IProvinceCityRepository _provinceCityRepository;

        public ProvinceCityManagerBase(IProvinceCityRepository provinceCityRepository)
        {
            _provinceCityRepository = provinceCityRepository;
        }

        public virtual async Task<ProvinceCity> CreateAsync(
        string name, string slug, string description)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(slug, nameof(slug));
            Check.NotNullOrWhiteSpace(description, nameof(description));

            var provinceCity = new ProvinceCity(
             GuidGenerator.Create(),
             name, slug, description
             );

            return await _provinceCityRepository.InsertAsync(provinceCity);
        }

        public virtual async Task<ProvinceCity> UpdateAsync(
            Guid id,
            string name, string slug, string description, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(slug, nameof(slug));
            Check.NotNullOrWhiteSpace(description, nameof(description));

            var provinceCity = await _provinceCityRepository.GetAsync(id);

            provinceCity.Name = name;
            provinceCity.Slug = slug;
            provinceCity.Description = description;

            provinceCity.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _provinceCityRepository.UpdateAsync(provinceCity);
        }

    }
}