using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Baccarat
{
    class CommonFunction
    {
        private CommonFunction() { }

        public static List<GameResultInfo> gameResultInfos = new List<GameResultInfo>()
        {
            new GameResultInfo { result = GameResult.BANKER_WIN, Name = "庄家赢" },
            new GameResultInfo { result = GameResult.PLAYER_WIN, Name = "闲家赢" }
        };

        public static GameResult StringToGameResult(String strResult)
        {
            foreach (GameResultInfo info in gameResultInfos)
            {
                if (info.Name.Equals(strResult))
                {
                    return info.result;
                }
            }
            return GameResult.RESULT_UNKNOW;
        }

        public static String GameResultToString(GameResult result)
        {
            foreach (GameResultInfo info in gameResultInfos)
            {
                if (info.result == result)
                {
                    return info.Name;
                }
            }
            return String.Empty;
        }

        public static T Clone<T>(T t)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            try
            {
                bf.Serialize(ms, t);
                ms.Seek(0, SeekOrigin.Begin);
                T result = (T)bf.Deserialize(ms);
                return result;
            }catch(Exception)
            {
                return default;
            }
            finally
            {
                ms.Close();
            }
        }

        public static Int32[] CalclutionBetIcons(Int32[] chips, Int32 value)
        {
            Int32 restValue = value;
            Int32[] icons = new int[chips.Length];
            while(true)
            {
                Int32 index = -1;
                Int32 chip = 0;
                for (Int32 i = 0; i < chips.Length; ++i)
                {
                    if (restValue >= chips[i])
                    {
                        if(chips[i] > chip)
                        {
                            chip = chips[i];
                            index = i;
                        }
                    }
                }
                if(index < 0)
                {
                    break;
                }
                icons[index] = restValue / chips[index];
                restValue = restValue % chips[index];
            }
            return icons;
        }
    }
}
