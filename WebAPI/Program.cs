using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.DependenciesResolvers.Autofac;

var builder = WebApplication.CreateBuilder(args);

// Autofac'i IoC Container olarak ayarlıyoruz
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// AutofacBusinessModule'ü sisteme kaydediyoruz
builder.Host.ConfigureContainer<ContainerBuilder>(builderOptions =>
{
    builderOptions.RegisterModule(new AutofacBusinessModule());
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
