using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Umbraco.Core.Models;
using Umbraco.Web;
using USNStarterKit.USNBusinessLogic;

namespace USNStarterKit.USNBlog
{
    interface IBlogService
    {
        string GetValueFromLanding(IPublishedContent node, string propertyAlias);

        string GetValueFromAncestor(IPublishedContent node, string ancestorAlias, string propertyAlias);

        IPublishedContent GetLanding(IPublishedContent node);

        IPublishedContent GetSiteRoot(IPublishedContent node, string rootNodeTypeAlias);
    }

    /// <summary>
    /// Summary description for USNDataService
    /// </summary>
    public class USNDataService : IBlogService
    {
        #region Singleton

        protected static volatile USNDataService m_Instance = new USNDataService();
        protected static object syncRoot = new Object();

        protected USNDataService() { }

        public static USNDataService Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    lock (syncRoot)
                    {
                        if (m_Instance == null)
                            m_Instance = new USNDataService();
                    }
                }

                return m_Instance;
            }
        }

        #endregion

        public string GetValueFromLanding(IPublishedContent node, string propertyAlias)
        {
            var landing = GetLanding(node);

            return landing.GetProperty(propertyAlias) == null ? string.Empty : landing.GetProperty(propertyAlias).Value.ToString();
        }


        public string GetValueFromAncestor(IPublishedContent node, string ancestorAlias, string propertyAlias)
        {
            string cacheKey = "USNBlog_GetValueFromAncestor_" + ancestorAlias;

            var root = USNCacheHelper.GetFromRequestCache(cacheKey) as IPublishedContent;
            if (root == null)
            {
                root = node.AncestorOrSelf(ancestorAlias);
                USNCacheHelper.AddToRequestCache(cacheKey, root);
            }

            return root.GetProperty(propertyAlias).Value.ToString();
        }

        public IPublishedContent GetLanding(IPublishedContent node)
        {
            string cacheKey = "GetLanding_USNBlogLanding_" + node.Id;

            var cached = USNCacheHelper.GetFromRequestCache(cacheKey) as IPublishedContent;
            if (cached != null)
            {
                return cached;
            }

            var landing = node.AncestorOrSelf("USNBlogLandingPage");

            // cache the result
            USNCacheHelper.AddToRequestCache(cacheKey, landing);

            return landing;
        }

        public IPublishedContent GetSiteRoot(IPublishedContent node, string rootNodeTypeAlias)
        {
            string cacheKey = "GetSiteRoot_USNBlogSiteRoot";
            string noBlogSiteRootcacheKey = "GetSiteRoot_No_USNBlogSiteRoot";


            // try to get the "no site root result" from cache
            var cachedNoSiteRoot = USNCacheHelper.GetFromRequestCache(noBlogSiteRootcacheKey) as string;
            if (!string.IsNullOrEmpty(cachedNoSiteRoot) && cachedNoSiteRoot == noBlogSiteRootcacheKey)
            {
                // we've already cached the fact that there is no root.
                return null;
            }

            // try to get the siteRoot from cache
            var cached = USNCacheHelper.GetFromRequestCache(cacheKey) as IPublishedContent;
            if (cached != null)
            {
                return cached;
            }

            // try to get the site root
            var root = node.AncestorOrSelf(rootNodeTypeAlias);

            // site root was not found, so just return null
            if (root == null)
            {
                // cache the fact that there is no site root
                USNCacheHelper.AddToRequestCache(noBlogSiteRootcacheKey, noBlogSiteRootcacheKey);
                return null;
            }

            // cache the result
            USNCacheHelper.AddToRequestCache(cacheKey, root);

            return root;
        }
    }
}