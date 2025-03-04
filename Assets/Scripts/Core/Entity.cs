
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    int MaxHealth;
    public Health Health;

    public abstract void OnDeath();
}