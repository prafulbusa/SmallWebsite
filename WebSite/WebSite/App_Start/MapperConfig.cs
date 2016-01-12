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
            Mapper.CreateMap<Message, MessageViewModel>();
            Mapper.CreateMap<Message, PageBModel>()                
                .ForMember(mes => mes.Code, expression => expression.MapFrom(model => model.Code))
                .ForMember(mes => mes.MessageBody, expression => expression.MapFrom(model => model.MessageBody));
            Mapper.CreateMap<PageBModel, Message>()
                .ForMember(mes => mes.MessageId, option => option.Ignore())
                .ForMember(pageB => pageB.Code, expression => expression.MapFrom(model => model.Code))
                .ForMember(pageB => pageB.MessageBody, expression => expression.MapFrom(model => model.MessageBody));

        }
    }
}