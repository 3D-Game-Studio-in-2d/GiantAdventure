using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Zenject;

public class SettingsCamera : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;
    private Player _player;
    
    [Inject]
    public void Initialize(Player player)
    {
        Debug.Log($"Initializing Camera.");
        _player = player;
    }

    /*private void Start()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
        
        if (_player == null)
            Debug.Log($"player null.");
        else
            Debug.Log($"player not null.");
            
        Debug.Log($"player Camera. {_player.Transform.position.ToString()}");
        _camera.Follow = _player.Transform;
        Debug.Log(_camera.Follow.ToString());
    }*/
    
    private void OnEnable()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();

        if (_player == null)
        {
            Debug.LogError("Player is null!");
            return;
        }

        if (_player.transform == null)
        {
            Debug.LogError("Player transform is null!");
            return;
        }

        Debug.Log($"Player is not null, setting camera follow to {_player.transform.position}");
        _camera.Follow = _player.transform;
        
        //transform.Rotate(15,0,0);
    }
}
