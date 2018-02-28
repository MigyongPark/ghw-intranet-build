using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using umbraco.MacroEngines;
using Umbraco.Core.Models;

namespace USNStarterKit.USNBusinessLogic
{

    /// <summary>
    /// Summary description for DynamicNodeEqualityComparer
    /// </summary>
    public class USNDynamicNodeEqualityComparer : IEqualityComparer<IPublishedContent>
    {
        public USNDynamicNodeEqualityComparer()
        {
        }

        public bool Equals(IPublishedContent x, IPublishedContent y)
        {
            bool id = x.Id == y.Id;
            return id;
        }

        public int GetHashCode(IPublishedContent obj)
        {
            int hashCode = obj.ToString().ToLower().GetHashCode();
            return hashCode;
        }
    }

}