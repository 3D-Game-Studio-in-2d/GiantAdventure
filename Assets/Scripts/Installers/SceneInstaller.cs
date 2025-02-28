using UnityEngine;
using Zenject;

namespace Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField]
        private Player _character;
        
        public override void InstallBindings()
        {
            Container.
                Bind<IMovable>() // Contact Type
                .To<Player>() // Result Type
                .FromComponentInNewPrefab(_character) // Создать из префаба зависимость
                // .FromComponentsInHierarchy() // Найдет объект из иерархии в дочерних (если GameObjectContext)
                // Если для SceneContext, то пройдет по всей сцене  
                // .FromInstance(_character)
                .AsSingle(); // Когда мы регистрируем зависимость, то этот компонент будет уникальным (Instance)
                // .AsCashed() // По одному контракту допустима реализация нескольких объектов
                // (Используется, когда надо получить массив зависимостей)
                // .AsTransient() // При каждом запросе будет создаваться отдельный экземпляр (С MonoBeh не работает)
        }
    }
}