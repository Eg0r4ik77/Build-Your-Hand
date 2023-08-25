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
        var pause = new Pause();
        
        pause.Register(_player);

        _enemyFactory.Created += pause.Register;
        
        pause.Register(_timer);
        pause.Register(_highlightingPanel);
        pause.Register(_hackableDoors.ConvertAll(door => (IPauseable)door));
        pause.Register(_shop);

        Container
            .Bind<Pause>()
            .FromInstance(pause)
            .AsSingle();
    }
}
