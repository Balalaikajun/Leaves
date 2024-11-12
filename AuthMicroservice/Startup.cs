using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using Acces.Context;
using Acces.Repositories;
using Microsoft.EntityFrameworkCore;
using AuthMicroservice.Services;


namespace UserMicroservice
{

    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Этот метод вызывается для добавления служб в контейнер зависимостей.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(); // Добавление поддержки контроллеров

            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddDbContext<LeavesDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));



            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder
                        .WithOrigins("http://localhost:3000") // указать фронтенд-URL
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            services.AddScoped<UserRepository>();
            services.AddScoped<UserSevice>();
            services.AddScoped<TokenService>();

            

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("Leaves", new OpenApiInfo { Title = "Leaves", Version = "v-1" });



                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Пожалуйста, введите действительный токен",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",


                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
                   
    });

                
              
            });


        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    
                    c.SwaggerEndpoint("/swagger/Leaves/swagger.json", "AuthService");
                });
            }
            else
            {
                app.UseHsts();
            }


            //app.UseHttpsRedirection();
            app.UseCors("AllowSpecificOrigin");
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Это должно работать при наличии нужных пакетов
            });
        }
    }
}
