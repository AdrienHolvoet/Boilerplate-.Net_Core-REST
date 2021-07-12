using AutoMapper;
using Boilerplate_REST.Business.Mappers;
using Boilerplate_REST.Business.Services.Implementations;
using Boilerplate_REST.Business.Services.Interfaces;
using Boilerplate_REST.Data.Contexts;
using Boilerplate_REST.Data.Interfaces;
using Boilerplate_REST.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Boilerplate
{
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

            services.AddDbContext<DatabaseContext>(opts => opts.UseInMemoryDatabase("database"));

            //Initialize Automapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();

            /*Transient objects are always different; a new instance is provided to every controller and every service. 
            Scoped objects are the same within a request, but different across different requests. 
            Singleton objects are the same for every object and every request.*/
            services.AddSingleton(mapper);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddTransient(typeof(IBookService), typeof(BookService));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Boilerplate", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Boilerplate v1"));
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
}
