using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class RootStrikeSpawner : MonoBehaviour
{
    private float attackDelay = 1f;
    private ForestEntConfig _config;
    
    [SerializeField]
    private RootStrike rootStrikePrefab;
    
    public void Initialize(ForestEntConfig config)
    {
        _config = config;
        attackDelay = _config.attackDelayRootStrike;
    }

    private void Start()
    {
        StartCoroutine(SpawnRootStrike());
    }

    private IEnumerator SpawnRootStrike()
    {
        yield return new WaitForSeconds(attackDelay);
        
        var rootStrike = Instantiate(rootStrikePrefab, transform.position, Quaternion.identity);
        rootStrike.Initialize(_config);
        Destroy(gameObject);
    }
}