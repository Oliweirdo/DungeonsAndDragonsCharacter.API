using DungeonsAndDragonsCharacter.API.Entities;
using DungeonsAndDragonsCharacter.API.Middleware;
using DungeonsAndDragonsCharacter.API.Migrations;
using DungeonsAndDragonsCharacter.API.Models;
using DungeonsAndDragonsCharacter.API.Models.Validators;
using DungeonsAndDragonsCharacter.API.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using DungeonsAndDragonsCharacter.API.Authorization;

namespace DungeonsAndDragonsCharacter.API
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
            var autenticationSettings = new AutenticationSettings();

            Configuration.GetSection("Autentication").Bind(autenticationSettings);

            services.AddSingleton(autenticationSettings);
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = "Bearer";
                option.DefaultScheme = "Bearer";
                option.DefaultChallengeScheme = "Bearer"; 

            }).AddJwtBearer(cfg => 
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = autenticationSettings.JwtIssuer,
                    ValidAudience = autenticationSettings.JwtIssuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(autenticationSettings.JwtKey)),
                };
            
            });

            services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();
            services.AddControllers().AddFluentValidation();
            services.AddDbContext<CharacterDbContext>();
            services.AddScoped<CharacterSeeder>();
            services.AddScoped<ICharacterService, CharacterService>();
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<RequestTimeMiddleware>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPasswordHasher<Gamer> ,PasswordHasher<Gamer>>();
            services.AddScoped<IValidator<RegisterGamerDto> , RegisterGamerDtoValidation>();
            services.AddScoped<IGamerContextService, GamerContextService>();
            services.AddHttpContextAccessor();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DungeonsAndDragonsCharacter.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CharacterSeeder seeder)
        {
            seeder.Seed();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DungeonsAndDragonsCharacter.API v1"));
            }
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseMiddleware<RequestTimeMiddleware>();
            app.UseAuthentication();
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
