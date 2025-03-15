using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZoneRegister : MonoBehaviour
{
    [NonSerialized] public Action<Player> OnTriggerStayWithPlayer; 
    
    private void OnTriggerStay(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            OnTriggerStayWithPlayer?.Invoke(player);
        }
    }
}
