using UnityEngine;
using Zenject;
using CockroachRunner;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameSettings gameSettings;

    [SerializeField] private GameScreenView gameScreenView;
    [SerializeField] private GraphView graphView;
        
    public override void InstallBindings()
    {
        Container.Bind<GameState>().AsSingle();
        Container.Bind<EventsManager>().AsSingle();

        Container.BindInstance(gameSettings).AsSingle();

        Container.BindInstance(gameScreenView).AsSingle();
        Container.BindInstance(graphView).AsSingle();
    }
}