using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using Zenject;

public class EnemySceneInstaller: MonoInstaller
{
    [SerializeField] private SlimeConfig config;
    [SerializeField] private WanderingFlameConfig wanderingFlameConfig;
    
    public override void InstallBindings()
    {
        Container.Bind<SlimeConfig>().FromInstance(config).AsSingle();
        Container.Bind<WanderingFlameConfig>().FromInstance(wanderingFlameConfig).AsSingle();
    } 
}