namespace Services;

public class EmailServiceFactory
{
    private readonly IServiceProvider _serviceProvider;

    public EmailServiceFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public EmailService Create()
    {
        return _serviceProvider.GetRequiredService<EmailService>();
    }
}
