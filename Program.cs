//File name: Program.cs
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor;
using Blazored.Modal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSyncfusionBlazor(options => { options.IgnoreScriptIsolation = true; });
builder.Services.AddBlazoredModal();



var app = builder.Build();

// Syncfusion License Key
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTkwMzQwQDMxMzkyZTM0MmUzMFlUdWRjMU55ZFBZbVU5bWRNU3JxdFc0UzNrYlA3VjhMSVJjV0F4NmNaNWc9");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
