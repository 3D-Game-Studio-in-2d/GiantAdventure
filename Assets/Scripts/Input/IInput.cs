using System;
using UnityEngine;

public interface IInput
{
    event Action<Vector3> ClickMove;
    event Action ClickJump;
    event Action ClickRoll;
    event Action ClickAttack;
}