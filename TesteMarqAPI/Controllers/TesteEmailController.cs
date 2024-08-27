using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailTestController : ControllerBase
{
    private readonly EmailService _emailService;

    public EmailTestController(EmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpGet("send-test-email")]
    public async Task<IActionResult> SendTestEmail()
    {
        try
        {
            await _emailService.TestEmailSending();
            return Ok("E-mail de teste enviado com sucesso!");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao enviar e-mail: {ex.Message}");
        }
    }
}
