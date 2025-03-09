using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class NextLevel : MonoBehaviour
{
    [SerializeField, FormerlySerializedAs("Время до перехода на следующий уровень")] 
    private float cooldownForNextLevel = 0.3f;
    [SerializeField, FormerlySerializedAs("Открыт ли следующий уровень изначально")] 
    private bool nextLevelOpen = false;
    
    [field: FormerlySerializedAs("Сцена следующего уровня")]
    public SceneLoadType sceneLoadType = SceneLoadType.ByName;

    [SerializeField]
    public int sceneIndex;

    [SerializeField]
    public string sceneName;
    
    public Action<float> OnLevelTransitionEffects;
    private Collider _collider;
    
    void Start()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = nextLevelOpen;
    }

    [ContextMenu("Открыть доступ к следующему уровню")]
    public void OpenNextLevel()
    {
        nextLevelOpen = true;
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (nextLevelOpen && other.TryGetComponent<Player>(out Player player))
        {
            StartNextLevel();
        }
    }

    private void StartNextLevel()
    {
        OnLevelTransitionEffects?.Invoke(cooldownForNextLevel);
        
        if (sceneLoadType == SceneLoadType.ByName)
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
