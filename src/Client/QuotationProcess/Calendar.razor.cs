using Foodtruck.Client.QuotationProcess.Helpers;
using Foodtruck.Shared.Reservations;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using FluentValidation;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Foodtruck.Client.QuotationProcess
{
    public partial class Calendar
    {
        [Inject] private QuotationProcessState QuotationProcessState { get; set; } = default!;
        [Inject] private IReservationService ReservationService { get; set; } = default!;
        [Inject] private NavigationManager NavigationManager { get; set; } = default!;
        [CascadingParameter] private MudTheme? Theme { get; set; }
        private ReservationDto.Create Model => QuotationProcessState.ReservationModel;
        private readonly ReservationDto.Validator calendarValidator = new();
        private MudForm form = default!;

        private IEnumerable<ReservationDto.Index>? reservations;
        private bool startDateConfirmed = false;
        private bool loading = true;

        protected override async Task OnInitializedAsync()
        {
            if (Model.End is not null) startDateConfirmed = true;

            reservations = (await ReservationService.GetIndexAsync()).Reservations;

            if (reservations is not null)
                foreach (var r in reservations)
                {
                    Console.WriteLine($"{r.Id} - {r.Start} - {r.End}");
                }

            loading = false;
            StateHasChanged();
        }

        private DateTime? PickerMonth => new(Model.Start?.Year ?? DateTime.Now.Year, Model.Start?.Month ?? DateTime.Now.Month, 1);
        private string ChipStyle(bool condition) => "color:" + (condition ? Theme?.Palette.PrimaryContrastText : Theme?.Palette.PrimaryLighten);
        private Variant ChipVariant(bool condition) => condition ? Variant.Filled : Variant.Outlined;

        private void ConfirmStartDate() => startDateConfirmed = true;
        private void EditStartDate()
        {
            Model.End = null;
            startDateConfirmed = false;
        }

        protected async Task Submit()
        {
            await form.Validate();

            if (!form.IsValid)
            {
                return;
            }

            Model.Start = Model.Start?.AddHours(11);
            Model.End = Model.End?.AddHours(16);
            QuotationProcessState.ConfigureReservation(Model.Start, Model.End);
            NavigationManager.NavigateTo("/aanvraag/formule-kiezen");
        }

        // MudDatePicker Functions
        private bool IsDateAlreadyBooked(DateTime dateTime) =>
            reservations is not null && reservations.Any(reservation =>
                dateTime.Date >= reservation.Start.AddDays(-1).Date &&
                dateTime.Date <= reservation.End.Date ||
                dateTime.Date < DateTime.Now.Date);

        private bool IsDateAvailableAsEnd(DateTime dateTime)
        {
            if (reservations is null || Model.Start is null)
                return true;

            ReservationDto.Index firstReservation = reservations
                .OrderBy(reservation => reservation.Start)
                .Where(reservation => reservation.Start.Date > Model.Start?.Date)
                .First();

            return reservations
                .Any(reservation => dateTime.Date < Model.Start?.AddDays(1).Date || dateTime.Date >= firstReservation.Start.Date);
        }

        private string AdditionalDateClassesFunc(DateTime dateTime) =>
            Model.Start is null ? "" :
            dateTime.Date == Model.Start?.Date ? "mud-range mud-range-start-selected" :
            dateTime.Date == Model.End?.Date ? "mud-range mud-range-end-selected mud-theme-primary" :
            dateTime.Date > Model.Start?.Date && dateTime.Date < Model.End?.Date ? "mud-range mud-range-between mud-theme-primary" : "";

    }
}
