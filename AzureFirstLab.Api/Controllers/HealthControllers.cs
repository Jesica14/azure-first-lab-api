using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace AzureFirstLab.Api.Controllers;

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public HealthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var environment = _configuration["ENVIRONMENT"] ?? "local";
        var timestamp = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ");
        var dbStatus = "disconnected";

        try
        {
            var connectionString =
                _configuration.GetConnectionString("DefaultConnection")
                ?? _configuration["DefaultConnection"];

            if (!string.IsNullOrEmpty(connectionString))
            {
                using var conn = new SqlConnection(connectionString);
                await conn.OpenAsync();
                dbStatus = "connected";
            }
            else
            {
                dbStatus = "not configured";
            }
        }
        catch
        {
            dbStatus = "disconnected";
        }

        return Ok(new
        {
            status = "healthy",
            environment,
            timestamp,
            database = dbStatus
        });
    }
}
