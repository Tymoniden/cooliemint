using Cooliemint.Api.Server;
using Cooliemint.Api.Server.Services;
using Cooliemint.Api.Server.Services.Storage;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.RegisterCooliemintServices();

var app = builder.Build();

var fileSystemService = app.Services.GetService<IFileSystemService>();
fileSystemService?.SetRootFolder(@"C:\Users\Steib\OneDrive - prodot GmbH\Desktop\Docker");

var settingsService = app.Services.GetService<ISettingsProvider>();
settingsService?.ReadSettings();
Console.WriteLine(settingsService.GetValue<int>("test"));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
