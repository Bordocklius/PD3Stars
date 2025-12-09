using PD3Stars.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD3Stars.Models
{
    public abstract partial class Brawler
    {
        public class BrawlerPAReadyState: BrawlerPABaseState
        {
            public BrawlerPAReadyState(BrawlerPAFSM fsm): base(fsm) { }

            public override void ExecutePA()
            {
                FSM.TransitionTo(FSM.BrawlerPAExecutingState);
            }
        }
    }
    
}
