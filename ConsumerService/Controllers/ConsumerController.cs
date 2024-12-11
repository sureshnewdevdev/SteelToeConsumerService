using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class ConsumerController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ConsumerController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet("consume")]
    public async Task<IActionResult> ConsumeProducer()
    {
        var client = _httpClientFactory.CreateClient("producerClient");

        // Use the service name registered in Eureka
        var response = await client.GetAsync("http://ProducerService/api/Product/products");

        if (response.IsSuccessStatusCode)
        {
            var products = await response.Content.ReadAsStringAsync();
            return Ok(products);
        }

        return StatusCode((int)response.StatusCode);
    }
}
