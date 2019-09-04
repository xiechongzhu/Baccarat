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
        public void ParseImage(Image image)
        {
            SendImageDelegate d = new SendImageDelegate(InternalParseImage);
            d.BeginInvoke(image, SendImageDataCallBack, null);
        }
        protected void SendImageDataCallBack(IAsyncResult result)
        {
            AsyncResult asyncResult = (AsyncResult)result;
            SendImageDelegate d = (SendImageDelegate)asyncResult.AsyncDelegate;
            Tuple<GameState, GameResult> retVal = d.EndInvoke(result);
            GameState currentGameState = retVal.Item1;
            GameResult currentGameResult = retVal.Item2;
            if (isRunning)
            {
                switch(gameState)
                {
                    case GameState.GAME_UNKNOW:
                    case GameState.GAME_END:
                        if(currentGameState == GameState.GAME_START)
                        {
                            gameState = currentGameState;
                            SendLog("新的一局开始了");
                        }
                        break;
                    case GameState.GAME_START:
                        if(currentGameState == GameState.GAME_UNKNOW)
                        {
                            SendLog("可以开始下注了");
                            gameState = GameState.GAME_GOING;
                            Bet();
                        }
                        break;
                    case GameState.GAME_GOING:
                        if(currentGameState == GameState.GAME_END)
                        {
                            String logString = "本局结束,结果:";
                            switch (currentGameResult)
                            {
                                case GameResult.RESULT_UNKNOW:
                                    logString += "未知";
                                    break;
                                case GameResult.BANKER_WIN:
                                    logString += "庄家胜";
                                    break;
                                case GameResult.DRAW_GAME:
                                    logString += "平局";
                                    break;
                                case GameResult.PLAYER_WIN:
                                    logString += "闲家胜";
                                    break;
                            }
                            SendLog(logString);
                            if (currentGameResult != GameResult.RESULT_UNKNOW)
                            {
                                gameResultList.Add(currentGameResult);
                            }
                            gameState = GameState.GAME_END;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private MainForm mainForm;
        protected abstract void Bet();
        public abstract Tuple<GameState, GameResult> InternalParseImage(Image image);
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
