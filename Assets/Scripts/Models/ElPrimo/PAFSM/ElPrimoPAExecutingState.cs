using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD3Stars.Models.ElPrimoModels
{
    public partial class ElPrimo
    {
        public class ElPrimoPAExecutingState: BrawlerPAExecutingState
        {
            public new ElPrimo Context => base.Context as ElPrimo;

            public ElPrimoPAExecutingState(ElPrimoPAFSM fsm): base(fsm) { }

            public override void OnEnter()
            {
                base.OnEnter();
                Context.PAExecuted();
            }
        }
    }
}
