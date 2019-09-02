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

            image.Dispose();
            return new Tuple<GameState, GameResult>(gameState, gameResult);
        }

        private bool isStartImage(Image image)
        {
            Color color;
            List<Color> colorList = new List<Color>();
            //391,224 ===>255,255,1
            color = ImageOperator.GetImageRgb(image, 391, 224);
            colorList.Add(color);
            //468,224 ===> 255,251,5
            color = ImageOperator.GetImageRgb(image, 468, 224);
            colorList.Add(color);
            //393,225 ===> 246,247,0
            color = ImageOperator.GetImageRgb(image, 393, 225);
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
    }
}
