using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Foodtruck.Client;
using MudBlazor.Services;
using Foodtruck.Shared.Formulas;
using Foodtruck.Client.Formulas;
using Foodtruck.Shared.Supplements;
using Foodtruck.Client.Supplements;
using Foodtruck.Client.Infrastructure;
using Foodtruck.Client.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Services.Reservations;
using Foodtruck.Shared.Reservations;
using Services.Formulas;
using Foodtruck.Client.QuotationProcess.Helpers;
using Services.Supplements;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddAuthorizationCore();
builder.Services.AddSingleton<FakeAuthenticationProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<FakeAuthenticationProvider>());
builder.Services.AddTransient<FakeAuthorizationMessageHandler>();
builder.Services.AddTransient<CleanErrorHandler>();

builder.Services.AddHttpClient("Project.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<CleanErrorHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Project.ServerAPI"));

//builder.Services.AddScoped<IFormulaService, FormulaService>();
builder.Services.AddScoped<IFormulaService, FakeFormulaService>();
//builder.Services.AddScoped<ISupplementService, SupplementService>();
builder.Services.AddScoped<ISupplementService, FakeSupplementService>();
builder.Services.AddScoped<IReservationService, FakeReservationService>();
builder.Services.AddScoped<QuotationProcessState>();

builder.Services.AddMudServices();
await builder.Build().RunAsync();
