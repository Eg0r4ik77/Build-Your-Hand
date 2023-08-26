using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace Enemies.CustomTasks
{
    [Category("CustomTask")]
    public class BombEnemyAttackTask : ActionTask
    {
        public BBParameter<BombEnemyAttack> Attack;

        private BombEnemyAttack _attack;

        protected override void OnExecute()
        {
            _attack = Attack.value;
            _attack.Attack();
        }
    }
}