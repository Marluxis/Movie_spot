using Microsoft.EntityFrameworkCore;
using MovieReviewsApp.Data;
using MovieReviewsApp.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Movie_spot.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddRazorPages(); // Add Razor Pages support
builder.Services.AddControllersWithViews();
// Add this to your service registrations
builder.Services.AddScoped<StatisticsService>();


// Configure EF Core with SQLite
string connStr = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connStr, sqlOptions =>
        sqlOptions.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

// Register services with appropriate lifetimes
builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<ReviewService>();

// Add caching support
builder.Services.AddResponseCaching();
builder.Services.AddMemoryCache();

// Add health checks
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>(); // Requires Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore package

var app = builder.Build();

// Apply migrations in a safer way
if (app.Environment.IsDevelopment())
{
    try
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database.");
    }
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

// Standard middleware pipeline
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseResponseCaching(); // Add caching middleware

app.UseAuthorization();

// Map both Razor Pages and controllers
app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Add health check endpoint
app.MapHealthChecks("/health");

app.Run();
