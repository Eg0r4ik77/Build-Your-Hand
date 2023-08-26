using System.Collections.Generic;
using Economy;
using UI;
using UnityEngine;
using Zenject;

public class LevelPauseInstaller : MonoInstaller
{
    [SerializeField] private Timer _timer;
    [SerializeField] private HighlightingPanel _highlightingPanel;
    [SerializeField] private List<HackableDoor> _hackableDoors;
    
    private readonly Pause _pause = new();

    private Player _player;
    private EnemyFactory _enemyFactory;
    private Shop _shop;
    
    [Inject]
    private void Construct(Player player, EnemyFactory enemyFactory, Shop shop)
    {
        _player = player;
        _enemyFactory = enemyFactory;
        _shop = shop;
    }
    
    public override void InstallBindings()
    {
        RegisterPauseables();

        Container
            .Bind<Pause>()
            .FromInstance(_pause)
            .AsSingle();
    }

    private void RegisterPauseables()
    {
        var pauseables = new List<IPauseable>()
        {
            _player,
            _timer, 
            _highlightingPanel,
            _shop
        };
        
        _pause.Register(pauseables);
        
        _pause.Register(_hackableDoors.ConvertAll(door => (IPauseable)door));
        _enemyFactory.Created += _pause.Register;
    }
}
