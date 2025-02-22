using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AxisFreeze : MonoBehaviour
{
    enum Axis
    {
        X,
        Y,
        Z
    }
    
    [SerializeField] private Axis axisForFreeze = Axis.Z;
    [SerializeField] private float freezePosition  = 0f;
    private Vector3 _position;
    
    void Update()
    {
        Vector3 newPosition = transform.position;

        switch (axisForFreeze)
        {
            case Axis.X:
                newPosition.x = freezePosition;
                break;
            case Axis.Y:
                newPosition.y = freezePosition;
                break;
            case Axis.Z:
                newPosition.z = freezePosition;
                break;
            default:
                break;
        }
        
        transform.position = newPosition;
    }
}
