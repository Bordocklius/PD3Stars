using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PD3Stars.ScriptableObjects
{
    [CreateAssetMenu(fileName = "InitialWeaponStats", menuName = "Scriptable Objects/InitialWeaponStats")]
    public class InitialWeaponStats: ScriptableObject
    {
        public float Damage;
        public float Speed;
    }
}
