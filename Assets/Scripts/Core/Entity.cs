using UnityEngine;
using Zenject;

public abstract class Entity : MonoBehaviour
{
    public Health Health;

    protected abstract void OnDeath();
}