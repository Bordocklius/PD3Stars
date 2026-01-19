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
        public class BrawlerHPRegeneratingState: BrawlerHPBaseState
        {           

            public BrawlerHPRegeneratingState(BrawlerHPFSM fsm) : base(fsm)
            {
            }            

            protected virtual void Context_OnPropertChanged(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName.Equals(nameof(Context.Health)))
                {
                    //CheckBrawlerHP();
                }
            }

            public override void FixedUpdate(float fixedDeltaTime)
            {
                Context.RegenerateHealth(fixedDeltaTime);
            }

            public override void OnEnter()
            {
                base.OnEnter();
                Context.PropertyChanged += Context_OnPropertChanged;
            }

            public override void OnExit()
            {
                Context.PropertyChanged -= Context_OnPropertChanged;
            }

            private void CheckBrawlerHP()
            {
                if (Context.Health <= 0)
                {
                    FSM.TransitionTo(FSM.BrawlerHPDeadState);
                }
                else if (Context.Health < PreviousHealth)
                {
                    FSM.TransitionTo(FSM.BrawlerHPCooldownState);
                }
                PreviousHealth = Context.Health;
            }
        }
    }
   
}
