using System.Reflection;
using AutoMapper;
using Hangfire;
using Hangfire.SqlServer;
using Helpers;
using Microsoft.OpenApi.Models;
using Museums.Api.Helpers;
using Museums.BusinessLayer;
using Museums.Core.Interfaces;
using Museums.Core.Mappers;
using Museums.Repository;
using Museums.Service.Scraping;
using Serilog;
using Vmartinez.RequestInspector.Extensores;
using VMtz.RequestInspector;

var builder = WebApplication.CreateBuilder(args);
//Serilog
builder.Host.UseSerilog((context, config) =>
{
    config.ReadFrom.Configuration(context.Configuration);
});

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
builder.Services.AddHostedService<WorkerService>();

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

builder.Services.AddCors(options => options.AddPolicy("AllowWebApp", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

//var connectionString = builder.Configuration.GetConnectionString("HangfireConnection");
//builder.Services.AddHangfire(configuration => configuration
//        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
//        .UseSimpleAssemblyNameTypeSerializer()
//        .UseRecommendedSerializerSettings()
//        .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
//        {
//            CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
//            SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
//            QueuePollInterval = TimeSpan.Zero,
//            UseRecommendedIsolationLevel = true,
//            DisableGlobalLocks = true
//        }))
//        ;
//builder.Services.AddHangfireServer();

builder.Services.AddRequestInpector();
var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
//}
app.UseCors("AllowWebApp");

app.UseMiddleware<HeadersMiddleware>();
app.UseMiddleware<RequestInspectorMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
//app.UseSerilogRequestLogging();
//app.UseHangfireDashboard();
//app.UseHangfireDashboard("/hangfire", new DashboardOptions
//{
//    DashboardTitle = "Sample Jobs",
//    Authorization = new[]
//    {
//        new  HangfireAuthorizationFilter("admin")
//    }
//});


//RecurringJob.AddOrUpdate<ScrapyBl>(
//    "Update Museum with id = 15",
//    //() => Console.WriteLine("Dummy-> Museo actualizado"),
//    //job => job.UpdateMuseumsAsync(new Museums.Core.Dtos.LogDto { MuseumIdInProcess = "15" }),
//    job => job.Process("15"),
//    Cron.Daily()
//);

app.Run();