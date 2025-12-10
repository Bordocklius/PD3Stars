namespace PD3Animations
{
    public partial class GenericAnimation<T> 
    {
        public class AnimationPauzedState : AnimationBaseState
        {
            public AnimationPauzedState(AnimationFSM fsm) : base(fsm)
            {
            }

            public override void ToglePaused()
            {
                FSM.TransitionTo(FSM.AnimationRunningState);
            }
        }
    }

}
