using System.Data.Entity;

namespace RepositoriesFacade
{
    public interface IContextFactory
    {
        DbContext GetDbContext();
    }
}