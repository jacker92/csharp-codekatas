using HeavyMetalBakeSale.Domain.Models;

namespace HeavyMetalBakeSale.Domain.Repositories
{
    public interface ICatalogItemAbbreviationMappingRepository
    {
        CatalogItemAbbreviationMapping Get(string abbreviation);
    }
}