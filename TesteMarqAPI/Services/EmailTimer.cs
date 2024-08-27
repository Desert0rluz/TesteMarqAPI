namespace Services;

public class ScheduledEmailService
{
    private readonly EmailServiceFactory _emailServiceFactory;
    private Timer timer;

    public ScheduledEmailService(EmailServiceFactory emailServiceFactory)
    {
        _emailServiceFactory = emailServiceFactory;
    }

    public void Start()
    {
        var timer = new Timer(async _ => await ExecuteTask(), null, TimeSpan.Zero, TimeSpan.FromHours(24));
    }

    private async Task ExecuteTask()
    {
        if (DateTime.Now.Hour == 8) 
        {
            var emailService = _emailServiceFactory.Create();
            await emailService.SendWinningBidEmailsAsync();
        }
    }
}
