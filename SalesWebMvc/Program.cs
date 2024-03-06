 using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore;
using SalesWebMvc.Data;
using SalesWebMvc.Services;
using System.Globalization;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddDbContext<SalesWebMvcContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SalesWebMvcContext") ?? throw new InvalidOperationException("Connection string 'SalesWebMvcContext' not found.")));

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        // Como apartir do .net 6 não tem o Startup.cs, para configurarmos um Seeding, terá que ser exatamente assim:

        // 1 => primeiro o AddScopped

        builder.Services.AddScoped<SeedingService>();


        builder.Services.AddScoped<SellerService>() ;

        builder.Services.AddScoped<DepartmentService>();

        builder.Services.AddScoped<SalesRecordService>();

        var app = builder.Build();

        // Configuração de Idioma do Sistema

        var enUS = new CultureInfo("en-US");
        RequestLocalizationOptions localizationOption = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-US"),
            SupportedCultures = new List<CultureInfo> { enUS },
            SupportedUICultures = new List<CultureInfo> { enUS }        
            
        };

        app.UseRequestLocalization(localizationOption);
        







        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())

        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.Services.CreateScope().ServiceProvider.GetRequiredService<SeedingService>().Seed();

       










        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}