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
        
        private float _delayBeforeExplosion;
        private float _secondsBeforeAttackPassed;

        protected override void OnExecute()
        {
            SetBlackboardParametersValues();
            Initialize();
        }

        protected override void OnUpdate()
        {
            _attack.Finished = false;

            if (!_attack.Finished)
            {
                TryStartAttack();
            }
        }

        private void TryStartAttack()
        {
            if (_secondsBeforeAttackPassed >= _delayBeforeExplosion)
            {
                _attack.Explode();
                EndAction(true);
            }
            else
            {
                _secondsBeforeAttackPassed += Time.deltaTime;
            }
        }
        
        private void SetBlackboardParametersValues()
        {
            _attack = Attack.value;
        }
        
        private void Initialize()
        {
            _delayBeforeExplosion = _attack.DelayBeforeExplosion;
            _secondsBeforeAttackPassed = 0;
        }
    }
}