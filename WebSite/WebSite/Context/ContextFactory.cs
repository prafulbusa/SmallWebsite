using Models.Context;
using RepositoriesFacade;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebSite.Context
{
    public class ContextFactory : IContextFactory
    {
        private readonly WebSiteContext wsContext = new WebSiteContext();


        public DbContext GetDbContext()
        {
            
            return wsContext;
        }
    }
}