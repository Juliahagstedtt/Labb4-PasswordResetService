namespace PasswordSystem;

public class PasswordResetService(IEmailProvider emailProvider, ITokenGenerator tokenGenerator, IRateLimiter rateLimiter, IUserRepository userRepository)
{
    IEmailProvider _emailProvider = emailProvider;
    ITokenGenerator _tokenGenerator = tokenGenerator;
    IRateLimiter _rateLimiter = rateLimiter;
    IUserRepository _userRepository = userRepository;

    public void SendResetEmail(string email)
    {
        if (!_userRepository.UserExists(email))
        {
            throw new InvalidOperationException($"No user found with email {email}.");
        }

        if (!_rateLimiter.CanSendEmail(email))
        {
            throw new InvalidOperationException($"Password reset email for {email} can only be sent once per day.");
        }

        string subject = "Password Reset Requested";
        string token = _tokenGenerator.Generate();
        string body = CreateEmailBody(token);
        _emailProvider.SendEmail(email, subject, body);
        _rateLimiter.RecordEmailSent(email);
    }

    private string CreateEmailBody(string token)
    {
        string link = $"https://example.com/reset?token={token}";
        return $"Click the following link to reset your password: {link}";
    }
}

public interface IEmailProvider
{
    public void SendEmail(string to, string subject, string body);
}

public interface ITokenGenerator
{
    public string Generate();
}

public interface IRateLimiter
{
    public bool CanSendEmail(string email);
    public void RecordEmailSent(string email);
}

public interface IUserRepository
{
    public bool UserExists(string email);
}