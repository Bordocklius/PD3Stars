using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD3Stars.Models
{
    public abstract partial class Brawler
    {
        public class BrawlerPADeadState: BrawlerPABaseState
        {
            public BrawlerPADeadState(BrawlerPAFSM fsm) : base(fsm)
            {
            }

            public override void BrawlerRevived()
            {
                FSM.TransitionTo(FSM.BrawlerPALoadingState);
            }
        }
    }
    
}
