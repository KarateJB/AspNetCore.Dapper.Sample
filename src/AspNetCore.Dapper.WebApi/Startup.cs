using AspNetCore.Dapper.WebApi.Models.Config;
using AspNetCore.Dapper.WebApi.Services;
using AspNetCore.Dapper.WebApi.Utils.Factory;
using Microsoft.OpenApi.Models;

namespace AspNetCore.Dapper.WebApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "AspNetCore.Dapper.WebApi", Version = "v1" });
        });

        services.AddOptions();
        services.Configure<AppSettings>(this.Configuration);

        // Services
        services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        services.AddSingleton<FolkDalService>();
        services.AddHostedService<SyncDataWorker>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AspNetCore.Dapper.WebApi v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
