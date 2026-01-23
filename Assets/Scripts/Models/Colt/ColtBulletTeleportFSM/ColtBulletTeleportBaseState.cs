using PD3Stars.FSM;
using PD3Stars.Models.ColtModels;
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
        public class ColtBulletTeleportBaseState : IState
        {
            public ColtBulletTeleportFSM FSM;
            public ColtBullet Context => FSM.Context;

            public ColtBulletTeleportBaseState(ColtBulletTeleportFSM fSM)
            {
                FSM = fSM;
            }

            public virtual void OnEnter() { }
            public virtual void OnExit() { }
            public virtual void FixedUpdate(float deltaTime) { }

            public virtual void TeleportRequested(Vector3 newPosition) { }
        }
    }
    
}
