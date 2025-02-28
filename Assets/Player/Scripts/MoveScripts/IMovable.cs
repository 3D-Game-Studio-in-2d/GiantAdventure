using System;
using UnityEngine;

public interface IMovable
{
        float Speed { get; set; }
        bool FacingRight { get; set; }
        Vector3 Velocity { get; set; }
        Transform Transform { get; set; }
        CharacterController CharacterController { get; }
}