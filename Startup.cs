using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();

        // Add session support
        services.AddSession(options =>
        {
            options.Cookie.Name = ".OnlineFoodOrdering.Session";
            options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust timeout as needed
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        // Other service configurations
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            // Production error handling
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        // Session middleware
        app.UseSession();

        // Static files middleware (if needed)
        app.UseStaticFiles();

        // Routing middleware
        app.UseRouting();

        // Authorization middleware
        app.UseAuthorization();

        // Endpoint routing
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }
}
