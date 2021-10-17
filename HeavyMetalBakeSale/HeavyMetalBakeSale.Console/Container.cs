using Autofac;
using AutoMapper;
using HeavyMetalBakeSale.Domain.Models;
using HeavyMetalBakeSale.Domain.Models.DTO;
using HeavyMetalBakeSale.Domain.Repositories;
using HeavyMetalBakeSale.Domain.Services;

namespace HeavyMetalBakeSale.Console
{
    public static class Container
    {
        public static IContainer Build()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<SalesCalculatorService>().As<ISalesCalculatorService>();
            builder.RegisterType<SalesCalculatorValidatorService>().As<ISalesCalculatorValidatorService>();
            builder.RegisterType<SalesCalculationRequestParserService>().As<ISalesCalculationRequestParserService>();
            builder.RegisterType<CatalogStockItemRepository>().As<ICatalogStockItemRepository>();
            builder.RegisterType<CatalogItemAbbreviationMappingRepository>().As<ICatalogItemAbbreviationMappingRepository>();
            builder.RegisterType<ItemCatalogService>().As<IItemCatalogService>();
            builder.RegisterType<PurchaseAssignmentConverterService>().As<IPurchaseAssignmentConverterService>();
            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<Display>().As<IDisplay>();
            builder.RegisterType<ItemAmountCalculatorService>().As<IItemAmountCalculatorService>();
            builder.Register(c => CreateMapper()).As<IMapper>();

            return builder.Build();
        }

        private static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CatalogItem, CatalogItemDTO>();
                cfg.CreateMap<CatalogStockItem, CatalogStockItemDTO>();
            });
            return config.CreateMapper();
        }
    }
}
