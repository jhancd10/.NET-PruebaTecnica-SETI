using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using SETI.Core.Helpers;
using SETI.Core.Services;
using SETI.Data.Class;
using SETI.Data.Common;
using SETI.Data.Interfaces.Helpers;
using SETI.Data.Interfaces.Services;
using SETI.WebApi;
using System.Text;

var myCors = "myCors";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myCors, builder =>
    {
        builder.WithOrigins("*");
    });
});

// Cache Initial Configuration
builder.Services.AddMemoryCache();

// Initial application parameters
var appSettingSection = builder.Configuration.GetSection("AppSettings");

// Configuring SETIDb connection
var setiConnectionString = builder.Configuration.GetConnectionString("SetiConnectionString");

builder.Services.AddDbContext<SetiDbContext>(options =>
{
    options.UseSqlServer(setiConnectionString);
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.Configure<AppSettings>(appSettingSection);

// Injecting services
builder.Services.AddScoped<IManualAccessDb, ManualAccessDb>();
builder.Services.AddScoped<IInvestmentProjectService, InvestmentProjectService>();
builder.Services.AddScoped<IProjectMovementService, ProjectMovementService>();
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Setting Cache data
var scopeFactory = app.Services.GetService<IServiceScopeFactory>();
using (var scope = scopeFactory.CreateScope())
{
    var provider = scope.ServiceProvider;

    // Obtengo una instancia del Servicio _investmentProjectService
    var _investmentProjectService = provider.GetRequiredService<IInvestmentProjectService>();
    
    // Obtengo la data de los proyectos validos en el periodo sgte al mes de ejecucion
    var currentValidProjects = _investmentProjectService.GetCurrentValidProjects();

    // Listado de identificadores de proyectos para consultar sus movimientos
    var projectsId = currentValidProjects.Select(x => x.ProjectId).ToList();
    var projectMovements = new List<InitialProjectMovement>();

    using (var setiDbContext = provider.GetRequiredService<SetiDbContext>())
    {
        // Usando EF Core realizo una consulta a la BD y obtengo todos los movimientos de los proyectos
        projectMovements = (from pm in setiDbContext.ProjectMovement
                            join dr in setiDbContext.DiscountRate
                            on pm.PeriodId equals dr.PeriodId
                            where projectsId.Contains(pm.ProjectId.Value)
                            select new InitialProjectMovement()
                            {
                                MovementId = pm.MovementId,
                                MovementAmount = pm.MovementAmount,
                                PeriodId = pm.PeriodId.Value,
                                ProjectId = pm.ProjectId.Value,
                                DiscountRatePercentage = Convert.ToDecimal(dr.DiscountRatePercentage) / 100
                            }).ToList();
    }
    
    // Guardo en cache la Data de los Brokers y Proyectos validos
    provider.GetRequiredService<IMemoryCache>().Set("CurrentValidProjects", currentValidProjects);
    
    // Guardo en cache los Movimientos de los Proyectos validos
    provider.GetRequiredService<IMemoryCache>().Set("ProjectMovements", projectMovements);
}

app.UseCors(x =>
    x.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();
