namespace Greeter.Tests;

public class FakeRateLimiter : IRateLimiter
{
    private Dictionary<string, DateTime> _lastSentTimes = new Dictionary<string, DateTime>();
    private DateTime _currentTime = DateTime.UtcNow;

    public bool CanSendEmail(string email)
    {
        if (!_lastSentTimes.ContainsKey(email))
        {
            return true;
        }

        TimeSpan timeSinceLastSent = _currentTime - _lastSentTimes[email];
        return timeSinceLastSent.TotalHours >= 24;
    }

    public void RecordEmailSent(string email)
    {
        _lastSentTimes[email] = _currentTime;
    }

    public bool WasRecorded(string email)
    {
        return _lastSentTimes.ContainsKey(email);
    }

    public void Advance24Hours()
    {
        _currentTime = _currentTime.AddHours(24);
    }
}