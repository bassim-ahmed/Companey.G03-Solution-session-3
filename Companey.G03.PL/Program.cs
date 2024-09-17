using Company.G03.BL.Interface;
using Company.G03.BL.Interfaces;
using Company.G03.BL.Repositories;
using Company.G03.DAL.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Companey.G03.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<AppDbContext>();//Allow DI For AppDbContext
            builder.Services.AddDbContext<AppDbContext>(options => {

                //options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnction"]);
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));

            });//Allow DI For AppDbContext

            builder.Services.AddScoped<IDepartmentRepository,DepartmentResitory>();//Allow DI For DepartmentResitory
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

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
}
