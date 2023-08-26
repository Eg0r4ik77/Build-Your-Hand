using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace Enemies.CustomTasks
{
    [Category("CustomTask")]
    public class MeleeEnemyAttackTask : ActionTask
    {
        public BBParameter<MeleeEnemyAttack> Attack;

        private MeleeEnemyAttack _attack;
        
        private float _cooldownSeconds;
        private float _secondsBeforeAttackPassed;

        protected override void OnExecute()
        {
            SetBlackboardParametersValues();
            Initialize();
        }

        protected override void OnUpdate()
        {
            if (_attack.Finished)
            {
                TryStartAttack();
            }
        }

        protected override void OnStop()
        {
            _attack.Stop();
        }

        private void TryStartAttack()
        {
            if (_secondsBeforeAttackPassed >= _cooldownSeconds)
            {
                _attack.Punch();
                _secondsBeforeAttackPassed = 0;
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
            _cooldownSeconds = _attack.Cooldown;
            _secondsBeforeAttackPassed = _cooldownSeconds;
        }
    }
}