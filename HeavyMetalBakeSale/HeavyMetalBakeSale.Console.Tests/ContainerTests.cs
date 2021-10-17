using Autofac;
using AutoMapper;
using Xunit;

namespace HeavyMetalBakeSale.Console.Tests
{
    public class ContainerTests
    {
        [Fact]
        public void Container_ShouldBuild()
        {
            var result = Container.Build();
            Assert.NotNull(result);

            var mapper = result.Resolve<IMapper>();
            Assert.NotNull(mapper);
        }

        [Fact]
        public void Container_ShouldResolveApplication()
        {
            var container = Container.Build();
            var application = container.Resolve<IApplication>();
            Assert.NotNull(application);
        }
    }
}
