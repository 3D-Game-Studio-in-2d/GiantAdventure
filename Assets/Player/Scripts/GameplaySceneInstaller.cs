using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private PlayerConfig _playerConfig;
        
        public override void InstallBindings()
        {
                Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle();
                Player player = Container.InstantiatePrefabForComponent<Player>(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity, null);
                Container.BindInterfacesAndSelfTo<Player>().FromInstance(player).AsSingle();
                
                Container.BindInterfacesAndSelfTo<DesktopInput>().AsSingle();
                Container.Bind<MovementHandler>().AsSingle().NonLazy();
        }
}