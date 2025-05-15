using Moq;
using Taxually.TechnicalTest.Clients;
using Taxually.TechnicalTest.Compliance.Models.Requests;
using Taxually.TechnicalTest.Compliance.Services.VatRegistration;
using Taxually.TechnicalTest.Compliance.Services.VatRegistration.Strategies;

namespace Taxually.TechnicalTest.Tests.Compliance.Services.VatRegistration;

public class VatRegistrationStrategyResolverTests
{
    private readonly Mock<IServiceProvider> _mockServiceProvider;
    private readonly IVatRegistrationStrategyResolver _resolver;

    public VatRegistrationStrategyResolverTests()
    {
        _mockServiceProvider = new Mock<IServiceProvider>();
        _resolver = new VatRegistrationStrategyResolver(_mockServiceProvider.Object);
    }

    [Fact]
    public void Resolve_ValidCountry_FR_Returns_Strategy()
    {
        // Arrange
        var request = new VatRegistrationRequest { Country = "FR" };
        var mockClient = new Mock<ITaxuallyQueueClient>();
        var frStrategy = new FrVatRegistrationStrategy(mockClient.Object);

        _mockServiceProvider
            .Setup(x => x.GetService(typeof(FrVatRegistrationStrategy)))
            .Returns(frStrategy);

        // Act
        var result = _resolver.Resolve(request);

        // Assert
        Assert.IsType<FrVatRegistrationStrategy>(result);
    }

    [Fact]
    public void Resolve_ValidCountry_GB_Returns_Strategy()
    {
        // Arrange
        var request = new VatRegistrationRequest { Country = "GB" };
        var mockClient = new Mock<ITaxuallyHttpClient>();
        var frStrategy = new GbVatRegistrationStrategy(mockClient.Object);

        _mockServiceProvider
            .Setup(x => x.GetService(typeof(GbVatRegistrationStrategy)))
            .Returns(frStrategy);

        // Act
        var result = _resolver.Resolve(request);

        // Assert
        Assert.IsType<GbVatRegistrationStrategy>(result);
    }

    [Fact]
    public void Resolve_ValidCountry_DE_Returns_Strategy()
    {
        // Arrange
        var request = new VatRegistrationRequest { Country = "DE" };
        var mockClient = new Mock<ITaxuallyQueueClient>();
        var frStrategy = new DeVatRegistrationStrategy(mockClient.Object);

        _mockServiceProvider
            .Setup(x => x.GetService(typeof(DeVatRegistrationStrategy)))
            .Returns(frStrategy);

        // Act
        var result = _resolver.Resolve(request);

        // Assert
        Assert.IsType<DeVatRegistrationStrategy>(result);
    }

    [Fact]
    public void Resolve_InvalidCountry_Throws_NotSupportedException()
    {
        var request = new VatRegistrationRequest { Country = "XX" };

        var exception = Assert.Throws<NotSupportedException>(() => _resolver.Resolve(request));
    }
}
