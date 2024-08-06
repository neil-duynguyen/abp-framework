using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ImportSample.Colors
{
    public abstract class ColorManagerBase : DomainService
    {
        protected IColorRepository _colorRepository;

        public ColorManagerBase(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }

        public virtual async Task<Color> CreateAsync(
        string name, string enName, string description, string slug, string code, bool isVerified)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(enName, nameof(enName));
            Check.NotNullOrWhiteSpace(description, nameof(description));
            Check.NotNullOrWhiteSpace(slug, nameof(slug));
            Check.NotNullOrWhiteSpace(code, nameof(code));

            var color = new Color(
             GuidGenerator.Create(),
             name, enName, description, slug, code, isVerified
             );

            return await _colorRepository.InsertAsync(color);
        }

        public virtual async Task<Color> UpdateAsync(
            Guid id,
            string name, string enName, string description, string slug, string code, bool isVerified, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(enName, nameof(enName));
            Check.NotNullOrWhiteSpace(description, nameof(description));
            Check.NotNullOrWhiteSpace(slug, nameof(slug));
            Check.NotNullOrWhiteSpace(code, nameof(code));

            var color = await _colorRepository.GetAsync(id);

            color.Name = name;
            color.EnName = enName;
            color.Description = description;
            color.Slug = slug;
            color.Code = code;
            color.IsVerified = isVerified;

            color.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _colorRepository.UpdateAsync(color);
        }

    }
}