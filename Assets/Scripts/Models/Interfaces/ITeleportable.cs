using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PD3Stars.Models
{
    public interface ITeleportable
    {
        public event EventHandler<TeleportEventArgs> TeleportEvent;
        public bool IsTeleportable { get; set; }

        public void TeleportRequested(Vector3 newPosition);

    }


    public class TeleportEventArgs : EventArgs
    {
        public Vector3 NewPosition { get; private set; }

        public TeleportEventArgs(Vector3 newPosition)
        {
            NewPosition = newPosition;
        }
    }

}
