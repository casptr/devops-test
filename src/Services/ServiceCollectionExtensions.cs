using Foodtruck.Shared.Formulas;
using Foodtruck.Shared.Quotations;
using Foodtruck.Shared.Reservations;
using Foodtruck.Shared.Supplements;
using Microsoft.Extensions.DependencyInjection;
using Services.Formulas;
using Services.Quotations;
using Services.Reservations;
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
        services.AddScoped<IFormulaService, FormulaService>();
        services.AddScoped<ISupplementService, SupplementService>();
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<IQuotationService, QuotationService>();
        // Add more services here...

        return services;
    }
}