using CurrencyRate.Model;
using CurrencyRate.Service.Commands;
using CurrencyRate.Service.Handlers;
using CurrencyRate.Service.Results;
using CurrencyRate.Service.Services;
using MediatR;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("logs.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseSerilog()
    .ConfigureServices(services => services.AddMemoryCache());

builder.Services.AddControllers();

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddSingleton<ICacheService<CurrencyRateStructure>, CacheService<CurrencyRateStructure>>()
    .AddScoped<ICurrencyRateFileService, CurrencyRateFileService>()
    .AddMediatR(typeof(GetCurrensyRateQueryHandler).Assembly)
    .AddScoped<IRequestHandler<GetCurrensyRateQuery, GetCurrencyRateResult>, GetCurrensyRateQueryHandler>()
    ;

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();