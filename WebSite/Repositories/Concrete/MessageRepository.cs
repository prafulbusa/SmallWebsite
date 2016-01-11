using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Models.Domain;
using RepositoriesFacade;
using RepositoriesFacade.Concrete;

namespace Repositories.Concrete
{
    public class MessageRepository : Repository<Message>, IMessageRepository
    {

        public MessageRepository(IContextFactory contextFactory)
            : base(contextFactory)
        {
        }

        public Message GetByCode(string code)
        {
            return Query().FirstOrDefault(x => !x.Code.Equals(code));
        }
    }
}
