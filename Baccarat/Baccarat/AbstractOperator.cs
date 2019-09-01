using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baccarat
{
    enum GameResult
    {
        RESULT_UNKNOW,  //结果未知
        BANKER_WIN,     //庄家赢
        PLAYER_WIN,     //闲家赢
        DRAW_GAME       //平局
    }

    enum GameState
    {
        GAME_UNKNOW,    //未知
        GAME_GOING,     //正在进行
        GAME_START,     //新的一局开始，可以开始下注了
        GAME_END        //本局结束
    }

    abstract class AbstractOperator
    {
        protected GameResult gameResult = GameResult.RESULT_UNKNOW;
        protected GameState gameState = GameState.GAME_UNKNOW;
        protected bool startBet = false;
        bool isRunning = false;
        List<GameResult> gameResultList = new List<GameResult>();

        public AbstractOperator(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        public void Start()
        {
            isRunning = true;
        }

        public void Reset()
        {
            isRunning = false;
            gameResult = GameResult.RESULT_UNKNOW;
            gameState = GameState.GAME_UNKNOW;
            gameResultList.Clear();
        }

        delegate Tuple<GameState, GameResult> SendImageDelegate(Image image);
        public void SendImageData(Image image)
        {
            SendImageDelegate d = new SendImageDelegate(ParseImage);
            d.BeginInvoke(image, SendImageDataCallBack, null);
        }
        void SendImageDataCallBack(IAsyncResult result)
        {
            AsyncResult asyncResult = (AsyncResult)result;
            SendImageDelegate d = (SendImageDelegate)asyncResult.AsyncDelegate;
            Tuple<GameState, GameResult> retVal = d.EndInvoke(result);
            if (isRunning)
            {
                if (retVal.Item2 != GameResult.RESULT_UNKNOW)
                {
                    gameResultList.Add(retVal.Item2);
                }
            }
        }

        private MainForm mainForm;
        protected abstract void Bet(Int32 price);
        public abstract Tuple<GameState, GameResult> ParseImage(Image image);
        protected void BrowserClick(Int32 x, Int32 y)
        {
            mainForm.BrowserClick(x, y);
        }
        protected void SendLog(String logString)
        {
            mainForm.addLog(logString);
        }
    }
}
