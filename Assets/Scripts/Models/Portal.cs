using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PD3Stars.Models
{
    public class Portal : UnityModelBaseClass
    {
        public Vector3 Destination { get; private set; }

        public Portal() { }

        public Portal(Vector3 destination)
        {
            Destination = destination;
        }
    }
}
