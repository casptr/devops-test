using Foodtruck.Shared.Customers;
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
                row.RelativeItem().Component(new AddressComponent("Factuuradres", Model.BillingAddress));
                row.ConstantItem(50);
                row.RelativeItem().Component(new AddressComponent("Evenementlocatie", Model.EventAddress));
            });
            column.Item().Row(row => {
                row.RelativeItem().AlignLeft().Text("(Inbegrepen producten)");
            });
            column.Item().Element(ComposeFormulaSupplementsTable);
            column.Item().Row(row => {
                row.RelativeItem().AlignLeft().Text("(Toegevoegde Supplementen)");
            });
            column.Item().Element(ComposeExtraSupplementsTable);

            var totalPrice = $"{Model.FormulaSupplementLines.Sum(x => x.SupplementPrice * x.Quantity) + Model.ExtraSupplementLines.Sum(x => x.SupplementPrice * x.Quantity)}";
            column.Item().PaddingRight(5).AlignRight().Text($"Totaal: €{totalPrice}").SemiBold();

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
                columns.ConstantColumn(25);
                columns.RelativeColumn(3);
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
            });
            table.Header(header =>
            {
                header.Cell().Element(CellStyle).Text("#");
                header.Cell().Element(CellStyle).Text("Product");
                header.Cell().Element(CellStyle).AlignRight().Text("prijs/eenheid");
                header.Cell().Element(CellStyle).AlignRight().Text("Aantal");
                header.Cell().Element(CellStyle).AlignRight().Text("Totaal");

                static IContainer CellStyle(IContainer container)
                {
                    return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                }
            });

            foreach (var item in Model.FormulaSupplementLines)
            {
                table.Cell().Element(CellStyle).Text("{Model.FormulaSupplementLines.IndexOf(item) + 1}");
                table.Cell().Element(CellStyle).Text(item.Name);
                table.Cell().Element(CellStyle).AlignRight().Text($"{item.SupplementPrice}");
                table.Cell().Element(CellStyle).AlignRight().Text($"{item.Quantity}");
                table.Cell().Element(CellStyle).AlignRight().Text($"€{item.SupplementPrice * item.Quantity}");

                static IContainer CellStyle(IContainer container)
                {
                    return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                }
            }
            table.Cell().ColumnSpan(5).AlignRight().Text($"subtotaal: €{Model.FormulaSupplementLines.Sum(x => x.SupplementPrice * x.Quantity)}");

        });
    }

    void ComposeExtraSupplementsTable(IContainer container)
    {
        container.Table(table =>
        {
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(25);
                columns.RelativeColumn(3);
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
            });
            table.Header(header =>
            {
                header.Cell().Element(CellStyle).Text("#");
                header.Cell().Element(CellStyle).Text("Product");
                header.Cell().Element(CellStyle).AlignRight().Text("prijs/eenheid");
                header.Cell().Element(CellStyle).AlignRight().Text("Aantal");
                header.Cell().Element(CellStyle).AlignRight().Text("Totaal");

                static IContainer CellStyle(IContainer container)
                {
                    return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                }
            });

            foreach (var item in Model.ExtraSupplementLines)
            {
                table.Cell().Element(CellStyle).Text("{Model.ExtraSupplementItems.IndexOf(item) + 1}");
                table.Cell().Element(CellStyle).Text(item.Name);
                table.Cell().Element(CellStyle).AlignRight().Text($"{item.SupplementPrice}");
                table.Cell().Element(CellStyle).AlignRight().Text($"{item.Quantity}");
                table.Cell().Element(CellStyle).AlignRight().Text($"€{item.SupplementPrice * item.Quantity}");

                static IContainer CellStyle(IContainer container)
                {
                    return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                }
            }
            table.Cell().ColumnSpan(5).AlignRight().Text($"subtotaal: €{Model.ExtraSupplementLines.Sum(x => x.SupplementPrice * x.Quantity)}");

        });
    }

    void ComposeHeader(IContainer container)
    {
        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column
                    .Item().Text($"Offerte #10{randomNumber.NextInt64(25, 99)}")
                    .FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

                column.Item().Text(text =>
                {
                    text.Span("Opgemaakt op: ").SemiBold();
                    text.Span($"{tomorrow}");
                });

                column.Item().Text(text =>
                {
                    text.Span("Geldig tot: ").SemiBold();
                    text.Span($"{nextMonth}:d");
                });
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
            column.Item().PaddingBottom(5).LineHorizontal(1);

            column.Item().Text($"{Address.Street} {Address.HouseNumber}");
            column.Item().Text($"{Address.City}, {Address.Zip}");
            // column.Item().Text("email@address.be");
            // column.Item().Text("049367883");
        });
    }
}