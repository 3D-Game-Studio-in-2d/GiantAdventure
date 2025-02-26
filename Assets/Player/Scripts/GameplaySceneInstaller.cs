using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _playerSpawnPoint;
        [SerializeField] private PlayerConfig _playerConfig;
        
        [SerializeField] private WanderingFlameConfig _wanderingFlameConfig;
        
        public override void InstallBindings()
        {
                Container.BindInterfacesAndSelfTo<DesktopInput>().AsSingle();
                Container.Bind<PlayerConfig>().FromInstance(_playerConfig).AsSingle();
                Container.Bind<WanderingFlameConfig>().FromInstance(_wanderingFlameConfig).AsSingle();
    
                // Создаём игрока и регистрируем его как IMovable
                Player player = Container.InstantiatePrefabForComponent<Player>(_playerPrefab, _playerSpawnPoint.position, Quaternion.identity, null);
                player.gameObject.SetActive(true);
                Container.BindInterfacesAndSelfTo<Player>().FromInstance(player).AsSingle();

                Container.Bind<MovementHandler>().AsSingle().NonLazy();
                // Теперь можно привязывать AttackHandler
                Container.BindInterfacesAndSelfTo<AttackController>()
                        //.FromComponentOn(player.gameObject)
                        //.FromComponentOnRoot() // Берёт компонент с корневого объекта (т.е. там же, где Player)
                        .FromNewComponentOn(player.gameObject)
                        .AsSingle()
                        //.WithArguments(Container.Resolve<IMovable>())
                        .NonLazy();

        }
}