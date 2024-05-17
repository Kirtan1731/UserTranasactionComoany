using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using FinanceTracker.DAL.Data;
using FinanceTracker.DAL.Interface;
using FinanceTracker.DAL.Repository;
using FinanceTracker.BLL.Interface;
using FinanceTracker.BLL.Services;
using FinanceTracker.DAL;
using FinanceTracker.DAL.Resources;
using FinanceTracker.BLL;
using FinanceTracker.DAL.Interface.TokenRepository;
using FinanceTracker.DAL.Repository.TokenRepository;
using FinanceTracker.Model.AuthoritySetting;
using Hangfire;
using FinanceTracker.Hangfire.Interface;
using FinanceTracker.Hangfire.Services;
using FinanceTracker.Hangfire.Model;


namespace FinanceTracker
{
    public class Startup
    {
        public FinanceTrackerConfiguration Configuration { get; }

        [System.Obsolete("This Constructor is Deprecated")] 
        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder() 
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            builder.AddEnvironmentVariables();
            Configuration = new FinanceTrackerConfiguration(builder.Build());
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection")));
            services.AddHangfireServer();
            
            services.AddControllers();

            services.AddAuthentication("Bearer")
             .AddIdentityServerAuthentication(options =>
             {
                 var option = Configuration.Authority;
                 options.Authority = option.Authority;
                 options.RequireHttpsMetadata = option.RequireHttpsMetadata;
                 options.ApiName = option.ApiName;
                 options.ApiSecret = option.ApiSecret;
             });

            services.AddAuthorizationCore();
            services.AddSwaggerGen();

            services.AddScoped<HttpClient>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddControllers();

            var connectionString = Configuration.GetConnectionString("DbConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

            services.Configure<MailSettings>(Configuration.GetSection("MailSettings"));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddTransient<IUserServices, UserServices>();
            services.AddTransient<IMailService, MailService>();
            services.AddTransient<IAuthorityRegistrationServices, AuthorityRegistrationServices>();
            services.AddScoped<IRestServiceClientRepository, RestServiceClientRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddScoped<IStatusRepository, StatusRepository>();
            services.AddTransient<IStatusServices, StatusServices>();
            services.AddScoped<IAdminRepository, AdminRepository>();
            services.AddTransient<IAdminService, AdminService>();

            services.AddSingleton<FinanceTrackerConfiguration>();

            services.AddCors(x => x.AddPolicy("AllowAllOrigins", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));

            services.AddSingleton(new AuthorityModel
            {
                TokenUrl = Configuration.GetSection(ValidationResources.TokenURL).Value,
                ClientId = Configuration.GetSection(ValidationResources.ClientID).Value,
                Secret = Configuration.GetSection(ValidationResources.AuthoritySecret).Value,
                BaseUri = Configuration.GetSection(ValidationResources.BaseUri).Value,
                BasePut = Configuration.GetSection(ValidationResources.BasePut).Value
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
                IdentityModelEventSource.ShowPII = true;
            }
           
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("AllowAllOrigins");
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
