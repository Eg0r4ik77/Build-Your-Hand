using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace Enemies.CustomTasks
{
	[Category("CustomTask")]
	public class ChaseTask : ActionTask
	{
		public BBParameter<EnemyMovement> EnemyMovement;
		
		private EnemyMovement _enemyMovement;
		
		protected override void OnExecute()
		{
			_enemyMovement = EnemyMovement.value;
			_enemyMovement.SwitchToRunning();
		}
		
		protected override void OnUpdate()
		{
			_enemyMovement.SetTargetAsDestination();
		}
		
		protected override void OnStop()
		{
			_enemyMovement.SwitchToWalking();
			_enemyMovement.Stop();
		}
	}
}