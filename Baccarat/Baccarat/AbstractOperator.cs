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
    abstract class AbstractOperator
    {
        protected GameResult gameResult = GameResult.RESULT_UNKNOW;
        protected GameState gameState = GameState.GAME_UNKNOW;
        bool isRunning;
        bool isNewRound;
        Int32 currentBetIndex;
        List<BetRule> betRules;
        List<GameResult> gameResultList = new List<GameResult>();
        GameResult betResult;
        Int32[] betCoins;

        public AbstractOperator(MainForm mainForm)
        {
            this.mainForm = mainForm;
        }

        public void Start()
        {
            isRunning = true;
            isNewRound = true;
        }

        public void Reset()
        {
            isRunning = false;
            gameResult = GameResult.RESULT_UNKNOW;
            gameState = GameState.GAME_UNKNOW;
            gameResultList.Clear();
        }

        protected void Stop()
        {
            Reset();
            mainForm.Stop();
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
            gameResult = retVal.Item2;
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
                            gameState = GameState.GAME_GOING;
                            PreBet();
                            if (betCoins != null)
                            {
                                Bet(betResult, betCoins);
                            }
                        }
                        break;
                    case GameState.GAME_GOING:
                        if(currentGameState == GameState.GAME_END)
                        {
                            String logString = "本局结束,结果:";
                            switch (gameResult)
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
                            if (gameResult != GameResult.RESULT_UNKNOW && gameResult != GameResult.DRAW_GAME)
                            {
                                gameResultList.Add(gameResult);
                            }
                            gameState = GameState.GAME_END;
                            if (betRules != null && betRules.Count > 0)
                            {
                                if (betResult != gameResult)
                                {
                                    SendLog("你输了");
                                }
                            }
                            if(betRules == null || betRules.Count == 0 || (currentBetIndex >= betRules.Count || betResult == gameResult))
                            {
                                isNewRound = true;
                                if (gameResultList.Count > Config.Instance().betMode)
                                {
                                    if ((betRules == null || betRules.Count == 0))
                                    {
                                        SendLog("没有配置下注规则，重新开始匹配");
                                    }
                                    else if (betResult == gameResult)
                                    {
                                        SendLog("你赢了，重新开始匹配");
                                    }
                                    else if (currentBetIndex >= betRules.Count)
                                    {
                                        SendLog("规则已用完，重新开始匹配");
                                    }
                                }
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        protected void PreBet()
        {
            betCoins = null;
            if (isNewRound)
            {
                betRules = null;
                Int32 mode = Config.Instance().betMode;
                if (mode == 1 && gameResultList.Count >= 1)
                {
                    List<GameRule1> list = Config.Instance().gameRule1;
                    foreach (GameRule1 rule1 in list)
                    {
                        if(rule1.result == gameResultList[gameResultList.Count - 1])
                        {
                            betRules = rule1.rules;
                            break;
                        }
                    }
                }
                else if (mode == 2 && gameResultList.Count >= 2)
                {
                    List<GameRule2> list = Config.Instance().gameRule2;
                    foreach (GameRule2 rule2 in list)
                    {
                        if (rule2.result1 == gameResultList[gameResultList.Count - 1] && rule2.result2 == gameResultList[gameResultList.Count - 2])
                        {
                            betRules = rule2.rules;
                            break;
                        }
                    }
                }
                else if (mode == 3 && gameResultList.Count >= 3)
                {
                    List<GameRule3> list = Config.Instance().gameRule3;
                    foreach (GameRule3 rule3 in list)
                    {
                        if (rule3.result1 == gameResultList[gameResultList.Count - 1] && rule3.result2 == gameResultList[gameResultList.Count - 2]
                            && rule3.result3 == gameResultList[gameResultList.Count - 3])
                        {
                            betRules = rule3.rules;
                            break;
                        }
                    }
                }
                if(betRules != null && betRules.Count > 0)
                {
                    SendLog("匹配到规则,开始循环下注");
                    currentBetIndex = 0;
                    isNewRound = false;
                    betCoins = CommonFunction.CalclutionBetIcons(Config.Instance().chips, betRules[currentBetIndex].betValue);
                    betResult = betRules[currentBetIndex].result;
                    String strLog = String.Format("下注:{0}, 金额:{1}", CommonFunction.GameResultToString(betResult), betRules[currentBetIndex].betValue);
                    SendLog(strLog);
                    currentBetIndex++;
                }
            }
            else
            {
                betCoins = CommonFunction.CalclutionBetIcons(Config.Instance().chips, betRules[currentBetIndex].betValue);
                betResult = betRules[currentBetIndex].result;
                String strLog = String.Format("下注:{0}, 金额:{1}", CommonFunction.GameResultToString(betResult), betRules[currentBetIndex].betValue);
                SendLog(strLog);
                currentBetIndex++;
            }
        }

        private MainForm mainForm;
        protected abstract void Bet(GameResult gameResult, Int32[] coins);
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
