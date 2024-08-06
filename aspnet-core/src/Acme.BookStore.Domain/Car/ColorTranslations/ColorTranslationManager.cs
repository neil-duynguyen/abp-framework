using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace ImportSample.ColorTranslations
{
    public abstract class ColorTranslationManagerBase : DomainService
    {
        protected IColorTranslationRepository _colorTranslationRepository;

        public ColorTranslationManagerBase(IColorTranslationRepository colorTranslationRepository)
        {
            _colorTranslationRepository = colorTranslationRepository;
        }

        public virtual async Task<ColorTranslation> CreateAsync(
        Guid colorId, string language, string name)
        {
            Check.NotNull(colorId, nameof(colorId));
            Check.NotNullOrWhiteSpace(language, nameof(language));
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var colorTranslation = new ColorTranslation(
             GuidGenerator.Create(),
             colorId, language, name
             );

            return await _colorTranslationRepository.InsertAsync(colorTranslation);
        }

        public virtual async Task<ColorTranslation> UpdateAsync(
            Guid id,
            Guid colorId, string language, string name, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNull(colorId, nameof(colorId));
            Check.NotNullOrWhiteSpace(language, nameof(language));
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var colorTranslation = await _colorTranslationRepository.GetAsync(id);

            colorTranslation.ColorId = colorId;
            colorTranslation.Language = language;
            colorTranslation.Name = name;

            colorTranslation.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _colorTranslationRepository.UpdateAsync(colorTranslation);
        }

    }
}