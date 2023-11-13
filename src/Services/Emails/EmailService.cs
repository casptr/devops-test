using Domain.Quotations;
using Foodtruck.Shared.Emails;
using Foodtruck.Shared.Pdfs;
using Foodtruck.Shared.Quotations;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using SendGrid;
using SendGrid.Helpers.Mail;
using static System.Net.Mime.MediaTypeNames;

namespace Services.Emails;

public class EmailService : IEmailService
{
    private readonly ISendGridClient sendGridClient;
    EmailAddress Sender = new EmailAddress("giovany.demurel@student.hogent.be", "Giovany van Blanche"); // TODO change this : Sendgrid settings - Sender Authentication - Single Sender Vertification
   
    
    public EmailService(ISendGridClient sendGridClient)
    {
        this.sendGridClient = sendGridClient ?? throw new ArgumentNullException(nameof(sendGridClient));
    }

    public async Task<bool> SendEmail(string text)
    {

        QuestPDF.Settings.License = LicenseType.Community;
        var document = new SampleDocument();
        string Base64String;

        using (var memstream = new MemoryStream())
        {
            document.GeneratePdf(memstream);
            Base64String = Convert.ToBase64String(memstream.ToArray());
        }


        SendGridMessage msg = new SendGridMessage();
        List<EmailAddress> recipients = new List<EmailAddress> { new EmailAddress("demurelgiovany@hotmail.com", "Your Name") };

        msg.SetSubject("Test");
        msg.SetFrom(Sender);
        msg.AddTos(recipients);
        msg.PlainTextContent = text;
        msg.Attachments = new List<Attachment>
        {
            new Attachment
            {
                Content = Base64String,
                Filename = "FILE_NAME.pdf",
                Type = "application/pdf",
                Disposition = "attachment"
            }
        };
        Response response = await sendGridClient.SendEmailAsync(msg);
        Console.WriteLine(response.StatusCode);


        return false;
    }


    public async Task<bool> SendQuotationPdfEmailController(string base64, string text )
    {

        SendGridMessage msg = new SendGridMessage();
        List<EmailAddress> recipients = new List<EmailAddress> { new EmailAddress("demurelgiovany@hotmail.com", "Your Name") };
        msg.SetSubject("Test2");
        msg.SetFrom(Sender);
        msg.AddTos(recipients);
        msg.PlainTextContent = text;
        msg.Attachments = new List<Attachment>
        {
            new Attachment
            {
                Content = base64,
                Filename = "FILE_NAME.pdf",
                Type = "application/pdf",
                Disposition = "attachment"
            }
        };
        Response response = await sendGridClient.SendEmailAsync(msg);
        Console.WriteLine(response.StatusCode);
        return response.IsSuccessStatusCode;
    }

    public Task<bool> SendQuotationPdfEmail(int quotationId)
    {
        throw new NotImplementedException();
    }

  


}

public class SampleDocument : IDocument
{
    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(50);
            page.Content().Background(Colors.Grey.Lighten3);

            page.Footer().AlignCenter().Text(text =>
            {
                text.CurrentPageNumber();
                text.Span(" / ");
                text.TotalPages();
            });
        });
    }
}