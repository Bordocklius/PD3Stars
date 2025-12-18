using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD3Stars.Models
{
    public abstract partial class Brawler
    {
        public class BrawlerPALoadingState: BrawlerPABaseState
        {
            public BrawlerPALoadingState(BrawlerPAFSM fsm): base(fsm) { }

            public override void FixedUpdate(float fixedDeltaTime)
            {
                Context.CountPATimer(fixedDeltaTime);
                //_timer += fixedDeltaTime;
                if(Context.PALoadTimer >= Context.PALoadingTime)
                {
                    FSM.TransitionTo(FSM.BrawlerPAReadyState);
                }
            }

            public override void OnEnter()
            {
                Context.PALoadTimer = 0f;
            }
        }
    }
}
