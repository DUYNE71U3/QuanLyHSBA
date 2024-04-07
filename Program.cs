using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QuanLyHSBA.Models;
using QuanLyHSBA.Repositories;

var builder = WebApplication.CreateBuilder(args);
// cấu hình EF core
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddDefaultTokenProviders()
        .AddDefaultUI()
        .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Đăng ký các Interface và entityframework
builder.Services.AddScoped<IDoctorRepository, EFDoctorRepository>();
builder.Services.AddScoped<ISpecialtyRepository, EFSpecialtyRepository>();
builder.Services.AddScoped<IPatientRepository, EFPatientRepository>();
builder.Services.AddScoped<IMedicineRepository, EFMedicineRepository>();
builder.Services.AddScoped<IMedicalRecordRepository, EFMedicalRecordRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
