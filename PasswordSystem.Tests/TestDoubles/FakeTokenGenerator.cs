namespace PasswordSystem.Tests;

// Återställa ett fake lösenord
public class FakeTokenGenerator : ITokenGenerator
{
    public string Token { get; private set; } = "";

    public string Generate()
    {
        Token = "FAKE-TOKEN-001";
        return Token;
    }
}
