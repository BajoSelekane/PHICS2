namespace SharedLibrary.Shared;

public interface IDateTimeProvider
{
    public DateTime UtcNow { get; }
}
