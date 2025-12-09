using PD3Stars.FSM;
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
        public class BrawlerHPBaseState : IState
        {
            protected float PreviousHealth;

            public BrawlerHPFSM FSM { get; private set; }

            public Brawler Context
            {
                get { return FSM.Context; }
            }

            public BrawlerHPBaseState(BrawlerHPFSM fsm)
            {
                FSM = fsm;
            }

            public virtual void FixedUpdate(float fixedDeltaTime) { }
            public virtual void OnEnter() 
            {
                PreviousHealth = Context.Health;
            }
            public virtual void OnExit() { }

        }
    }    
}
