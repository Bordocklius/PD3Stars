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
        public abstract class AnimationBaseState : IState
        {
            public AnimationFSM FSM { get; private set; }            

            public GenericAnimation<T> Context 
            {
                get { return FSM.Context; }
            }

            public AnimationBaseState(AnimationFSM fsm)
            {
                FSM = fsm;
            }
            public virtual void FixedUpdate(float fixedDeltaTime) { }
            public virtual void OnEnter() { }
            public virtual void OnExit() { }
            public virtual void ToglePaused() { }
            public virtual void StartAnimation() { }

            public virtual void ResetAnimation()
            {
                if(FSM.CurrentState != FSM.AnimationCreatedState)
                    FSM.TransitionTo(FSM.AnimationCreatedState);
            }         
        }
    }
}
