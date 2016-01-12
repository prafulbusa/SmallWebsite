using Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Context
{
    public partial class WebSiteContext : DbContext
    {
        public WebSiteContext()
            : base("name=WebSiteDB")
        {

        }

        public virtual DbSet<Message> Messages { get; set; }
    }
}
