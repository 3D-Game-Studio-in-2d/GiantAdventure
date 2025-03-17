using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateVinnerMenu : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            EnableBackground.Instance.gameObject.SetActive(true);
        }
    }
}
