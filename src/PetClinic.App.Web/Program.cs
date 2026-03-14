using PetClinic.Application;
using PetClinic.Infrastructure;
using PetClinic.Infrastructure.Data;
using PetClinic.App.Web.Components;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services.AddLocalization(opts => opts.ResourcesPath = "Resources");
builder.Services.Configure<RequestLocalizationOptions>(opts =>
{
    string[] supportedCultures = ["en", "es", "de", "pt", "ru", "tr", "ko", "fa"];
    opts.SetDefaultCulture("en")
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    await DataSeeder.SeedAsync(app.Services);
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseRequestLocalization();
app.UseHttpsRedirection();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
