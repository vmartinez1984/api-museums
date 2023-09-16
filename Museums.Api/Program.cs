using System.Reflection;
using AutoMapper;
using Microsoft.OpenApi.Models;
using Museums.Api.Helpers;
using Museums.BusinessLayer;
using Museums.Core.Mappers;
using Museums.Service.Scraping;
using Serilog;
using Vmartinez.RequestInspector.Extensores;
using VMtz.RequestInspector;
using Museums.Repository.Sql.Helpers;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);
//Serilog
builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddControllers();
builder.Services.AddScoped<ScrapService>();
// Add services to the container.
builder.Services.AddRepositorySql();
//bussines LAyer
builder.Services.AddBl();
//services
builder.Services.AddHostedService<WorkerService>();
//HttpClientHandler httpClientHandler = new HttpClientHandler()
//{
//    ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; }
//};
//builder.Services.AddHttpClient("ignoreSSL", c =>
//{ }).ConfigurePrimaryHttpMessageHandler(h => httpClientHandler);
builder.Services.AddHttpClient();
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
        Version = "v1.1",
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

builder.Services.AddCors(options =>
{
    string frontEndUrl;

    frontEndUrl = builder.Configuration.GetSection("FrontEndUrl").Value;
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins(frontEndUrl)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithExposedHeaders("*");
    });
});

builder.Services.AddRequestInpector();
var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
//}
app.UseCors();

app.UseMiddleware<HeadersMiddleware>();
app.UseMiddleware<RequestInspectorMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();