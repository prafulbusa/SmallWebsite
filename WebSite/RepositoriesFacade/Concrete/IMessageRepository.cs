using Models.Domain;

namespace RepositoriesFacade.Concrete
{
    public interface IMessageRepository:IRepository<Message>
    {
        Message GetByCode(string code);
    }
}