using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD3Animations
{
    public partial class GenericAnimation<T> 
    { 
    
        public class AnimationRunningState: AnimationBaseState
        {
            public AnimationRunningState(AnimationFSM fsm) : base(fsm)
            {
            }

            public override void FixedUpdate(float fixedDeltaTime)
            {
                Context.UpdateAnimation(fixedDeltaTime);
                if (Context.Progress >= 1)
                {
                    FSM.TransitionTo(FSM.AnimationEndedState);
                }
            }

            public override void ToglePaused()
            {
                FSM.TransitionTo(FSM.AnimationPauzedState);
            }            
        }
    }

}
