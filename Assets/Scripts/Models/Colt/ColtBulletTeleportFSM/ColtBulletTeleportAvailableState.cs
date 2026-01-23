using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PD3Stars.Models.ColtModels
{
    public partial class ColtBullet
    {
        public class ColtBulletTeleportAvailableState : ColtBulletTeleportBaseState
        {
            public ColtBulletTeleportAvailableState(ColtBulletTeleportFSM fsm) : base(fsm) { }

            public override void OnEnter()
            {
                Context.IsTeleportable = true;
            }

            public override void TeleportRequested(Vector3 newPosition)
            {
                Context.TeleportBullet(newPosition);
                FSM.TransitionTo(FSM.ColtBulletTeleportCDState);
            }
        }
    }

    
}
