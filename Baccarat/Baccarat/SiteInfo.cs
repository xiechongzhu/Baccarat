using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baccarat
{
    enum ESite
    {
        SIET_JINSHA     //金沙
    }

    enum ESubSite
    {
        AG_SITE         //AG女优厅
    }

    class SiteInfo
    {
        private Dictionary<String, ESite> siteMap = new Dictionary<string, ESite>();
        private static SiteInfo _SiteInfo = new SiteInfo();

        private SiteInfo()
        {
            siteMap["https://m.83361199.com"] = ESite.SIET_JINSHA;
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
    }
}
