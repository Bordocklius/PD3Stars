using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PD3Stars.FSM;

namespace PD3Animations
{
    public partial class GenericAnimation<T>
    {
        public class AnimationFSM : FiniteStateMachine
        {
            public GenericAnimation<T> Context { get; private set; }

            public AnimationCreatedState AnimationCreatedState { get; private set; }
            public AnimationRunningState AnimationRunningState { get; private set; }
            public AnimationEndedState AnimationEndedState { get; private set; }
            public AnimationPauzedState AnimationPauzedState { get; private set; }

            public new AnimationBaseState CurrentState
            {
                get { return base.CurrentState as AnimationBaseState; }
            }

            public AnimationFSM(GenericAnimation<T> context)
            {
                Context = context;
                AnimationCreatedState = new AnimationCreatedState(this);
                AnimationRunningState = new AnimationRunningState(this);
                AnimationEndedState = new AnimationEndedState(this);
                AnimationPauzedState = new AnimationPauzedState(this);
                TransitionTo(AnimationCreatedState);
            }
        }
    }
}
