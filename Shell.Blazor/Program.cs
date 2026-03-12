using MFE.Shared.Auth;
using Shell.Blazor.Components;
using Shell.Blazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Registramos JWT authentication usando la libreria que creamos
builder.Services.AddMfeJwtAuthentication(builder.Configuration);

// Ahora para leer y escribir cookies en Blazor Server
builder.Services.AddHttpContextAccessor();

// Shell
builder.Services.AddSingleton<MfeNavigationService>();
builder.Services.AddScoped<AuthService>();

// Health check
builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapHealthChecks("/health");

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
