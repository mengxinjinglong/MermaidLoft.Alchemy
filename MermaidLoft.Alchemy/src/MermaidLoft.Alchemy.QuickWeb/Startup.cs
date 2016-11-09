using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MermaidLoft.Alchemy.Common;
using Infrastructure.Dapper;
using Microsoft.AspNetCore.Http;
using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using System.Reflection;
using System.Linq;
using MermaidLoft.Alchemy.QuickWeb.Core;
using System.Text;

namespace MermaidLoft.Alchemy.QuickWeb
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            //初始化配置信息
            ConnectionConfig.Instance.SetConnectString("server=127.0.0.1;database=Alchemy;uid=root;pwd=123456;");
            ConfigSettings.Initialize();
        }

        public IConfigurationRoot Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // services.AddIdentity<User, IdentityRole>();

            //services.AddSingleton<IUserService,UserService>();
            //services.AddSingleton<IUserQueryService, UserQueryService>();
            //services.AddSingleton<IProductService, ProductService>();
            //services.AddSingleton<IProductQueryService, ProductQueryService>();
            //services.AddSingleton<ICouponService, CouponService>();
            //services.AddSingleton<ICouponQueryService, CouponQueryService>();
            //注册EncodingProvider，默认情况下均不支持，引入System.Text.Encoding.CodePages
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            // Add framework services.
            services.AddMvc();
            var builder = new ContainerBuilder(); 
            builder.RegisterType<AuthorizeValidator>().As<IAuthorizeValidator>().SingleInstance();
            //builder.RegisterType<UserService>().As<IUserService>().SingleInstance();
            //builder.RegisterType<UserQueryService>().As<IUserQueryService>().SingleInstance();
            //builder.RegisterType<ProductService>().As<IProductService>().SingleInstance();
            //builder.RegisterType<ProductQueryService>().As<IProductQueryService>().SingleInstance();
            //builder.RegisterType<CouponService>().As<ICouponService>().SingleInstance();
            //builder.RegisterType<CouponQueryService>().As<ICouponQueryService>().SingleInstance();

            builder.RegisterAssemblyTypes(new Assembly[] {
                Assembly.Load(new AssemblyName("MermaidLoft.Alchemy.BaseDomain"))
            })//.Where(item =>item.Name.EndsWith("Service"))
              .AsImplementedInterfaces()//仅仅注册实现接口class的type
              .SingleInstance();//注册为单利

            //builder.RegisterAssemblyTypes(new Assembly[] {
            //    Assembly.Load(new AssemblyName("MermaidLoft.Alchemy.BaseDomain"))
            //});//注册dll中class的type，不包括interface

            builder.Populate(services);
            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //Authentication 2016.10.14
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationScheme = "UserToken",
                LoginPath = new PathString("/User/Login"),
                AccessDeniedPath = new PathString("/Home/Error"),
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });


            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }


            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

    }

}
