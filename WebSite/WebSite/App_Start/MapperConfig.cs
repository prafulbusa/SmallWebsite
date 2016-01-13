using System;
using System.Linq;
using AutoMapper;
using Models.Domain;
using System.Collections.Generic;
using System.IO;
using System.Web.Security;
using WebSite.Models;

namespace WebSite.App_Start
{
    public static class MapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.CreateMap<Message, PageBViewModel>();
            Mapper.CreateMap<Message, SendMessageModel>();                
            Mapper.CreateMap<SendMessageModel, Message>();

        }
    }
}