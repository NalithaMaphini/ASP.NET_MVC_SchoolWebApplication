using Microsoft.EntityFrameworkCore;
using SchoolWebApp.Controllers;
using SchoolWebApp.Data;
using SchoolWebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// pro spusteni programu z VS - zakomentovat nize, pridani databaze - na hosting someee
//builder.Services.AddDbContext<ApplicationDbContext>
//    (options => {
//        options.UseSqlServer(builder.Configuration["ConnectionStrings:SchoolDbConnection"]);
//    });


//pridani databaze - na hosting somee
builder.Services.AddDbContext<ApplicationDbContext>
    (options => {
        options.UseSqlServer(builder.Configuration.GetConnectionString("tomasnovak.somee.com"));
    });
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<SubjectService>();
builder.Services.AddScoped<GradesService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
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
