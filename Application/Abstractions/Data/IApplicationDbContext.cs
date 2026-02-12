using Domain;


namespace Application.Abstractions.Data;

public interface IApplicationDbContext
{
    //Set<User> Users { get; }
  
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
