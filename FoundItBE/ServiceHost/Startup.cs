using FoundItBE.Domain;
using FoundItBE.Helpers;
using FoundItBE.Infrastructure;
using FoundItBE.Infrastructure.InfrastructureOptions;
using FoundItBE.Validation;

namespace FoundItBE.ServiceHost;

public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
        {
            var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });
        services.Configure<GmailConnectionOptions>(Configuration.GetSection(GmailConnectionOptions.GmailOptionsKey));

        services.AddDomain();
        services.AddValidation();
        services.AddHelpers();
        services.AddInstrstructure();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseStaticFiles();
    }
}