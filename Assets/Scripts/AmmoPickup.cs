using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    bool collected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"&& !collected)
        {
            AudioManager.instance.PlaySFX(3);
            PlayerController.instance.activeGun.GetAmmo();
            collected = true;
            Destroy(gameObject);
        }
    }
}
