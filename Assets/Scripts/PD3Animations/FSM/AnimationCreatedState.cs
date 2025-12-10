using PD3Stars.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD3Animations
{
    public partial class GenericAnimation<T>
    {
        public class AnimationCreatedState: AnimationBaseState
        {
            public AnimationCreatedState(AnimationFSM fsm) : base(fsm)
            {
            }

            public override void OnEnter()
            {
                Context.TotalElapsed = 0;
            }

            public override void StartAnimation()
            {
                FSM.TransitionTo(FSM.AnimationRunningState);
            }
        }
    }

}
