using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Movies.Application.Handlers;
using MediatR;
using Movies.Core.Repositories;
using Movies.Core.Repositories.Base;
using Movies.Infrastructure.Data;
using Movies.Infrastructure.Repositories;
using Movies.Infrastructure.Repositories.Base;

namespace Movies.API;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddApiVersioning();
        services.AddDbContext<MovieContext>(
            m => m.UseSqlServer(Configuration.GetConnectionString("MovieConnection")), ServiceLifetime.Singleton);
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Movie Review API", Version = "v1" });
        });
        services.AddAutoMapper(typeof(Startup));
        services.AddMediatR(typeof(CreateMovieCommandHandler).GetTypeInfo().Assembly);
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddTransient<IMovieRepository, MovieRepository>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Movie Review API V1");
        });
    }
}