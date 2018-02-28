using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using USNStarterKit.USNBusinessLogic;
using Umbraco.Core.Models;


namespace USNStarterKit.USNBlog
{
    /// <summary>
    /// Summary description for PostService
    /// </summary>
    public class USNPostService
    {
        protected static volatile USNPostService m_Instance;

        protected static object syncRoot;

        public static USNPostService Instance
        {
            get
            {
                bool mInstance = USNPostService.m_Instance != null;
                if (!mInstance)
                {
                    lock (USNPostService.syncRoot)
                    {
                        mInstance = USNPostService.m_Instance != null;
                        if (!mInstance)
                        {
                            USNPostService.m_Instance = new USNPostService();
                        }
                    }
                }
                USNPostService postService = USNPostService.m_Instance;
                return postService;
            }
        }

        static USNPostService()
        {
            USNPostService.m_Instance = new USNPostService();
            USNPostService.syncRoot = new object();
        }

        public IEnumerable<IPublishedContent> GetPosts(IPublishedContent currentPage)
        {
            IEnumerable<IPublishedContent> nodes;
            string str = "GetPosts_USNPosts_" + currentPage.Id;
            IEnumerable<IPublishedContent> fromRequestCache = USNCacheHelper.GetFromRequestCache(str) as IEnumerable<IPublishedContent>;
            bool flag = fromRequestCache == null;
            if (flag)
            {
                string[] strArrays = new string[] { };
                IEnumerable<IPublishedContent> descendentsOrSelf = this.GetDescendentsOrSelf(currentPage, "PageBlogPost", strArrays);
                IOrderedEnumerable<IPublishedContent> nodes1 = descendentsOrSelf.OrderByDescending<IPublishedContent, DateTime>((IPublishedContent x) =>
                {
                    DateTime value = DateTime.Parse(x.GetProperty("postDate").Value.ToString());
                    return value;
                });
                USNCacheHelper.AddToRequestCache(str, nodes1);
                nodes = nodes1;
            }
            else
            {
                nodes = fromRequestCache;
            }
            return nodes;
        }


        public IEnumerable<IPublishedContent> GetPosts(int nodeId, IPublishedContent currentPage)
        {
            IEnumerable<IPublishedContent> nodes;
            string str = "GetPosts_USNPosts_" + nodeId;
            IEnumerable<IPublishedContent> fromRequestCache = USNCacheHelper.GetFromRequestCache(str) as IEnumerable<IPublishedContent>;
            bool flag = fromRequestCache == null;
            if (flag)
            {
                string[] strArrays = new string[] { };
                IEnumerable<IPublishedContent> descendentsOrSelf = this.GetDescendentsOrSelf(currentPage, "PageBlogPost", strArrays);
                IOrderedEnumerable<IPublishedContent> nodes1 = descendentsOrSelf.OrderByDescending<IPublishedContent, DateTime>((IPublishedContent x) =>
                {
                    DateTime value = DateTime.Parse(x.GetProperty("postDate").Value.ToString());
                    return value;
                });
                USNCacheHelper.AddToRequestCache(str, nodes1);
                nodes = nodes1;
            }
            else
            {
                nodes = fromRequestCache;
            }
            return nodes;
        }


        public IEnumerable<IPublishedContent> GetPosts(IPublishedContent currentPage, string category, int pageNo, int itemsPerPage, int month, int year)
        {
            bool flag;

            IEnumerable<IPublishedContent> posts = this.GetPosts(currentPage);
            IEnumerable<IPublishedContent> list = posts;

            flag = (month != 0 ? false : year == 0);
            bool length = flag;
            if (!length)
            {
                list = this.GetPostsDateFiltered(month, year, list);
            }

            length = string.IsNullOrEmpty(category);
            if (!length)
            {
                list = this.GetPosts(category, "postCategories", list);
            }

            IEnumerable<IPublishedContent> nodes = list;
            IEnumerable<IPublishedContent> nodes1 = nodes.Where<IPublishedContent>((IPublishedContent x) =>
            {
                bool nodeTypeAlias = x.DocumentTypeAlias.IndexOf("PageBlogPost") > -1;
                return nodeTypeAlias;
            }).Distinct<IPublishedContent>(new USNDynamicNodeEqualityComparer());
            IEnumerable<IPublishedContent> contentForPage = this.GetContentForPage(nodes1, pageNo, itemsPerPage);
            IEnumerable<IPublishedContent> nodes2 = contentForPage;
            return nodes2;
        }

        protected List<IPublishedContent> GetPosts(string item, string alias, IEnumerable<IPublishedContent> sorted)
        {
            bool flag;
            List<IPublishedContent> nodes = new List<IPublishedContent>();
            IEnumerator<IPublishedContent> enumerator = sorted.GetEnumerator();
            try
            {
                while (true)
                {
                    flag = enumerator.MoveNext();
                    if (!flag)
                    {
                        break;
                    }
                    IPublishedContent current = enumerator.Current;
                    List<string> strs = new List<string>();

                    if (current.GetProperty(alias).Value != null)
                    {
                        string[] strArrays = current.GetProperty(alias).Value.ToString().ToLower().Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        IEnumerable<string> strs1 = strArrays.Select<string, string>((string x) =>
                        {
                            string str = x.Trim();
                            return str;
                        });
                        strs.AddRange(strs1);
                        flag = !strs.Contains(item.ToLower());
                        if (!flag)
                        {
                            nodes.Add(current);
                        }
                    }
                }
            }
            finally
            {
                flag = enumerator == null;
                if (!flag)
                {
                    enumerator.Dispose();
                }
            }
            List<IPublishedContent> nodes1 = nodes;
            return nodes1;
        }

        public IPublishedContent GetNextPost(IPublishedContent current)
        {
            IEnumerable<IPublishedContent> posts = this.GetPosts(current);
            IPublishedContent next = USNPostService.GetNext(posts, current);
            IPublishedContent node = next;
            return node;
        }

        public IPublishedContent GetPreviousPost(IPublishedContent current)
        {
            IEnumerable<IPublishedContent> posts = this.GetPosts(current);
            IPublishedContent next = USNPostService.GetNext(posts.Reverse<IPublishedContent>(), current);
            IPublishedContent node = next;
            return node;
        }

        private static IPublishedContent GetNext(IEnumerable<IPublishedContent> siblings, IPublishedContent current)
        {
            IPublishedContent node;
            bool id;
            bool flag = false;
            IEnumerator<IPublishedContent> enumerator = siblings.GetEnumerator();
            try
            {
                while (true)
                {
                    id = enumerator.MoveNext();
                    if (!id)
                    {
                        break;
                    }
                    IPublishedContent node1 = enumerator.Current;
                    id = !flag;
                    if (!id)
                    {
                        node = node1;
                        return node;
                    }
                    id = node1.Id != current.Id;
                    if (!id)
                    {
                        flag = true;
                    }
                }
            }
            finally
            {
                id = enumerator == null;
                if (!id)
                {
                    enumerator.Dispose();
                }
            }
            node = null;
            return node;
        }

        protected IEnumerable<IPublishedContent> GetContentForPage(IEnumerable<IPublishedContent> nodes, int pageNo, int itemsPerPage)
        {
            IEnumerable<IPublishedContent> nodes1;
            int num;
            int num1;
            bool flag = pageNo != -2;
            if (flag)
            {
                num = (pageNo < 0 ? 0 : pageNo);
                int num2 = num;
                int num3 = num2 * itemsPerPage;
                num1 = (itemsPerPage < nodes.Count<IPublishedContent>() - num3 ? itemsPerPage : nodes.Count<IPublishedContent>() - num3);
                int num4 = num1;
                flag = num3 < nodes.Count<IPublishedContent>();
                nodes1 = (flag ? nodes.ToList<IPublishedContent>().GetRange(num3, num4) : new List<IPublishedContent>());
            }
            else
            {
                nodes1 = nodes;
            }
            return nodes1;
        }

        protected List<IPublishedContent> GetPostsDateFiltered(int month, int year, IEnumerable<IPublishedContent> sorted)
        {
            bool flag;
            bool flag1;
            bool flag2;
            List<IPublishedContent> nodes = new List<IPublishedContent>();
            int currentYear = -1;
            int currentMonth = -1;
            IEnumerator<IPublishedContent> enumerator = sorted.GetEnumerator();
            try
            {
                while (true)
                {
                    flag = enumerator.MoveNext();
                    if (!flag)
                    {
                        break;
                    }
                    IPublishedContent n = enumerator.Current;
                    DateTime date = DateTime.Parse(n.GetProperty("postDate").Value.ToString());
                    flag1 = (month == 0 ? true : year == 0);
                    flag = flag1;
                    if (flag)
                    {
                        flag = year == 0;
                        if (flag)
                        {
                            nodes.Add(n);
                        }
                        else
                        {
                            currentYear = date.Year;
                            flag = year != currentYear;
                            if (!flag)
                            {
                                nodes.Add(n);
                            }
                        }
                    }
                    else
                    {
                        currentMonth = date.Month;
                        currentYear = date.Year;
                        flag2 = (month != currentMonth ? true : year != currentYear);
                        flag = flag2;
                        if (!flag)
                        {
                            nodes.Add(n);
                        }
                    }
                }
            }
            finally
            {
                flag = enumerator == null;
                if (!flag)
                {
                    enumerator.Dispose();
                }
            }
            List<IPublishedContent> nodes1 = nodes;
            return nodes1;
        }

        private IEnumerable<IPublishedContent> GetDescendentsOrSelf(IPublishedContent currentPage, string targetAlias, IEnumerable<string> stopAliases)
        {
            IPublishedContent landing = USNDataService.Instance.GetLanding(currentPage);
            List<IPublishedContent> nodes = new List<IPublishedContent>();
            List<IPublishedContent> nodes1 = new List<IPublishedContent>();
            nodes1.Add(landing);
            while (true)
            {
                bool nodeTypeAlias = nodes1.Count<IPublishedContent>() > 0;
                if (!nodeTypeAlias)
                {
                    break;
                }
                IPublishedContent node = nodes1.FirstOrDefault<IPublishedContent>();
                nodeTypeAlias = node == null;
                if (nodeTypeAlias)
                {
                    break;
                }
                else
                {
                    nodes1.Remove(node);
                    nodeTypeAlias = !(node.DocumentTypeAlias.IndexOf(targetAlias) > -1);
                    if (nodeTypeAlias)
                    {
                        nodeTypeAlias = !stopAliases.Contains<string>(node.DocumentTypeAlias);
                        if (nodeTypeAlias)
                        {
                            List<IPublishedContent> items = node.Children.ToList();
                            items.Reverse();
                            List<IPublishedContent>.Enumerator enumerator = items.GetEnumerator();
                            try
                            {
                                while (true)
                                {
                                    nodeTypeAlias = enumerator.MoveNext();
                                    if (!nodeTypeAlias)
                                    {
                                        break;
                                    }
                                    IPublishedContent current = enumerator.Current;
                                    nodes1.Insert(0, current);
                                }
                            }
                            finally
                            {
                                ((IDisposable)enumerator).Dispose();
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    else
                    {
                        nodes.Insert(0, node);
                    }
                }
            }
            IEnumerable<IPublishedContent> dynamicNodes2 = nodes;
            return dynamicNodes2;
        }
    }
}