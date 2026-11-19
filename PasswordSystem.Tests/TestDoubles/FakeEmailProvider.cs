namespace PasswordSystem.Tests;

public record SentEmail(string To, string Subject, string Body);

// Låtsas skicka email
public class FakeEmailProvider : IEmailProvider
{
    public List<SentEmail> SentEmails { get; } = [];


    public void SendEmail(string to, string subject, string body) // tom lista där alla mejl sparas
    {
        SentEmails.Add(new SentEmail(to, subject, body));
    }
}
