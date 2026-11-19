namespace PasswordSystem.Tests;

public class PasswordResetServiceTests
{
    [Fact]
    public void SendResetEmail_SendsTokenToCorrectEmail()
    {
        // Arrange
        var fakeEmailProvider = new FakeEmailProvider();
        var fakeTokenGenerator = new FakeTokenGenerator();
        var registerEmails = new FakeUserRepository();
        var testEmail = "test@test.com";

        var sut = new PasswordResetService(fakeEmailProvider, fakeTokenGenerator, registerEmails);
        registerEmails.AddUser(testEmail,"password123");


        // Act
        sut.SendResetEmail(testEmail);

        // Assert
        var sentEmail = Assert.Single(fakeEmailProvider.SentEmails);
        Assert.Equal(testEmail, sentEmail.To);
        Assert.Contains(fakeTokenGenerator.Token, sentEmail.Body);
    }

    [Fact] // Kontrollera att listan med skickade mejl är tom
    public void SendResetEmail_UnregisteredEmail_DoesNotSendEmail()
    {
        // Arrange
        var fakeEmailProvider = new FakeEmailProvider();
        var fakeTokenGenerator = new FakeTokenGenerator();
        var registerEmails = new FakeUserRepository();
        var sut = new PasswordResetService(fakeEmailProvider, fakeTokenGenerator, registerEmails);
        var testEmail = "test@test.com";

        // Act
        sut.SendResetEmail(testEmail);

        // Assert
        Assert.Empty(fakeEmailProvider.SentEmails);
    }
}
