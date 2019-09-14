using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baccarat
{
    public enum GameResult
    {
        RESULT_UNKNOW,  //结果未知
        BANKER_WIN,     //庄家赢
        PLAYER_WIN,     //闲家赢
        DRAW_GAME       //平局
    }

    public enum GameState
    {
        GAME_UNKNOW,    //未知
        GAME_GOING,     //正在进行
        GAME_START,     //新的一局开始，可以开始下注了
        GAME_END        //本局结束
    }

    [Serializable]
    public class GameResultInfo
    {
        public GameResult result { get; set; }
        public string Name { get; set; }
    }

    public enum ESite
    {
        SIET_JINSHA     //金沙
    }

    public enum ESubSite
    {
        AG_SITE         //AG女优厅
    }

    class SubSiteInfo
    {
        public SubSiteInfo(String siteName, ESubSite site)
        {
            this.siteName = siteName;
            this.site = site;
        }
        public String siteName { get; set; }
        public ESubSite site { get; set; }
    }

    class MainSiteInfo
    {
        public MainSiteInfo(String siteName, ESite site, String configFile, String url)
        {
            this.siteName = siteName;
            this.site = site;
            this.configFile = configFile;
            this.url = url;
        }
        public String siteName { get; set; }
        public ESite site { get; set; }
        public Dictionary<ESubSite, SubSiteInfo> subSites { get; set; }
        public String configFile { get; set; }
        public String url;
    }

    class SiteInfo
    {
        private static SiteInfo _SiteInfo = new SiteInfo();
        private Dictionary<ESite, MainSiteInfo> mainSites = new Dictionary<ESite, MainSiteInfo>();

        private void Jinsha_Init()
        {
            MainSiteInfo mainSiteInfo = new MainSiteInfo("金沙", ESite.SIET_JINSHA, "./金沙.xml", "https://m.83361199.com");
            mainSiteInfo.subSites = new Dictionary<ESubSite, SubSiteInfo>();
            mainSiteInfo.subSites[ESubSite.AG_SITE] = new SubSiteInfo("AG女优厅", ESubSite.AG_SITE);
            mainSites[ESite.SIET_JINSHA] = mainSiteInfo;
        }

        private SiteInfo()
        {
            //站点信息--金沙
            Jinsha_Init();
        }

        public static SiteInfo Instance()
        {
            return _SiteInfo;
        }

        public List<MainSiteInfo> GetMainSites()
        {
            return mainSites.Values.ToList<MainSiteInfo>();
        }

        public String mainSiteUrl(ESite site)
        {
            return mainSites[site].url;
        }

        public List<SubSiteInfo> GetSubSites(ESite mainSite)
        {
            return mainSites[mainSite].subSites.Values.ToList<SubSiteInfo>();
        }

        public String GetConfigFileName(ESite mainSite)
        {
            return mainSites[mainSite].configFile;
        }
    }
}
