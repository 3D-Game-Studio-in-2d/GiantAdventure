using System;
using UnityEngine;
using Zenject;

public class DesktopInput : IInput, ITickable
{
    public event Action<Vector3> ClickMove;
    public event Action ClickJump;
    public event Action ClickRoll;

    public DesktopInput()
    {
        Debug.Log("DesktopInput Создан");

    }
    
    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        Vector3 moveInput = new Vector3(x, 0, 0);
        ClickMove?.Invoke(moveInput);
    }

    public void Tick()
    {
        float x = Input.GetAxis("Horizontal");
        Vector3 moveInput = new Vector3(x, 0, 0);
        ClickMove?.Invoke(moveInput);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ClickJump?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            ClickRoll?.Invoke();
        }
    }
}