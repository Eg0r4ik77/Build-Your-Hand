using PuzzleGames;
using Skills;
using UnityEngine;

public class HackableDoor : MonoBehaviour, IHackable, IPauseable
{
    [SerializeField] private PuzzleGame _puzzleGame;
    
    private readonly int _openingAnimationHash = Animator.StringToHash("Open");

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    
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
    
    public void SetPaused(bool paused)
    {
        _animator.enabled = !paused;
    }
        
    private void Open()
    {
        _animator.Play(_openingAnimationHash);
    }

    private void ApplyHack()
    {
        Open();
    }
}