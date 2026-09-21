using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependenciesResolvers.Autofac;
using core.DependencyResolvers;
using core.Utilities.IoC;
using core.Utilities.Security.Encyption;
using core.Utilities.Security.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

using core.DependencyResolvers;
using core.Extensions;
using core.Utilities.IoC;

// Not: Projendeki ilgili namespace'leri (using Core.Utilities.Security.JWT; vb.) eklemeyi unutma.

var builder = WebApplication.CreateBuilder(args);

// Autofac'i IoC Container olarak ayarlıyoruz
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// AutofacBusinessModule'ü sisteme kaydediyoruz
builder.Host.ConfigureContainer<ContainerBuilder>(builderOptions =>
{
    builderOptions.RegisterModule(new AutofacBusinessModule());
});

// CORS Politikası Kaydı
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowOrigin",
        policyBuilder => policyBuilder.WithOrigins("http://localhost:3000").AllowAnyHeader());
});

// JWT Authentication Kaydı
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidIssuer = tokenOptions.Issuer,
            ValidAudience = tokenOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
        };
    });

builder.Services.AddDependencyResolvers(new ICoreModule[]
    {
        new CoreModule()
    });

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
}

// Cors politikası her zaman Routing ve Auth işlemlerinden önce gelmelidir.
app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseRouting();

// Authentication (Kimlik Doğrulama) mutlaka Authorization'dan (Yetkilendirme) önce yazılmalıdır.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();