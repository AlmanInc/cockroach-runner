using UnityEngine;
using Zenject;
using CockroachRunner;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameScreenView gameScreenView;
    [SerializeField] private GraphView graphView;
        
    public override void InstallBindings()
    {                
        Container.BindInstance(gameScreenView).AsSingle();
        Container.BindInstance(graphView).AsSingle();
    }
}