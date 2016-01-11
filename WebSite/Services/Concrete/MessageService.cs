using Models.Domain;
using RepositoriesFacade;
using RepositoriesFacade.Concrete;
using ServicesFacade.Concrete;

namespace Services.Concrete
{
    public class MessageService : Service<Message>, IMessageService
    {
        private readonly IMessageRepository _messageRepository;

        protected override IRepository<Message> Repository
        {
            get { return _messageRepository; }
        }

        public MessageService(IMessageRepository messageRepository)
        {
            this._messageRepository = messageRepository;
        }

        public Message GetByCode(string code)
        {
            return _messageRepository.GetByCode(code);
        }

    }
}
