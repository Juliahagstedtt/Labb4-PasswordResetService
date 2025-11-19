namespace PasswordSystem.Tests;

public class PasswordResetTests
{
    [Fact]
    public void SendResetEmail_SendsTokenToCorrectEmail()
    {
        // Arrange
        var _fakeEmailProvider = new FakeEmailProvider();
        var _fakeTokenGenerator = new FakeTokenGenerator();
        var _sut = new PasswordResetService(_fakeEmailProvider, _fakeTokenGenerator);
        var _testEmail = "test@test.com";

        // Act
        _sut.SendResetEmail(_testEmail);

        // Assert
        Assert.Equal(_testEmail, _fakeEmailProvider.SentTo);
        Assert.Contains(_fakeTokenGenerator.Token, _fakeEmailProvider.Body);
    }
}
