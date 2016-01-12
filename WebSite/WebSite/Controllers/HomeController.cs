using AutoMapper;
using Models.Domain;
using ServicesFacade.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMessageService _messageService;
        
        public HomeController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public ActionResult PageA()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PageA(PageAModel pageAModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("PageB", new { code = pageAModel.Code });
            }
            return View(pageAModel);
        }

        public ActionResult PageB(string code)
        {
            if (String.IsNullOrEmpty(code))
            {
                return View("Error");
            }

            var message = _messageService.GetByCode(code);
            PageBModel model;
            if (message != null)
            {
                model = Mapper.Map<Message, PageBModel>(message);
            }
            else
            {
                model = new PageBModel { Code = code };
            }           

            return View(model);
        }

        [HttpPost]
        public ActionResult SendMessage(PageBModel pageBModel)
        {
            if (ModelState.IsValid)
            {
                var message = Mapper.Map<PageBModel, Message>(pageBModel);
                //product.CreatedBy = WebSecurity.CurrentUserName;
                //product.CreatedOn = DateTime.Now;
                var result = _messageService.Create(message);
                var model = Mapper.Map<Message, PageBModel>(result);
                _messageService.SaveChanges();
                return PartialView("PageBMessage", pageBModel); 
            }
            return View(pageBModel);
        }

    }
}