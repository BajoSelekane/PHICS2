using Application;
using Application.Abstractions.Data;
using Application.Features.Appointments;
using Application.Features.Appointments.Commands;
using Application.Features.Patients.Commands;
using Application.Features.Patients.Handlers;
using Application.Features.TimeSlots.Query;
using Application.Interfaces;
using Infrastructure.Config;
using Infrastructure.DataLayer; 
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using PHICS2.Components;
using PHICS2.Components.Account;
using PHICS2.Data;
using PHICS2.Endpoints.Patients;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMudServices();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
// Swagger for testing endpoints
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(
        typeof(ApplicationAssemblyReference).Assembly));
builder.Services.AddScoped<IMediator>(provider => provider.GetRequiredService<IMediator>());

//builder.Services.AddMediatR(cfg =>
//    cfg.RegisterServicesFromAssemblies(typeof(CreateAppointmentHandler).Assembly));


builder.Services.Configure<TwilioSettings>(
    builder.Configuration.GetSection("Twilio"));

builder.Services.AddScoped<ITwilioService, TwilioSmsService>();


builder.Services.AddScoped<IApplicationDbContext>(provider =>
    provider.GetRequiredService<Infrastructure.DataLayer.ApplicationDbContext>());


builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://localhost:7284/");
});


builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ;
builder.Services.AddDbContext<PHICS2.Data.ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentityCore<ApplicationUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
    })
    .AddEntityFrameworkStores<PHICS2.Data.ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreatePatientHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(UpdatePatientCommandHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetAvailableTimeSlotsHandler).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(BookAppointmentCommandHandler).Assembly));
//BookAppointmentCommandHandler


var app = builder.Build();

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
    app.UseMigrationsEndPoint();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();
app.MapBlazorHub();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(PHICS2.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.Run();
