using System.Net;
using System.Net.Mail;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Services;

public class EmailService
{
    private readonly TesteMarqAPIContext _context;

    public EmailService(TesteMarqAPIContext context)
    {
        _context = context;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var smtpClient = new SmtpClient("smtp.office365.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("testemarq@hotmail.com", "!@#$%^123456"),
            EnableSsl = true,
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress("testemarq@hotmail.com"),
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(toEmail);

        await smtpClient.SendMailAsync(mailMessage);
    }

    public async Task SendWinningBidEmailsAsync()
    {
        var cars = await _context.Cars.ToListAsync();

        foreach (var car in cars)
        {
            var highestBid = await _context.Bids
                .Where(b => b.CarId == car.CarId)
                .OrderByDescending(b => b.BidValue)
                .FirstOrDefaultAsync();

            if (highestBid != null)
            {
                var user = await _context.Users.FindAsync(highestBid.UserId);
                if (user != null)
                {
                    var subject = $"Parabéns! Você tem o maior lance para o carro {car.CarModel}";
                    var body = $"Seu lance de {highestBid.BidValue:C} para o carro {car.CarModel} foi o mais alto.";

                    await SendEmailAsync(user.UserEmail, subject, body);
                }
            }
        }
    }

    public async Task TestEmailSending()
    {
        string toEmail = "luizmartinsluz@gmail.com";
        string subject = "Teste de Envio de E-mail";
        string body = "Este é um teste de envio de e-mail usando o SmtpClient no .NET.";

        await SendEmailAsync(toEmail, subject, body);
    }

}
