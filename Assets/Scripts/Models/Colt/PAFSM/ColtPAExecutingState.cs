using PD3Stars.Models;

namespace PD3Stars.Models.ColtModels
{
    public partial class Colt
    {
        public class ColtPAExecutingState: BrawlerPAExecutingState
        {
            public new Colt Context => base.Context as Colt;

            private float _timer;

            public ColtPAExecutingState(ColtPAFSM fsm) : base(fsm)
            {
            }

            public override void FixedUpdate(float fixedDeltaTime)
            {
                _timer += fixedDeltaTime;
                if(_timer > Context.FireDelay)
                {
                    if(Context.CurrentMagSize > 0)
                    {
                        _timer -= Context.FireDelay;
                        Context.PAExecuted();
                    }
                    else
                    {
                        FSM.TransitionTo(FSM.BrawlerPALoadingState);
                    }
                }
            }

            public override void OnEnter()
            {
                base.OnEnter();
                _timer = Context.FireDelay;
                Context.CurrentMagSize = Context.MagSize;
            }            

        }
    }
    
}
