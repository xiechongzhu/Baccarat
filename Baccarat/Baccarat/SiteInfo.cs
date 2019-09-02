using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baccarat
{
    public enum ESite
    {
        SIET_JINSHA     //金沙
    }

    public enum ESubSite
    {
        AG_SITE         //AG女优厅
    }

    class SiteInfo
    {
        private Dictionary<String, ESite> siteMap = new Dictionary<string, ESite>();
        private Dictionary<ESite, Dictionary<String, ESubSite>> subSiteMap = new Dictionary<ESite, Dictionary<String, ESubSite>>();
        private static SiteInfo _SiteInfo = new SiteInfo();

        private SiteInfo()
        {
            siteMap["https://m.83361199.com"] = ESite.SIET_JINSHA;
            subSiteMap[ESite.SIET_JINSHA] = new Dictionary<string, ESubSite>();
            subSiteMap[ESite.SIET_JINSHA]["AG女优厅"] = ESubSite.AG_SITE;
        }

        public static SiteInfo Instance()
        {
            return _SiteInfo;
        }

        public List<String> GetSiteNames()
        {
            List<String> nameList = new List<string>();
            foreach(var item in siteMap)
            {
                nameList.Add(item.Key);
            }
            return nameList;
        }

        public List<String> GetSubSiteNames(ESite site)
        {
            List<String> nameList = new List<string>();
            foreach (var item in subSiteMap[site])
            {
                nameList.Add(item.Key);
            }
            return nameList;
        }

        public ESite MainSite(String siteName)
        {
            return siteMap[siteName];
        }

        public ESubSite GetSubSite(ESite mainSite, String subSiteName)
        {
            return subSiteMap[mainSite][subSiteName];
        }
    }
}
