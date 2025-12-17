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
            private float _timer;
            public float LoadingTime { get; set; } = 2f;

            public BrawlerPALoadingState(BrawlerPAFSM fsm): base(fsm) { }

            public override void FixedUpdate(float fixedDeltaTime)
            {
                Context.CountPATimer(fixedDeltaTime);
                //_timer += fixedDeltaTime;
                if(_timer >= LoadingTime)
                {
                    FSM.TransitionTo(FSM.BrawlerPAReadyState);
                }
            }

            public override void OnEnter()
            {
                _timer = 0f;
            }
        }
    }
}
