using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using Zenject;

public class SettingsCamera : MonoBehaviour
{
    private CinemachineVirtualCamera _camera;
    private CinemachineFramingTransposer _transposer;
    private Player _player;
    private IInput _input;

    [SerializeField] private Vector3 rotation;
    [SerializeField] private Vector3 offset;
    
    [Inject]
    public void Initialize(Player player, IInput input)
    {
        Debug.Log($"Initializing Camera.");
        _player = player;

        _input = input;
        _input.ClickMove += ChangeOffsetOrientation;
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
        _transposer = _camera.GetComponentInChildren<CinemachineFramingTransposer>();

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
        
        RotateCamera();
    }

    private void RotateCamera()
    {
        transform.rotation = Quaternion.Euler(rotation);
    }

    private void ChangeOffsetOrientation(Vector3 inputVector)
    {
        _transposer.m_TrackedObjectOffset.x = offset.x * inputVector.x;
    }
}
