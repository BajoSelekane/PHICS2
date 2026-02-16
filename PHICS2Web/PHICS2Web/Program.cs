using Application;
using Application.Abstractions.Data;
using Application.Features.Appointments;
using Application.Features.Appointments.Commands;
using Application.Features.Patients.Commands;
using Application.Features.Patients.Handlers;
using Application.Features.TimeSlots.Query;
using Application.Interfaces;
using Infrastructure.Config;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using PHICS2Web.Client.Pages;
using PHICS2Web.Components;
using PHICS2Web.Data;
using PHICS2Web.Endpoints.Patients;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();


builder.Services.Configure<TwilioSettings>(
    builder.Configuration.GetSection("Twilio"));

builder.Services.AddScoped<ITwilioService, TwilioSmsService>();


builder.Services.AddScoped<IApplicationDbContext>(provider =>
    provider.GetRequiredService<Infrastructure.DataLayer.ApplicationDbContext>());


builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://localhost:7122/");
});


builder.Services.AddCascadingAuthenticationState();
//builder.Services.AddScoped<IdentityRedirectManager>();
//builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
})
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreatePatientHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(UpdatePatientCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetAvailableTimeSlotsHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(BookAppointmentCommandHandler).Assembly));

var app = builder.Build();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode();

app.MapPatientEndpoints();

app.MapPost("/api/CreatePatient", async (CreatePatientCommand cmd, ISender sender) =>
{
    return await sender.Send(cmd);
});
app.MapPut("/{id:guid}", async (Guid id,
               [FromBody] UpdatePatientCommand cmd,
               ISender sender) =>
{
    var command = cmd with { Id = id };

    var result = await sender.Send(command);

    return Results.Ok(result);
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(PHICS2Web.Client._Imports).Assembly);

app.Run();
