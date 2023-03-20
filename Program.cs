using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using UnitessTestApp.Api.Core.Configuration;
using UnitessTestApp.Api.Core.Interfaces.Repositories;
using UnitessTestApp.Api.Core.Interfaces.Services;
using UnitessTestApp.Api.Core.Repositories;
using UnitessTestApp.Api.Core.Services;
using UnitessTestApp.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(builder.Configuration.GetSection(nameof(UnitessConfiguration)).Get<UnitessConfiguration>());

builder.Services.AddSingleton(builder.Configuration.GetSection(nameof(AuthenticationConfiguration)).Get<AuthenticationConfiguration>());

builder.Services.AddSingleton(builder.Configuration.GetSection(nameof(TokenConfiguration)).Get<TokenConfiguration>());

var tokenConfig = builder.Configuration.GetSection(nameof(TokenConfiguration)).Get<TokenConfiguration>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = tokenConfig.Issuer,
            ValidAudience = tokenConfig.Audience,
            ValidateLifetime = true,
            IssuerSigningKey = TokenConfiguration.GetSymmetricSecurityKey(),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<ICarService, CarService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/exception");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
