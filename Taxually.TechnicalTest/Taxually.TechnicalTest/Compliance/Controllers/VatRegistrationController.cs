using Microsoft.AspNetCore.Mvc;
using Taxually.TechnicalTest.Compliance.Models.Requests;
using Taxually.TechnicalTest.Compliance.Services.VatRegistration;
using Taxually.TechnicalTest.Compliance.Validators;

namespace Taxually.TechnicalTest.Compliance.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VatRegistrationController : ControllerBase
{
    private readonly IVatRegistrationStrategyResolver _strategyResolver;

    public VatRegistrationController(IVatRegistrationStrategyResolver strategyResolver)
    {
        ArgumentNullException.ThrowIfNull(strategyResolver);

        _strategyResolver = strategyResolver;
    }

    /// <summary>
    /// Registers a company for a VAT number in a given country
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
    public async Task<ActionResult> Post(
        [FromBody] VatRegistrationRequest request,
        [FromServices] IVatRegistrationRequestValidator validator)
    {
        var errors = validator.Validate(request);
        if (errors.Count != 0)
        {
            return BadRequest(new ValidationProblemDetails
            {
                Title = "Validation Failed",
                Errors = { ["ValidationErrors"] = errors.ToArray() }
            });
        }

        try
        {
            var strategy = _strategyResolver.Resolve(request);
            await strategy.RegisterAsync(request);
        }
        catch (NotSupportedException ex)
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Unsupported Country Code",
                Detail = ex.Message
            });
        }

        return Ok();
    }
}


