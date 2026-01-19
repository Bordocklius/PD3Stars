using PD3Stars.FSM;
using UnityEngine;

namespace PD3Stars.Models
{
    public abstract partial class Brawler
    {
        public class BrawlerHPFSM: FiniteStateMachine
        {
            // Brawler on which this FSM runs
            public Brawler Context { get; private set; }

            // Various HP states
            public BrawlerHPRegeneratingState BrawlerHPRegeneratingState { get; protected set; }
            public BrawlerHPCooldownState BrawlerHPCooldownState { get; protected set; }
            public BrawlerHPDeadState BrawlerHPDeadState { get; protected set; }

            public bool IsAlive => CurrentState != BrawlerHPDeadState;

            // Current state returned from base as a HealthBaseState
            public new BrawlerHPBaseState CurrentState
            {
                get { return base.CurrentState as BrawlerHPBaseState; }
            }

            public BrawlerHPFSM(Brawler context)
            {
                Context = context;
                BrawlerHPRegeneratingState = new BrawlerHPRegeneratingState(this);
                BrawlerHPCooldownState = new BrawlerHPCooldownState(this);
                BrawlerHPDeadState = new BrawlerHPDeadState(this);
                TransitionTo(BrawlerHPCooldownState);
            }
        }
    }
}
