﻿using Foodtruck.Shared.Formulas;
using Foodtruck.Shared.Supplements;
using Microsoft.Extensions.DependencyInjection;
using Services.Formulas;
using Services.Supplements;

namespace Services;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds all services to the DI container.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddFoodtruckServices(this IServiceCollection services)
    {
        services.AddScoped<IFormulaService, FakeFormulaService>();
        services.AddScoped<ISupplementService, FakeSupplementService>();
        // Add more services here...

        return services;
    }
}