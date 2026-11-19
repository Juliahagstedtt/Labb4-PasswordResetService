namespace PasswordSystem;

public interface IUserRepository
{
    bool IsEmailRegistered(string email);

    public void AddUser(string email, string password);

}