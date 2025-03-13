using UnityEngine;
using Zenject;

public abstract class Entity : MonoBehaviour
{
    public Health Health;

    public abstract void OnDeath();
}