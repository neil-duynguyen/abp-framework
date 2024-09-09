/*using Acme.BookStore.CarDto;
using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using ImportSample.VehicleBrands;
using ImportSample.VehicleCategories;
using ImportSample.VehicleEngines;
using ImportSample.VehicleModels;
using ImportSample.VehicleModelStyles;
using ImportSample.VehicleYearModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using static Acme.BookStore.DataMap;

namespace Acme.BookStore
{
    public class CarServices : ApplicationService
    {
        private readonly IRepository<ImportSample.Colors.Color, Guid> _colorRepository;
        private readonly IRepository<VehicleModelStyle, Guid> _vehicleModeStyleRepository;
        private readonly IRepository<VehicleBrand, Guid> _vehicleBrandRepository;
        private readonly IRepository<VehicleModel, Guid> _vehicleModelRepository;
        private readonly IRepository<VehicleYearModel, Guid> _vehicleYearModelRepository;
        private readonly IRepository<VehicleCategory, Guid> _vehicleCategoryRepository;
        private readonly IRepository<VehicleEngine, Guid> _vehicleEngineRepository;
        private readonly IMapper _mapper;

        public CarServices(IRepository<ImportSample.Colors.Color, Guid> colorRepository, IRepository<VehicleModelStyle, Guid> vehicleModeStyleRepository, IRepository<VehicleBrand, Guid> vehicleBrandRepository, IRepository<VehicleModel, Guid> vehicleModelRepository, IRepository<VehicleYearModel, Guid> vehicleYearModelRepository, IRepository<VehicleCategory, Guid> vehicleCategoryRepository, IRepository<VehicleEngine, Guid> vehicleEngineRepository, IMapper mapper)
        {
            _colorRepository = colorRepository;
            _vehicleModeStyleRepository = vehicleModeStyleRepository;
            _vehicleBrandRepository = vehicleBrandRepository;
            _vehicleModelRepository = vehicleModelRepository;
            _vehicleYearModelRepository = vehicleYearModelRepository;
            _vehicleCategoryRepository = vehicleCategoryRepository;
            _vehicleEngineRepository = vehicleEngineRepository;
            _mapper = mapper;
        }

        public async Task<ColorViewDto> CreateColor(ColorViewDto colorViewDto)
        {
            var insert = await _colorRepository.InsertAsync(ObjectMapper.Map<ColorViewDto, ImportSample.Colors.Color>(colorViewDto));

            return ObjectMapper.Map<ImportSample.Colors.Color, ColorViewDto>(insert);
        }

        public async Task<Guid> CreateVehicleModelStyle(VehicleModelStyleDto input)
        {
            var vehicelModelStile = _mapper.Map<VehicleModelStyle>(input);
            var createVehicle = await _vehicleModeStyleRepository.InsertAsync(vehicelModelStile);
            return createVehicle.Id;
        }
        
        public async Task<Guid> CreateVehicleBrand(VehicleBrandDto vehicleBrandDto)
        {
            *//* var existingBrand = await _vehicleBrandRepository.FindAsync(h => h.Name.Equals(vehicleBrandDto.Name));

             if (existingBrand != null)
             {
                 return existingBrand.Id;
             }*//*

            var vehicleBrand = _mapper.Map<VehicleBrand>(vehicleBrandDto);
            
            var createdBrand = await _vehicleBrandRepository.InsertAsync(vehicleBrand, autoSave:true);
            return createdBrand.Id;
        }


        public async Task<Guid> CreateVehicleModel(VehicleModelDto vehicleModelDto)
        {
            var vehicle = new VehicleModelDto(vehicleModelDto.Name, vehicleModelDto.Description, vehicleModelDto.VehicleBrandId);
            var createVehicleModel = await _vehicleModelRepository.InsertAsync(_mapper.Map<VehicleModel>(vehicle));
            return createVehicleModel.Id;
        }

        public async Task CreateVehicleYearModel(VehicleYearModelDto vehicleYearModelDto)
        {
            var vehicle = new VehicleYearModelDto(vehicleYearModelDto.Year, vehicleYearModelDto.VehicleModelId);
            var createVehicleYearMode = await _vehicleYearModelRepository.InsertAsync(_mapper.Map<VehicleYearModel>(vehicle));
        }

        public async Task CreateVehicleCategory(VehicleCategoryDto vehicleCategoryDto)
        {
            var vehicle = new VehicleCategoryDto(vehicleCategoryDto.Name);
            var createVehicleCategory = await _vehicleCategoryRepository.InsertAsync(_mapper.Map<VehicleCategory>(vehicle));
        }

        public async Task CreateVehicleEngine(VehicleEngineDto vehicleEngineDto)
        {
            var vehicle = new VehicleEngineDto(vehicleEngineDto.Description, vehicleEngineDto.VehicleBrandId);
            var createVehicleEngine = await _vehicleEngineRepository.InsertAsync(_mapper.Map<VehicleEngine>(vehicle));
        }

        public string Separator(string input)
        {
            return input.Replace("\"", string.Empty)
                             .Replace("{", string.Empty)
                             .Replace("}", string.Empty);
        }

        public async Task<List<DataVehicle>> ImportFileExcel(IFormFile formFile)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header,
                ShouldSkipRecord = x => x.Row.Parser.Record.All(string.IsNullOrWhiteSpace),
                AllowComments = true,
                TrimOptions = TrimOptions.Trim
            };

            List<DataVehicle> data = new List<DataVehicle>();
            List<string> headers;
            try
            {


                using (var stream = formFile.OpenReadStream())
                using (var reader = new StreamReader(stream, Encoding.UTF8))
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Context.RegisterClassMap<DataVehicleMap>();

                    csv.Read();
                    csv.ReadHeader();
                    headers = csv.HeaderRecord.ToList();

                    int startIndex = headers.FindIndex(h => h.Equals("Loại xe", StringComparison.OrdinalIgnoreCase));

                    while (csv.Read())
                    {
                        var dataVehicle = csv.GetRecord<DataVehicle>();
                        var technicalFeatures = new Dictionary<string, string>();

                        for (int i = startIndex; i < headers.Count; i++)
                        {
                            var columnName = headers[i];
                            var value = csv.GetField(i);
                            if (!string.IsNullOrWhiteSpace(value))
                            {
                                technicalFeatures[columnName] = value;
                            }
                        }

                        var technicalFeature = dataVehicle.TechnicalFeature = JsonConvert.SerializeObject(technicalFeatures);

                        data.Add(dataVehicle); ;

                        //Create VehicleBrand
                        var idVehicleBrand = await CreateVehicleBrand(new VehicleBrandDto() { Name = dataVehicle.NameBrand, Description = dataVehicle.DescriptionBand });

                        //Create VehicleModel
                       *//* var idVehicleModel = await CreateVehicleModel(new VehicleModelDto() { Name = dataVehicle.NameModel, Description = dataVehicle.DescriptionModel, VehicleBrandId = idVehicleBrand });

                        //Create VehicleYearModel
                       // await CreateVehicleYearModel(new VehicleYearModelDto() { Year = dataVehicle.Year, VehicleModelId = idVehicleModel });

                        //Create VehicleCategory
                        await CreateVehicleCategory(new VehicleCategoryDto() { Name = dataVehicle.NameCategory });

                        //Create VehicleEngine
                        await CreateVehicleEngine(new VehicleEngineDto() { Description = dataVehicle.DescriptionEngine, VehicleBrandId = idVehicleBrand });

                        //Create CreateVehicleModelStyle
                        await CreateVehicleModelStyle(new VehicleModelStyleDto() { Name = dataVehicle.NameVersion, Description = dataVehicle.DetailURL, TechnicalFeature = dataVehicle.TechnicalFeature = JsonConvert.SerializeObject(technicalFeatures) });*//*


                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                throw;
            }
            return data;
        }

    }
}*/