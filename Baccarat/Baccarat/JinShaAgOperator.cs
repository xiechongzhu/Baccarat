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
        public JinShaAgOperator(MainForm mainForm) : base(mainForm)
        {

        }

        protected override void Bet(int price)
        {
            throw new NotImplementedException();
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
