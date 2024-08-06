using ImportSample.VehicleGalleryItems;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace ImportSample.VehicleGalleryItems
{
    public partial interface IVehicleGalleryItemRepository : IRepository<VehicleGalleryItem, Guid>
    {
        Task<VehicleGalleryItemWithNavigationProperties> GetWithNavigationPropertiesAsync(
    Guid id,
    CancellationToken cancellationToken = default
);

        Task<List<VehicleGalleryItemWithNavigationProperties>> GetListWithNavigationPropertiesAsync(
            string? filterText = null,
            int? orderMin = null,
            int? orderMax = null,
            string? assetPath = null,
            VehicleGalleryItemType? type = null,
            Guid? vehicleBrandId = null,
            Guid? vehicleModelId = null,
            Guid? vehicleYearModelId = null,
            Guid? vehicleModelStyleId = null,
            Guid? vehicleBodyStyleId = null,
            string? sorting = null,
            int maxResultCount = int.MaxValue,
            int skipCount = 0,
            CancellationToken cancellationToken = default
        );

        Task<List<VehicleGalleryItem>> GetListAsync(
                    string? filterText = null,
                    int? orderMin = null,
                    int? orderMax = null,
                    string? assetPath = null,
                    VehicleGalleryItemType? type = null,
                    string? sorting = null,
                    int maxResultCount = int.MaxValue,
                    int skipCount = 0,
                    CancellationToken cancellationToken = default
                );

        Task<long> GetCountAsync(
            string? filterText = null,
            int? orderMin = null,
            int? orderMax = null,
            string? assetPath = null,
            VehicleGalleryItemType? type = null,
            Guid? vehicleBrandId = null,
            Guid? vehicleModelId = null,
            Guid? vehicleYearModelId = null,
            Guid? vehicleModelStyleId = null,
            Guid? vehicleBodyStyleId = null,
            CancellationToken cancellationToken = default);
    }
}