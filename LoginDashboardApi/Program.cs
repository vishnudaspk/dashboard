using LoginDashboardApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AspNetCoreRateLimit; // Required for rate limiting
using Microsoft.Extensions.Configuration; // Required for GetSection

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 1. Configure JWT settings
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.ASCII.GetBytes(jwtSettings["Key"] ?? throw new InvalidOperationException("JWT Key not found in configuration."));

// 2. Add TokenService
builder.Services.AddScoped<TokenService>();

// 3. Add Authentication and JWT Bearer
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();

// 4. Configure Rate Limiting
// Needed to load configuration from appsettings.json
builder.Services.AddOptions();
// Needed to store rate limit counters and ip rules
builder.Services.AddMemoryCache();
// Load general configuration from appsettings.json
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
// Load ip rules from appsettings.json
builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));
// Inject counter and rules stores
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
// The IProcessingStrategy is an interface that is responsible for identifying, blocking and logging calls that have reached the limit.
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
// Configuration (resolvers, counter key builders)
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage(); // More detailed errors in dev
}
else
{
    // Add basic error handling for production
    app.UseExceptionHandler("/error"); // You'd create an error handling endpoint
    app.UseHsts();
}

app.UseHttpsRedirection();

// Enable Rate Limiting Middleware
app.UseIpRateLimiting();

app.UseAuthentication(); // Must be before UseAuthorization
app.UseAuthorization();

app.MapControllers();

app.Run();
