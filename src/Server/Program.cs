using System;
using FluentValidation;
using FluentValidation.AspNetCore;
using Foodtruck.Persistence;
using Foodtruck.Server.Authentication;
using Foodtruck.Server.Middleware;
using Foodtruck.Shared.Formulas;
using Foodtruck.Shared.Supplements;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddFoodtruckServices();

// Fluentvalidation
builder.Services.AddValidatorsFromAssemblyContaining<FormulaDto.Mutate.Validator>();
builder.Services.AddValidatorsFromAssemblyContaining<SupplementDto.Mutate.Validator>();
builder.Services.AddFluentValidationAutoValidation();

// Swagger | OAS 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Since we subclass our dto's we need a more unique id.
    options.CustomSchemaIds(type => type.DeclaringType is null ? $"{type.Name}" : $"{type.DeclaringType?.Name}.{type.Name}");
    options.EnableAnnotations();
}).AddFluentValidationRulesToSwagger();

// Database
builder.Services.AddDbContext<BogusDbContext>();

// (Fake) Authentication
builder.Services.AddAuthentication("Fake Authentication")
                .AddScheme<AuthenticationSchemeOptions, FakeAuthenticationHandler>("Fake Authentication", null);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

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
app.UseMiddleware<ExceptionMiddleware>();
app.UseRouting();

//app.UseAuthentication();
//app.UseAuthorization();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

/*using (var scope = app.Services.CreateScope())
{ // Require a DbContext from the service provider and seed the database.
    var dbContext = scope.ServiceProvider.GetRequiredService<BogusDbContext>();
    FakeSeeder seeder = new(dbContext);
    seeder.Seed();
}*/

app.Run();
