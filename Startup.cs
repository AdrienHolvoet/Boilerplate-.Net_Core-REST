using AutoMapper;
using Boilerplate.Business.Services.Interfaces;
using Boilerplate.Business.Mappers;
using Boilerplate.Business.Services.Implementations;
using Boilerplate.Data.Contexts;
using Boilerplate.Data.Interfaces;
using Boilerplate.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

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
            //Database Connection String
            services.AddDbContext<DatabaseContext>(options =>
            {
                //for pgsql db
                 options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));

                // use this when you have SQL server db
                // options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            //Uncomment if you don't want to configure a local/cloud db
            //services.AddDbContext<DatabaseContext>(opts => opts.UseInMemoryDatabase("database"));

            //Initialize Automapper
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();

            /*Transient objects are always different; a new instance is provided to every controller and every service. 
            Scoped objects are the same within a request, but different across different requests. 
            Singleton objects are the same for every object and every request.*/
            services.AddHttpClient();
            services.AddSingleton(mapper);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));

            services.AddTransient(typeof(IRecipeService), typeof(RecipeService));
            services.AddTransient(typeof(IAuthenticationService), typeof(AuthenticationService));
            services.AddTransient(typeof(IJwtService), typeof(JwtService));
            services.AddTransient(typeof(IImageService), typeof(ImageService));
            services.AddTransient(typeof(IFbAuthenticationService), typeof(FbAuthenticationService));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Boilerplate", Version = "v1" });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = Configuration["Jwt:Issuer"],
                     ValidAudience = Configuration["Jwt:Issuer"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                 };
             });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Boilerplate v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
