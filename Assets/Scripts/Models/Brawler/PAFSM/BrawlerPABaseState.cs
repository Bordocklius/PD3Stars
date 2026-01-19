using PD3Stars.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD3Stars.Models
{
    public abstract partial class Brawler 
    { 
        public class BrawlerPABaseState: IState
        {
            protected float PreviousHealth;
            public BrawlerPAFSM FSM { get; private set; }

            public Brawler Context => FSM.Context;

            public BrawlerPABaseState(BrawlerPAFSM fsm)
            {
                FSM = fsm;
            }

            public virtual void FixedUpdate(float fixedDeltaTime) 
            {
                //CheckBrawlerHP();
            }

            public virtual void OnEnter() { }
            public virtual void OnExit() { }
            public virtual void ExecutePA() { }
            public virtual void PAFinished() 
            {
                FSM.TransitionTo(FSM.BrawlerPALoadingState);
            }

            public virtual void BrawlerDied()
            {
                FSM.TransitionTo(FSM.BrawlerPADeadState);
            }

            public virtual void BrawlerRevived() { }

            private void CheckBrawlerHP()
            {
                if (Context.Health <= 0)
                {
                    FSM.TransitionTo(FSM.BrawlerPADeadState);
                }
                //else if (Context.Health < PreviousHealth)
                //{
                //    FSM.TransitionTo(FSM.BrawlerPACooldownState);
                //}
                PreviousHealth = Context.Health;
            }
        }    
    }
}
