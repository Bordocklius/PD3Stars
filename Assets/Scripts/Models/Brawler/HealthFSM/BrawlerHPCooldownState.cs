using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD3Stars.Models
{
    public abstract partial class Brawler
    {
        public class BrawlerHPCooldownState: BrawlerHPBaseState
        {

            private float _timer;
            public float TimerDelay { get; set; } = 3f;

            public BrawlerHPCooldownState(BrawlerHPFSM fsm) : base(fsm)
            {                
            }

            protected virtual void Context_OnPropertChanged(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName.Equals(nameof(Context.Health)))
                {
                    CheckBrawlerHP();
                }
            }

            public override void OnEnter()
            {
                Context.PropertyChanged += Context_OnPropertChanged;
                _timer = 0f;
            }

            public override void OnExit()
            {
                Context.PropertyChanged -= Context_OnPropertChanged;
            }

            public override void FixedUpdate(float fixedDeltaTime)
            {
                _timer+= fixedDeltaTime;
                if(_timer >= TimerDelay)
                {
                    FSM.TransitionTo(FSM.BrawlerHPRegeneratingState);
                }
            }

            private void CheckBrawlerHP()
            {
                if (Context.Health <= 0)
                {
                    FSM.TransitionTo(FSM.BrawlerHPDeadState);
                }
                else if (Context.Health < PreviousHealth)
                {
                    _timer = 0f;
                }
                PreviousHealth = Context.Health;
            }
        }

    }
}
