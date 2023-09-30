using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DecayingMarine
{
    public class DamageImpact
    {
        public float Damage;
        public float Force;
        public Transform Attacker;

        public DamageImpact(float damage, float force, Transform attacker)
        {
            Damage = damage;
            Force = force;
            Attacker = attacker;
        }
    }
}
