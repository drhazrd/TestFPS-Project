using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public string theGun;

    bool collected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !collected)
        {
            AudioManager.instance.PlaySFX(4);
            PlayerController.instance.AddGun(theGun);
            collected = true;
            Destroy(gameObject);
        }
    }
}
