using Microsoft.AspNetCore.Mvc;
using SendEmailDesignerPatternNet.API.Domain.Models.Requests;
using SendEmailDesignerPatternNet.API.Services;

namespace SendEmailDesignerPatternNet.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WeatherForecastController(IMailService mailService) : ControllerBase
{
    // POST SEND
    [HttpPost]
    [Route("enviar")]
    public async Task<IActionResult> SendMail([FromForm] MailRequest request)
    {
        try
        {
            await mailService.SendEmailAsync(request);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    // private static readonly string[] Summaries = new[]
    // {
    //     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    // };
    //
    // private readonly ILogger<WeatherForecastController> _logger;
    //
    // public WeatherForecastController(ILogger<WeatherForecastController> logger)
    // {
    //     _logger = logger;
    // }
    //
    // [HttpGet(Name = "GetWeatherForecast")]
    // public IEnumerable<WeatherForecast> Get()
    // {
    //     return Enumerable.Range(1, 5).Select(index => new WeatherForecast
    //         {
    //             Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
    //             TemperatureC = Random.Shared.Next(-20, 55),
    //             Summary = Summaries[Random.Shared.Next(Summaries.Length)]
    //         })
    //         .ToArray();
    // }
}