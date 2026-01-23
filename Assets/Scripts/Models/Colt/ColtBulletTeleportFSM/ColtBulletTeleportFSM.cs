using PD3Stars.FSM;
using PD3Stars.Models.ColtModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD3Stars.Models.ColtModels
{
    public partial class ColtBullet
    {
        public class ColtBulletTeleportFSM : FiniteStateMachine
        {
            /// <summary>
            /// Context this FSM runs on
            /// </summary>
            public ColtBullet Context { get; private set; }

            // Various bullet states
            public ColtBulletTeleportAvailableState ColtBulletTeleportAvailableState { get; protected set; }
            public ColtBulletTeleportCDState ColtBulletTeleportCDState { get; protected set; }

            public new ColtBulletTeleportBaseState CurrentState
                => base.CurrentState as ColtBulletTeleportBaseState;

            public ColtBulletTeleportFSM(ColtBullet context)
            {
                Context = context;
                ColtBulletTeleportAvailableState = new ColtBulletTeleportAvailableState(this);
                ColtBulletTeleportCDState = new ColtBulletTeleportCDState(this);
                TransitionTo(ColtBulletTeleportAvailableState);
            }
        }
    }

    
}
