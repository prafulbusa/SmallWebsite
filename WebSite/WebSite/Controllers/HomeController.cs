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
                //var product = productViewModel.To<ProductViewModel, Product>();
                //product.CreatedBy = WebSecurity.CurrentUserName;
                //product.CreatedOn = DateTime.Now;
                //product.TitleMobile = product.Title;
                //_productService.Create(product);
                //_productService.SaveChanges();
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
                model = new PageBModel { Code = message.Code, MessageBody = message.MessageBody };
            }
            else
            {
                model = new PageBModel { Code = code };
            }           

            return View(model);
        }

        public ActionResult View1()
        {
            return View();
        }

        [HttpPost]
        public ActionResult View1(PageBModel pageBModel)
        {
            if (ModelState.IsValid)
            {
               // var product = productViewModel.To<ProductViewModel, Product>();
               //product.CreatedBy = WebSecurity.CurrentUserName;
                //product.CreatedOn = DateTime.Now;
                //product.TitleMobile = product.Title;
                var message = new Message();
                message.Code = pageBModel.Code;
                message.MessageBody = pageBModel.MessageBody;
                _messageService.Create(message);
                _messageService.SaveChanges();
                return RedirectToAction("PageA");
            }
            return View(pageBModel);
        }

    }
}