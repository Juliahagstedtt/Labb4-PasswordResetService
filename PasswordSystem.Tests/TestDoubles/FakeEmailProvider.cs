namespace Greeter.Tests;

public class FakeEmailProvider : IEmailProvider
{
    public string SentTo { get; private set; } = "";
    public string Body { get; private set; } = "";

    public void SendEmail(string to, string subject, string body)
    {
        Body = body;
        SentTo = to;
    }
}
