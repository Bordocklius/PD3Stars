using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD3Stars.Models
{
    public abstract partial class Brawler
    {
        public class BrawlerHPDeadState : BrawlerHPBaseState
        {
            private float _timer;

            private float _reviveTimer = 10f;

            public BrawlerHPDeadState(BrawlerHPFSM fsm) : base(fsm)
            {
            }

            public override void OnEnter()
            {
                _timer = 0f;
            }

            public override void FixedUpdate(float fixedDeltaTime)
            {
                _timer += fixedDeltaTime;
                if (_timer >= _reviveTimer)
                {
                    Context.HPFSM_OnBrawlerRevived();
                    FSM.TransitionTo(FSM.BrawlerHPCooldownState);
                }
            }
        }

    }
}
