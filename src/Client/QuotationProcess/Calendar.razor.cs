using Foodtruck.Client.QuotationProcess.Helpers;
using Foodtruck.Shared.Reservations;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Foodtruck.Client.QuotationProcess
{
    public partial class Calendar
    {
        [Inject] private QuotationProcessState QuotationProcessState { get; set; } = default!;
        [Inject] private IReservationService ReservationService { get; set; } = default!;
        [CascadingParameter] private MudTheme? Theme { get; set; }

        private IEnumerable<ReservationDto.Index>? reservations;
        private DateTime? startDate;
        private DateTime? endDate;
        private bool startDateConfirmed = false;
        protected override async Task OnInitializedAsync()
        {
            ReservationResult.Index result = await ReservationService.GetIndexAsync();
            reservations = result.Reservations;

            if (reservations is not null)
                foreach (var r in reservations)
                {
                    Console.WriteLine($"{r.Id} - {r.Start} - {r.End}");
                }
            StateHasChanged();
        }

        private DateTime? PickerMonth => new(startDate?.Year ?? DateTime.Now.Year, startDate?.Month ?? DateTime.Now.Month, 1);

        private string ChipStyle(bool condition) => "color:" + (condition ? Theme?.Palette.PrimaryContrastText : Theme?.Palette.PrimaryLighten);
        private Variant ChipVariant(bool condition) => condition ? Variant.Filled : Variant.Outlined;

        private void SetStartDate(DateTime? dateTime) => startDate = dateTime?.AddHours(11);
        private void SetEndDate(DateTime? dateTime) => endDate = dateTime?.AddHours(16);
        private void ConfirmStartDate() => startDateConfirmed = true;
        private void EditStartDate()
        {
            endDate = null;
            startDateConfirmed = false;
        }
        protected static async Task Submit()
        {

            // FluentValidation.Results.ValidationResult validation = calendarValidator.Validate(model);


            //if (!validation.IsValid)
            //{
            //    Console.WriteLine("hmm");
            //    return;
            //}
            // QuotationProcessState.ConfigureReservation(start, end);
        }


        private bool IsDateAlreadyBooked(DateTime dateTime)
        {
            if (reservations is null)
                return false;

            return reservations
                .Any(reservation => dateTime >= reservation.Start.AddDays(-1) && dateTime <= reservation.End || dateTime < DateTime.Now);
        }

        private bool IsDateAvailableAsEnd(DateTime dateTime)
        {
            if (reservations is null || startDate is null)
                return true;

            ReservationDto.Index firstReservation = reservations
                .OrderBy(reservation => reservation.Start)
                .Where(reservation => reservation.Start > startDate)
                .First();

            return reservations
                .Any(reservation => dateTime < startDate || dateTime.Date >= firstReservation.Start.Date);
        }

        private string AdditionalDateClassesFunc(DateTime dateTime)
        {
            if (reservations is null || startDate is null)
                return "";

            ReservationDto.Index firstReservation = reservations
                .OrderBy(reservation => reservation.Start)
                .Where(reservation => reservation.Start.Day > startDate?.Day)
                .First();

            return
                dateTime.Date == startDate?.Date ? "mud-range mud-range-start-selected" :
                dateTime.Date == endDate?.Date ? "mud-range mud-range-end-selected mud-theme-primary" :
                dateTime > startDate && dateTime < endDate ? "mud-range mud-range-between mud-theme-primary" :
                "";
        }
    }
}
