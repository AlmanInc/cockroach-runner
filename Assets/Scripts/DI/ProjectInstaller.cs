using UnityEngine;
using Zenject;
using CockroachRunner;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<GameState>().AsSingle();
        Container.Bind<EventsManager>().AsSingle();
    }
}