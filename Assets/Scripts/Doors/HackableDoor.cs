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
    }

    private void OnDisable()
    {
        _puzzleGame.Finished -= ApplyHack;
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