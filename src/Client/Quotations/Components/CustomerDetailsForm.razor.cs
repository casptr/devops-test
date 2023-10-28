using Domain.Quotations;
using Domain.Formulas;
using Ardalis.GuardClauses;
using Domain.Supplements;
using Domain.Customers;
using Address = Domain.Customers.Address;
using Domain.Common;
using Microsoft.AspNetCore.Components;
using static Foodtruck.Client.Quotations.Index;

namespace Foodtruck.Client.Quotations;

public partial class CustomerDetailsForm
{
    [Parameter]
    public CustomerDetailsModel Model { get; set; }
}


