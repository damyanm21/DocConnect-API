using DocConnect.Business.Abstraction.Services;
using DocConnect.Business.Services;
using DocConnect.Data;
using DocConnect.Data.Abstraction.Repositories;
using DocConnect.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using DocConnect.Data.Models.Domains;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DocConnect.Presentation.Api.MiddleWares.ErrorHandlerMiddleware;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Settings.Configuration;
using DocConnect.Business.Models.Email;
using DocConnect.Business.Models;
using DocConnect.Business.TokenManager;
using DocConnect.Business.Abstraction.TokenManager;
using static DocConnect.Data.Models.Utilities.Constants.ApplicationBuilderConstants;


var builder = WebApplication.CreateBuilder(args);

string corsAllowHost = builder.Configuration.GetValue<string>("CORS_ALLOW_HOST");

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(string.IsNullOrEmpty(corsAllowHost) ? "https://localhost:3000" : corsAllowHost)
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddEndpointsApiExplorer();

// Add services to the container.
var connectionString = string.Empty;

if (builder.Environment.IsDevelopment())
{
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}
else
{
    var databaseName = builder.Configuration.GetValue<string>("DATABASE_NAME");
    var serverName = builder.Configuration.GetValue<string>("DATABASE_URL");
    var username = builder.Configuration.GetValue<string>("DATABASE_USER");
    var password = builder.Configuration.GetValue<string>("DATABASE_PASSWORD");

    connectionString = $"Server={serverName};Database={databaseName};Uid={username};Pwd={password};";
}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseMySql(connectionString,
        new MySqlServerVersion("8.0.34"),
        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.GetName().Name));
});

builder.Services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromHours(AllTokenProvidesExpiryTimeInHours));

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedEmail = false;
        options.SignIn.RequireConfirmedPhoneNumber = false;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireDigit = true;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequiredLength = 8;
    })
    .AddRoles<IdentityRole<string>>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);


builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
       .AddJwtBearer(options =>
       {
           options.SaveToken = true;
           options.TokenValidationParameters = new TokenValidationParameters()
           {
               // Issuer, Audience, Secret should be stored in a more secure way.
               ClockSkew = TimeSpan.Zero,
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateIssuerSigningKey = true,
               ValidAudience = builder.Configuration["JWTSettings:Audience"],
               ValidIssuer = builder.Configuration["JWTSettings:Issuer"],
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTSettings:Secret"]))
           };
       });

builder.Services.AddSingleton<ITokenManager, InMemoryTokenManager>();
builder.Services.AddSingleton<AzureEmailConfiguration>();

builder.Services.AddScoped<IEmailSender, AzureEmailSender>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<ISpecialistService, SpecialistService>();
builder.Services.AddScoped<ISpecialistRepository, SpecialistRepository>();
builder.Services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();
builder.Services.AddScoped<ISpecialtyService, SpecialtyService>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IJWTService, JWTService>();
builder.Services.AddScoped<ITokenService, TokenService>();


builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddMemoryCache();

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Error()
            .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddSerilog(dispose: true);
});

string environmentType = builder.Configuration.GetValue<string>("APPLICATION_ENVIRONMENT");

// Add Swagger services for development and test environments
if (environmentType != ApplicationEnvironmentProduction)
{
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "DocConnect.Presentation.Api", Version = "v1" });
    });
}

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

if (environmentType != ApplicationEnvironmentProduction)
{
    app.UseSwagger();

    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "DocConnect.Presentation.Api"); });
}

app.UseCors();

app.MapControllers();

app.Run();
