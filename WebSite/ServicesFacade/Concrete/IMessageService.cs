using Models.Domain;

namespace ServicesFacade.Concrete
{
    public interface IMessageService : IService<Message>
    {
        Message GetByCode(string code);
    }
}
