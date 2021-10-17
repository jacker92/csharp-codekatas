using AutoMapper;
using HeavyMetalBakeSale.Domain.Models;
using HeavyMetalBakeSale.Domain.Models.DTO;
using HeavyMetalBakeSale.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeavyMetalBakeSale.Domain.Services
{
    public class PurchaseAssignmentConverterService : IPurchaseAssignmentConverterService
    {
        private ICatalogStockItemRepository _itemRepository;
        private ICatalogItemAbbreviationMappingRepository _catalogItemAbbreviationMappingRepository;
        private IMapper _mapper;

        public PurchaseAssignmentConverterService(ICatalogStockItemRepository itemRepository,
                                                  ICatalogItemAbbreviationMappingRepository catalogItemAbbreviationMappingRepository,
                                                  IMapper mapper)
        {
            _itemRepository = itemRepository ?? throw new ArgumentNullException(nameof(itemRepository));
            _catalogItemAbbreviationMappingRepository = catalogItemAbbreviationMappingRepository ?? throw new ArgumentNullException(nameof(catalogItemAbbreviationMappingRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public IList<PurchaseAssignment> ConvertItems(IList<ParsedSalesTotalCalculationRequestResult> items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            var stockItems = _itemRepository.Get();

            return items.Select(x =>
            {
                var mapping = _catalogItemAbbreviationMappingRepository.Get(x.Abbreviation);
                var item = _itemRepository.Get(mapping.CatalogStockItemID);

                return new PurchaseAssignment
                {
                    CatalogItem = _mapper.Map<CatalogStockItem, CatalogStockItemDTO>(item),
                    AmountToPurchase = x.Amount
                };
            }).ToList();

        }
    }
}
