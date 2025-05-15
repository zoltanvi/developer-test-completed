using Microsoft.AspNetCore.Mvc;
using Moq;
using Taxually.TechnicalTest.Compliance.Constants;
using Taxually.TechnicalTest.Compliance.Controllers;
using Taxually.TechnicalTest.Compliance.Models.Requests;
using Taxually.TechnicalTest.Compliance.Services.VatRegistration;
using Taxually.TechnicalTest.Compliance.Services.VatRegistration.Strategies;
using Taxually.TechnicalTest.Compliance.Validators;

namespace Taxually.TechnicalTest.Tests.Compliance.Controllers;

public class VatRegistrationControllerTests
{
    private readonly Mock<IVatRegistrationStrategyResolver> _mockStrategyResolver;
    private readonly Mock<IVatRegistrationRequestValidator> _mockValidator;
    private readonly VatRegistrationController _controller;

    public VatRegistrationControllerTests()
    {
        _mockStrategyResolver = new Mock<IVatRegistrationStrategyResolver>();
        _mockValidator = new Mock<IVatRegistrationRequestValidator>();
        _controller = new VatRegistrationController(_mockStrategyResolver.Object);
    }

    [Fact]
    public async Task Post_ValidationError_Returns_BadRequest()
    {
        // Arrange
        var request = new VatRegistrationRequest();

        var validationErrors = new List<string> { "some error" };
        _mockValidator
            .Setup(v => v.Validate(It.IsAny<VatRegistrationRequest>()))
            .Returns(validationErrors);

        // Act
        var result = await _controller.Post(request, _mockValidator.Object);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var problemDetails = Assert.IsType<ValidationProblemDetails>(badRequestResult.Value);
        Assert.Equal(ErrorMessages.ValidationFailedTitle, problemDetails.Title);
    }

    [Fact]
    public async Task Post_UnsupportedCountryCode_Returns_BadRequest()
    {
        // Arrange
        var request = new VatRegistrationRequest
        {
            CompanyId = "1",
            CompanyName = "Test",
            Country = "XX"
        };

        _mockValidator
            .Setup(v => v.Validate(It.IsAny<VatRegistrationRequest>()))
            .Returns(new List<string>());

        _mockStrategyResolver
            .Setup(s => s.Resolve(It.IsAny<VatRegistrationRequest>()))
            .Throws(new NotSupportedException(ErrorMessages.UnsupportedCountryCodeTitle));

        // Act
        var result = await _controller.Post(request, _mockValidator.Object);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var problemDetails = Assert.IsType<ProblemDetails>(badRequestResult.Value);
        Assert.Equal(ErrorMessages.UnsupportedCountryCodeTitle, problemDetails.Title);
    }

    [Fact]
    public async Task Post_ValidRequest_Returns_Ok()
    {
        // Arrange
        var request = new VatRegistrationRequest
        {
            CompanyId = "1",
            CompanyName = "Test",
            Country = "GB"
        };

        var mockStrategy = new Mock<IVatRegistrationStrategy>();

        _mockValidator.
            Setup(v => v.Validate(It.IsAny<VatRegistrationRequest>()))
            .Returns(new List<string>());

        _mockStrategyResolver
            .Setup(s => s.Resolve(It.IsAny<VatRegistrationRequest>()))
            .Returns(mockStrategy.Object);

        mockStrategy
            .Setup(m => m.RegisterAsync(It.IsAny<VatRegistrationRequest>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Post(request, _mockValidator.Object);

        // Assert
        var okResult = Assert.IsType<OkResult>(result);
    }
}
