using System;
using UnityEngine;

namespace Enemies
{
    public interface IEnemyTarget : IApplyableDamage
    {
        public Vector3 CenterPosition { get; }
        public CapsuleCollider Collider { get; }
        public event Action Died;
    }
}