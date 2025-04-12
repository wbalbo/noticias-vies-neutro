using System.Threading.RateLimiting;
using OpenAI;
using OpenAI.Managers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") // frontend dev URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Configuration.AddUserSecrets<Program>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddScoped<G1Extractor>();
builder.Services.AddScoped<UOLExtractor>();
builder.Services.AddScoped<IExtractorFactory, ExtractorFactory>();

builder.Services.AddSingleton(_ =>
{
    var apiKey = builder.Configuration["OpenAI:ApiKey"] ??
        throw new InvalidOperationException("OpenAI API Key not found in configuration.");

    return new OpenAIService(new OpenAiOptions
    {
        ApiKey = apiKey
    });
});
builder.Services.AddScoped<IOpenAiService, OpenAiService>();

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10, // max 10 requests
                Window = TimeSpan.FromMinutes(1),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 2
            }));
    options.RejectionStatusCode = 429;
});

var app = builder.Build();

app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection(); // Uncomment if you want to enforce HTTPS
app.UseAuthorization();
app.MapControllers();
app.UseRateLimiter();

app.Run();
