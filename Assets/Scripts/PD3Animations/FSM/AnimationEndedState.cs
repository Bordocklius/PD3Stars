namespace PD3Animations
{
    public partial class GenericAnimation<T> 
    {
        public class AnimationEndedState : AnimationBaseState
        {
            public AnimationEndedState(AnimationFSM fsm) : base(fsm)
            {
            }
        }
    }

}
