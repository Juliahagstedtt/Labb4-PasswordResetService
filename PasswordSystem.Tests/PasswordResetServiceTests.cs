namespace Greeter.Tests;

public class PasswordResetTests
{
    private readonly FakeEmailProvider _fakeEmailProvider;
    private readonly FakeTokenGenerator _fakeTokenGenerator;
    private readonly FakeRateLimiter _fakeRateLimiter;
    private readonly PasswordResetService _sut;
    private readonly string _testEmail;

    public PasswordResetTests()
    {
        _fakeEmailProvider = new FakeEmailProvider();
        _fakeTokenGenerator = new FakeTokenGenerator();
        _fakeRateLimiter = new FakeRateLimiter();
        _sut = new PasswordResetService(_fakeEmailProvider, _fakeTokenGenerator, _fakeRateLimiter);
        _testEmail = "test@test.com";
    }

    [Fact]
    public void SendResetEmail_SendsToCorrectEmail()
    {
        // Act
        _sut.SendResetEmail(_testEmail);

        // Assert
        Assert.Equal(_testEmail, _fakeEmailProvider.SentTo);
        Assert.Contains(_fakeTokenGenerator.Token, _fakeEmailProvider.Body);
    }

    [Fact]
    public void SendResetEmail_FirstRequest_Succeeds()
    {
        // Act
        _sut.SendResetEmail(_testEmail);

        // Assert
        Assert.Equal(_testEmail, _fakeEmailProvider.SentTo);
        Assert.True(_fakeRateLimiter.WasRecorded(_testEmail));
    }

    [Fact]
    public void SendResetEmail_SecondRequestWithin24Hours_ThrowsException()
    {
        // Act
        _sut.SendResetEmail(_testEmail); // First request

        // Assert
        var exception = Assert.Throws<InvalidOperationException>(() => _sut.SendResetEmail(_testEmail));
        Assert.Contains("once per day", exception.Message);
    }

    [Fact]
    public void SendResetEmail_RequestAfter24Hours_Succeeds()
    {
        // Act
        _sut.SendResetEmail(_testEmail); // First request
        _fakeRateLimiter.Advance24Hours(); // Simulate 24 hours passing
        _sut.SendResetEmail(_testEmail); // Second request after 24 hours

        // Assert
        Assert.Equal(_testEmail, _fakeEmailProvider.SentTo); // Email was sent successfully
    }
}
