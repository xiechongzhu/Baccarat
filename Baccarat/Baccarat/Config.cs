using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baccarat
{
    [Serializable]
    public class BetRule
    {
        public GameResult result { get; set; }
        public Int32 betValue { get; set; }
    }

    [Serializable]
    public class GameRule1
    {
        public GameResult result { get; set; }
        public List<BetRule> rules = new List<BetRule>();
    }

    [Serializable]
    public class GameRule2
    {
        public GameResult result2 { get; set; }
        public GameResult result1 { get; set; }
        public List<BetRule> rules = new List<BetRule>();
    }

    [Serializable]
    public class GameRule3
    {
        public GameResult result3 { get; set; }
        public GameResult result2 { get; set; }
        public GameResult result1 { get; set; }
        public List<BetRule> rules = new List<BetRule>();
    }


    public class Config
    {
        private Config()
        {

        }
        private static Config _instance = new Config();
        public static Config Instance()
        {
            return _instance;
        }

        public Int32[] chips = new int[5];
        public Int32 betMode;
        public List<GameRule1> gameRule1 = new List<GameRule1>();
        public List<GameRule2> gameRule2 = new List<GameRule2>();
        public List<GameRule3> gameRule3 = new List<GameRule3>();

        public bool Write(String filename)
        {
            System.IO.FileStream file = null;
            try
            {
                System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(Config));
                file = System.IO.File.Create(filename);
                writer.Serialize(file, _instance);
            }
            catch(Exception)
            {
                if (file != null)
                {
                    file.Close();
                }
                return false;
            }
            if (file != null)
            {
                file.Close();
            }
            return true;
        }

        public void Read(String filename)
        {
            System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(Config));
            System.IO.StreamReader file = null;
            try
            {
                file = new System.IO.StreamReader(filename);
                _instance = (Config)reader.Deserialize(file);
            }
            catch(Exception)
            {
                if (file != null)
                {
                    file.Close();
                }
            }
            if (file != null)
            {
                file.Close();
            }
        }
    }
}
