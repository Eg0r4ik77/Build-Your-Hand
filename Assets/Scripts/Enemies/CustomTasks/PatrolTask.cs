using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.CustomTasks{

	[Category("CustomTask")]
	public class PatrolTask : ActionTask{
		public BBParameter<NavMeshAgent> Agent;
		public BBParameter<float> Range;

		private Vector3 _centrePoint;
		private NavMeshAgent OriginAgent => Agent.value;

		protected override void OnExecute()
		{
			_centrePoint = OriginAgent.transform.position;
		}

		protected override void OnUpdate(){
			if (OriginAgent && OriginAgent.isOnNavMesh && OriginAgent.remainingDistance <= OriginAgent.stoppingDistance)
			{
				TrySetNewWayPoint();
			}
		}

		private void TrySetNewWayPoint()
		{
			Vector3 point = _centrePoint + Random.insideUnitSphere * Range.value;

			if (NavMesh.SamplePosition(point, out NavMeshHit hit, Range.value, NavMesh.AllAreas))
			{
				OriginAgent.SetDestination(hit.position);
			}
		}
	}
}