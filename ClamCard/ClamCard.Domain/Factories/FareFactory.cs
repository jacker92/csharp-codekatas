using ClamCard.Domain.Models;
using ClamCard.Domain.Models.Fares;
using System.Collections.Immutable;

namespace ClamCard.Domain.Factories
{
    public class FareFactory
    {
        private readonly IReadOnlyDictionary<Zone, IZoneFare> _zoneFareProviders;

        public FareFactory()
        {
            var zoneFareType = typeof(IZoneFare);
            _zoneFareProviders = zoneFareType.Assembly
                .ExportedTypes
                .Where(x => zoneFareType.IsAssignableFrom(x) && !x.IsAbstract && !x.IsInterface)
                .Select(x => Activator.CreateInstance(x))
                .Cast<IZoneFare>()
                .ToImmutableDictionary(x => x.Zone, x => x);
        }

        public IZoneFare GetFareFor(Zone zone)
        {
            return _zoneFareProviders[zone];
        }
    }
}