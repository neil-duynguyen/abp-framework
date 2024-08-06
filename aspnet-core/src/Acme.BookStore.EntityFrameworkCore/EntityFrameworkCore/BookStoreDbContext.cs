using ImportSample;
using ImportSample.Colors;
using ImportSample.ColorTranslations;
using ImportSample.Districts;
using ImportSample.ProvinceCities;
using ImportSample.VehicleBodyStyles;
using ImportSample.VehicleBrands;
using ImportSample.VehicleCategories;
using ImportSample.VehicleDriveTrains;
using ImportSample.VehicleEngines;
using ImportSample.VehicleGalleryItems;
using ImportSample.VehicleModels;
using ImportSample.VehicleModelStyles;
using ImportSample.VehicleTransmissions;
using ImportSample.VehicleYearModels;
using ImportSample.Wards;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Acme.BookStore.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class BookStoreDbContext :
    AbpDbContext<BookStoreDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }
    public DbSet<IdentitySession> Sessions { get; set; }
    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    public DbSet<VehicleGalleryItem> VehicleGalleryItems { get; set; } = null!;
    public DbSet<VehicleModelStyle> VehicleModelStyles { get; set; } = null!;
    public DbSet<VehicleYearModel> VehicleYearModels { get; set; } = null!;
    public DbSet<VehicleTransmission> VehicleTransmissions { get; set; } = null!;
    public DbSet<VehicleModel> VehicleModels { get; set; } = null!;
    public DbSet<VehicleEngine> VehicleEngines { get; set; } = null!;
    public DbSet<VehicleDriveTrain> VehicleDriveTrains { get; set; } = null!;
    public DbSet<VehicleCategory> VehicleCategories { get; set; } = null!;
    public DbSet<VehicleBrand> VehicleBrands { get; set; } = null!;
    public DbSet<VehicleBodyStyle> VehicleBodyStyles { get; set; } = null!;
    public DbSet<Ward> Wards { get; set; } = null!;
    public DbSet<District> Districts { get; set; } = null!;
    public DbSet<ProvinceCity> ProvinceCities { get; set; } = null!;
    public DbSet<ColorTranslation> ColorTranslations { get; set; } = null!;
    public DbSet<Color> Colors { get; set; } = null!;

    #endregion

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(BookStoreConsts.DbTablePrefix + "YourEntities", BookStoreConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
        builder.Entity<Book>(b => {
            b.ToTable("Books");
        });

        if (builder.IsHostDatabase())
        {
            builder.Entity<Color>(b =>
            {
                b.ToTable(ImportSampleConsts.DbTablePrefix + "Colors", ImportSampleConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(Color.Name)).IsRequired();
                b.Property(x => x.EnName).HasColumnName(nameof(Color.EnName)).IsRequired();
                b.Property(x => x.Description).HasColumnName(nameof(Color.Description)).IsRequired();
                b.Property(x => x.Slug).HasColumnName(nameof(Color.Slug)).IsRequired();
                b.Property(x => x.Code).HasColumnName(nameof(Color.Code)).IsRequired();
                b.Property(x => x.IsVerified).HasColumnName(nameof(Color.IsVerified));
            });

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<ColorTranslation>(b =>
            {
                b.ToTable(ImportSampleConsts.DbTablePrefix + "ColorTranslations", ImportSampleConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Language).HasColumnName(nameof(ColorTranslation.Language)).IsRequired();
                b.Property(x => x.Name).HasColumnName(nameof(ColorTranslation.Name)).IsRequired();
                b.HasOne<Color>().WithMany().IsRequired().HasForeignKey(x => x.ColorId).OnDelete(DeleteBehavior.NoAction);
            });

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<ProvinceCity>(b =>
            {
                b.ToTable(ImportSampleConsts.DbTablePrefix + "ProvinceCities", ImportSampleConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(ProvinceCity.Name)).IsRequired();
                b.Property(x => x.Slug).HasColumnName(nameof(ProvinceCity.Slug)).IsRequired();
                b.Property(x => x.Description).HasColumnName(nameof(ProvinceCity.Description)).IsRequired();
            });

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<District>(b =>
            {
                b.ToTable(ImportSampleConsts.DbTablePrefix + "Districts", ImportSampleConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(District.Name)).IsRequired();
                b.Property(x => x.Slug).HasColumnName(nameof(District.Slug)).IsRequired();
                b.Property(x => x.Description).HasColumnName(nameof(District.Description)).IsRequired();
                b.HasOne<ProvinceCity>().WithMany().IsRequired().HasForeignKey(x => x.ProvinceCityId).OnDelete(DeleteBehavior.NoAction);
            });

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<Ward>(b =>
            {
                b.ToTable(ImportSampleConsts.DbTablePrefix + "Wards", ImportSampleConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(Ward.Name)).IsRequired();
                b.Property(x => x.Description).HasColumnName(nameof(Ward.Description)).IsRequired();
                b.Property(x => x.Slug).HasColumnName(nameof(Ward.Slug)).IsRequired();
                b.HasOne<District>().WithMany().IsRequired().HasForeignKey(x => x.DistrictId).OnDelete(DeleteBehavior.NoAction);
            });

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<VehicleBodyStyle>(b =>
            {
                b.ToTable(ImportSampleConsts.DbTablePrefix + "VehicleBodyStyles", ImportSampleConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.EnName).HasColumnName(nameof(VehicleBodyStyle.EnName)).IsRequired();
                b.Property(x => x.Name).HasColumnName(nameof(VehicleBodyStyle.Name)).IsRequired();
                b.Property(x => x.Slug).HasColumnName(nameof(VehicleBodyStyle.Slug)).IsRequired();
                b.Property(x => x.Description).HasColumnName(nameof(VehicleBodyStyle.Description)).IsRequired();
                b.Property(x => x.IsVerified).HasColumnName(nameof(VehicleBodyStyle.IsVerified));
            });

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<VehicleBrand>(b =>
            {
                b.ToTable(ImportSampleConsts.DbTablePrefix + "VehicleBrands", ImportSampleConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(VehicleBrand.Name)).IsRequired();
                b.Property(x => x.Slug).HasColumnName(nameof(VehicleBrand.Slug)).IsRequired();
                b.Property(x => x.Description).HasColumnName(nameof(VehicleBrand.Description)).IsRequired();
                b.Property(x => x.IsVerified).HasColumnName(nameof(VehicleBrand.IsVerified));
            });

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<VehicleCategory>(b =>
            {
                b.ToTable(ImportSampleConsts.DbTablePrefix + "VehicleCategories", ImportSampleConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(VehicleCategory.Name)).IsRequired();
                b.Property(x => x.Description).HasColumnName(nameof(VehicleCategory.Description)).IsRequired();
                b.Property(x => x.Slug).HasColumnName(nameof(VehicleCategory.Slug)).IsRequired();
                b.Property(x => x.IsVerified).HasColumnName(nameof(VehicleCategory.IsVerified));
            });

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<VehicleDriveTrain>(b =>
            {
                b.ToTable(ImportSampleConsts.DbTablePrefix + "VehicleDriveTrains", ImportSampleConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.EnName).HasColumnName(nameof(VehicleDriveTrain.EnName)).IsRequired();
                b.Property(x => x.Name).HasColumnName(nameof(VehicleDriveTrain.Name)).IsRequired();
                b.Property(x => x.Slug).HasColumnName(nameof(VehicleDriveTrain.Slug)).IsRequired();
                b.Property(x => x.Description).HasColumnName(nameof(VehicleDriveTrain.Description)).IsRequired();
                b.Property(x => x.IsVerified).HasColumnName(nameof(VehicleDriveTrain.IsVerified));
            });

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<VehicleEngine>(b =>
            {
                b.ToTable(ImportSampleConsts.DbTablePrefix + "VehicleEngines", ImportSampleConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Label).HasColumnName(nameof(VehicleEngine.Label)).IsRequired();
                b.Property(x => x.HorsePower).HasColumnName(nameof(VehicleEngine.HorsePower));
                b.Property(x => x.Torque).HasColumnName(nameof(VehicleEngine.Torque));
                b.Property(x => x.Slug).HasColumnName(nameof(VehicleEngine.Slug)).IsRequired();
                b.Property(x => x.Description).HasColumnName(nameof(VehicleEngine.Description)).IsRequired();
                b.Property(x => x.Type).HasColumnName(nameof(VehicleEngine.Type));
                b.Property(x => x.IsVerified).HasColumnName(nameof(VehicleEngine.IsVerified));
                b.HasOne<VehicleBrand>().WithMany().HasForeignKey(x => x.VehicleBrandId).OnDelete(DeleteBehavior.SetNull);
            });

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<VehicleModel>(b =>
            {
                b.ToTable(ImportSampleConsts.DbTablePrefix + "VehicleModels", ImportSampleConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(VehicleModel.Name)).IsRequired();
                b.Property(x => x.Description).HasColumnName(nameof(VehicleModel.Description)).IsRequired();
                b.Property(x => x.Slug).HasColumnName(nameof(VehicleModel.Slug)).IsRequired();
                b.Property(x => x.IsVerified).HasColumnName(nameof(VehicleModel.IsVerified));
                b.HasOne<VehicleBrand>().WithMany().IsRequired().HasForeignKey(x => x.VehicleBrandId).OnDelete(DeleteBehavior.NoAction);
            });

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<VehicleTransmission>(b =>
            {
                b.ToTable(ImportSampleConsts.DbTablePrefix + "VehicleTransmissions", ImportSampleConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Label).HasColumnName(nameof(VehicleTransmission.Label)).IsRequired();
                b.Property(x => x.Speeds).HasColumnName(nameof(VehicleTransmission.Speeds));
                b.Property(x => x.Description).HasColumnName(nameof(VehicleTransmission.Description)).IsRequired();
                b.Property(x => x.Slug).HasColumnName(nameof(VehicleTransmission.Slug)).IsRequired();
                b.Property(x => x.Type).HasColumnName(nameof(VehicleTransmission.Type));
                b.Property(x => x.IsVerified).HasColumnName(nameof(VehicleTransmission.IsVerified));
            });

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<VehicleYearModel>(b =>
            {
                b.ToTable(ImportSampleConsts.DbTablePrefix + "VehicleYearModels", ImportSampleConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(VehicleYearModel.Name)).IsRequired();
                b.Property(x => x.Slug).HasColumnName(nameof(VehicleYearModel.Slug)).IsRequired();
                b.Property(x => x.Description).HasColumnName(nameof(VehicleYearModel.Description)).IsRequired();
                b.Property(x => x.Year).HasColumnName(nameof(VehicleYearModel.Year));
                b.Property(x => x.IsVerified).HasColumnName(nameof(VehicleYearModel.IsVerified));
                b.HasOne<VehicleModel>().WithMany().IsRequired().HasForeignKey(x => x.VehicleModelId).OnDelete(DeleteBehavior.NoAction);
            });

        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<VehicleModelStyle>(b =>
            {
                b.ToTable(ImportSampleConsts.DbTablePrefix + "VehicleModelStyles", ImportSampleConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Name).HasColumnName(nameof(VehicleModelStyle.Name)).IsRequired();
                b.Property(x => x.Description).HasColumnName(nameof(VehicleModelStyle.Description)).IsRequired();
                b.Property(x => x.Slug).HasColumnName(nameof(VehicleModelStyle.Slug)).IsRequired();
                b.Property(x => x.Highlight).HasColumnName(nameof(VehicleModelStyle.Highlight));
                b.Property(x => x.Capacity).HasColumnName(nameof(VehicleModelStyle.Capacity));
                b.Property(x => x.OperatingRange).HasColumnName(nameof(VehicleModelStyle.OperatingRange));
                b.Property(x => x.CombinedKm).HasColumnName(nameof(VehicleModelStyle.CombinedKm));
                b.Property(x => x.CityKm).HasColumnName(nameof(VehicleModelStyle.CityKm));
                b.Property(x => x.HighwayKm).HasColumnName(nameof(VehicleModelStyle.HighwayKm));
                b.Property(x => x.BatteryType).HasColumnName(nameof(VehicleModelStyle.BatteryType));
                b.Property(x => x.StandardCharging).HasColumnName(nameof(VehicleModelStyle.StandardCharging));
                b.Property(x => x.RapidCharging).HasColumnName(nameof(VehicleModelStyle.RapidCharging));
                b.Property(x => x.SafetyRate).HasColumnName(nameof(VehicleModelStyle.SafetyRate));
                b.Property(x => x.Seats).HasColumnName(nameof(VehicleModelStyle.Seats));
                b.Property(x => x.Doors).HasColumnName(nameof(VehicleModelStyle.Doors));
                b.Property(x => x.StandardFeature).HasColumnName(nameof(VehicleModelStyle.StandardFeature));
                b.Property(x => x.TechnicalFeature).HasColumnName(nameof(VehicleModelStyle.TechnicalFeature));
                b.Property(x => x.MadeIn).HasColumnName(nameof(VehicleModelStyle.MadeIn)).IsRequired();
                b.Property(x => x.Status).HasColumnName(nameof(VehicleModelStyle.Status));
                b.HasOne<VehicleYearModel>().WithMany().IsRequired().HasForeignKey(x => x.VehicleYearModelId).OnDelete(DeleteBehavior.NoAction);
                b.HasOne<VehicleCategory>().WithMany().HasForeignKey(x => x.VehicleCategoryId).OnDelete(DeleteBehavior.SetNull);
                b.HasOne<VehicleBodyStyle>().WithMany().HasForeignKey(x => x.VehicleBodyStyleId).OnDelete(DeleteBehavior.SetNull);
                b.HasOne<VehicleEngine>().WithMany().HasForeignKey(x => x.VehicleEngineId).OnDelete(DeleteBehavior.SetNull);
                b.HasOne<VehicleTransmission>().WithMany().HasForeignKey(x => x.VehicleTransmissionId).OnDelete(DeleteBehavior.SetNull);
                b.HasOne<VehicleDriveTrain>().WithMany().HasForeignKey(x => x.VehicleDriveTrainId).OnDelete(DeleteBehavior.SetNull);
                b.HasMany(x => x.Colors).WithOne().HasForeignKey(x => x.VehicleModelStyleId).IsRequired().OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<VehicleModelStyleColor>(b =>
            {
                b.ToTable(ImportSampleConsts.DbTablePrefix + "VehicleModelStyleColor", ImportSampleConsts.DbSchema);
                b.ConfigureByConvention();

                b.HasKey(
                    x => new { x.VehicleModelStyleId, x.ColorId }
                );

                b.HasOne<VehicleModelStyle>().WithMany(x => x.Colors).HasForeignKey(x => x.VehicleModelStyleId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                b.HasOne<Color>().WithMany().HasForeignKey(x => x.ColorId).IsRequired().OnDelete(DeleteBehavior.Cascade);

                b.HasIndex(
                        x => new { x.VehicleModelStyleId, x.ColorId }
                );
            });
        }
        if (builder.IsHostDatabase())
        {
            builder.Entity<VehicleGalleryItem>(b =>
            {
                b.ToTable(ImportSampleConsts.DbTablePrefix + "VehicleGalleryItems", ImportSampleConsts.DbSchema);
                b.ConfigureByConvention();
                b.Property(x => x.Order).HasColumnName(nameof(VehicleGalleryItem.Order));
                b.Property(x => x.AssetPath).HasColumnName(nameof(VehicleGalleryItem.AssetPath)).IsRequired();
                b.Property(x => x.Type).HasColumnName(nameof(VehicleGalleryItem.Type));
                b.HasMany(x => x.VehicleBrands).WithOne().HasForeignKey(x => x.VehicleGalleryItemId).IsRequired().OnDelete(DeleteBehavior.NoAction);
                b.HasMany(x => x.VehicleModels).WithOne().HasForeignKey(x => x.VehicleGalleryItemId).IsRequired().OnDelete(DeleteBehavior.NoAction);
                b.HasMany(x => x.VehicleYearModels).WithOne().HasForeignKey(x => x.VehicleGalleryItemId).IsRequired().OnDelete(DeleteBehavior.NoAction);
                b.HasMany(x => x.VehicleModelStyles).WithOne().HasForeignKey(x => x.VehicleGalleryItemId).IsRequired().OnDelete(DeleteBehavior.NoAction);
                b.HasMany(x => x.VehicleBodyStyles).WithOne().HasForeignKey(x => x.VehicleGalleryItemId).IsRequired().OnDelete(DeleteBehavior.NoAction);
            });

            builder.Entity<VehicleGalleryItemVehicleBrand>(b =>
            {
                b.ToTable(ImportSampleConsts.DbTablePrefix + "VehicleGalleryItemVehicleBrand", ImportSampleConsts.DbSchema);
                b.ConfigureByConvention();

                b.HasKey(
                    x => new { x.VehicleGalleryItemId, x.VehicleBrandId }
                );

                b.HasOne<VehicleGalleryItem>().WithMany(x => x.VehicleBrands).HasForeignKey(x => x.VehicleGalleryItemId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                b.HasOne<VehicleBrand>().WithMany().HasForeignKey(x => x.VehicleBrandId).IsRequired().OnDelete(DeleteBehavior.Cascade);

                b.HasIndex(
                        x => new { x.VehicleGalleryItemId, x.VehicleBrandId }
                );
            });
            builder.Entity<VehicleGalleryItemVehicleModel>(b =>
            {
                b.ToTable(ImportSampleConsts.DbTablePrefix + "VehicleGalleryItemVehicleModel", ImportSampleConsts.DbSchema);
                b.ConfigureByConvention();

                b.HasKey(
                    x => new { x.VehicleGalleryItemId, x.VehicleModelId }
                );

                b.HasOne<VehicleGalleryItem>().WithMany(x => x.VehicleModels).HasForeignKey(x => x.VehicleGalleryItemId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                b.HasOne<VehicleModel>().WithMany().HasForeignKey(x => x.VehicleModelId).IsRequired().OnDelete(DeleteBehavior.Cascade);

                b.HasIndex(
                        x => new { x.VehicleGalleryItemId, x.VehicleModelId }
                );
            });
            builder.Entity<VehicleGalleryItemVehicleYearModel>(b =>
            {
                b.ToTable(ImportSampleConsts.DbTablePrefix + "VehicleGalleryItemVehicleYearModel", ImportSampleConsts.DbSchema);
                b.ConfigureByConvention();

                b.HasKey(
                    x => new { x.VehicleGalleryItemId, x.VehicleYearModelId }
                );

                b.HasOne<VehicleGalleryItem>().WithMany(x => x.VehicleYearModels).HasForeignKey(x => x.VehicleGalleryItemId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                b.HasOne<VehicleYearModel>().WithMany().HasForeignKey(x => x.VehicleYearModelId).IsRequired().OnDelete(DeleteBehavior.Cascade);

                b.HasIndex(
                        x => new { x.VehicleGalleryItemId, x.VehicleYearModelId }
                );
            });
            builder.Entity<VehicleGalleryItemVehicleModelStyle>(b =>
            {
                b.ToTable(ImportSampleConsts.DbTablePrefix + "VehicleGalleryItemVehicleModelStyle", ImportSampleConsts.DbSchema);
                b.ConfigureByConvention();

                b.HasKey(
                    x => new { x.VehicleGalleryItemId, x.VehicleModelStyleId }
                );

                b.HasOne<VehicleGalleryItem>().WithMany(x => x.VehicleModelStyles).HasForeignKey(x => x.VehicleGalleryItemId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                b.HasOne<VehicleModelStyle>().WithMany().HasForeignKey(x => x.VehicleModelStyleId).IsRequired().OnDelete(DeleteBehavior.Cascade);

                b.HasIndex(
                        x => new { x.VehicleGalleryItemId, x.VehicleModelStyleId }
                );
            });
            builder.Entity<VehicleGalleryItemVehicleBodyStyle>(b =>
            {
                b.ToTable(ImportSampleConsts.DbTablePrefix + "VehicleGalleryItemVehicleBodyStyle", ImportSampleConsts.DbSchema);
                b.ConfigureByConvention();

                b.HasKey(
                    x => new { x.VehicleGalleryItemId, x.VehicleBodyStyleId }
                );

                b.HasOne<VehicleGalleryItem>().WithMany(x => x.VehicleBodyStyles).HasForeignKey(x => x.VehicleGalleryItemId).IsRequired().OnDelete(DeleteBehavior.Cascade);
                b.HasOne<VehicleBodyStyle>().WithMany().HasForeignKey(x => x.VehicleBodyStyleId).IsRequired().OnDelete(DeleteBehavior.Cascade);

                b.HasIndex(
                        x => new { x.VehicleGalleryItemId, x.VehicleBodyStyleId }
                );
            });
        }
    }
}
