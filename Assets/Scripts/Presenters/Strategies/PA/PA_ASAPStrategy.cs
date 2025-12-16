using PD3Stars.Models;
using PD3Stars.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD3Stars.Strategies.PA
{
    public class PA_ASAPStrategy : PAStrategyBase
    {
        public PA_ASAPStrategy(Brawler context, BrawlerPresenter contextPresenter) : base(context, contextPresenter)
        {
        }

        public override void Update(float deltaTime)
        {
            AttackDirection = ContextPresenter.transform.forward;
            Context.PARequested();
        }
    }
}
