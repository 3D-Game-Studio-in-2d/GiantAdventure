using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private SettingsCamera cameraPrefab;
        
        private Player _player;
        
        public override void InstallBindings()
        {
                InitInput();
                InitPlayer();
                InitCamera();
        }

        private void InitInput()
        {
                Container.BindInterfacesAndSelfTo<DesktopInput>().AsSingle();
        }
        
        private void InitPlayer()
        {
                Container.Bind<PlayerConfig>().FromInstance(playerConfig).AsSingle();
                
                _player = Container.InstantiatePrefabForComponent<Player>(playerPrefab,
                        playerSpawnPoint.position, Quaternion.identity, null);
                Container.BindInterfacesAndSelfTo<Player>().FromInstance(_player).AsSingle().NonLazy();
                
                Container.Bind<MovementHandler>().AsSingle().NonLazy();
                Container.BindInterfacesAndSelfTo<AttackController>()
                        //.FromComponentOn(player.gameObject)
                        //.FromComponentOnRoot() // Берёт компонент с корневого объекта (т.е. там же, где Player)
                        .FromNewComponentOn(_player.gameObject)
                        .AsSingle()
                        //.WithArguments(Container.Resolve<IMovable>())
                        .NonLazy();
        }
        
        private void InitCamera()
        {
                var camera = Container.InstantiatePrefabForComponent<SettingsCamera>(cameraPrefab,
                        Vector3.zero, Quaternion.identity, null);
                Container.BindInterfacesAndSelfTo<SettingsCamera>().FromInstance(camera).AsSingle();
        }
}
