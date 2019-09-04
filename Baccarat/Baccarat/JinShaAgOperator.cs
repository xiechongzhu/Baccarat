using Baccarat.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baccarat
{
    class JinShaAgOperator : AbstractOperator
    {
        private Dictionary<Int32, Point> chipPositions = new Dictionary<int, Point>();
        private Dictionary<GameResult, Point> betPositions = new Dictionary<GameResult, Point>();

        public JinShaAgOperator(MainForm mainForm) : base(mainForm)
        {
            //筹码位置
            chipPositions[10] = new Point(600, 400);
            chipPositions[50] = new Point(635, 400);
            chipPositions[100] = new Point(670, 400);
            chipPositions[500] = new Point(705, 400);
            chipPositions[1000] = new Point(740, 400);

            //下注“平局”的点击区域
            betPositions[GameResult.DRAW_GAME] = new Point(411, 322);
            //下注“庄家”的点击区域
            betPositions[GameResult.BANKER_WIN] = new Point(414, 339);
            //下注“闲家”的点击区域
            betPositions[GameResult.PLAYER_WIN] = new Point(413, 365);
        }

        protected override void Bet()
        {
            
        }

        public override Tuple<GameState, GameResult> InternalParseImage(Image image)
        {

            GameState gameState = GameState.GAME_UNKNOW;
            GameResult gameResult = GameResult.RESULT_UNKNOW;

            if(isStartImage(image))
            {
                gameState = GameState.GAME_START;
                gameResult = GameResult.RESULT_UNKNOW;
            }
            else if(isEndImage(image, out gameResult))
            {
                gameState = GameState.GAME_END;
            }

            image.Dispose();
            return new Tuple<GameState, GameResult>(gameState, gameResult);
        }

        private bool isStartImage(Image image)
        {
            //用开局的"准备下注"黄色字体判断
            Color color;
            List<Color> colorList = new List<Color>();
            //389,216 ===>255,255,1
            color = ImageOperator.GetImageRgb(image, 389, 216);
            colorList.Add(color);
            //448,215 ===> 255,251,5
            color = ImageOperator.GetImageRgb(image, 448, 215);
            colorList.Add(color);
            //430,220 ===> 246,247,0
            color = ImageOperator.GetImageRgb(image, 430, 220);
            colorList.Add(color);

            foreach(Color item in colorList)
            {
                Int32 r = item.R;
                Int32 g = item.G;
                Int32 b = item.B;
                if(r < 240 || g < 240 || b > 10)
                {
                    return false;
                }
            }
            return true;
        }

        private bool isEndImage(Image image, out GameResult gameResult)
        {
            gameResult = GameResult.RESULT_UNKNOW;
            Color color;
            color = ImageOperator.GetImageRgb(image, 86, 212);
            if(color.B > 190)
            {
                gameResult = GameResult.PLAYER_WIN;
                return true;
            }

            color = ImageOperator.GetImageRgb(image, 208, 214);
            if(color.R > 200)
            {
                gameResult = GameResult.BANKER_WIN;
                return true;
            }

            color = ImageOperator.GetImageRgb(image, 134, 214);
            if(color.G > 100)
            {
                gameResult = GameResult.DRAW_GAME;
                return true;
            }

            return false;
        }
    }
}
