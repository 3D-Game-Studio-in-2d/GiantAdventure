
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    int MaxHealth;
    Health Health;

    public abstract void OnDeath();
}