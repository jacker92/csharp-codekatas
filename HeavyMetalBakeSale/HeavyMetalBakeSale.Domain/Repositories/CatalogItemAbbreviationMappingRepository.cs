using HeavyMetalBakeSale.Domain.Exceptions;
using HeavyMetalBakeSale.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HeavyMetalBakeSale.Domain.Repositories
{
    public class CatalogItemAbbreviationMappingRepository : ICatalogItemAbbreviationMappingRepository
    {
        private IList<CatalogItemAbbreviationMapping> _mappings = new List<CatalogItemAbbreviationMapping>
        {
            new CatalogItemAbbreviationMapping() {Abbreviation = "B", CatalogStockItemID = 1},
            new CatalogItemAbbreviationMapping() {Abbreviation = "M", CatalogStockItemID = 2},
            new CatalogItemAbbreviationMapping() {Abbreviation = "C", CatalogStockItemID = 3},
            new CatalogItemAbbreviationMapping() {Abbreviation = "W", CatalogStockItemID = 4}
        };

        public CatalogItemAbbreviationMapping Get(string abbreviation)
        {
            if (string.IsNullOrWhiteSpace(abbreviation))
            {
                throw new ArgumentException($"'{nameof(abbreviation)}' cannot be null or whitespace.", nameof(abbreviation));
            }

            var result = _mappings.SingleOrDefault(x => x.Abbreviation.Equals(abbreviation, StringComparison.InvariantCultureIgnoreCase));

            if (result == null)
            {
                throw new CatalogItemMappingNotFoundException(abbreviation);
            }

            return result;
        }
    }
}
