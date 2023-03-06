using NodeCanvas.Framework;
using UnityEngine;

namespace Enemies.CustomTasks{

	public class RotateToTargetTask : ActionTask
	{
		public BBParameter<Transform> Origin;
		public BBParameter<Transform> Target;
		public BBParameter<float> RotationSpeed;

		protected override void OnUpdate()
		{
			RotateToTarget();
		}

		private void RotateToTarget()
		{
			Vector3 direction = (Target.value.position - Origin.value.transform.position).normalized;
			direction.y = 0;
			Quaternion rotation = Quaternion.LookRotation(direction);
			
			Origin.value.transform.rotation =
				Quaternion.Lerp(Origin.value.transform.rotation, rotation, RotationSpeed.value * Time.deltaTime);
		}
	}
}