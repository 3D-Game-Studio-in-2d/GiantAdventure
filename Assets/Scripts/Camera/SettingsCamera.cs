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
    private CinemachineConfiner _confiner;
    private CameraZone _zone;
    
    [SerializeField] private Vector3 rotation;
    //[SerializeField] private Vector3 lookOffsetX;
    
    [Inject]
    public void Initialize(Player player, IInput input,  CameraZone cameraZone)
    {
        _player = player;

        //_input = input;
        //_input.ClickMove += ChangeOffsetOrientation;
        
        _zone = cameraZone;
    }
    
    private void OnEnable()
    {
        _camera = GetComponent<CinemachineVirtualCamera>();
        _transposer = _camera.GetComponentInChildren<CinemachineFramingTransposer>();
        
        _confiner = GetComponent<CinemachineConfiner>();
        _confiner.m_BoundingVolume = _zone.GetComponent<Collider>();
        
        if (InitPlayer()) return;

        RotateCamera();
    }

    private bool InitPlayer()
    {
        if (_player == null)
        {
            Debug.LogError("Player is null!");
            return true;
        }

        if (_player.transform == null)
        {
            Debug.LogError("Player transform is null!");
            return true;
        }

        _camera.Follow = _player.transform;
        return false;
    }

    private void RotateCamera()
    {
        transform.rotation = Quaternion.Euler(rotation);
    }

    /*private void ChangeOffsetOrientation(Vector3 inputVector)
    {
        if (inputVector.magnitude > 0.1f)
        {
            _transposer.m_TrackedObjectOffset.x = lookOffsetX.x;
        }
        else if (inputVector.magnitude < 0.1f)
        {
            _transposer.m_TrackedObjectOffset.x = -lookOffsetX.x;
        }
        
    }*/
}
