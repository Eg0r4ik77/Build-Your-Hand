using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace Enemies.CustomTasks{

	[Category("CustomTask")]
	public class CheckRangeTask : ConditionTask{
		public BBParameter<float> Range;
		public BBParameter<Transform> Origin;
		public BBParameter<Transform> Target;

		protected override bool OnCheck()
		{
			if (Target.isNull)
			{
				return false;				
			}
			
			float distance = Vector3.Distance(Origin.value.position, Target.value.position);
			return distance <= Range.value;
		}
	}
}