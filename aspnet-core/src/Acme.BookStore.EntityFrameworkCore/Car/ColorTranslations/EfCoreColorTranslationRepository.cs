using ImportSample.Colors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Acme.BookStore.EntityFrameworkCore;

namespace ImportSample.ColorTranslations
{
    public abstract class EfCoreColorTranslationRepositoryBase : EfCoreRepository<BookStoreDbContext, ColorTranslation, Guid>
    {
        public EfCoreColorTranslationRepositoryBase(IDbContextProvider<BookStoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        public virtual async Task<ColorTranslationWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbContext = await GetDbContextAsync();

            return (await GetDbSetAsync()).Where(b => b.Id == id)
                .Select(colorTranslation => new ColorTranslationWithNavigationProperties
                {
                    ColorTranslation = colorTranslation,
                    Color = dbContext.Set<Color>().FirstOrDefault(c => c.Id == colorTranslation.ColorId)
                }).FirstOrDefault();
        }

        public virtual async Task<List<ColorTranslationWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            string? language = null,
            string? name = null,
            Guid? colorId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, language, name, colorId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ColorTranslationConsts.GetDefaultSorting(true) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        protected virtual async Task<IQueryable<ColorTranslationWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
        {
            return from colorTranslation in (await GetDbSetAsync())
                   join color in (await GetDbContextAsync()).Set<Color>() on colorTranslation.ColorId equals color.Id into colors
                   from color in colors.DefaultIfEmpty()
                   select new ColorTranslationWithNavigationProperties
                   {
                       ColorTranslation = colorTranslation,
                       Color = color
                   };
        }

        protected virtual IQueryable<ColorTranslationWithNavigationProperties> ApplyFilter(
            IQueryable<ColorTranslationWithNavigationProperties> query,
            string? filterText,
            string? language = null,
            string? name = null,
            Guid? colorId = null)
        {
            return query
                .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.ColorTranslation.Language!.Contains(filterText!) || e.ColorTranslation.Name!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(language), e => e.ColorTranslation.Language.Contains(language))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.ColorTranslation.Name.Contains(name))
                    .WhereIf(colorId != null && colorId != Guid.Empty, e => e.Color != null && e.Color.Id == colorId);
        }

        public virtual async Task<List<ColorTranslation>> GetListAsync(
            string? filterText = null,
            string? language = null,
            string? name = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default)
        {
            var query = ApplyFilter((await GetQueryableAsync()), filterText, language, name);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ColorTranslationConsts.GetDefaultSorting(false) : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
        }

        public virtual async Task<long> GetCountAsync(
            string? filterText = null,
            string? language = null,
            string? name = null,
            Guid? colorId = null,
            CancellationToken cancellationToken = default)
        {
            var query = await GetQueryForNavigationPropertiesAsync();
            query = ApplyFilter(query, filterText, language, name, colorId);
            return await query.LongCountAsync(GetCancellationToken(cancellationToken));
        }

        protected virtual IQueryable<ColorTranslation> ApplyFilter(
            IQueryable<ColorTranslation> query,
            string? filterText = null,
            string? language = null,
            string? name = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Language!.Contains(filterText!) || e.Name!.Contains(filterText!))
                    .WhereIf(!string.IsNullOrWhiteSpace(language), e => e.Language.Contains(language))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name));
        }
    }
}