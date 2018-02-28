using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Collections.Specialized;
using System.Web;


namespace USNStarterKit.USNBusinessLogic
{
    /// <summary>
    /// Summary description for USNVideoHelper
    /// </summary>
    public static class USNVideoHelper
    {
        public static string GetVideoInfo(string videoURL, out string videoURLModified, out string dataRemote)
        {
            string output = "";
            videoURLModified = "";
            dataRemote = "";

            if (videoURL.IndexOf("www.youtube.com") > -1 || videoURL.IndexOf("youtu.be") > -1)
            {

                string videoID = GetYouTubeVideoID(videoURL);
                if (videoID != String.Empty)
                {
                    if (videoURL.IndexOf("youtu.be") > -1)
                    {
                        videoURLModified = "http://www.youtube.com/watch?v=" + videoID;
                    }
                    else
                        videoURLModified = videoURL;

                    output = String.Format("http://img.youtube.com/vi/{0}/0.jpg", videoID);
                }

                return output;

            }
            else if (videoURL.IndexOf("/vimeo.com") > -1)
            {
                videoURLModified = videoURL;
                string videoID = "";

                string videoImage = GetVimeoVideoImage(videoURL, out videoID);

                dataRemote = string.Format("data-remote=\"http://player.vimeo.com/video/{0}\"", videoID);

                if (videoImage != "")
                {
                    output = videoImage;
                }

                return output;

            }
            else
            {

                return output = "";
            }
        }

        public static string GetYouTubeVideoID(string videoURL)
        {
            try
            {
                if (videoURL.IndexOf("www.youtube.com") > -1)
                {

                    int iqs = videoURL.IndexOf('?');
                    string querystring = (iqs < videoURL.Length - 1) ? videoURL.Substring(iqs + 1) : String.Empty;
                    NameValueCollection nmvideoID = HttpUtility.ParseQueryString(querystring);

                    string videoID = nmvideoID.Get("v");

                    return videoID;
                }
                else if (videoURL.IndexOf("youtu.be") > -1)
                {
                    string youtubeUrl = System.Web.HttpContext.Current.Server.HtmlEncode(videoURL);

                    int length = youtubeUrl.Length;
                    int IDLength = length - youtubeUrl.LastIndexOf("/") - 1;

                    string videoID = youtubeUrl.Substring((youtubeUrl.LastIndexOf("/") + 1), IDLength);

                    return videoID;
                }
                else
                {
                    return "";
                }
            }
            catch
            {
                return "";
            }

        }


        public static string GetVimeoVideoImage(string videoURL, out string videoID)
        {
            string output = "";
            videoID = "";

            try
            {
                string vimeoUrl = System.Web.HttpContext.Current.Server.HtmlEncode(videoURL);

                int length = vimeoUrl.Length;
                int IDLength = length - vimeoUrl.LastIndexOf(".com") - 5;

                videoID = vimeoUrl.Substring((vimeoUrl.LastIndexOf(".com") + 5), IDLength);

                XmlDocument doc = new XmlDocument();
                doc.Load("http://vimeo.com/api/v2/video/" + videoID + ".xml");
                XmlElement root = doc.DocumentElement;
                string vimeoThumb = root.FirstChild.SelectSingleNode("thumbnail_medium").ChildNodes[0].Value;
                output = vimeoThumb;
                return output;
            }
            catch
            {
                return output;
            }

        }
    }
}