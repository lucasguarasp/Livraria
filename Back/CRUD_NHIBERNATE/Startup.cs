using CRUD_Livraria.Extentions;
using CRUD_Livraria.Gestores;
using CRUD_Livraria.Interfaces.Gestores;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NHibernate.Tool.hbm2ddl;
using System;

namespace CRUD_NHIBERNATE
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        [Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("http://localhost:53135",
                                        "http://localhost:4200"
                                        )
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                });
            });

            //services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            //services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Serialize);


            services.AddMvcCore()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddApiExplorer()
                    .AddAuthorization();

            #region conection
            var connStr = Configuration.GetConnectionString("DefaultConnection");
            DataBaseExtention.CreateDatabase(connStr);
            //var _sessionFactory = Fluently.Configure()
            //     .Database(PostgreSQLConfiguration.Standard.ConnectionString(connStr))
            //     .Mappings(x => x.FluentMappings.AddFromAssembly(GetType().Assembly))
            //     .ExposeConfiguration(cfg => new SchemaExport(cfg).Execute(true, true, false))
            //     .BuildSessionFactory();

            var _sessionFactory = Fluently.Configure()
                 .Database(PostgreSQLConfiguration.Standard.ConnectionString(connStr))
                 .Mappings(x => x.FluentMappings.AddFromAssembly(GetType().Assembly))
                 .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                 .BuildSessionFactory();

            services.AddScoped(f =>
            {
                return _sessionFactory.OpenSession();
            });
            #endregion conection


            #region DependencyContext
            services.AddTransient<ILivroGestor, LivroGestor>();
            services.AddTransient<IAutorGestor, AutorGestor>();
            #endregion DependencyContext

            services.AddControllersWithViews();

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(" / Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

        }

    }
}
