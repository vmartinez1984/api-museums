using System.Reflection;
using AutoMapper;
using Microsoft.OpenApi.Models;
using Museums.Api.Helpers;
using Museums.BusinessLayer;
using Museums.Core.Interfaces;
using Museums.Core.Mappers;
using Museums.Repository;
using Museums.Service.Scraping;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
//Serilog
builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});
// var logger = new LoggerConfiguration()
// .ReadFrom.Configuration(builder.Configuration)
// .Enrich.FromLogContext()
// .CreateLogger();
// builder.Logging.ClearProviders();
// builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
// Add services to the container.
builder.Services.AddScoped<ScrapService>();
builder.Services.Configure<DbSettings>(builder.Configuration.GetSection("DbSettings"));
builder.Services.AddScoped<IMuseumRepository, MuseumRepository>();
builder.Services.AddScoped<ILogRepository, LogRepository>();
builder.Services.AddScoped<ICrontabRepository, CrontabRepository>();
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IMuseum, MuseumBl>();
builder.Services.AddScoped<ICrontabBl, CrontabBl>();
builder.Services.AddScoped<ILogBl, LogBl>();
builder.Services.AddScoped<IScrapyBl, ScrapyBl>();
builder.Services.AddScoped<IUnitOfWorkBl, UnitOfWorkBl>();
//services
//builder.Services.AddHostedService<WorkerService>();

//Mappers
var mapperConfig = new MapperConfiguration(mapperConfig =>
{
    mapperConfig.AddProfile<MuseumMapper>();
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Api de museos de la CDMX",
        Description = @"Es una ApiRest que muestra información de museos de la ciudad de México, con el objetivo 
        de probar los workerservices.
        probando el scraping web para obtener de detalles de costos, información adicional que no esta en el json.
        <br/>La información fue publicada por el Sistema de información de Cultura/Secretaría de cultura 
        Consultado en https://sic.cultura.gob.mx/opendata/d/9_museo_directorio.json el 10 de agosto de 2022",
        Contact = new OpenApiContact
        {
            Name = "Víctor Martínez",
            Url = new Uri("mailto:ahal_tocob@hotmail.com")
        }
    });
    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<HeadersMiddleware>();
app.UseMiddleware<ExampleMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseSerilogRequestLogging();

app.Run();