using AutoMapper;
using Interfaces;
using Models.Domain;
using ServicesFacade.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly IEmailSender _emailSender;

        public HomeController(IMessageService messageService, IEmailSender emailSender)
        {
            _messageService = messageService;
            _emailSender = emailSender;
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
            PageBViewModel model;
            if (message != null)
            {
                model = Mapper.Map<Message, PageBViewModel>(message);
            }
            else
            {
                model = new PageBViewModel { Code = code };
            }           

            return View(model);
        }

        [HttpPost]
        public ActionResult SendMessage(SendMessageModel sendMessageModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var message = _messageService.GetByCode(sendMessageModel.Code);
                    Message result;
                    if (message != null)
                    {
                        message.MessageBody = sendMessageModel.MessageBody;
                        message.UpdatedDate = DateTime.UtcNow;
                        result = _messageService.Update(message);
                    }
                    else
                    {
                        message = Mapper.Map<SendMessageModel, Message>(sendMessageModel);
                        message.CreatedDate = DateTime.UtcNow;
                        message.UpdatedDate = DateTime.UtcNow;
                        result = _messageService.Create(message);
                    }
                    _messageService.SaveChanges();
                    var viewModel = Mapper.Map<Message, PageBViewModel>(result);

                    return PartialView("PageBMessage", viewModel);
                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Validation Error");
        }


        [HttpPost]
        public ActionResult SendEmailMessage(SendEmailMessageModel sendMessageModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _emailSender.SendMail(sendMessageModel.Email, sendMessageModel.Subject, sendMessageModel.MessageEmail);
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                catch (Exception ex)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
                }   
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, "Validation Error");
        }
    }
}