using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mainframe.BuyerSupplier.Core.BusinessEntities;
using Mainframe.BuyerSupplier.Data;
using Mainframe.BuyerSupplier.Data.Data_Services;
using Mainframe.BuyerSupplier.Data.DataServices;
using Mainframe.BuyerSupplier.Engine;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Mainframe.BuyerSupplier.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc();
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("InventoryDatabase")));
      
            services.AddScoped<IStandardInventoryBusinessEntity, StandardInventoryBusinessEntity>();
            services.AddScoped<IStandardInventoryDataService, StandardInventoryDataService>();
            services.AddScoped<IUserBusinessEntity, UserBusinessEntity>();
            services.AddScoped<IUserService, UsersService>();
   			services.AddScoped<ISupplierBaseBusinessEntity, SupplierBusinessEntity>();
            services.AddScoped<ISupplierBaseService, SupplierBaseDataService>();
            services.AddScoped<IDeliverySlotsBusinessEntity, DeliverySlotsBusinessEntity>();
            services.AddScoped<IDeliverySlotsDataService, DeliverySlotsDataService>();
            services.AddScoped<ISupplierInventoryBusinessEntity, SupplierInventoryBusinessEntity>();
            services.AddScoped<ISupplierInventoryDataService, SupplierInventoryDataService>();
            services.AddScoped<IInventoryItemSubCategoryService, InventoryItemSubCategoryDataService>();
            services.AddScoped<IInventoryItemSubCategoryBusinessEntity, InventoryItemSubCategoryBusinessEntity>();
            services.AddScoped<ISupplierStandardInventoryService, SupplierStandardInventoryService>();
            services.AddScoped<ISupplierStandardInventoryBusinessEntity, SupplierStandardInventoryBusinessEntity>();
            services.AddScoped<IUnitOfMeasureBusinessEntity, UnitOfMeasureBusinessEntity>();
            services.AddScoped<IUnitOfMeasureDataService, UnitOfMeasureDataService>();
            services.AddScoped<IInventoryItemCategoriesBusinessEntity, InventoryItemCategoriesBusinessEntity>();
            services.AddScoped<IInventoryItemCategoriesDataService, InventoryItemCategoriesDataService>();
            services.AddScoped<ITimeWavesDataService, TimeWavesDataService>();
            services.AddScoped<ITimeWavesBusinessEntity,TimeWavesBusinessEntity>();
            services.AddScoped<IDeliveryCostConfigurationDataService, DeliveryCostConfigurationDataService>();
            services.AddScoped<IDeliveryCostConfigurationBusinessEntity, DeliveryCostConfigurationBusinessEntity>();
            services.AddScoped<IOrderBusinessEntity, OrderBusinessEntity>();
            services.AddScoped<IOrderDataService, OrderDataService>();
            services.AddScoped<IFileServerBusinessEntity, FileServerBusinessEntity>();
            services.AddScoped<IFileServerDataService, FileServerDataService>();
            services.AddScoped<IZoneBusinessEntity, ZoneBusinessEntity>();
            services.AddScoped<IZoneDataService, ZoneDataService>();
            services.AddScoped<IVehicleTypeBusinessEntity, VehicleTypeBusinessEntity>();
            services.AddScoped<IVehicleTypeDataService, VehicleTypeDataService>();
            services.AddScoped<IVehicleBusinessEntity, VehicleBusinessEntity>();
            services.AddScoped<IVehicleDataService, VehicleDataService>();
            services.AddScoped<IWatchListDataService, WatchListDataService>();

            services.AddScoped<IDiscountConfigurationBusinessEntity, DiscountConfigurationBusinessEntity>();
            services.AddScoped<IDiscountConfigurationDataService, DiscountConfigurationDataService>();
            services.AddScoped<ICommissionConfigurationBusinessEntity, CommissionConfigurationBusinessEntity>();
            services.AddScoped<ICommissionConfigurationDataService, CommissionConfigurationDataService>();
            services.AddScoped<IWatchListBusinessEntity, WatchListBusinessEntity>();

            services.AddScoped<IOptimizationEngine, OptimizationEngine>();
            services.AddScoped<IWaveManagement, WaveManagement>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(p =>
             p
             .AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader()
             .AllowCredentials());

              app.UseMvc();
            
        }
    }
}

