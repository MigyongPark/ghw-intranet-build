using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using Umbraco.Web;
using Umbraco.Core.Models;
using USN.UrlPicker.Umbraco.Models;

namespace USNStarterKit.USNBusinessLogic
{
    /// <summary>
    /// Summary description for USNUrlHelper
    /// </summary>
    public static class USNUrlHelper
    {
        public static LinkInfo GetSingleUrlFromUrlPicker(UrlPicker link)
        {
            LinkInfo linkInfo = null;
            if (link != null && link.Url != null)
            {
                if (link.Type == UrlPicker.UrlPickerTypes.Media)
                {
                    linkInfo = new LinkInfo();
                    linkInfo.LinkType = UrlPicker.UrlPickerTypes.Media;

                    linkInfo.LinkUmbracoNode = link.TypeData.Media;

                    linkInfo.LinkURL = link.Url;

                    if (link.Meta.NewWindow)
                    {
                        linkInfo.LinkTitle = " title=\"" + umbraco.library.GetDictionaryItem("USN New Window Title Tag") + "\" ";
                        linkInfo.LinkTarget = "target=\"_blank\"";
                        linkInfo.LinkIcon = "<i class=\"ion-android-open after\"></i>";
                    }

                    if (link.Meta.Title == String.Empty)
                    {
                        linkInfo.LinkCaption = link.TypeData.Media.Name;
                    }
                    else
                    {
                        linkInfo.LinkCaption = link.Meta.Title;
                    }
                }
                else if (link.Type == UrlPicker.UrlPickerTypes.Content)
                {
                    linkInfo = new LinkInfo();

                    linkInfo.LinkUmbracoNode = link.TypeData.Content;
                    linkInfo.LinkType = UrlPicker.UrlPickerTypes.Content;

                    linkInfo.LinkURL = link.Url;

                    if (link.Meta.NewWindow)
                    {
                        linkInfo.LinkTitle = " title=\"" + umbraco.library.GetDictionaryItem("USN New Window Title Tag") + "\" ";
                        linkInfo.LinkTarget = "target=\"_blank\"";
                        linkInfo.LinkIcon = "<i class=\"ion-android-open after\"></i>";
                    }

                    if (link.Meta.Title == String.Empty)
                    {
                        linkInfo.LinkCaption = link.TypeData.Content.Name;
                    }
                    else
                    {
                        linkInfo.LinkCaption = link.Meta.Title;
                    }

                    //Document types ending _AN should be linked to anchor position on page.
                    if (link.TypeData.Content.DocumentTypeAlias.IndexOf("_AN") != -1)
                    {
                        var pageComponentsNode = link.TypeData.Content.Parent;
                        var parentNode = pageComponentsNode.Parent;
                        string anchor = "#pos_" + link.TypeData.Content.Id.ToString();
                        linkInfo.LinkURL = parentNode.Url + anchor;
                    }


                }
                else if (link.Type == UrlPicker.UrlPickerTypes.Url)
                {
                    linkInfo = new LinkInfo();

                    linkInfo.LinkUmbracoNode = null;
                    linkInfo.LinkType = UrlPicker.UrlPickerTypes.Url;

                    linkInfo.LinkURL = link.Url;

                    if (link.Meta.NewWindow)
                    {
                        linkInfo.LinkTitle = " title=\"" + umbraco.library.GetDictionaryItem("USN New Window Title Tag") + "\" ";
                        linkInfo.LinkTarget = "target=\"_blank\"";
                        linkInfo.LinkIcon = "<i class=\"ion-android-open after\"></i>";
                    }

                    if (link.Meta.Title == String.Empty)
                    {
                        linkInfo.LinkCaption = link.Url;
                    }
                    else
                    {
                        linkInfo.LinkCaption = link.Meta.Title;
                    }
                }
            }

            return linkInfo;
        }

        public static string GetSingleUrlFromJObject(JObject link, out string linkTitle, out string linkTarget, out string linkIcon, out IPublishedContent node)
        {
            string linkURL = String.Empty;
            linkTitle = String.Empty;
            linkTarget = String.Empty;
            linkIcon = String.Empty;
            node = null;

            if (link.Value<bool>("isInternal"))
            {
                var umbracoHelper = new Umbraco.Web.UmbracoHelper(Umbraco.Web.UmbracoContext.Current);

                node = umbracoHelper.TypedContent(link.Value<int>("internal"));

                if (node != null)
                {
                    linkURL = node.Url;

                    if (link.Value<bool>("newWindow"))
                    {
                        linkTitle = " title=\"" + umbraco.library.GetDictionaryItem("USN New Window Title Tag") + "\" ";
                        linkTarget = "target=\"_blank\"";
                        linkIcon = "<i class=\"ion-android-open after\"></i>";
                    }

                    //Document types ending _AN should be linked to anchor position on page.
                    if (node.DocumentTypeAlias.IndexOf("_AN") != -1)
                    {
                        var pageComponentsNode = node.Parent;
                        var parentNode = pageComponentsNode.Parent;
                        string anchor = "#pos_" + node.Id.ToString();
                        linkURL = parentNode.Url + anchor;
                    }
                }
            }
            else
            {
                linkURL = link.Value<string>("link");

                if (link.Value<bool>("newWindow"))
                {
                    linkTitle = " title=\"" + umbraco.library.GetDictionaryItem("USN New Window Title Tag") + "\" ";
                    linkTarget = "target=\"_blank\"";
                    linkIcon = "<i class=\"ion-android-open after\"></i>";
                }
            }

            return linkURL;
        }
    }
}