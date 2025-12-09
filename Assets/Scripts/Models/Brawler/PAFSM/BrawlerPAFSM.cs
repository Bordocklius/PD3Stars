using PD3Stars.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace PD3Stars.Models
{
    public abstract partial class Brawler
    {
        public class BrawlerPAFSM: FiniteStateMachine
        {
            // Brawler this FSM runs on
            public Brawler Context { get; private set; }

            // States
            public BrawlerPALoadingState BrawlerPALoadingState { get; set; }
            public BrawlerPAReadyState BrawlerPAReadyState { get; set; }
            public BrawlerPAExecutingState BrawlerPAExecutingState { get; set; }
            public BrawlerPACooldownState BrawlerPACooldownState { get; set; }
            public BrawlerPADeadState BrawlerPADeadState { get; set; }

            // Current state
            public new BrawlerPABaseState CurrentState => base.CurrentState as BrawlerPABaseState;

            public BrawlerPAFSM(Brawler context) 
            {
                Context = context;
                BrawlerPALoadingState = new BrawlerPALoadingState(this);
                BrawlerPAReadyState = new BrawlerPAReadyState(this);
                BrawlerPAExecutingState = new BrawlerPAExecutingState(this);
                BrawlerPACooldownState = new BrawlerPACooldownState(this);
                BrawlerPADeadState = new BrawlerPADeadState(this);
                TransitionTo(BrawlerPALoadingState);
            }
            
        }
    }
}
