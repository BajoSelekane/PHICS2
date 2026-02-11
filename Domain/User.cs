

namespace Domain;

public sealed class User: BaseEntity
{
    
    public string? Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string? PasswordHash { get; set; }
}
