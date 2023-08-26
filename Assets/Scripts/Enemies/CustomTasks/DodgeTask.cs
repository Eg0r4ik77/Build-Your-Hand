using Enemies;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions{

	[Category("CustomTasks")]
	public class DodgeTask : ActionTask{
		
		// public BBParameter<float> Range;
		// public BBParameter<Transform> Target;
		// public BBParameter<RangeEnemy> Origin;
		//  
		// private float _range => Range.value;
		// private RangeEnemy _origin => Origin.value;
		// private Transform _target => Target.value;
		// private NavMeshAgent _agent => _origin.Agent;
		//
		// private bool _isStopped;
		// private float _speedModifier = 1.5f;
		//
		// protected override void OnExecute()
		// {
		// 	_isStopped = false;
		// 	_agent.updateRotation = false;
		// 	_origin.Speed *= _speedModifier;
		// }
		//
		// protected override void OnUpdate(){
		// 	if (_origin.IsAttackAnimationPlaying())
		// 		return;
		//
		// 	if (!_isStopped && _agent && _agent.isOnNavMesh &&
		// 	    _agent.remainingDistance <= _agent.stoppingDistance)
		// 	{
		// 		TrySetNewWayPoint();
		// 	}
		// }
		//
		// private void TrySetNewWayPoint()
		// {
		// 	Vector3 point = _origin.transform.position + Random.insideUnitSphere * Random.Range(Range.value / 2, Range.value);
		// 	
		// 	if (_agent && NavMesh.SamplePosition(point, out NavMeshHit hit, _range, NavMesh.AllAreas)
		// 	                && Vector3.Distance(point, _target.position)
		// 	                >= Vector3.Distance(_origin.transform.position, _target.position))
		// 	{
		// 		_agent.SetDestination(hit.position);
		// 		_origin.SetDodgingAnimationFlag(true);
		// 	}
		// 	else
		// 	{
		// 		TrySetNewWayPoint();
		// 	}
		// }
		//
		// protected override void OnStop()
		// {
		// 	_isStopped = true;
		// 	_origin.Speed /= _speedModifier;
		// 	_agent.updateRotation = true;
		// 	_origin.SetDodgingAnimationFlag(false);
		// 	_agent.SetDestination(_origin.transform.position);
		// 	base.OnStop();
		// }
	}
}