using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using asistPatentCore.Service;
using asistPatentCore.Service.IConstractor;
using asistPatentCore.Web.Profile;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Http;

namespace asistPatentCore.Web
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
            services.AddControllersWithViews();
            services.AddRazorPages()
        .AddRazorRuntimeCompilation();

            var config = new AutoMapper.MapperConfiguration(c =>
            {
                c.AddProfile(new CustomProfile());
            });
            
            
            services.AddAuthentication().AddFacebook(option => {
                option.AppId = "276966980291317";
                option.AppSecret = "170516f59270b844ea29bb6529c83da3";
                
            });
            
            services.AddTransient<ICookieService, CookieService>();
            services.AddTransient<ISocialLoginService, SocialLoginService>();
            services.AddTransient<IDefaultValuesService, DefaultValuesService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IUsersService, UsersService>();
            //services.AddSingleton<IUsersService>(x =>
            //    new UsersService( x.GetRequiredService<IMapper>()));

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            services.AddScoped(sp => sp.GetRequiredService<IHttpContextAccessor>().HttpContext);
            services.AddHttpContextAccessor();
            services.AddSession(options =>
            {
                //İlgili keyler Redis'den 24 saat sonra silinir.
                options.IdleTimeout = TimeSpan.FromHours(1);
            });

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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}");
            });
        }
    }
}
