using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD3Stars.Models
{
    public abstract partial class Brawler
    {
        public class BrawlerPAExecutingState: BrawlerPABaseState
        {
            public BrawlerPAExecutingState(BrawlerPAFSM fsm): base(fsm) { }

            public override void OnEnter()
            {
                Context.PALoadTimer = 0f;
            }
        }

    }
}
