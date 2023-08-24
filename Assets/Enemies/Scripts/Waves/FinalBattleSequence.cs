using UnityEngine;

public class FinalBattleSequence : BattleSequence
{
    [SerializeField] private Timer _timer;
    
    private void Start()
    {
        _timer.Finished += base.StartNextWave;
    }

    protected override void InitializeSequence()
    {
        base.InitializeSequence();
        DetectAll();
    }

    protected override void FinishSequence()
    {
        ResetCurrentWave();
        StartNextWave();
    }
    
    protected override void StartNextWave()
    {
        _timer.StartTimer();
    }
}