using AdidataDbContext.Models.Mysql.PTPDev;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Minio;
using Newtonsoft.Json.Serialization;
using PTP.AuthenticationRepository;
using PTP.Dto;
using PTP.Interface;
using PTP.Service;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BasicProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Database
            services.AddDbContext<PTPDevContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("PTP"),
                Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.28-mariadb")));

            // Repository & Service
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<AuthenticationService>();

            // Minio
            services.Configure<MinioSettings>(Configuration.GetSection("Minio"));
            services.AddSingleton<IMinioClient>(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>()
                               .GetSection("Minio")
                               .Get<MinioSettings>();
                return new MinioClient()
                    .WithEndpoint(config.Endpoint)
                    .WithCredentials(config.AccessKey, config.SecretKey)
                    .WithSSL(config.UseSSL)
                    .Build();
            });

            // Controllers
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                });

            // JWT Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = Configuration["Jwt:Issuer"],
                   ValidAudience = Configuration["Jwt:Audience"],
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"])),
               };
               options.Events = new JwtBearerEvents
               {
                   OnChallenge = context =>
                   {
                       context.HandleResponse();
                       context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                       context.Response.ContentType = "application/json";
                       var result = JsonSerializer.Serialize(new { message = "Unauthorized Token" });
                       return context.Response.WriteAsync(result);
                   }
               };
           });

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Recruitment API Services",
                    Version = "v1",
                    Description = "API Documentation - Protected with Basic Auth"
                });

                // JWT Bearer
                var securitySchema = new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition("Bearer", securitySchema);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securitySchema, new string[] { } }
                });

            });

            // CORS
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddDistributedMemoryCache();

            // Session
            services.AddSession(option => option.IdleTimeout = TimeSpan.FromMinutes(5));

            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAll");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Swagger Middleware
            app.UseSwagger();

            app.UseWhen(context => context.Request.Path.StartsWithSegments("/swagger"), appBuilder =>
            {
                appBuilder.Use(async (context, next) =>
                {
                    string authHeader = context.Request.Headers["Authorization"];

                    if (authHeader != null && authHeader.StartsWith("Basic "))
                    {
                        var encoded = authHeader.Substring("Basic ".Length).Trim();
                        var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(encoded));
                        var parts = decoded.Split(':');

                        if (parts.Length == 2)
                        {
                            var username = parts[0];
                            var password = parts[1];

                            if (username == "superadmin" && password == "123123")
                            {
                                await next.Invoke();
                                return;
                            }
                        }
                    }

                    context.Response.Headers["WWW-Authenticate"] = "Basic realm=\"Swagger\"";
                    context.Response.StatusCode = 401;
                });
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Documentation V1");
                c.RoutePrefix = "swagger";
                c.DocumentTitle = "Recruitment API Documentation";
                c.DocExpansion(DocExpansion.List);

            });

            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

           
        }
    }
}
