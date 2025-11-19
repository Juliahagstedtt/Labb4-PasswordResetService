namespace PasswordSystem.Tests;

public class FakeUserRepository : IUserRepository
{
    private HashSet<string> _registeredEmails = new HashSet<string>();

    public bool UserExists(string email)
    {
        return _registeredEmails.Contains(email);
    }

    public void RegisterUser(string email)
    {
        _registeredEmails.Add(email);
    }
}
