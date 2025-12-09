using PD3Stars.Models;

namespace PD3Stars.Models.ColtModels
{
    public partial class Colt
    {
        public class ColtPAExecutingState: BrawlerPAExecutingState
        {
            public new Colt Context => base.Context as Colt;

            private float _timer;
            private int _currentMagSize;

            public ColtPAExecutingState(ColtPAFSM fsm) : base(fsm)
            {
            }

            public override void FixedUpdate(float fixedDeltaTime)
            {
                _timer += fixedDeltaTime;
                if(_timer > Context.FireDelay)
                {
                    if(_currentMagSize > 0)
                    {
                        FireBullet();
                    }
                    else
                    {
                        FSM.TransitionTo(FSM.BrawlerPALoadingState);
                    }
                }
            }

            public override void OnEnter()
            {
                _timer = Context.FireDelay;
                _currentMagSize = Context.MagSize;
            }

            private void FireBullet()
            {
                _currentMagSize--;
                _timer -= Context.FireDelay;
                Context.PAExecuted();
            }

        }
    }
    
}
