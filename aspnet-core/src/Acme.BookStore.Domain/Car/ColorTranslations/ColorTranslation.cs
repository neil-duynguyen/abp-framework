using ImportSample.Colors;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;

using Volo.Abp;

namespace ImportSample.ColorTranslations
{
    public abstract class ColorTranslationBase : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string Language { get; set; }

        [NotNull]
        public virtual string Name { get; set; }
        public Guid ColorId { get; set; }

        protected ColorTranslationBase()
        {

        }

        public ColorTranslationBase(Guid id, Guid colorId, string language, string name)
        {

            Id = id;
            Check.NotNull(language, nameof(language));
            Check.NotNull(name, nameof(name));
            Language = language;
            Name = name;
            ColorId = colorId;
        }

    }
}