using Microsoft.AspNetCore.Mvc;
using Taxually.TechnicalTest.Models.Requests;
using Taxually.TechnicalTest.Services;

namespace Taxually.TechnicalTest.Controllers;

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
    public async Task<ActionResult> Post([FromBody] VatRegistrationRequest request)
    {
        var strategy = _strategyResolver.Resolve(request);
        await strategy.RegisterAsync(request);

        return Ok();
    }
}


