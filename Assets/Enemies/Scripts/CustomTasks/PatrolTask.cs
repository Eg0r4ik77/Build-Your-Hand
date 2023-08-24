using System.Threading;
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace Enemies.CustomTasks{

	[Category("CustomTask")]
	public class PatrolTask : ActionTask
	{
		public BBParameter<Patrol> Patrol;
		public BBParameter<EnemyMovement> Movement;

		private CancellationTokenSource _cancellationTokenSource;

		private Patrol _patrol;
		private EnemyMovement _movement;

		private Vector3 _centrePoint;
		private bool _isMoving;
		
		protected override void OnExecute()
		{
			SetBlackboardParametersValues();
			Initialize();

			_patrol.SetRandomDestination();
		}

		protected override void OnUpdate()
		{
			if (_movement.IsMoving && _movement.ReachedDestination())
			{
				_movement.Stop();
				_patrol.SetRandomDestinationAsync(_cancellationTokenSource);
			}
		}
		
		protected override void OnStop()
		{
			_movement.Stop();
			_cancellationTokenSource.Cancel();

			base.OnStop();
		}
		
		private void SetBlackboardParametersValues()
		{
			_movement = Movement.value;
			_patrol = Patrol.value;
		}

		private void Initialize()
		{
			_patrol.Initialize(_movement);
			_patrol.SetStartPosition(_patrol.transform.position);
			
			_cancellationTokenSource = new CancellationTokenSource();
		}
	}
}