using Domain.Common;
using Foodtruck.Shared.Customers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Services.Pdfs.QuotationPdfs;
public class QuotationDocument : IDocument
{
    QuotationModel Model { get; }

    public QuotationDocument(QuotationModel model)
    {
        Model = model;
    }

    DateTime tomorrow = new DateTime().AddDays(1);
    DateTime nextMonth = new DateTime().AddDays(31);

    Random randomNumber = new Random();




    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Margin(50);

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);

                page.Footer().AlignCenter().Text(text =>
                {
                    text.CurrentPageNumber();
                    text.Span(" / ");
                    text.TotalPages();
                });
            });
    }

    void ComposeContent(IContainer container)
    {
        container.PaddingVertical(40).Column(column =>
        {
            column.Spacing(20);

            column.Item().Row(row =>
            {
                row.RelativeItem().Component(new CustomerComponent(Model.Customer));
                row.ConstantItem(50);
                row.RelativeItem().Column(column =>
                {
                    column.Item().Component(new AddressComponent("Factuuradres", Model.BillingAddress));
                    column.Item().Component(new AddressComponent("Evenementadres", Model.EventAddress));
                });
            });

            column.Item().BorderBottom(1).BorderColor(Colors.Grey.Darken1).Element(ComposeFormulaSupplementsTable);

            //column.Item().BorderBottom(1).BorderColor(Colors.Grey.Darken1).Element(ComposeExtraSupplementsTable);


            if (!string.IsNullOrWhiteSpace(Model.ExtraInfo))
                column.Item().PaddingTop(25).Element(ComposeComments);
        });
    }

    void ComposeFormulaSupplementsTable(IContainer container)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.RelativeColumn(2);
                columns.RelativeColumn(6);
                columns.RelativeColumn(3);
                columns.RelativeColumn(3);
                columns.RelativeColumn(3);
            });
            table.Header(header =>
            {
                header.Cell().Element(CellStyle).AlignCenter().Text("Aantal");
                header.Cell().Element(CellStyle).AlignCenter().Text("Omschrijving");
                header.Cell().Element(CellStyle).AlignCenter().Text("Eenhprijs (€)");
                header.Cell().Element(CellStyle).AlignCenter().Text("Bedrag (€)");
                header.Cell().Element(CellStyle).AlignCenter().Text("BTW");

                static IContainer CellStyle(IContainer container)
                {
                    return container.DefaultTextStyle(x => x.SemiBold().LineHeight((float)1.8)).PaddingVertical(0).Border(1).BorderColor(Colors.Grey.Darken1).Background(Colors.Grey.Lighten1);
                }
            });
            table.Cell().Element(CellStyle).AlignCenter().Text($"1");
            table.Cell().Element(CellStyle).Text($"Formule: {Model.Formula.Title}").SemiBold();
            table.Cell().Element(CellStyle).AlignRight().Text($"{new Money(Model.FoodtruckPrice)}");
            table.Cell().Element(CellStyle).AlignRight().Text($"{new Money(Model.FoodtruckPrice)}");
            table.Cell().Element(CellStyle).AlignRight().Text($"€{new Money((Model.FoodtruckPrice) * 21 / 100M)}");


            table.Cell().Element(CellStyle).Text("");
            table.Cell().Element(CellStyle).Text($"Mobiele bar Blanche btw 21%");
            table.Cell().Element(CellStyle).Text("");
            table.Cell().Element(CellStyle).Text("");
            table.Cell().Element(CellStyle).Text("");


            foreach (var item in Model.FormulaSupplementLines)
            {
                table.Cell().Element(CellStyle).AlignCenter().Text($"");
                table.Cell().Element(CellStyle).Text($"incl. {item.Quantity}x {item.Name} btw {(int)(item.SupplementVat/item.SupplementPrice*100)}%");
                table.Cell().Element(CellStyle).AlignRight().Text($"");
                table.Cell().Element(CellStyle).AlignRight().Text($"");
                table.Cell().Element(CellStyle).AlignRight().Text($"");
            }

            table.Cell().Element(CellStyle).Text("");
            table.Cell().Element(CellStyle).Text("");
            table.Cell().Element(CellStyle).Text("");
            table.Cell().Element(CellStyle).Text("");
            table.Cell().Element(CellStyle).Text("");

            //decimal pricePerGuest = 12;
            //table.Cell().Element(CellStyle).AlignCenter().Text($"{Model.NumberOfGuests}");
            //table.Cell().Element(CellStyle).Text($"{Model.Formula.Title} {Model.NumberOfGuests} personen BTW 12%");
            //table.Cell().Element(CellStyle).AlignRight().Text($"{new Money(pricePerGuest)}");
            //table.Cell().Element(CellStyle).AlignRight().Text($"{new Money(Model.NumberOfGuests * pricePerGuest)}");
            //table.Cell().Element(CellStyle).AlignRight().Text($"€{new Money(Model.NumberOfGuests * pricePerGuest * 12M / 100M)}");

            //table.Cell().Element(CellStyle).Text("");
            //table.Cell().Element(CellStyle).Text("");
            //table.Cell().Element(CellStyle).Text("");
            //table.Cell().Element(CellStyle).Text("");
            //table.Cell().Element(CellStyle).Text("");

            foreach (var item in Model.ExtraSupplementLines)
            {
                table.Cell().Element(CellStyle).AlignCenter().Text($"{item.Quantity}");
                table.Cell().Element(CellStyle).Text($"{item.Name} btw {(int)(item.SupplementVat / item.SupplementPrice * 100)}%");
                table.Cell().Element(CellStyle).AlignRight().Text($"{new Money(item.SupplementPrice)}");
                table.Cell().Element(CellStyle).AlignRight().Text($"{new Money(item.SupplementPrice * item.Quantity)}");
                table.Cell().Element(CellStyle).AlignRight().Text($"€{new Money((item.SupplementPrice * item.Quantity) * item.SupplementVat / 100M)}");
            }

            table.Cell().Element(FooterHeader).AlignCenter().Text("Belastbaar");
            table.Cell().ColumnSpan(2).Element(FooterHeader).AlignCenter().Text("BTW");
            table.Cell().ColumnSpan(2).Element(FooterHeader).AlignCenter().Text("Totaal");

            //decimal totaltaxable = 0;
            //decimal totalbtw = 0;
            //foreach (var item in Model.FormulaSupplementLines)
            //{
            //    totaltaxable += item.SupplementPrice * item.Quantity;
            //    totalbtw += item.SupplementPrice * item.SupplementVat / 100M * item.Quantity;
            //}
            //foreach (var item in Model.ExtraSupplementLines)
            //{
            //    totaltaxable += item.SupplementPrice * item.Quantity;
            //    totalbtw += item.SupplementPrice * item.SupplementVat / 100M * item.Quantity;
            //}
            //totaltaxable += Model.FoodtruckPrice;
            //totaltaxable += Model.NumberOfGuests * pricePerGuest;

            //totalbtw += Model.FoodtruckPrice * 21M / 100M;
            //totalbtw += Model.NumberOfGuests * pricePerGuest * 12M /100M;

            table.Cell().Element(CellStyle).AlignCenter().PaddingTop(1).PaddingBottom(1).Text($"{new Money(Model.Price)}");
            table.Cell().ColumnSpan(2).Element(CellStyle).AlignCenter().PaddingTop(1).PaddingBottom(1).Text($"{new Money(Model.VatTotal)}");
            table.Cell().ColumnSpan(2).Element(CellStyle).AlignCenter().PaddingTop(1).PaddingBottom(1).Text($"{new Money(Model.Price + Model.VatTotal)}");

            static IContainer FooterHeader(IContainer container)
            {
                return container.DefaultTextStyle(x => x.FontSize(11).LineHeight((float)1.8)).PaddingVertical(0).Border(1).BorderBottom(0).BorderColor(Colors.Grey.Darken1).Background(Colors.Grey.Lighten2);
            }

            static IContainer CellStyle(IContainer container)
            {
                return container.BorderVertical(1).BorderColor(Colors.Grey.Medium).PaddingHorizontal(5);
            }
        });
    }
    void ComposeHeader(IContainer container)
    {
        container.Row(row =>
        {   
            row.RelativeItem().Column(column =>
            {
                column.Item().PaddingVertical(20).Text("blanche-logo.png");
                column
                    .Item().Text($"Offerte #10{randomNumber.NextInt64(25, 99)}")
                    .FontSize(14).SemiBold().FontColor(Colors.Grey.Darken3);

                column.Item().Text(text =>
                {
                    text.Span("Opgemaakt op: ").SemiBold();
                    text.Span($"{DateTime.Now:dd/MM/yyyy}");
                });

                column.Item().Text(text =>
                {
                    text.Span("Geldig tot: ").SemiBold();
                    text.Span($"{DateTime.Now.AddDays(30):dd/MM/yyyy}");
                });
            });
            row.RelativeItem().Column(column =>
            {
                column.Item().AlignRight().Text("BLANCHE Mobiele Bar");
                column.Item().AlignRight().Text("Willem Dewaele");
                column.Item().AlignRight().Text("Albert Liénartstraat 19");
                column.Item().AlignRight().Text("9300 Aalst");
                column.Item().AlignRight().Text("BTW: 0825.292.925");
            });
            //row.ConstantItem(175).Image(LogoImage);
        });
    }

    void ComposeComments(IContainer container)
    {
        container.ShowEntire().Background(Colors.Grey.Lighten3).Padding(10).Column(column =>
        {
            column.Spacing(5);
            column.Item().Text("Extra informatie").FontSize(14).SemiBold();
            column.Item().Text(Model.ExtraInfo);
        });
    }
}

public class CustomerComponent : IComponent
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? CompanyName { get; set; }
    public string? CompanyNumber { get; set; }
    public CustomerComponent(CustomerDto.Detail customer ) 
    { 
        Firstname = customer.Firstname; 
        Lastname = customer.Lastname;
        Email = customer.Email;
        Phone = customer.Phone;
        CompanyName = customer.CompanyName;
        CompanyNumber = customer.CompanyNumber;
    }

    public void Compose(IContainer container) 
    { 
        container.ShowEntire().Column(column =>
        {
            column.Spacing(0);
            column.Item().Text("Aan").SemiBold();
            column.Item().PaddingBottom(2).LineHorizontal(1);
            column.Item().Text($"{Firstname} {Lastname}");
            column.Item().Text($" {Email}");
            column.Item().Text($"{Phone}");
            column.Item().Text($"{CompanyName}");
            column.Item().Text($"{CompanyNumber}");
            
        });
    }
}

public class AddressComponent : IComponent
{
    private string Title { get; }
    private AddressDto Address { get; }

    public AddressComponent(string title, AddressDto address)
    {
        Title = title;
        Address = address;
    }

    public void Compose(IContainer container)
    {
        container.ShowEntire().Column(column =>
        {
            column.Spacing(2);
            column.Item().Text(Title).SemiBold();
            column.Item().PaddingBottom(2).LineHorizontal(1);
            column.Item().Text($"{Address.Street} {Address.HouseNumber}");
            column.Item().Text($"{Address.City}, {Address.Zip}");
        });
    }
}