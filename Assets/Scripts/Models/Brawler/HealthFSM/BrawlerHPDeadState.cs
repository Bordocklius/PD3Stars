using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD3Stars.Models
{
    public abstract partial class Brawler
    {
        public class BrawlerHPDeadState: BrawlerHPBaseState
        {
            public BrawlerHPDeadState(BrawlerHPFSM fsm): base(fsm) 
            {
            }
        }

    }
}
