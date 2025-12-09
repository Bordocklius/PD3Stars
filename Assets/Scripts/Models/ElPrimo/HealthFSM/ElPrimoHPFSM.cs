using PD3Stars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD3Stars.Models.ElPrimoModels
{
    public partial class ElPrimo
    {
        public class ElPrimoHPFSM: BrawlerHPFSM
        {
            public new ElPrimo Context { get; set; }

            public ElPrimoHPFSM(ElPrimo context) : base(context)
            {
                BrawlerHPRegeneratingState = new BrawlerHPRegeneratingState(this);
                BrawlerHPCooldownState = new BrawlerHPCooldownState(this);
                BrawlerHPDeadState = new BrawlerHPDeadState(this);
                TransitionTo(BrawlerHPCooldownState);
            }
        }
    }
}
