using UnityEngine;
using Zenject;
using CockroachRunner;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameSettings gameSettings;
    
    public override void InstallBindings()
    {
        Container.Bind<EventsManager>().AsSingle();

        Container.BindInstance(gameSettings).AsSingle();
    }
}