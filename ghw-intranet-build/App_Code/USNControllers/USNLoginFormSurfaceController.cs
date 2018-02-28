using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using Umbraco.Core.Models;
using System.Web.Security;
using umbraco.cms.businesslogic.member;
using USNStarterKit.USNModels;

namespace USNStarterKit.USNControllers
{

    /// <summary>
    /// Summary description for USNLoginFormSurfaceController
    /// </summary>
    public class USNLoginFormSurfaceController : Umbraco.Web.Mvc.SurfaceController
    {

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleLoginSubmit(USNLoginFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            // Login
            if (Membership.ValidateUser(model.Username, model.Password))
            {
                FormsAuthentication.SetAuthCookie(model.Username, false);

                return Redirect(umbraco.library.NiceUrl(Convert.ToInt32(CurrentPage.GetProperty("loginSuccessPage").Value)));
            }
            else
            {
                TempData.Add("LoginFailure", umbraco.library.GetDictionaryItem("USN Login Form Login Error"));
                return RedirectToCurrentUmbracoPage();
            }
        }
    }

}