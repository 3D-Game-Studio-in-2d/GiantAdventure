using System;
using UnityEngine;

public interface IMovable
{
        float Speed { get; set; }
        bool FacingRight { get; set; }
        Vector3 Velocity { get; set; }
        Vector3 Position { get; set; }
        CharacterController CharacterController { get; }
}