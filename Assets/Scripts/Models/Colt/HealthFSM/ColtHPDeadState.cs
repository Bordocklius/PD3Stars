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
        public class ColtHPDeadState: BrawlerHPDeadState
        {
            public ColtHPDeadState(ColtHPFSM fsm) : base(fsm) { } 
        }
    }
    
}
