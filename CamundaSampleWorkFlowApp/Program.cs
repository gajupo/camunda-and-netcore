using CamundaSampleWorkFlowApp.Models;
using CamundaSampleWorkFlowApp.Models.Core;
using CamundaSampleWorkFlowApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Bind CamundaSettings from the appsettings.json
var camundaSettings = builder.Configuration.GetSection("CamundaSettings").Get<CamundaSettings>();
var bpmProccessSettings = builder.Configuration.GetSection("BpmProccessSettings").Get<BpmProcessSettings>();

builder.Services.AddSingleton(camundaSettings);
builder.Services.AddSingleton(bpmProccessSettings);

// Register the Zeebe client service
builder.Services.AddSingleton<IZeebeClientService, ZeebeClientService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
