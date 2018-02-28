using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core;
using Umbraco.Core.Events;
using Umbraco.Core.Models;
using Umbraco.Core.Publishing;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace USNStarterKit.USNBusinessLogic
{
    /// <summary>
    /// Summary description for RegisterEvents
    /// </summary>
    public class USNRegisterEvents : ApplicationEventHandler
    {
        public USNRegisterEvents()
        {
            ContentService.Saved += Go;
            ContentService.Trashing += Trashing;
            
            //TODO: Notifications for unpublish events currently not working
            //http://issues.umbraco.org/issue/U4-8739
            //Will implement when this bug has been fixed in future releases.
            //ContentService.UnPublishing += Unpublishing;
        }

        private void Go(IContentService sender, SaveEventArgs<IContent> args)
        {
            foreach (var node in args.SavedEntities)
            {
                if (node.ContentType.Alias == "USNStandardPage" || node.ContentType.Alias == "USNStandardPageBlogPost")
                {

                    if (node.Children().Count() == 0)
                    {
                        var c = ApplicationContext.Current.Services.ContentService;
                        var content = c.CreateContent("Page Components", node.Id, "USNStandardPageComponents");
                        content.SetValue("disableDelete", true);
                        c.SaveAndPublishWithStatus(content);
                    }
                    else
                    {
                        bool found = false;

                        foreach (var child in node.Children())
                        {
                            if (child.Name == "Page Components")
                            {
                                found = true;
                            }
                        }
                        if (found == false)
                        {
                            var c = ApplicationContext.Current.Services.ContentService;
                            var content = c.CreateContent("Page Components", node.Id, "USNStandardPageComponents");
                            content.SetValue("disableDelete", true);
                            c.SaveAndPublishWithStatus(content);
                        }
                    }
                }
                else if (node.ContentType.Alias == "USNHomepage" || node.ContentType.Alias == "USNAdvancedPage" || node.ContentType.Alias == "USNAdvancedPageBlogPost")
                {
                    if (node.Children().Count() == 0)
                    {
                        var c = ApplicationContext.Current.Services.ContentService;
                        var content = c.CreateContent("Page Components", node.Id, "USNAdvancedPageComponents");
                        content.SetValue("disableDelete", true);
                        c.SaveAndPublishWithStatus(content);
                    }
                    else
                    {
                        bool found = false;

                        foreach (var child in node.Children())
                        {
                            if (child.Name == "Page Components")
                            {
                                found = true;
                            }
                        }
                        if (found == false)
                        {
                            var c = ApplicationContext.Current.Services.ContentService;
                            var content = c.CreateContent("Page Components", node.Id, "USNAdvancedPageComponents");
                            content.SetValue("disableDelete", true);
                            c.SaveAndPublishWithStatus(content);
                        }
                    }
                }
            }
        }

        private void Trashing(IContentService sender, MoveEventArgs<IContent> e)
        {
            foreach (var node in e.MoveInfoCollection)
            {
                if (node.Entity.HasProperty("disableDelete"))
                {
                    var contentService = ApplicationContext.Current.Services.ContentService;
                    IContent content = contentService.GetById(node.Entity.Id);
                    if (content.HasProperty("disableDelete") && content.GetValue<bool>("disableDelete"))
                    {
                        e.Cancel = true;
                        e.Messages.Add(new EventMessage("Deletion of this item has been blocked", "This item is important to the successfull operation of this website.<br>If you would still like to delete this item, please uncheck the 'Disable delete' field on the 'Properties' tab.", EventMessageType.Warning));
                    }
                }
            }
        }

        //private void Unpublishing(IPublishingStrategy sender, PublishEventArgs<IContent> e)
        //{
        //    foreach (var node in e.PublishedEntities)
        //    {
        //        if (node.HasProperty("disableDelete") && node.GetValue<bool>("disableDelete"))
        //        {
        //            e.Cancel = true;
        //            e.Messages.Add(new EventMessage("Unpublishing of this item has been blocked", "This item is important to the successfull operation of this website.<br>If you would still like to unpublish this item, please uncheck the 'Disable delete' field on the 'Properties' tab.", EventMessageType.Warning));
        //        }
        //    }
        //}
    }

}