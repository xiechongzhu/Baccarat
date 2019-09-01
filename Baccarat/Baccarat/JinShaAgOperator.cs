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
        JinShaAgOperator(MainForm mainForm) : base(mainForm)
        {

        }

        protected override void Bet(int price)
        {
            throw new NotImplementedException();
        }

        public override Tuple<GameState, GameResult> ParseImage(Image image)
        {
            throw new NotImplementedException();
        }
    }
}
