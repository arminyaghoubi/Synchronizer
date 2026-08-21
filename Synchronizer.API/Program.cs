using Hangfire;
using Synchronizer.Application;
using Synchronizer.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddPersistenceServices(builder.Configuration)
    .AddBackgroundJobServices(builder.Configuration)
    .AddProviderAdapterServices(builder.Configuration)
    .AddApplicationServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHangfireDashboard(options: new DashboardOptions
{
    DashboardTitle = "Synchronizer",
    DarkModeEnabled = true,
});

app.Run();

