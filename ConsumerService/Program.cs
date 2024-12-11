using Steeltoe.Common.Http.Discovery;
using Steeltoe.Discovery.Client;

var builder = WebApplication.CreateBuilder(args);

// Add Steeltoe Discovery Client to enable service discovery
builder.Services.AddDiscoveryClient(builder.Configuration);

// Add HttpClient with service discovery enabled
builder.Services.AddHttpClient("producerClient")
    .AddServiceDiscovery();

// Add controllers
builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();
app.UseDiscoveryClient();

app.MapControllers();

app.Run();
