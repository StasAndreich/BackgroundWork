using BackgroundWork.BackgroundService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add background service dependency.
builder.Services.AddHostedService<QueueService>();
// Register queue. It needs to available from all places. Same instance.
builder.Services.AddSingleton<IBackgroundQueue, BackgroundQueue>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

