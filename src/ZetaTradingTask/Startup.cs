using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ZetaTradingTask.Application.Abstractions;
using ZetaTradingTask.Application.Repositories;
using ZetaTradingTask.Application.Services;
using ZetaTradingTask.Database;
using ZetaTradingTask.WebApi.Middleware;

namespace ZetaTradingTask
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<MigratorService>();
            services.AddControllers();

            services.AddDbContext<AppDbContext>(x => x.UseSqlServer
            (
                _configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Missing the connection string")
            ));

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ZetaTradingTask",
                    Version = "v1",
                });
                options.SupportNonNullableReferenceTypes();
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INodeRepository, NodeRepository>();
            services.AddScoped<IJournalRepository, JournalRepository>();

            services.AddScoped<INodeService, NodeService>();
            services.AddScoped<ITreeService, TreeService>();
            services.AddScoped<IJournalService, JournalService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                context.Request.EnableBuffering();
                await next();
            });
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            app.UseSwagger();
            app.UseSwaggerUI();
        }
    }
}