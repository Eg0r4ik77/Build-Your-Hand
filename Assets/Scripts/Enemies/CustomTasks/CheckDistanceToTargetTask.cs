using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace Enemies.CustomTasks{

	[Category("CustomTask")]
	public class CheckDistanceToTargetTask : ConditionTask
	{
		public BBParameter<Enemy> Enemy;
		public BBParameter<float> MaxDistance;

		protected override bool OnCheck()
		{
			Enemy enemy = Enemy.value;
			float maxDistance = MaxDistance.value;

			if (enemy.Target != null)
			{
				float distance = enemy.GetDistanceToCurrentTarget();
				return distance <= maxDistance;
			}

			return false;
		}
	}
}