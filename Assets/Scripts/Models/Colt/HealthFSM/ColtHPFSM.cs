using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PD3Stars.Models;

namespace PD3Stars.Models.ColtModels
{
    public partial class Colt
    {
        public class ColtHPFSM : BrawlerHPFSM
        {
            public new Colt Context { get; private set; }

            public ColtHPFSM(Colt context): base(context)
            {
                BrawlerHPRegeneratingState = new BrawlerHPRegeneratingState(this);
                BrawlerHPCooldownState = new BrawlerHPCooldownState(this);
                BrawlerHPDeadState = new BrawlerHPDeadState(this);
                TransitionTo(BrawlerHPCooldownState);
            }
        }
    }
}
