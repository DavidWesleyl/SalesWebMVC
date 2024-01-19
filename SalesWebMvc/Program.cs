 using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesWebMvc.Data;
using SalesWebMvc.Services;

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


        builder.Services.AddScoped<SellerService>();

        builder.Services.AddScoped<DepartmentService>();

        var app = builder.Build();



        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }


        // 2 => Depois o CreateScope, exatamente onde está esse.
        // Essa seria a configuração no Startup.cs
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