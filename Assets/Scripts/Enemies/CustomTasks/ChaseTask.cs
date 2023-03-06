using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.CustomTasks
{
	[Category("CustomTask")]
	public class ChaseTask : ActionTask
	{
		public BBParameter<Transform> Target;
		public BBParameter<NavMeshAgent> Agent;
		public BBParameter<float> MinimumDistance;

		private NavMeshAgent OriginAgent => Agent.value;
		private Transform OriginTarget => Target.value;
		
		protected override void OnUpdate()
		{
			if (!OriginAgent || !OriginAgent.isOnNavMesh)
			{
				return;
			}

			Vector3 targetPosition = OriginTarget.position;
			float distance = Vector3.Distance(OriginAgent.transform.position, targetPosition);

			if (distance > MinimumDistance.value)
			{
				OriginAgent.SetDestination(targetPosition);
			}
			else
			{
				StopAgent();
			}
		}

		protected override void OnStop()
		{
			if (OriginAgent && OriginAgent.enabled)
			{
				StopAgent();
			}
		}

		private void StopAgent()
		{
			OriginAgent.isStopped = true;
		}
	}
}