using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private SettingsCamera cameraPrefab;
        [SerializeField] private HealthView enemyHealthViewPrefab;
        [SerializeField] private DeadView deadView;
        [SerializeField] private SoundConfig soundConfig;
        
        private Player _player;
        
        public override void InstallBindings()
        {
                Container.Bind<SoundConfig>().FromInstance(soundConfig).AsSingle();

                InitInput();
                InitPlayer();
                InitCamera();
                InitEnemyHealth();
        }

        private void InitEnemyHealth()
        {
                //var healthViewParent = new GameObject("HealthViews").transform;
                
                // Регистрируем фабрику для HealthView
                Container.BindFactory<HealthView, HealthViewFactory>()
                        .FromComponentInNewPrefab(enemyHealthViewPrefab)
                        .UnderTransformGroup("HealthViews");
        }

        private void InitInput()
        {
                Container.BindInterfacesAndSelfTo<DesktopInput>().AsSingle();
        }
        
        private void InitPlayer()
        {
                Container.Bind<PlayerConfig>().FromInstance(playerConfig).AsSingle();
                Container.BindInterfacesAndSelfTo<AttackPlayerStats>().AsSingle();
                
                _player = Container.InstantiatePrefabForComponent<Player>(playerPrefab,
                        playerSpawnPoint.position, Quaternion.identity, null);
                Container.BindInterfacesAndSelfTo<Player>().FromInstance(_player).AsSingle();
                
                Container.Bind<MovementHandler>().AsSingle().NonLazy();
                Container.BindInterfacesAndSelfTo<AttackController>()
                        .FromNewComponentOn(_player.gameObject)
                        .AsSingle()
                        .NonLazy();
                
                Container.BindInterfacesAndSelfTo<PlayerAnimatorController>()
                        .FromNewComponentOn(_player.gameObject)
                        .AsSingle()
                        .NonLazy();
                
                // Передать зависимость Health в UI
                Container.Bind<PlayerHealthView>().FromComponentsInHierarchy().AsSingle();
                var healthBar = Container.Resolve<PlayerHealthView>();
                healthBar.Initialize(_player.Health);
                
                Container.Bind<DeadView>().FromInstance(deadView).AsSingle();
                var deadViewPrefab = Container.Resolve<DeadView>();
                deadViewPrefab.Initialize(_player.Health);
        }
        
        private void InitCamera()
        {
                Container.BindInterfacesAndSelfTo<CameraZone>().FromComponentInHierarchy().AsSingle();
                var camera = Container.InstantiatePrefabForComponent<SettingsCamera>(cameraPrefab,
                        Vector3.zero, Quaternion.identity, null);
                Container.BindInterfacesAndSelfTo<SettingsCamera>().FromInstance(camera).AsSingle();
        }
}
