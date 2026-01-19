using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PD3Stars.Presenters
{
    public interface IDamageable
    {
        public void TakeDamage(float damage, IDamageSource source);
    }
}
