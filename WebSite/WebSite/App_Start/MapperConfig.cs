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
                //.ForMember(model => model.RegularImages,
                //    expression => expression.ResolveUsing<RecipeRegularImagesUrlResolver>())
                //.ForMember(model => model.RecipeImageUrl,
                //    expression => expression.ResolveUsing<RecipeImageUrlResolver>());

        }
    }
}