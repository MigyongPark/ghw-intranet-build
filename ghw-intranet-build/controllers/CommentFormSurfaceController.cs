using ContentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using System.Net.Mail;

namespace ContentAPI.Controllers
{
    public class CommentFormSurfaceController : SurfaceController
    {
        string userName = System.Web.HttpContext.Current.User.Identity.Name;
    
        public ActionResult Index()
        {
            return PartialView("CommentForm", new CommentFormViewModel());
        }

        [HttpPost]
        public ActionResult HandleFormSubmit(CommentFormViewModel model)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            //create comment
            var newComment = Services.ContentService.CreateContent(model.Summary, 5999, "grapevineItem");
            newComment.SetValue("grapevineItemHeadline", model.Summary);
            newComment.SetValue("bodyText", model.Message);
            newComment.SetValue("grapevinePostedBy", model.Postedby);
            newComment.SetValue("grapevineWard", model.Ward);
            newComment.SetValue("grapevineSite", model.Site);
            newComment.SetValue("grapevinePostedTel", model.PhoneNo);
            newComment.SetValue("grapevinePostedEmail", model.Email);

            Services.ContentService.SaveAndPublish(newComment);

            TempData["success"] = true;

           //if  (model.Email != null)
           //{
           //     //Send email
           //     MailMessage mail = new MailMessage();
           //     //var from = new MailAddress(Email.Text);
           //     mail.From = new MailAddress("Noreply@nhs.net");
           //     mail.To.Add(model.Email);
           //     mail.Subject = "Notification email - Grapevine: Message has been published.";
           //     mail.IsBodyHtml = true;

           //     mail.Body = "<!DOCTYPE html>";
           //     mail.Body += "<html>";
           //     mail.Body += "<head>";
           //     mail.Body += "<style>";
           //     mail.Body += "body {color: #000000; background: #ffffff; font-family: Arial, helvetica, sans-serif;}";
           //     mail.Body += "table, th, td {border: 1px solid #ccc; border-collapse: collapse; vertical-align: top; width:100%;}";
           //     mail.Body += "td {padding: 10px; width:60%; color: #330000}";
           //     mail.Body += "th {color: #330000; background: #e3e8ed; padding: 10px; text-align: left; width:30%;}";
           //     mail.Body += "th.emailInfo, td.emailInfo {color: #999; font-size: 80%;}";
           //     mail.Body += "h3 {padding-left: 0; font-weight: 400; color: #005eb8}";
           //     mail.Body += "h4 {padding-left: 0; font-weight: 100;}";
           //     mail.Body += "</style>";
           //     mail.Body += "</head>";
           //     mail.Body += "<body>";
           //     mail.Body += "<h3>" + model.Summary + "</h3>";
           //     mail.Body += "<h4>Message : " + model.Message + "</h4>";
           //     //mail.Body += "<h4>Posted by : " + model.Postedby + "</h4>";
           //     //mail.Body += "<h4>Site : " + model.Site + "</h4>";
           //     //mail.Body += "<h4>Phone no: " + model.PhoneNo + "</h4>";
           //     //mail.Body += "<h4>Email: " + model.Email + "</h4>";
           //     mail.Body += "</body>";
           //     mail.Body += "</html>";

           //     SmtpClient smtp = new SmtpClient();

           //     smtp.Send(mail);
           // }
               
            return RedirectToCurrentUmbracoPage();
        }

    }
}
