using Economy;
using PuzzleGames;
using UnityEngine;
using Zenject;

public class LevelInstaller : MonoInstaller
{
    [SerializeField] private Player _player;
    [SerializeField] private CursorSwitcher _cursorSwitcher;
    [SerializeField] private PauseMenu _pauseMenu;
    [SerializeField] private PuzzleGamesSwitcher _puzzleGamesSwitcher;
    [SerializeField] private Shop _shop;
    [SerializeField] private GameFinisher _gameFinisher;
    
    public override void InstallBindings()
    {
        Container
            .Bind<Player>()
            .FromInstance(_player)
            .AsSingle();

        Container
            .Bind<CursorSwitcher>()
            .FromInstance(_cursorSwitcher)
            .AsSingle();
        
        Container
            .Bind<PauseMenu>()
            .FromInstance(_pauseMenu)
            .AsSingle();
        
        Container
            .Bind<PuzzleGamesSwitcher>()
            .FromInstance(_puzzleGamesSwitcher)
            .AsSingle();
        
        Container
            .Bind<Shop>()
            .FromInstance(_shop)
            .AsSingle();
        
        Container
            .Bind<GameFinisher>()
            .FromInstance(_gameFinisher)
            .AsSingle();
    }
}
