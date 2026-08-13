using Hangfire;
using Synchronizer.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistenceServices(builder.Configuration)
    .AddBackgroundJobServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHangfireDashboard(options: new DashboardOptions
{
    DashboardTitle = "Synchronizer",
    DarkModeEnabled = true,
});

app.Run();

