using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.ComponentModel.DataAnnotations;
using Umbraco.Core.Models;
using createsend_dotnet;
using USNStarterKit.USNModels;
using Umbraco.Web;
using System.Threading.Tasks;
using MailChimp;
using MailChimp.Helper;
using MailChimp.Lists;

namespace USNStarterKit.USNControllers
{
    /// <summary>
    /// Summary description for USNNewsletterSignupSurfaceController
    /// </summary>
    public class USNNewsletterSignupSurfaceController : Umbraco.Web.Mvc.SurfaceController
    {
        public ActionResult Index(int NodeID, int ActualPageID)
        {
            var model = new USNNewsletterFormViewModel();
            model.ActualPageID = ActualPageID;
            model.CurrentNodeID = NodeID;

            return PartialView("USNForms/USN_NewsletterSignup", model);
        }

        public ActionResult TextSignup(int NodeID, int ActualPageID)
        {
            var model = new USNNewsletterFormViewModel();
            model.ActualPageID = ActualPageID;
            model.CurrentNodeID = NodeID;

            return PartialView("USNAdvancedPageComponents/USN_AC_TextSignupSection", model);
        }

        public ActionResult SubpageSignup(int NodeID, int ActualPageID)
        {
            var model = new USNNewsletterFormViewModel();
            model.ActualPageID = ActualPageID;
            model.CurrentNodeID = NodeID;

            return PartialView("USNAdvancedPageComponents/USN_AC_SignupSubpageListingSection", model);
        }

        [HttpPost]
        public async Task<ActionResult> HandleNewsletterSubmit(USNNewsletterFormViewModel model)
        {
            System.Threading.Thread.Sleep(1000);

            string lsReturnValue = "";

            var currentNode = Umbraco.TypedContent(model.CurrentNodeID);
            var currentPage = Umbraco.TypedContent(model.ActualPageID);

            IPublishedContent homeNode = currentPage.AncestorOrSelf(1);
            var settingsFolder = Umbraco.TypedContent(homeNode.GetProperty("websiteConfigurationNode").Value);
            var globalSettings = settingsFolder.Children.Where(x => x.DocumentTypeAlias == "USNGlobalSettings").First();

            if (!ModelState.IsValid)
            {
                return JavaScript(String.Format("$(NewsletterError{0}).show();$(NewsletterError{0}).html('<div class=\"info\"><p>{1}</p></div>');", model.CurrentNodeID, umbraco.library.GetDictionaryItem("USN Newsletter Form General Error")));
            }
            try
            {
                if (globalSettings.GetProperty("emailMarketingPlatform").Value.ToString() == "Campaign Monitor")
                {
                    AuthenticationDetails auth = new ApiKeyAuthenticationDetails(globalSettings.GetProperty("newsletterAPIKey").Value.ToString());

                    string subsciberListID = String.Empty;

                    if (currentNode.GetProperty("newsletterSubscriberListID").Value.ToString() != String.Empty)
                        subsciberListID = currentNode.GetProperty("newsletterSubscriberListID").Value.ToString();
                    else
                        subsciberListID = globalSettings.GetProperty("defaultNewsletterSubscriberListID").Value.ToString();

                    Subscriber loSubscriber = new Subscriber(auth, subsciberListID);

                    List<SubscriberCustomField> customFields = new List<SubscriberCustomField>();

                    string subscriberID = loSubscriber.Add(model.Email, model.FirstName + " " + model.LastName, customFields, false);
                }
                else if (globalSettings.GetProperty("emailMarketingPlatform").Value.ToString() == "MailChimp")
                {

                    var mc = new MailChimpManager(globalSettings.GetProperty("newsletterAPIKey").Value.ToString());

                    string subsciberListID = String.Empty;

                    if (currentNode.GetProperty("newsletterSubscriberListID").Value.ToString() != String.Empty)
                        subsciberListID = currentNode.GetProperty("newsletterSubscriberListID").Value.ToString();
                    else
                        subsciberListID = globalSettings.GetProperty("defaultNewsletterSubscriberListID").Value.ToString();


                    var email = new EmailParameter()
                    {
                        Email = model.Email
                    };

                    var myMergeVars = new MergeVar();
                    myMergeVars.Add("FNAME", model.FirstName);
                    myMergeVars.Add("LNAME", model.LastName);

                    EmailParameter results = mc.Subscribe(subsciberListID,email, myMergeVars, "html", false, true, false, false);
                }

                lsReturnValue = String.Format("<div class=\"spc alert alert-success alert-dismissible fade in\" role=\"alert\"><div class=\"info\">{0}</div></div>", currentNode.GetProperty("submissionMessage").Value.ToString());
                return Content(lsReturnValue);
            }
            catch (Exception ex)
            {
                return JavaScript(String.Format("$(NewsletterError{0}).show();$(NewsletterError{0}).html('<div class=\"info\"><p>{1}</p><p>{2}</p></div>');", model.CurrentNodeID, umbraco.library.GetDictionaryItem("USN Newsletter Form Signup Error"), ex.Message));
            }
        }
    }
}