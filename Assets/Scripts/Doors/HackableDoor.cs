using Doors;
using PuzzleGames;
using Skills;
using UnityEngine;

public class HackableDoor : Door, IHackable
{
    [SerializeField] private PuzzleGame _puzzleGame;
    
    private void OnEnable()
    {
        _puzzleGame.Finished += ApplyHack;
        Pause.Instance.OnPaused += SetPaused;
    }

    private void OnDisable()
    {
        _puzzleGame.Finished -= ApplyHack;
        Pause.Instance.OnPaused -= SetPaused;
    }
    
    public bool TryHack()
    {
        if (!_puzzleGame.IsFinished)
        {
            _puzzleGame.StartGame();
            return true;
        }

        return false;
    }

    private void ApplyHack()
    {
        Open();
    }
}