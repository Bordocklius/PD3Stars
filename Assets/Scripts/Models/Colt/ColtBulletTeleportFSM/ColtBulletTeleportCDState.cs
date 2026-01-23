using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD3Stars.Models.ColtModels
{
    public partial class ColtBullet
    {
        public class ColtBulletTeleportCDState : ColtBulletTeleportBaseState
        {
            public ColtBulletTeleportCDState(ColtBulletTeleportFSM fsm) : base(fsm) { }

            public override void OnEnter()
            {
                Context._teleportCDTimer = 0f;
                Context.IsTeleportable = false;
            }

            public override void FixedUpdate(float fixedDeltaTime)
            {
                Context.CountTeleportCDTimer(fixedDeltaTime);
                if(Context._teleportCDTimer >= Context.TeleportCooldown)
                {
                    FSM.TransitionTo(FSM.ColtBulletTeleportAvailableState);
                }
            }


        }
    }

    
}
