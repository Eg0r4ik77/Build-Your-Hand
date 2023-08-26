using Enemies;
using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace CustomTasks{

	[Category("CustomTask")]
	public class CheckDetectedTask : ConditionTask
	{
		public BBParameter<Enemy> Enemy;

		protected override bool OnCheck()
		{
			Enemy enemy = Enemy.value;
			bool targetDetected = enemy.Target != null;
			
			return targetDetected;
		}
	}
}