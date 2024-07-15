using UnityEngine;
using Zenject;
using CockroachRunner;

public class BaseInstaller : MonoInstaller
{
    [SerializeField] private GameSettings gameSettings;

    public override void InstallBindings()
    {
        Container.BindInstance(gameSettings).AsSingle();
    }
}