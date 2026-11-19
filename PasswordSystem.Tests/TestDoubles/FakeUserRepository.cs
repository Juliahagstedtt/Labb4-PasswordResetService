namespace PasswordSystem.Tests;

public class FakeUserRepository : IUserRepository
{
    public List<string> RegisterEmails { get; } = [];

    public void AddUser(string email, string password)
    {
        RegisterEmails.Add(email);            
    }

    public bool IsEmailRegistered(string email)
    {
        return RegisterEmails.Contains(email);
    }

}