using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using Zenject;

public class EnemySceneInstaller: MonoInstaller
{
    [SerializeField] private SlimeConfig config;
    [SerializeField] private WanderingFlameConfig wanderingFlameConfig;
    [SerializeField] private BanditConfig banditConfig;
    
    public override void InstallBindings()
    {
        Container.Bind<SlimeConfig>().FromInstance(config).AsSingle();
        Container.Bind<WanderingFlameConfig>().FromInstance(wanderingFlameConfig).AsSingle();
        Container.Bind<BanditConfig>().FromInstance(banditConfig).AsSingle();
    } 
}